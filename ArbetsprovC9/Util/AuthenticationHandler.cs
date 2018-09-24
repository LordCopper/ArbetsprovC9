using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Authentication;
using System.Web.Caching;
using System.Web;
using System.Collections.Generic;
using System.Text;
using ArbetsprovC9.Models;
using ArbetsprovC9.Models.Util;

namespace ArbetsprovC9.Util
{
    public class AuthenticationHandler: DelegatingHandler
    {
        /// <summary>
        /// Majoriteten av den här filen är direkt kopierad från expemlet, jag gjorde så för att bättre lära mig och förstå hur allt hänger ihop.
        /// Jag har gjort några mindre ändringar för att få det att kompilera och för att få det lite mer i linje med hur jag skulle ha skrivit det.
        /// </summary>

        private const string AccountEndpoint = "https://accounts.spotify.com/api/token";
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthenticationHandler(string clientId, string clientSecret, HttpMessageHandler httpMessageHandler) :base(httpMessageHandler)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAuthenticationTokenAsync());
            }
      
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetAuthenticationTokenAsync()
        {
            string cacheKey = "SpotifyWebApiSession-Token" + _clientId;
            
            string token = HttpRuntime.Cache[cacheKey] as string;

            if (token == null)
            {
                DateTime timeBeforeRequest = DateTime.Now;

                AuthenticationResponse response = await GetAuthenticationTokenResponse();
                
                token = response?.AccessToken;
                if (token == null)
                {
                    throw new AuthenticationException("Spotify authentication failed");
                }

                DateTime timeTokenExpires = timeBeforeRequest.AddSeconds(response.ExpiresIn);

                HttpRuntime.Cache.Add(cacheKey, token, null, timeTokenExpires, Cache.NoSlidingExpiration, CacheItemPriority.High,null);
            }
            return token;
        }

        private async Task<AuthenticationResponse> GetAuthenticationTokenResponse()
        {
            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            string authHeader = BuildAuthHeader();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, AccountEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            requestMessage.Content = content;

            HttpResponseMessage response = await client.SendAsync(requestMessage);
            string responseString = await response.Content.ReadAsStringAsync();

            AuthenticationResponse authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);
            return authenticationResponse;
        }

        private string BuildAuthHeader()
        {
            return Base64Encode(_clientId + ":" + _clientSecret);
        }

        private string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

    }
}