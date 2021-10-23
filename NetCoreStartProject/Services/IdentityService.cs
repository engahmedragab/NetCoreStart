using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;
using NetCoreStartProject.Enums;
using NetCoreStartProject.Extensions;
using NetCoreStartProject.Options;

namespace NetCoreStartProject.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IExternalProvidersIdentityService _externalProvidersIdentityService;
        private readonly DataContext _context;
        
        public IdentityService (
            UserManager<IdentityUser> userManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            IExternalProvidersIdentityService externalProvidersIdentityService,
            DataContext context)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _externalProvidersIdentityService = externalProvidersIdentityService;
            _context = context;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password, IUrlHelper url = null, string reqestSchema = "")
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            var confirmationEmailLink = await _userManager.GenrateEmailConfirmationUrlAsync(newUser, url, reqestSchema);
            return new AuthenticationResult
            {
                Success = true,
                ConfirmationEmailLink = confirmationEmailLink
            };
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password, IUrlHelper url = null, string reqestSchema = "")
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User does not exist"}
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User/password combination is wrong"}
                };
            }

            if (!user.EmailConfirmed)
            {
                var confirmationEmailLink = await _userManager.GenrateEmailConfirmationUrlAsync(user, url, reqestSchema);
                return new AuthenticationResult
                {
                    Success = true,
                    ConfirmationEmailLink = confirmationEmailLink
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> ConfirmEmailAsync(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/token combination is wrong" }
                };
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"The User ID { userId } is invalid" }
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Error happen Confirm Email" }
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult {Errors = new[] {"Invalid Token"}};
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult {Errors = new[] {"This token hasn't expired yet"}};
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token does not exist"}};
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has expired"}};
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been invalidated"}};
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been used"}};
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token does not match this JWT"}};
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> IsEmailInUseAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && user.EmailConfirmed)
            {
                return new AuthenticationResult
                {
                    Success = true
                };
            }
            return new AuthenticationResult
            {
                Success = false,
                Errors = new[] {
                    user != null ? "Error happen User not exsists" : string.Empty ,
                    user != null && user.EmailConfirmed ? "Error happen Confirm Email" : string.Empty
                }
            };
        }

        public async Task<AuthenticationResult> HasPasswordAsync(string userId)
        {
            if (userId == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User combination is wrong" }
                };
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"The User ID { userId } is invalid" }
                };
            }

            var userHasPassword = await _userManager.HasPasswordAsync(user);

            if (userHasPassword)
            {
                return new AuthenticationResult
                {
                    Success = true
                };
            }
            return new AuthenticationResult
            {
                Success = false,
                Errors = new[] { "This user has no password" } 
            };
        }

        public async Task<AuthenticationResult> AddPasswordAsync(string userId, string password)
        {
            if (userId == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User combination is wrong" }
                };
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"The User ID { userId } is invalid" }
                };
            }

            var hasPassword = await HasPasswordAsync(userId);

            if (hasPassword.Success)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "This user already has password" }
                };
            }

            var result = await _userManager.AddPasswordAsync(user, password);

            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = result.Errors.Select(x => x.Description)
                };
            }
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> ForgotPasswordAsync(string userEmail, IUrlHelper url = null, string reqestSchema = "")
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            if (!user.EmailConfirmed)
            {
                var confirmationEmailLink = await _userManager.GenrateEmailConfirmationUrlAsync(user, url, reqestSchema);
                return new AuthenticationResult
                {
                    Success = false,
                    ConfirmationEmailLink = confirmationEmailLink
                };
            }

            var resetPasswordLink = await _userManager.GenrateForgetPasswardUrlAsync(user, url, reqestSchema);
            return new AuthenticationResult
            {
                Success = true,
                ResetPasswardLink = resetPasswordLink
            };

        }

        public async Task<AuthenticationResult> ResetPasswordAsync(string userEmail, string token, string password)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            if (!user.EmailConfirmed)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] {"user email is not confirmed"}
                };
            }

            var result = await _userManager.ResetPasswordAsync(user,token, password);
           
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "Error happen while Reset the passward" }
                };
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            }
            return await GenerateAuthenticationResultForUserAsync(user);

        }

        public async Task<AuthenticationResult> ChangePasswordAsync(string userId, string password, string newPassword)
        {
            if (userId == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User combination is wrong" }
                };
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"The User ID { userId } is invalid" }
                };
            }

            var hasPassword = await HasPasswordAsync(userId);

            if (hasPassword.Success)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "This user already has password" }
                };
            }

            var result = await _userManager.ChangePasswordAsync(user,
                   password,newPassword);

            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = result.Errors.Select(x => x.Description)
                };
            }
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> LoginWithExternalProvidersAsync(string accessToken, ExternalProvidersType externalProvidersType = ExternalProvidersType.Facebook)
        {
            switch (externalProvidersType)
            {
                case ExternalProvidersType.Facebook:
                    var FacebookValidateTokenResult = _externalProvidersIdentityService.ValidateFacebookAccessTokenAsync(accessToken);
                    if (!FacebookValidateTokenResult.Result.Data.IsValid)
                    {
                        return new AuthenticationResult
                        {
                            Errors = new[] { "Invalid Facebook Token" }
                        };
                    }
                    var userInfo = _externalProvidersIdentityService.GetFacebookUserInfoAsync(accessToken).Result;

                    if (userInfo == null)
                    {
                        return new AuthenticationResult
                        {
                            Errors = new[] { "Invalid Facebook User" }
                        };
                    }

                    var user = await _userManager.FindByEmailAsync(userInfo.Email);

                    if (user == null)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };
                var createdResult = await _userManager.CreateAsync(identityUser);
                if (!createdResult.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "something went wrong when create user" }
                    };
                }

                return await GenerateAuthenticationResultForUserAsync(identityUser);
            }
                    return await GenerateAuthenticationResultForUserAsync(user);
                case ExternalProvidersType.LinkedIn:
                    var userInfol = _externalProvidersIdentityService.GetLinkedInUserInfoAsync(accessToken).Result;

                    if (userInfol == null)
                    {
                        return new AuthenticationResult
                        {
                            Errors = new[] { "Invalid Facebook User" }
                        };
                    }

                    var userl = await _userManager.FindByEmailAsync(userInfol.Elements.FirstOrDefault().ElementHandle.EmailAddress);

                    if (userl == null)
                    {
                        IdentityUser identityUser = new IdentityUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfol.Elements.FirstOrDefault().ElementHandle.EmailAddress,
                            UserName = userInfol.Elements.FirstOrDefault().ElementHandle.EmailAddress
                        };
                        var createdResult = await _userManager.CreateAsync(identityUser);
                        if (!createdResult.Succeeded)
                        {
                            return new AuthenticationResult
                            {
                                Errors = new[] { "something went wrong when create user" }
                            };
                        }

                        return await GenerateAuthenticationResultForUserAsync(identityUser);
                    }
                    return await GenerateAuthenticationResultForUserAsync(userl);
            }
            return null;
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

       
    }
}