using AgenciaTransito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaTransito.Classes
{
    public class clsVehiculo
    {


        private DBTransitoEntities1 DBTransito = new DBTransitoEntities1();// objeto de la bd que permite manipular el CRUD de los objetos generados por el entityFramework
        public Vehiculo vehiculo { get; set; } // permite manipular y acceder a los atributos de la tabla infraccion


        public String Insertar()
        {
            try
            {
                DBTransito.Vehiculoes.Add(vehiculo); // agg una nueva infraccion a la tabla infraccion (INSERT)
                DBTransito.SaveChanges();//guarda cambios en la bd
                return "vehiculo con placa " + vehiculo.Placa + " ingresado correctamente";
            }
            catch (Exception ex)
            {
                return "error al insertar el vehiculo " + ex.Message;
            }

        }

        public Vehiculo Consultar(String PlacaVehiculo)
        {
            //EXPRESIONES LAMBDA:funciones anonimas que permiten filtrar los datos de una tabla
            //FirstOrDefault: devuelve el primer elemento que cumpla con la condicion de la expresion lambda
            Vehiculo veh = DBTransito.Vehiculoes.FirstOrDefault(e => e.Placa == PlacaVehiculo);//consulta la infraccion por PlacaVehiculo
            return veh;
        }

        public List<Vehiculo> ConsultarTodos()
        {
            return DBTransito.Vehiculoes
                .OrderBy(e => e.Placa)
                .ToList();//consulta todos los empleados por placa
        }
    }
}