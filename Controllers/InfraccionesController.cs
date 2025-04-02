using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgenciaTransito.Classes;
using AgenciaTransito.Models;

namespace AgenciaTransito.Controllers

{
    [RoutePrefix("api/infracciones")]
    public class InfraccionesController : ApiController
    {

        [HttpGet]
        [Route("ConsultarTodas")]
        public List<Infraccion> ConsultarTodas()
        {

            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.ConsultarTodas();

        }

        [HttpGet]
        [Route("ConsultarPorPlaca")]
        public IHttpActionResult ConsultarPorPlaca(string placa)
        {
            try
            {
                DBTransitoEntities1 db = new DBTransitoEntities1(); // Reemplaza con tu contexto de base de datos

                var resultado = from infraccion in db.Infraccions
                                join vehiculo in db.Vehiculoes on infraccion.PlacaVehiculo equals vehiculo.Placa
                                join foto in db.FotoInfraccions on infraccion.idFotoMulta equals foto.idInfraccion into fotos
                                where infraccion.PlacaVehiculo == placa
                                select new
                                {
                                    infraccion.PlacaVehiculo,
                                    vehiculo.TipoVehiculo,
                                    vehiculo.Marca,
                                    vehiculo.Color,
                                    infraccion.FechaInfraccion,
                                    infraccion.TipoInfraccion,
                                    infraccion.idFotoMulta,
                                    NombresFotos = fotos.Select(f => f.NombreFoto).ToList()
                                };

                if (resultado == null || !resultado.Any())
                {
                    return NotFound();
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        


        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Infraccion infraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion(); 

            //SE LE ASIGNA EL OBJETO empleado AL OBJETO empleado DE LA CLASE clsEmpleado 
            Infraccion.infraccion = infraccion;
            return Infraccion.Insertar();
        }
    }
}