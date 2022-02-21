using NetCoreStartProject.Contracts.V1.Responses.ExternalProviders;
using NetCoreStartProject.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NetCoreStartProject.Services.Identity
{
    public class ExternalProvidersIdentityService : IExternalProvidersIdentityService
    {
        private const string FacebookTokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}"; 
        private const string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        private const string LinkedInTokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string LinkedInUserInfoUrl = "https://api.linkedin.com/v2/clientAwareMemberHandles?q=members&projection=(elements*(primary,type,handle~))"; //"https://api.linkedin.com/v2/me";
        private const string LinkedInCallbackUrl = "https://www.linkedin.com/oauth/v2/accessToken?grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}";

        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly LinkedInAuthSettings _linkedInAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public ExternalProvidersIdentityService(
            IHttpClientFactory httpClientFactory,
            FacebookAuthSettings facebookAuthSettings,
            LinkedInAuthSettings linkedInAuthSettings
            )
        {
            _httpClientFactory = httpClientFactory;
            _facebookAuthSettings = facebookAuthSettings;
            _linkedInAuthSettings = linkedInAuthSettings;
        }
        public async Task<FacebookUserInfoResult> GetFacebookUserInfoAsync(string accessToken)
        {
            string formattedUrl = String.Format(FacebookUserInfoUrl, accessToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
        }

        public async Task<FacebookTokenValidatorResult> ValidateFacebookAccessTokenAsync(string accessToken)
        {
            string formattedUrl = String.Format(FacebookTokenValidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTokenValidatorResult>(responseAsString);
        }

        public async Task<LinkedInAccessTokenResult> GetLinkedInCallbackAsync(string code,string state = "")
        {
            string formattedUrl = String.Format(LinkedInCallbackUrl, code , _linkedInAuthSettings.RedirectUri, _linkedInAuthSettings.AppId , _linkedInAuthSettings.AppSecret);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LinkedInAccessTokenResult>(responseAsString);
        }
        public async Task<LinkedInEmailUserInfoResult> GetLinkedInUserInfoAsync(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await httpClient.GetAsync(LinkedInUserInfoUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LinkedInEmailUserInfoResult>(responseAsString);
        }

        public async Task<LinkedInTokenValidatorResult> ValidateLinkedInAccessTokenAsync(string accessToken)
        {
            string formattedUrl = String.Format(LinkedInTokenValidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LinkedInTokenValidatorResult>(responseAsString);
        }
    }
}
