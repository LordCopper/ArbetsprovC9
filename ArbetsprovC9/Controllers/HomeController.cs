using ArbetsprovC9.Models;
using ArbetsprovC9.Models.Base;
using ArbetsprovC9.Models.ViewModels;
using ArbetsprovC9.Util;
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



        public async Task<ResultViewModel> GetSearchResult(SearchCriteria searchCriteria)
        {
            Search searchSpotifyApi = new Search();
            SearchArtistResults searchResults = await searchSpotifyApi.SearchArtistsAsync(searchCriteria.Genre, searchCriteria.FromYear, searchCriteria.ToYear);
            ResultViewModel finalResults = await searchSpotifyApi.GeneratePresentationModel(searchResults);
            return finalResults;
        }

        public async Task<ResultViewModel> GetUrlSearchResult(string url)
        {
            Search searchSpotifyApi = new Search();
            SearchArtistResults searchResults = await searchSpotifyApi.SearchNextArtistsAsync(url);
            ResultViewModel finalResults = await searchSpotifyApi.GeneratePresentationModel(searchResults);
            return finalResults;
        }

        public async Task<ActionResult> Index()
        {

            //SearchArtistResults results = await SearchArtistsAsync("hip hop");
            //SearchTrackResults track = await SearchTracksAsync(results.Artists.Items[0].Name);
            //BaseTrack baseTrack = track.Tracks.Items.FirstOrDefault(x => x.PreviewURL != null);
            return View(new SearchCriteria());
        }

        [HttpPost]
        public async Task<ActionResult> Index(SearchCriteria searchCriteria)
        {
            ResultViewModel searchResults = await GetSearchResult(searchCriteria);           
            return View("Result", searchResults);
        }

        public async Task<ActionResult> Result(string navUrl)
        {
            ResultViewModel searchResults = await GetUrlSearchResult(navUrl);
            return View(searchResults);
        }
    }
}