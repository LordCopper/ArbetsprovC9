using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArbetsprovC9.Models
{
    public class SearchCriteria
    {
        [Display(Name = "Genre: ")]
        public string Genre { get; set; }

        [Display(Name = "Från år: ")]
        public string FromYear { get; set; }
        [Display(Name = "Till år: ")]
        public string ToYear { get; set; }
    }
}