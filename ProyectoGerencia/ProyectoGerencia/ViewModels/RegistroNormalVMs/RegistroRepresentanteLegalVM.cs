using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGerencia.ViewModels.RegistroNormalVMs
{
    public class RegistroRepresentanteLegalVM
    {
        [Required(ErrorMessage = "Debe seleccionar un tipo de identificación")]
        [Display(Name = "Tipo de Identificación")]
        public string TipoSeleccionado { set; get; }
        public IEnumerable<SelectListItem> Tipos { set; get; }

        [Required(ErrorMessage = "Ingrese la identificación")]
        [Display(Name = "Identificación")]
        [MinLength(5, ErrorMessage = "La identificación debe contener más de 5 caracteres")]
        [MaxLength(30, ErrorMessage = "La identificación no puede contener más de 30 caracteres")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre completo")]
        [Display(Name = "Nombre completo")]
        [MinLength(2, ErrorMessage = "El nombre debe contener más de 2 caracteres")]
        [MaxLength(100, ErrorMessage = "El nombre no puede contener más de 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el correo electrónico")]
        [Display(Name = "Correo electrónico")]
        [MinLength(7, ErrorMessage = "El correo electrónico debe contener más de 7 caracteres")]
        [MaxLength(100, ErrorMessage = "El correo electrónicon no puede contener más de 100 caracteres")]
        [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }

        //public RegistroPersonaJuridicaVM PersonaJuridica { get; set; }

        public string CorreoElectronicoPersonaJuridica { get; set; }

        public string ContrasenaPersonaJuridica { get; set; }

        public string Documento { get; set; }
    }
}