using ProyectoGerencia.ViewModels.RegistroNormalVMs;
using ProyectoGerencia.DataBase.Configuration;
using System;
using System.Linq;
using System.Web.Mvc;
using ProyectoGerencia.DataBase.Entities;
using System.Net.Mail;
using System.Net;
using ProyectoGerencia.BusinessLogic;

namespace ProyectoGerencia.Controllers
{
    public class RegistroController : Controller
    {
        public ActionResult Registro()
        {
            RegistroVM vm = new RegistroVM();
            vm.Tipos = new[]
            {
                new SelectListItem { Value = "1", Text = "Nacional" },
                new SelectListItem { Value = "2", Text = "DIMEX" },
                new SelectListItem { Value = "3", Text = "Pasaporte" }
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Registro(RegistroVM ViewModel)
        {   
            //Se valida que el correo y el número de identificación no existan
            using (Context context = new Context())
            {
                bool error = false;
                if (context.Personas.SingleOrDefault(s => s.Identificacion.Equals(ViewModel.Identificacion)) != null)
                {
                    ViewBag.Error = "La identificación ya existe registrada";
                    error = true;
                }
                else if (context.Cuentas.SingleOrDefault(s => s.Correo.Equals(ViewModel.Correo)) != null)
                {
                    ViewBag.Error = "El correo ya está en uso, por favor digite uno nuevo";
                    error = true;

                }

                if (error)
                {
                    ViewModel.Tipos = new[]
                    {
                        new SelectListItem { Value = "1", Text = "Nacional" },
                        new SelectListItem { Value = "2", Text = "DIMEX" },
                        new SelectListItem { Value = "3", Text = "Pasaporte" }
                    };
                    return View(ViewModel);
                }
            }
            //Se crea la persona
            Persona per = new Persona()
            {
                Tipo = (Persona.TiposDeIdentificacion)Enum.Parse(typeof(Persona.TiposDeIdentificacion), ViewModel.TipoSeleccionado),
                Identificacion = ViewModel.Identificacion,
                Nombre = ViewModel.Nombre,
                Telefono = ViewModel.Telefono,
                DireccionFisica = ViewModel.Direccion,
                Cuenta = new Cuenta()
                {
                    Activado = false,
                    Correo = ViewModel.Correo,
                    CodigoDeVerificacion = GenerarCodigo()
                }
            };
            //Se agrega la persona a la base
            using (Context context = new Context())
            {
                context.Personas.Add(per);

                context.SaveChanges();
            }
            //Se envia el correo
            new EmailService().SendEmail(per.Cuenta.Correo, per.Cuenta.CodigoDeVerificacion);
            //Se redirecciona
            return RedirectToAction("NotificarActivacion", "Registro", new NotificarActivacionVM() { email = per.Cuenta.Correo });
        }

        public ActionResult Confirmacion(string c)
        {
            string Email = new Encriptacion().Desencriptar(c);

            Context context = new Context();
            Cuenta Account = context.Cuentas.SingleOrDefault(s => s.Correo.Equals(Email));
            if (Account == null || Account.Activado)
            {
                return new HttpNotFoundResult();
            }

            return View(new ConfirmacionVM() { TerminosCondiciones = false, Correo = Email });
        }

        [HttpPost]
        public ActionResult Confirmacion(ConfirmacionVM model)
        {
            Context context = new Context();
            Cuenta account = context.Cuentas.SingleOrDefault(s => s.Correo.Equals(model.Correo));

            if (account != null)
            {
                if (account.CodigoDeVerificacion.Equals(model.CodigoVerificacion))
                {
                    account.Contrasena = new Encriptacion().Encriptar(model.Contrasena);
                    account.Activado = true;

                    context.SaveChanges();

                    return RedirectToAction("PostActivacion", "Registro");
                }
                else
                {
                    ViewBag.Error = "Código de activación erroneo";
                    return View(model);
                }
            }
            return new HttpNotFoundResult(); ;
        }

        public ActionResult NotificarActivacion(NotificarActivacionVM vm)
        {
            ViewBag.correo = vm.email;
            return View();
        }

        public ActionResult PostActivacion()
        {
            return View();
        }

        private string GenerarCodigo()
        {
            Context context = new Context();

            const string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!@#$%^&*()_+'[]{}";
            string code;
            Random rnd = new Random();
            do
            {
                code = "";
                for (int i = 0; i < 10; i++)
                {
                    code += chars[rnd.Next(chars.Length)];
                }
            } while (context.Cuentas.Where(s => s.CodigoDeVerificacion == code).FirstOrDefault() != null);

            return code;
        }
    }
}