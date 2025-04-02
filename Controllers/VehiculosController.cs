using AgenciaTransito.Classes;
using AgenciaTransito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgenciaTransito.Controllers
{

    [RoutePrefix("api/vehiculos")]
    public class VehiculosController : ApiController
    {


        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Vehiculo> ConsultarTodos()
        {

            clsVehiculo Vehiculo = new clsVehiculo();
            return Vehiculo.ConsultarTodos();

        }


        [HttpGet]
        [Route("ConsultarXPlaca")]
        public Vehiculo ConsultarXPlacaVehiculo(string PlacaVehiculo)
        {

            clsVehiculo Vehiculo = new clsVehiculo();
            return Vehiculo.Consultar(PlacaVehiculo);

        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Vehiculo vehiculo)
        {
            clsVehiculo Vehiculo = new clsVehiculo();

            //SE LE ASIGNA EL OBJETO empleado AL OBJETO empleado DE LA CLASE clsEmpleado 
            Vehiculo.vehiculo = vehiculo;

            return Vehiculo.Insertar();
        }


    }
}