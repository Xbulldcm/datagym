//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class APPOINTMENT_TABLE
    {
        public int APPOINTMENT_ID { get; set; }
        public string NAME { get; set; }
        public string LASTNAME { get; set; }
        public Nullable<int> CONTACT_NUMBER { get; set; }
        public Nullable<System.DateTime> DATE_APPOINTMENT { get; set; }
        public string TIME_APPOINTMENT { get; set; }
        public string MESSAGE_APPOINTMENT { get; set; }
        public Nullable<int> SITE_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
    }
}
