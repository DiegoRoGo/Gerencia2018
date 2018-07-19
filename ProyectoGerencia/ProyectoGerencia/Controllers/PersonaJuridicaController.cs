using ProyectoGerencia.BusinessLogic;
using ProyectoGerencia.DataBase.Configuration;
using ProyectoGerencia.DataBase.Entities;
using ProyectoGerencia.ViewModels.RegistroNormalVMs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGerencia.Controllers
{
    public class PersonaJuridicaController : Controller
    {
        // GET: PersonaJuridica
        public ActionResult Create()
        {
            return View(new RegistroPersonaJuridicaVM());
        }

        [HttpPost]
        public ActionResult Create(RegistroPersonaJuridicaVM Persona)
        {
            if (ModelState.IsValid)
            {
                using (Context Context = new Context())
                {
                    if (Context.PersonasJuridicas.Any(x => x.Correo == Persona.CorreoElectronico))
                    {
                        ViewBag.Error = "Ya hay un usuario con este correo.";
                        return View(new RegistroPersonaJuridicaVM());
                    }
                }

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Persona.postedFile.SaveAs(path + Path.GetFileName(Persona.postedFile.FileName));
                RegistroRepresentanteLegalVM Representante = new RegistroRepresentanteLegalVM()
                {
                    CorreoElectronicoPersonaJuridica = Persona.CorreoElectronico,
                    ContrasenaPersonaJuridica = Persona.Contrasena,
                    Documento = Persona.postedFile.FileName
                };

                return RedirectToAction("RegistrarPrimerRepresentante", "PersonaJuridica", Persona);
            }
            return View();
        }
        
        public ActionResult RegistrarPrimerRepresentante(RegistroPersonaJuridicaVM Persona)
        {
            return View(new RegistroRepresentanteLegalVM()
            {
                Tipos = new[]
                {
                    new SelectListItem { Value = "1", Text = "Nacional" },
                    new SelectListItem { Value = "2", Text = "DIMEX" },
                    new SelectListItem { Value = "3", Text = "Pasaporte" }
                },
                ContrasenaPersonaJuridica = Persona.Contrasena,
                CorreoElectronicoPersonaJuridica = Persona.CorreoElectronico
            });
        }

        [HttpPost]
        public ActionResult RegistrarPrimerRepresentante(RegistroRepresentanteLegalVM Representante)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("RegistrarSegundoRepresentante", "PersonaJuridica", Representante);
            }
            Representante.Tipos = new[]
            {
                new SelectListItem { Value = "1", Text = "Nacional" },
                new SelectListItem { Value = "2", Text = "DIMEX" },
                new SelectListItem { Value = "3", Text = "Pasaporte" }
            };
            return View(Representante);
        }

        public ActionResult RegistrarSegundoRepresentante(RegistroRepresentanteLegalVM RepresentanteLegal)
        {
            return View(new RegistroSegundoRepresentanteLegalVM
            {
                Tipos = new[]
                {
                    new SelectListItem { Value = "1", Text = "Nacional" },
                    new SelectListItem { Value = "2", Text = "DIMEX" },
                    new SelectListItem { Value = "3", Text = "Pasaporte" }
                },
                CorreoElectronicoPersonaJuridica = RepresentanteLegal.CorreoElectronicoPersonaJuridica,
                ContrasenaPersonaJuridica = RepresentanteLegal.ContrasenaPersonaJuridica,
                Documento = RepresentanteLegal.Documento,
                CorreoRep1 = RepresentanteLegal.Correo,
                IdentificacionRep1 = RepresentanteLegal.Identificacion,
                NombreRep1 = RepresentanteLegal.Nombre,
                TipoIdentificacionRep1 = RepresentanteLegal.TipoSeleccionado
            });
        }

        [HttpPost]
        public ActionResult RegistrarSegundoRepresentante(RegistroSegundoRepresentanteLegalVM RepresentanteLegal)
        {
            if (ModelState.IsValid)
            {
                if(RepresentanteLegal.Cedula != RepresentanteLegal.IdentificacionRep1)
                {
                    return RedirectToAction("RegistroDeOperadores", "PersonaJuridica", RepresentanteLegal);
                }
            }
            RepresentanteLegal.Tipos = new[]
            {
                new SelectListItem { Value = "1", Text = "Nacional" },
                new SelectListItem { Value = "2", Text = "DIMEX" },
                new SelectListItem { Value = "3", Text = "Pasaporte" }
            };
            return View(RepresentanteLegal);
        }

        public ActionResult RegistroDeOperadores(RegistroSegundoRepresentanteLegalVM Representante)
        {
            return View(new RegistroDeOperadoresVM()
            {
                ContrasenaPersonaJuridica = Representante.ContrasenaPersonaJuridica,
                CorreoElectronicoPersonaJuridica = Representante.CorreoElectronicoPersonaJuridica,
                Documento = Representante.Documento,
                CorreoRep1 = Representante.CorreoRep1,
                CorreoRep2 = Representante.CorreoElect,
                IdentificacionRep1 = Representante.IdentificacionRep1,
                IdentificacionRep2 = Representante.Cedula,
                NombreRep1 = Representante.NombreRep1,
                NombreRep2 = Representante.NombreCompleto,
                TipoIdentificacionRep1 = Representante.TipoIdentificacionRep1,
                TipoIdentificacionRep2 = Representante.TipoSeleccionado
            });
        }

        [HttpPost]
        public ActionResult RegistroDeOperadores(RegistroDeOperadoresVM Operador)
        {
            if (String.IsNullOrEmpty(Operador.Email))
            {
                if(Operador.Operadores.Count > 0)
                {
                    string Cod = GenerarCodigo();
                    using (var Context = new Context())
                    {
                        var PersonaJuridica = Context.PersonasJuridicas.Add(new DataBase.Entities.PersonaJuridica
                        {
                            Contrasena = Operador.ContrasenaPersonaJuridica,
                            Correo = Operador.CorreoElectronicoPersonaJuridica,
                            Documento = Operador.Documento,
                            Activacion = false,
                            Codigo = Cod,
                            Operadores = new List<Cuenta>(),
                            RepresentanteLegales = new List<RepresentanteLegal>()
                        });

                        foreach (var Item in Operador.Operadores)
                        {
                            PersonaJuridica.Operadores.Add(Context.Cuentas.Where(x=> x.Correo == Item).SingleOrDefault());
                        }

                        PersonaJuridica.RepresentanteLegales.Add(new DataBase.Entities.RepresentanteLegal
                        {
                            CorreoElectronico = Operador.CorreoRep1,
                            Identificacion = Operador.IdentificacionRep1,
                            Nombre = Operador.NombreRep1,
                            Tipo = (RepresentanteLegal.TipoIdentificacion)Enum.Parse(typeof(RepresentanteLegal.TipoIdentificacion), Operador.TipoIdentificacionRep1)
                        });

                        PersonaJuridica.RepresentanteLegales.Add(new DataBase.Entities.RepresentanteLegal
                        {
                            CorreoElectronico = Operador.CorreoRep2,
                            Identificacion = Operador.IdentificacionRep2,
                            Nombre = Operador.NombreRep2,
                            Tipo = (RepresentanteLegal.TipoIdentificacion)Enum.Parse(typeof(RepresentanteLegal.TipoIdentificacion), Operador.TipoIdentificacionRep2)
                        });

                        Context.SaveChanges();
                    }
                    return RedirectToAction("ConfirmacionRegistro", "PersonaJuridica", new { Email = Operador.CorreoElectronicoPersonaJuridica, Codigo = Cod });
                }
                ViewBag.Error = "Es necesario registrar operadores";
                return View(Operador);
            }
            
            using(var Context = new Context())
            {
                var Cuenta = Context.Cuentas.Where(x => x.Correo == Operador.Email).SingleOrDefault();
                if (Cuenta != null)
                {
                    if(Cuenta.PersonaJuridica != null)
                    {
                        ViewBag.Error = "El usuario ya es operador de una persona juridica, no se ha podido agregar";
                        return View(Operador);
                    }
                    if (Operador.Operadores.Any(x=> x == Operador.Email))
                    {
                        ViewBag.Error = "El operador ya habia sido agregado";
                        return View(Operador);
                    }
                    Operador.Operadores.Add(Operador.Email);
                    Operador.Email = "";
                    return View(Operador);
                }
            }
            ViewBag.Error = "El correo es inexistente";
            return View(Operador);
        }


        public ActionResult ConfirmacionRegistro(string Email, string Codigo)
        {
            new EmailService().SendEmail(Email, Codigo, "PersonaJuridica/Confirmacion");
            ViewBag.correo = Email;
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

        public ActionResult Confirmacion(string Email)
        {
            Email = new Encriptacion().Desencriptar(Email);
            using(Context Context = new Context())
            {
                var Persona = Context.PersonasJuridicas.Where(x => x.Correo == Email && !x.Activacion).SingleOrDefault();
                if (Persona != null)
                {
                    Persona.Activacion = true;
                    Context.SaveChanges();
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}