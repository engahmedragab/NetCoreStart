using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using NetCoreStartProject.Contracts.V1;
using NetCoreStartProject.Contracts.V1.Requests;
using NetCoreStartProject.Contracts.V1.Responses;
using NetCoreStartProject.Services;

namespace NetCoreStartProject.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(state => state.Errors.Select(Error => Error.ErrorMessage))
                });
            }
            
            var response = await _identityService.RegisterAsync(request.Email, request.Password,Url, Request.Scheme);

            if (!response.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                ConfirmationMail = response.ConfirmationEmailLink
            });
        }

        [HttpGet(ApiRoutes.Identity.MailConfarm)]
        public async Task<IActionResult> ConfirmEmail(MailConfirmationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(state => state.Errors.Select(Error => Error.ErrorMessage))
                });
            }

            var response = await _identityService.ConfirmEmailAsync(request.UserId, HttpUtility.UrlDecode(request.ConfirmtionToken));

            if (!response.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = response.Token,
                RefreshToken = response.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var response = await _identityService.LoginAsync(request.Email, request.Password,Url ,Request.Scheme);

            if (!response.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = response.Token,
                RefreshToken = response.RefreshToken,
                ConfirmationMail = response.ConfirmationEmailLink
            });
        }
        
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!response.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = response.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = response.Token,
                RefreshToken = response.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.EmailInUse)]
        public async Task<IActionResult> IsEmailInUse([FromBody] UserEmailRequest request)
        {
            var response = await _identityService.IsEmailInUseAsync(request.UserEmail);

            if (!response.Success)
            {
                return BadRequest(new UserInUseResponse
                {
                    UserInUse = response.Success,
                    Errors = response.Errors
                });
            }

            return Ok(new UserInUseResponse
            {
                UserInUse = response.Success
            });
        }

        [HttpPost(ApiRoutes.Identity.UserHasPassword)]
        public async Task<IActionResult> IsUserHasPassword([FromBody] PasswordRequest request)
        {
            var response = await _identityService.HasPasswordAsync(request.UserId);

            if (!response.Success)
            {
                return BadRequest(new PasswordResponse
                {
                    UserHasPassword = response.Success,
                    Errors = response.Errors
                });
            }

            return Ok(new PasswordResponse
            {
                UserHasPassword = response.Success
            });
        }

        [HttpPost(ApiRoutes.Identity.AddPassword)]
        public async Task<IActionResult> AddPassword([FromBody] PasswordRequest request)
        {

            var response = await _identityService.AddPasswordAsync(request.UserId,request.Password);

            if (!response.Success)
            {
                return BadRequest(new PasswordResponse
                {
                    UserHasPassword = response.Success,
                    Errors = response.Errors
                });
            }

            return Ok(new PasswordResponse
            {
                UserHasPassword = response.Success
            });

        }

        [HttpPost(ApiRoutes.Identity.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordRequest request)
        {
            var response = await _identityService.ChangePasswordAsync(request.UserId,request.Password,request.NewPassword);

            if (!response.Success)
            {
                return BadRequest(new PasswordResponse
                {
                    UserHasPassword = response.Success,
                    Errors = response.Errors
                });
            }

            return Ok(new PasswordResponse
            {
                UserHasPassword = response.Success
            });

        }

        [HttpPost(ApiRoutes.Identity.ForgetPassword)]
        public async Task<IActionResult> ForgotPassword([FromBody] UserEmailRequest request)
        {
            var response = await _identityService.ForgotPasswordAsync(request.UserEmail);

            if (!response.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(new ForgotPasswordResponse
            {
                ForgetToken = response.ResetPasswardLink
            });
        }

        [HttpPost(ApiRoutes.Identity.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var response = await _identityService.ResetPasswordAsync(request.UserEmail , HttpUtility.UrlDecode(request.Token) , request.Password);

            if (!response.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = response.Token,
                RefreshToken = response.RefreshToken
            });
        }

    }
}