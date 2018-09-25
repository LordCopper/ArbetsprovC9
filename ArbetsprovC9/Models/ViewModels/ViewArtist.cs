using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ArbetsprovC9.Models.Base;

namespace ArbetsprovC9.Models.ViewModels
{
    public class ViewArtist
    {
        public string Id { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Spotify Länk")]
        public string ExternalUrl { get; set; }

        [Display(Name = "Andra genres")]
        public string Genres { get; set; }

        public string APIEndpointHRefForDetails { get; set; }

        public Image Image { get; set; }

        public bool HasPreviewTrack { get; set; }

        public bool HasTracks { get; set; }

        public bool HasImage { get; set; }

        public ViewTrack PreviewTrack { get; set; }

        public ViewArtist() { }

        public ViewArtist(BaseArtist baseArtist)
        {
            Name = baseArtist.Name;

            Id = baseArtist.Id;

            APIEndpointHRefForDetails = baseArtist.APIEndpointHRefForDetails;

            if (baseArtist.ExternalUrls.ContainsKey("spotify"))
            {
                ExternalUrl = baseArtist.ExternalUrls["spotify"];
            }

            foreach(string genre in baseArtist.Genres)
            {
                Genres += genre+", ";
            }
            if( Genres != null)
            {
                Genres = Genres.Remove(Genres.Length - 2);
            }

            HasImage = false;

            if (baseArtist.Image.Count() > 0)
            {
                Image = baseArtist.Image[0];
                HasImage = true;
            }

            HasPreviewTrack = false;
            HasTracks = false;
        }
    }
}