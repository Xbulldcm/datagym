using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataGYM.Models
{
    public class MembershipViewModel
    {

        public int MEMBERSHIP_ID { get; set; }
        [Display(Name = "Nombre de la membresía")]
        [Required(ErrorMessage = "Debe ingresar el nombre de membresía")]
        public string DESCRIPTION { get; set; }
        [Display(Name = "Tipo de membresía")]
        [Required(ErrorMessage = "Debe ingresar un tipo de membresía")]
        public string MEM_TYPE { get; set; }

    }
}