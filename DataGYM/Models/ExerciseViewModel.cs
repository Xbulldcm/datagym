using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataGYM.Models
{
    public class ExerciseViewModel
    {

        public int EXERCISE_ID { get; set; }
        [Display(Name = "Tipo de ejercicio")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de ejercicio")]
        public int EXERCISE_TYPE_ID { get; set; }
        [Display(Name = "Tipo de ejercicio")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de ejercicio")]
        public string EXERCISE_TYPE_ID_NAME { get; set; }
        [Display(Name = "Día del ejercicio")]
        [Required(ErrorMessage = "Debe ingresar un día para el ejercicio")]
        public string EXERCISE_DAY { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string EXERCISE_NAME { get; set; }
        [Display(Name = "Repeticiones")]
        [Required(ErrorMessage = "Debe ingresar la cantidad de repeticiones")]
        public int EXERCISE_REP_COUNT { get; set; }
        [Display(Name = "Rondas")]
        [Required(ErrorMessage = "Debe ingresar la cantidad de rondas")]
        public int EXERCISE_REP_ROUND { get; set; }
        [Display(Name = "Notas")]
        public string EXECISE_NOTE { get; set; }
        [Display(Name = "Estado")]
        public bool IS_ACTIVE { get; set; }
        public string ROUTINE_ID { get; set; }
    }
}