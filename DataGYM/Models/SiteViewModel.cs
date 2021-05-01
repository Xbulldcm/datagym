using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataGYM.Models
{
    public class SiteViewModel
    {

        public int SITE_ID { get; set; }
        [Display(Name = "Localización")]
        [Required(ErrorMessage = "Debe ingresar una localización")]
        public string LOCATION { get; set; }

    }
}