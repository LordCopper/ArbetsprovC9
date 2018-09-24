using ArbetsprovC9.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models.ViewModels
{
    public class ViewTrack
    {
        public string Id { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Smakprov")]
        public string PreviewURL { get; set; }

        [Display(Name = "Explicit")]
        public bool IsExplicit { get; set; }

        public string APIEndpointHRefForDetails { get; set; }

        [Display(Name = "Spotify Länk")]
        public string ExternalUrl { get; set; }
        
        public ViewTrack() { }

        public ViewTrack(BaseTrack baseTrack)
        {
            Name = baseTrack.Name;

            Id = baseTrack.Id;

            APIEndpointHRefForDetails = baseTrack.APIEndpointHRefForDetails;

            IsExplicit = baseTrack.IsExplicit;

            if (baseTrack.ExternalUrls.ContainsKey("spotify"))
            {
                ExternalUrl = baseTrack.ExternalUrls["spotify"];
            }

            if(!String.IsNullOrWhiteSpace(baseTrack.PreviewURL))
            {
                PreviewURL = baseTrack.PreviewURL;
            }

        }
    }
}