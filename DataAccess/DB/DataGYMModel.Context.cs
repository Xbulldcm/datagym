﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataGYMEntities : DbContext
    {
        public DataGYMEntities()
            : base("name=DataGYMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EXERCISE_TABLE> EXERCISE_TABLE { get; set; }
        public virtual DbSet<EXERCISE_TYPE_TABLE> EXERCISE_TYPE_TABLE { get; set; }
        public virtual DbSet<MEMBERSHIP_TABLE> MEMBERSHIP_TABLE { get; set; }
        public virtual DbSet<PAY_TABLE> PAY_TABLE { get; set; }
        public virtual DbSet<ROLE_TABLE> ROLE_TABLE { get; set; }
        public virtual DbSet<ROUTINE_TABLE> ROUTINE_TABLE { get; set; }
        public virtual DbSet<SITE_TABLE> SITE_TABLE { get; set; }
        public virtual DbSet<USER_TABLE> USER_TABLE { get; set; }
        public virtual DbSet<APPOINTMENT_TABLE> APPOINTMENT_TABLE { get; set; }
    }
}