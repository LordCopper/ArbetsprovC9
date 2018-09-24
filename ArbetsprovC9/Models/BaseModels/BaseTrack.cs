using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models.Base
{
    public class BaseTrack
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewURL { get; set; }

        [JsonProperty("explicit")]
        public bool IsExplicit { get; set; }

        [JsonProperty("is_playable")]
        public bool IsPlayable { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("href")]
        public string APIEndpointHRefForDetails { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }


        [JsonProperty("artists")]
        public BaseArtist[] Artists { get; set; }

        [JsonProperty("available_markets")]
        public string[] AvailableMarkets { get; set; }

        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty("duration_ms")]
        public int DurationInMilliSeconds { get; set; }

        [JsonProperty("external_ids")]
        public Dictionary<string, string> ExternalIds { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

    }
}