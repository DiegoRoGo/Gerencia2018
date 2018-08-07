using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.Pago
{
    public class PagoVM
    {
        public List<string> Montos { get; set; }

        [Required]
        public int NewMonto { get; set; }
    }
}