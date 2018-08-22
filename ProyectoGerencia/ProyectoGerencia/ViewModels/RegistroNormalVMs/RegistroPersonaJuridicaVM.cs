using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.RegistroNormalVMs
{
    public class RegistroPersonaJuridicaVM
    {
        [Required(ErrorMessage = "Ingrese el correo electrónico")]
        [Display(Name = "Correo electrónico")]
        [MinLength(7, ErrorMessage = "El correo electrónico debe contener más de 7 caracteres")]
        [MaxLength(100, ErrorMessage = "El correo electrónicon no puede contener más de 100 caracteres")]
        [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "El correo no es válido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [MaxLength(40, ErrorMessage = "La contraseña no puede tener más de 40 caracteres")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-z])(.*)$", ErrorMessage = "La contraseña debe tener al menos una letra y un número")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "La confirmación de la  contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña")]
        [Compare("Contrasena", ErrorMessage = "Ambas contraseñas deben de ser iguales")]
        public string ConfirmarContrasena { get; set; }

        [Required(ErrorMessage = "El archivo es requerido")]
        [Display(Name = "Archivo")]
        public HttpPostedFileBase postedFile { get; set; }

        public string NombreDoc { get; set; }
    }
}