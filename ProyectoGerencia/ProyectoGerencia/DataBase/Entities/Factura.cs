using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Entities
{
    public class Factura
    {
        public int FacturaId { get; set; }

        public string NombreImpuesto { get; set; }

        public string TipoImpuesto { get; set; }

        public string DescripcionImpuesto { get; set; }

        public DateTime Date { get; set; }

        public double Debito { get; set; }

        public double Credito { get; set; }

        public virtual PersonaJuridica PersonaJuridica { get; set; }
    }
}