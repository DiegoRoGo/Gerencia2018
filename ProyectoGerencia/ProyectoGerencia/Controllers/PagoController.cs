using ProyectoGerencia.ViewModels.Pago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGerencia.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Index()
        {
            return View(new PagoVM {
                Montos = new List<string>(),
                NewMonto = 0
            });
        }

        [HttpPost]
        public ActionResult Index(PagoVM Pago)
        {
            if (Pago.Montos == null)
                Pago.Montos = new List<string>();
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