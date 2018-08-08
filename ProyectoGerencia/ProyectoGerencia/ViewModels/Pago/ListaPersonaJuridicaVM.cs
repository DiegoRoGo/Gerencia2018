using ProyectoGerencia.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.Pago
{
    public class ListaPersonaJuridicaVM
    {
        public ICollection<PersonaJuridica> PersonasJuridicas { get; set; }
    }
}