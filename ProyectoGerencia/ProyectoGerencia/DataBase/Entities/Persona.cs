using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Entities
{
    public class Persona
    {
        public enum TiposDeIdentificacion
        {
            Nacional = 1,
            DIMEX = 2,
            Pasaporte = 3
        }
        public int PersonaId { get; set; }

        [Required]
        [StringLength(30)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(500)]
        public string DireccionFisica { get; set; }

        [Required]
        public TiposDeIdentificacion Tipo { get; set; }
        
        public virtual Cuenta Cuenta { get; set; }
    }
}