using ProyectoGerencia.DataBase.Configuration;
using ProyectoGerencia.ViewModels.RegistroNormalVMs;
using System;
using System.Collections.Generic;
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
                        return View(new RegistroPersonaJuridicaVM());
                    }
                }

                RegistroRepresentanteLegalVM Representante = new RegistroRepresentanteLegalVM()
                {
                    CorreoElectronicoPersonaJuridica = Persona.CorreoElectronico,
                    ContrasenaPersonaJuridica = Persona.Contrasena
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
            RegistroRepresentanteLegal2VM R2 = new RegistroRepresentanteLegal2VM
            {
                Tipos = new[]
                {
                    new SelectListItem { Value = "1", Text = "Nacional" },
                    new SelectListItem { Value = "2", Text = "DIMEX" },
                    new SelectListItem { Value = "3", Text = "Pasaporte" }
                },
                CorreoElectronicoPersonaJuridica = RepresentanteLegal.CorreoElectronicoPersonaJuridica,
                ContrasenaPersonaJuridica = RepresentanteLegal.ContrasenaPersonaJuridica,
                CorreoRep1 = RepresentanteLegal.Correo,
                IdentificacionRep1 = RepresentanteLegal.Identificacion,
                NombreRep1 = RepresentanteLegal.Nombre,
                TipoIdentificacionRep1 = RepresentanteLegal.TipoSeleccionado
            };
            return View(R2);
        }

        [HttpPost]
        public ActionResult RegistrarSegundoRepresentante(RegistroRepresentanteLegal2VM RepresentanteLegal)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
    }
}