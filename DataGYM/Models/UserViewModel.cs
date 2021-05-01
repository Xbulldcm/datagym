using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess.DB;

namespace DataGYM.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 1)]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string LastName { get; set; }
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "Debe ingresar una identificación")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 9)]
        public string Identification { get; set; }
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Debe ingresar un correo electrónico")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Email { get; set; }
        [Display(Name = "Dirección")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string Address { get; set; }
        [Display(Name = "Número telefónico")]
        [Required(ErrorMessage = "Debe ingresar un número telefónico")]
        public Nullable<int> PhoneNumber { get; set; }
        [Display(Name = "Nombre del Manager")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 1)]
        public string ManagerName { get; set; }
        [Display(Name = "Membresía")]
        [Required(ErrorMessage = "Debe seleccionar una membresía")]
        public Nullable<int> FKMB { get; set; }
        public List<MEMBERSHIP_TABLE> lista_membresias { get; set; }
        public string FKPAY { get; set; }
        public List<PAY_TABLE> lista_pagos { get; set; }
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public Nullable<int> FKROLE { get; set; }
        public List<ROLE_TABLE> lista_roles { get; set; }
        public Nullable<int> FKRT { get; set; }
        public List<ROUTINE_TABLE> lista_rutinas { get; set; }
        public List<RoutineViewModel> lista_rutinasVM { get; set; }
        public List<PayViewModel> lista_pagosVM { get; set; }
        [Display(Name = "Site")]
        [Required(ErrorMessage = "Debe seleccionar un site")]
        public Nullable<int> FKSITE { get; set; }
        public List<SITE_TABLE> lista_sites { get; set; }
        [Display(Name = "Estado")]
        public bool IS_ACTIVE { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
        public string nombreRol { get; set; }
        public string nombreMembresia{ get; set; }
        public string nombreSite { get; set; }
        public string nombreCompletoIdent { get; set; }
    }
}