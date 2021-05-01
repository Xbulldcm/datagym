using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataGYM.Models
{
    public class PayViewModel
    {

        public string PAYMENT_ID { get; set; }
        [Display(Name = "Tipo de pago")]
        [Required(ErrorMessage = "Debe ingresar un tipo de pago")]
        public string PAYMENT_TYPE { get; set; }
        [Display(Name = "Monto")]
        public string CARD_NUMBER { get; set; }
        [Display(Name = "Estado")]
        public bool IS_ACTIVE { get; set; }
        [Display(Name = "Expira")]
        [Required(ErrorMessage = "Debe seleccionar la fecha en la que expira")]
        public DateTime EXPIRES { get; set; }
        public List<UserViewModel> lista_usuariosVM { get; set; }

    }
}