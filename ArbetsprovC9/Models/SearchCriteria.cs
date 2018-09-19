using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models
{
    public class SearchCriteria
    {
        public string Genre { get; set; }
        public string Year { get; set; }
        public bool OnlyUnknownArtists { get; set; }
    }
}