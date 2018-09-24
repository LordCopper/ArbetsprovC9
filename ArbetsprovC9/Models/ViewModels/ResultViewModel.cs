using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models.ViewModels
{
    public class ResultViewModel
    {
        public List<ViewArtist> viewArtists { get; set; }

        public int Limit { get; set; }

        public string Next { get; set; }
        
        public int Offset { get; set; }
        
        public string Previous { get; set; }
        
        public int Total { get; set; }
    }
}