using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.Pago
{
    public class PagoVM
    {
        [Required(ErrorMessage = "El monto por abonar debe de ser válido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Digite montos mayores a ₡1")]
        public double Monto { get; set; }
        
        public List<string> Montos { get; set; }

        [Required]
        public int NewMonto { get; set; }
    }
}