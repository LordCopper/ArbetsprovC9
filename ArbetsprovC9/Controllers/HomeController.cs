using ArbetsprovC9.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArbetsprovC9.Controllers
{
    public class HomeController : Controller
    {
        private const string ClientId = "996d0037680544c987287a9b0470fdbb";
        private const string ClientSecret = "5a3c92099a324b8f9e45d77e919fec13";

        protected const string BaseUrl = "https://api.spotify.com/";

        private HttpClient GetDefaultClient()
        {
            AuthenticationHandler authHandler = new AuthenticationHandler(ClientId, ClientSecret, new HttpClientHandler());

            HttpClient client = new HttpClient(authHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            return client;
        }

        public async Task<SearchResults> SearchAsync(string genre, string fromYear = null, string toYear = null, string offset = null)
        {
            var client = GetDefaultClient();

            string url = "/v1/search?q=tag%3Ahipster%20";
            if (!string.IsNullOrWhiteSpace(genre))
            {
                genre = genre.Replace(" ", "%20").Replace("\"", "");
                url += "genre:%22" + genre + "%22";
            }
 
            if (fromYear != null && toYear != null)
            {
                url += "year:" + fromYear + "-" + toYear;
            }
            else if (fromYear != null || toYear != null)
            {
                url += "year:" + fromYear + toYear;
            }
 
            if (offset != null)
                url += "&limit=" + offset;

            string response = await client.GetStringAsync(url);

            var artistResponse = JsonConvert.DeserializeObject<SearchResults>(response);
            return artistResponse;
        }

        public ActionResult Index()
        {
            SearchResults results =  SearchAsync("hip hop").Result;
            return View();
        }

    }
}