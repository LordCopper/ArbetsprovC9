using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models.Base
{
    public class SearchArtistResults
    {
        [JsonProperty("artists")]
        public SearchArtistCollection Artists { get; set; }
    }

    public class SearchArtistCollection
    {

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public IList<BaseArtist> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class SearchTrackResults
    {
        [JsonProperty("tracks")]
        public SearchTrackCollection Tracks { get; set; }
    }

    public class SearchTrackCollection
    {

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public IList<BaseTrack> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}