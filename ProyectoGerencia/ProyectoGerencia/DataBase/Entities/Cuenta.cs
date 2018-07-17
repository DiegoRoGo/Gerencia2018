using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Entities
{
    public class Cuenta
    {
        [ForeignKey("Persona")]
        public int CuentaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Correo { get; set; }
        [StringLength(40)]
        public string Contrasena { get; set; }
        [Required]
        public bool Activado { get; set; }
        [StringLength(10)]
        public string CodigoDeVerificacion { get; set; }

        public virtual Persona Persona { get; set; }
    }
}