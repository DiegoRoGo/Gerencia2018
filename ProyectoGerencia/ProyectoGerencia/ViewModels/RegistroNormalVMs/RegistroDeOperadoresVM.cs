using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.RegistroNormalVMs
{
    public class RegistroDeOperadoresVM
    {
        public RegistroDeOperadoresVM()
        {
            Operadores = new List<string>();
        }

        public string CorreoElectronicoPersonaJuridica { get; set; }

        public string ContrasenaPersonaJuridica { get; set; }

        public string Documento { get; set; }

        public string IdentificacionRep1 { get; set; }

        public string TipoIdentificacionRep1 { get; set; }

        public string NombreRep1 { get; set; }

        public string CorreoRep1 { get; set; }

        public string IdentificacionRep2 { get; set; }

        public string TipoIdentificacionRep2 { get; set; }

        public string NombreRep2 { get; set; }

        public string CorreoRep2 { get; set; }

        public string Email { get; set; }

        public List<string> Operadores { get; set; }
    }
}