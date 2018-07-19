using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.ViewModels.RegistroNormalVMs
{
    public class ConfirmacionVM
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El código de verificación es requerido")]
        [Display(Name = "Código de verificación")]
        public string CodigoVerificacion { get; set; }

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
        [Compare("Contrasena", ErrorMessage = "La contraseña y la confirmación deben ser iguales")]
        public string Confirmacion { get; set; }
        
        [Display(Name = @"He leído y acepto los <a href=""localhost:50420/Registro/Terminos"" target=""_blank"">terminos y condiciones</a>")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe aceptar los terminos y condiciones para activar su cuenta.")]
        public bool TerminosCondiciones { get; set; }
    }
}