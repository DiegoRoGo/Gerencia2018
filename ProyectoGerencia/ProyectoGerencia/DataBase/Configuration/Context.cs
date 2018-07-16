using ProyectoGerencia.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Configuration
{
    public class Context : DbContext
    {
        public Context() : base("GerenciaDB")
        {
        }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure StudentId as FK for StudentAddress
            modelBuilder.Entity<Persona>()
                        .HasRequired(s => s.Cuenta)
                        .WithRequiredPrincipal(ad => ad.Persona);

        }
    }
}