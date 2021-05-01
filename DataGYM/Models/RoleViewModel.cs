using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataGYM.Models
{
    public class RoleViewModel
    {

        public int ROLE_ID { get; set; }
        [Display(Name = "Nombre del rol")] 
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string NAME { get; set; }
        [Display(Name = "Descripción del rol")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        public string DESCRIPTION { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Debe seleccionar el estado del rol")]
        public bool IS_ACTIVE { get; set; }

    }
}