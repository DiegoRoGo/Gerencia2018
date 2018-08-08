using ProyectoGerencia.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.Pago
{
    public class AbonarVM
    {
        public int FacturaId { get; set; }


        public double SaldoTotal { get; set; }

        [Required(ErrorMessage = "El monto por abonar debe de ser válido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Digite montos mayores a ₡1")]
        public double MontoAbonar { get; set; }
    }
}