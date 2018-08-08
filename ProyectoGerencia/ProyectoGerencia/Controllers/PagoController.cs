using ProyectoGerencia.ViewModels.Pago;
using ProyectoGerencia.DataBase.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoGerencia.ViewModels.RegistroNormalVMs;

namespace ProyectoGerencia.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Index()
        {
            return View(new PagoVM
            {
                Montos = new List<string>(),
                NewMonto = 0
            });
        }


        public ActionResult Detalles(int id)
        {
            var model = new DetallesVM();
            var context = new Context();
            model.PersonaJuridica = context.PersonasJuridicas.SingleOrDefault(persona => persona.PersonaJuridicaId == id);

            return View(model);
        }

        public ActionResult ListaPersonaJuridica()
        {
            var model = new ListaPersonaJuridicaVM();
            using (Context Context = new Context())
            {
                model.PersonasJuridicas = Context.PersonasJuridicas.ToList();
            }
            return View(model);

        }


        [HttpPost]
        public ActionResult Index(PagoVM Pago)
        {

            var model = new PagoVM();
            //lista de facturas
            var lista = new List<SelectListItem>();

            using (Context Context = new Context())
            {
                var personasJuridicas = Context.PersonasJuridicas.ToList();
                foreach (var persona in personasJuridicas)
                {
                    lista.Add(new SelectListItem() { Value = persona.PersonaJuridicaId.ToString()/*, Text = persona.Nombre + " " + persona.Apellido*/ });
                }
            }


            if (Pago.Montos == null)
            {
                Pago.Montos = new List<string>();
            }
            Pago.Montos.Add(Pago.NewMonto.ToString());
            ViewBag.Message = "Pago hecho correctamente";
            return View(new PagoVM
            {
                Montos = Pago.Montos,
                NewMonto = 0
            });
        }
    }
}