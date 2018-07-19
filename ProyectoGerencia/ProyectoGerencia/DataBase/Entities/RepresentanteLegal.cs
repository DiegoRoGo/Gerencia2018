using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Entities
{
    public class RepresentanteLegal
    {
        public enum TipoIdentificacion
        {
            Nacional = 1,
            DIMEX = 2,
            Pasaporte = 3
        }
        public int RepresentanteLegalId { get; set; }

        [Required]
        [StringLength(30)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }

        [Required]
        public TipoIdentificacion Tipo { get; set; }
        
    }
}