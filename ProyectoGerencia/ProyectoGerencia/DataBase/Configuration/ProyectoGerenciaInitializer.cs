using ProyectoGerencia.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.DataBase.Configuration
{
    public class ProyectoGerenciaInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var personas = new List<Persona>
            {
                new Persona {Identificacion = "116700763", Nombre = "Javier Fernandez Alvarado", Telefono = "61041397", DireccionFisica = "Cartago, Cartago, Costa Rica", Tipo = Persona.TiposDeIdentificacion.Nacional },
            };

            personas.ForEach(persona => context.Personas.Add(persona));

            var personasJuridicas = new List<PersonaJuridica>
            {
                new PersonaJuridica {Correo = "javierf97@yahoo.com", Contrasena = "LV{XNc}9od", Documento = "ClassDiagram.pdf", Activacion = true },
            };

            personasJuridicas.ForEach(personaJuridica => context.PersonasJuridicas.Add(personaJuridica));

            var representantes = new List<RepresentanteLegal>
            {
                new RepresentanteLegal {Identificacion = "12345678", Nombre = "Representante1", CorreoElectronico = "test@test.com", Tipo = RepresentanteLegal.TipoIdentificacion.Nacional},
                new RepresentanteLegal {Identificacion = "87654321", Nombre = "Representante2", CorreoElectronico = "test2@test2.com", Tipo = RepresentanteLegal.TipoIdentificacion.Nacional}
            };

            representantes.ForEach(representante => context.RepresentanteLegales.Add(representante));

            var cuentas = new List<Cuenta>
            {
                new Cuenta { Correo = "javier_f13@outlook.com", Contrasena = "LV{XNc}9od", Activado = true, CodigoDeVerificacion = "Rtg%L$COPl", PersonaJuridica = personasJuridicas[0] }
            };

            cuentas.ForEach(cuenta => context.Cuentas.Add(cuenta));

            var facturas = new List<Factura>
            {
                new Factura { NombreImpuesto = "Bienes Inmuebles", TipoImpuesto = "Debito", DescripcionImpuesto = "Bien Inmueble", Date = DateTime.Now, Debito = 10000 , Credito = 0, PersonaJuridica = personasJuridicas[0] },
                new Factura { NombreImpuesto = "Bienes Inmuebles", TipoImpuesto = "Credito", DescripcionImpuesto = "Bien Inmueble", Date = DateTime.Now, Debito = 0 , Credito = 3000, PersonaJuridica = personasJuridicas[0] }
            };

            facturas.ForEach(factura => context.Facturas.Add(factura));

            context.SaveChanges();
            InitializeDatabase(context);
        }
    }
}