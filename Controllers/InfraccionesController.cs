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
        [Route("ConsultarXPlacaVehiculo")]
        public Infraccion ConsultarXPlacaVehiculo(string PlacaVehiculo)
        {

            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.Consultar(PlacaVehiculo);

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