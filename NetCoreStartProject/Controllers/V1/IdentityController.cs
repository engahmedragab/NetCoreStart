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
            
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password,Url, Request.Scheme);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                ConfirmationMail = authResponse.ConfirmationEmailLink
            });
        }
        [HttpGet(ApiRoutes.Identity.MailConfarm)]
        public async Task<IActionResult> ConfirmEmail(MailConfirmationRequest request /*string UserId , string  ConfirmtionToken*/)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(state => state.Errors.Select(Error => Error.ErrorMessage))
                });
            }

            var authResponse = await _identityService.ConfirmEmailAsync(request.UserId, HttpUtility.UrlDecode(request.ConfirmtionToken));

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password,Url ,Request.Scheme);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                ConfirmationMail = authResponse.ConfirmationEmailLink
            });
        }
        
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}