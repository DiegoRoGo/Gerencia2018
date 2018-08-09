using ProyectoGerencia.ViewModels.Pago;
using ProyectoGerencia.DataBase.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoGerencia.ViewModels.RegistroNormalVMs;
using ProyectoGerencia.DataBase.Entities;
using ProyectoGerencia.BusinessLogic;

namespace ProyectoGerencia.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Index()
        {
            return View(new PagoVM
            /*{
                Montos = new List<string>(),
                NewMonto = 0
            }*/());
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
            using (Context context = new Context())
            {
                //context.Database.Connection.Open();
                model.PersonasJuridicas = context.PersonasJuridicas.ToList();
            }
            return View(model);

        }

        private Factura CrearFactura(string _NombreImpuesto, string _TipoImpuesto, string _DescripcionImpuesto,
            DateTime _Date, double _Credito, int _PersonaJuridicaId)
        {
            Factura factura = new Factura();
            factura.NombreImpuesto = _NombreImpuesto;
            factura.TipoImpuesto = _TipoImpuesto;
            factura.DescripcionImpuesto = _DescripcionImpuesto;
            factura.Date = DateTime.Now;
            factura.Debito = 0;
            factura.Credito = _Credito;
            factura.PersonaJuridica.PersonaJuridicaId = _PersonaJuridicaId;
            return factura;
        }
        
        public ActionResult Abonar(double Saldo, int Id)
        {
            return View(new AbonarVM
            {
                SaldoTotal = Saldo,
                FacturaId = Id,
                MontoAbonar = 0
            });
        }

        public ActionResult Notificacion(string Email)
        {
            ConfirmacionPago(Email);
            return View();
        }


        //Parte 2 - Notificacion de Pagos - Daniel Lepiz / Javier Fernandez

        // Clase Controllers > Nombre del nuevo controller para Sprint 4
        public void ConfirmacionPago(string Email)
        {
            new EmailService().Facturas(Email);// Get de email del usuario a la DB
        }


        [HttpPost]
        public ActionResult Abonar(AbonarVM AbonarModel)
        {
            if (ModelState.IsValid && AbonarModel.MontoAbonar <= AbonarModel.SaldoTotal)
            {
                using (var Context = new Context())
                {
                    var Facs = Context.Facturas.ToList();
                    var Fac = Context.Facturas.SingleOrDefault(x => x.FacturaId == AbonarModel.FacturaId);
                    Context.Facturas.Add(new Factura
                    {
                        NombreImpuesto = Fac.NombreImpuesto,
                        TipoImpuesto = "Credito",
                        DescripcionImpuesto = Fac.DescripcionImpuesto,
                        Date = DateTime.Now,
                        Debito = 0,
                        Credito = AbonarModel.MontoAbonar,
                        PersonaJuridica = Fac.PersonaJuridica
                    });
                    Context.SaveChanges();
                    return RedirectToAction("Notificacion", "Pago", new { Email = Fac.PersonaJuridica.Correo });
                }
            }
            ViewBag.Error = "Se produjo un error al momento de abonar.";
            return View(AbonarModel);
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
            return View();

            //if (Pago.Montos == null)
            //{
            //    Pago.Montos = new List<string>();
            //}
            //Pago.Montos.Add(Pago.NewMonto.ToString());
            //ViewBag.Message = "Pago hecho correctamente";
            //return View(new PagoVM
            //{
            //    Montos = Pago.Montos,
            //    NewMonto = 0
            //});
        }
    }
}