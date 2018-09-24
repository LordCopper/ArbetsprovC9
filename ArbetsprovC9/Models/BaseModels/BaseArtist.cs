using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models.Base
{
    public class BaseArtist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        [JsonProperty("href")]
        public string APIEndpointHRefForDetails { get; set; }

        [JsonProperty("images")]
        public Image[] Image { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

    }
}

