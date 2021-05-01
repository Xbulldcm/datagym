using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataGYM.Models
{
    public class ExerciseTypeViewModel
    {

        public int EXERCISE_TYPE_ID { get; set; }
        [Display(Name = "Tipo de ejercicio")]
        [Required(ErrorMessage = "Debe ingresar un tipo de ejercicio")]
        public string EXERCISE_TYPE_NAME { get; set; }

    }
}