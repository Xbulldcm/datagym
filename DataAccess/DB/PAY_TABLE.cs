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
    
    public partial class PAY_TABLE
    {
        public string PAYMENT_ID { get; set; }
        public string PAYMENT_TYPE { get; set; }
        public string CARD_NUMBER { get; set; }
        public System.DateTime EXPIRES { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> PAID_DATE { get; set; }
        public int USER_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}