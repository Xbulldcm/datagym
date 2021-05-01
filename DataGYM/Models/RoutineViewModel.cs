using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess.DB;

namespace DataGYM.Models
{
    public class RoutineViewModel
    {

        public string ROUTINE_ID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre para la rutina")]
        public string Name_Routine { get; set; }
        [Display(Name = "Nota")]
        public string NOTE_Routine { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Debe seleccionar un usuario para la rutina")]
        public int USER_ID { get; set; }
        [Display(Name = "Fecha de creación")]
        public System.DateTime CREATE_DATE_ROUTINE { get; set; }
        [Display(Name = "Estado")]
        public bool IS_ACTIVE { get; set; }
        public List<EXERCISE_TABLE> lista_ejercicios { get; set; }
        public List<ExerciseViewModel> lista_ejerciciosVM { get; set; }
        public EXERCISE_TABLE ejercicio_temp { get; set; }
        public List<EXERCISE_TYPE_TABLE> lista_tipos_ejercicios { get; set; }
        public List<UserViewModel> lista_usuarios { get; set; }
        public string nombreUsuario { get; set; }
    }

    public enum Dias
    {
        Lunes,
        Martes,
        Miércoles,
        Jueves,
        Viernes,
        Sábado,
        Domingo
    }
}