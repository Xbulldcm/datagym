using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess.DB;

namespace DataGYM.Models
{
    public class AppointmentViewModel
    {

        public int APPOINTMENT_ID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string NAME { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string LASTNAME { get; set; }
        [Display(Name = "Número de contacto")]
        [Required(ErrorMessage = "Debe ingresar un número de contacto")]
        public Nullable<int> CONTACT_NUMBER { get; set; }
        [Display(Name = "Fecha de la cita")]
        [Required(ErrorMessage = "Debe seleccionar una fecha para cita")]
        public Nullable<System.DateTime> DATE_APPOINTMENT { get; set; }
        [Display(Name = "Hora de la cita")]
        [Required(ErrorMessage = "Debe seleccionar la hora de la cita")]
        public string TIME_APPOINTMENT { get; set; }
        [Display(Name = "Mensaje")]
        public string MESSAGE_APPOINTMENT { get; set; }
        [Display(Name = "Site")]
        public Nullable<int> SITE_ID { get; set; }
        [Display(Name = "Estado")]
        public bool IS_ACTIVE { get; set; }
        public List<SITE_TABLE> lista_sites { get; set; }

    }
}