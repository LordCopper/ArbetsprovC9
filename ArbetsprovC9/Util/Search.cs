using ArbetsprovC9.Models.Base;
using ArbetsprovC9.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ArbetsprovC9.Util
{
    public class Search
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

        public async Task<ResultViewModel> GeneratePresentationModel(SearchArtistResults searchResults)
        {
            SearchTrackResults searchTracks;
            BaseTrack previewTrack;
            ResultViewModel finalResults = new ResultViewModel();

            finalResults.viewArtists = new List<ViewArtist>();
            finalResults.Limit = searchResults.Artists.Limit;
            finalResults.Next = searchResults.Artists.Next;
            finalResults.Total = searchResults.Artists.Total;
            finalResults.Previous = searchResults.Artists.Previous;
            finalResults.Offset = searchResults.Artists.Offset;

            foreach (var artist in searchResults.Artists.Items)
            {
                ViewArtist viewArtist = new ViewArtist(artist);

                searchTracks = await SearchTracksAsync(artist.Name);
                if (searchTracks.Tracks.Items.Count() != 0)
                {
                    previewTrack = searchTracks.Tracks.Items.FirstOrDefault(x => x.PreviewURL != null);
                    if (previewTrack != null)
                    {
                        viewArtist.PreviewTrack = new ViewTrack(previewTrack);
                        viewArtist.HasPreviewTrack = true;
                        viewArtist.HasTracks = true;
                    }
                    else
                    {
                        viewArtist.PreviewTrack = new ViewTrack(searchTracks.Tracks.Items[0]);
                        viewArtist.HasPreviewTrack = false;
                        viewArtist.HasTracks = true;
                    }
                }
                else
                {
                    viewArtist.HasPreviewTrack = false;
                    viewArtist.HasTracks = false;
                }

                finalResults.viewArtists.Add(viewArtist);
            }

            return finalResults;
        }


        public async Task<SearchArtistResults> SearchArtistsAsync(string genre, string fromYear = "", string toYear = "")
        {
            HttpClient client = GetDefaultClient();
            string url = "/v1/search?q=tag%3Ahipster%20";
            if (!string.IsNullOrWhiteSpace(genre))
            {
                genre = genre.Replace(" ", "%20").Replace("\"", "");
                url += "genre%3A%22" + genre + "%22";
            }

            if (!string.IsNullOrWhiteSpace(fromYear) && !string.IsNullOrWhiteSpace(toYear))
            {
                url += "year%3A" + fromYear + "-" + toYear;
            }
            else if (!string.IsNullOrWhiteSpace(fromYear) || !string.IsNullOrWhiteSpace(toYear))
            {
                url += "year%3A" + fromYear + toYear;
            }

            url += "&type=artist";

            string response = await client.GetStringAsync(url);

            SearchArtistResults artistResponse = JsonConvert.DeserializeObject<SearchArtistResults>(response);
            return artistResponse;
        }

        public async Task<SearchArtistResults> SearchNextArtistsAsync(string nextUrl)
        {
            HttpClient client = GetDefaultClient();
            string response = await client.GetStringAsync(nextUrl);
            SearchArtistResults artistResponse = JsonConvert.DeserializeObject<SearchArtistResults>(response);
            return artistResponse;
        }


        public async Task<SearchTrackResults> SearchTracksAsync(string artistName)
        {
            //https://api.spotify.com/v1/artists/0TnOYISbd1XYRBk9myaseg/top-tracks

            var client = GetDefaultClient();

            string url = "/v1/search?q=artist%3A%22" + artistName + "%22";
            url += "&type=track&market=SE";

            //string url = "/v1/artists/0TnOYISbd1XYRBk9myaseg/top-tracks";

            string response = await client.GetStringAsync(url);

            SearchTrackResults artistResponse = JsonConvert.DeserializeObject<SearchTrackResults>(response);
            return artistResponse;
        }
    }
}