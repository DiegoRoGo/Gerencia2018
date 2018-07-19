
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Entities
{
    public class PersonaJuridica
    {
        public int PersonaJuridicaId { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public string Documento { get; set; }

        public virtual ICollection<Cuenta> Operadores { get; set; }

        public virtual ICollection<RepresentanteLegal> RepresentanteLegales { get; set; }
    }
}