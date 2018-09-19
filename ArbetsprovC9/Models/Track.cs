using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models
{
    public class Track
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewURL { get; set; }

        [JsonProperty("explicit")]
        public bool IsExplicit { get; set; }

        [JsonProperty("is_playable")]
        public bool IsPlayable { get; set; }

        [JsonProperty("href")]
        public string APIEndpointHRefForDetails { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string,string> ExternalUrls { get; set; }
    }
}
