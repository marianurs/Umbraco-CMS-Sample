using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Services.Account;
using RestSharp;
using System.Net;

namespace PolRegio.Services.Account
{
    public class SocialMediaService : ISocialMediaService
    {
        private const string facebookApiBaseUrl = "https://graph.facebook.com/";
        private const string facebookGetAccessTokenUrl = "/oauth/access_token";
        private const string facebookUserUrl = "/{id}";
        private const string facebookAppId = "219805455090553";
        private const string facebookAppSecret = "b06712c7592aaf7719d8482ee0c72e75";

        private const string googleApiBaseUrl = "https://www.googleapis.com/";
        private const string googleVerifyTokenUrl = "/oauth2/v1/tokeninfo";

        public bool ValidateToken(SocialMediaFormViewModel model)
        {
            return model.Type == "facebook"
                ? IsFacebookTokenValid(model)
                : IsGoogleTokenValid(model);
        }

        private bool IsFacebookTokenValid(SocialMediaFormViewModel model)
        {
            var client = new RestClient(facebookApiBaseUrl);
            var request = new RestRequest(facebookGetAccessTokenUrl, Method.GET);
            request.AddQueryParameter("grant_type", "client_credentials");
            request.AddQueryParameter("client_id", facebookAppId);
            request.AddQueryParameter("client_secret", facebookAppSecret);
            var response = client.Execute<dynamic>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var accessToken = response.Data["access_token"];
            var userRequest = new RestRequest(facebookUserUrl);
            userRequest.AddUrlSegment("id", model.IdToken);
            userRequest.AddQueryParameter("access_token", accessToken.ToString());

            var userResponse = client.Execute<dynamic>(userRequest);

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return userResponse.Data["id"] == model.IdToken;
        }

        private bool IsGoogleTokenValid(SocialMediaFormViewModel model)
        {
            var client = new RestClient(googleApiBaseUrl);
            var request = new RestRequest(googleVerifyTokenUrl, Method.GET);
            request.AddQueryParameter("access_token", model.AccessToken);
            var response = client.Execute<dynamic>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return response.Data["user_id"] == model.IdToken
                && response.Data["email"] == model.UserEmail;
        }
    }
}
