using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AgenciaTransito.Models;

namespace AgenciaTransito.Classes
{
    public class clsInfraccion
    {
        private DBTransitoEntities1 DBTransito = new DBTransitoEntities1();// objeto de la bd que permite manipular el CRUD de los objetos generados por el entityFramework
        public Infraccion infraccion { get; set; } // permite manipular y acceder a los atributos de la tabla infraccion

        public String Insertar()
        {
            try
            {
                DBTransito.Infraccions.Add(infraccion); // agg una nueva infraccion a la tabla infraccion (INSERT)
                DBTransito.SaveChanges();//guarda cambios en la bd
                return "infracción por " +infraccion.TipoInfraccion+ " ingresada correctamente para el vehiculo con placa " + infraccion.PlacaVehiculo+" "+infraccion.FechaInfraccion;
            }
            catch (Exception ex)
            {
                return "error al insertar la infracción " + ex.Message;
            }

        }

        public String Actualizar()
        {
            //para corroborar que si se actualizo primero consultamos la placa
            Infraccion inf = Consultar(infraccion.PlacaVehiculo);
            if (inf == null)
            {
                return "la placa no es valida";
            }
            DBTransito.Infraccions.AddOrUpdate(infraccion);//actualiza el empleado de la tabla empleadoes
            DBTransito.SaveChanges();
            return "se ha actualizado el empleado correctamente";



        }
        public Infraccion Consultar(String PlacaVehiculo)
        {
            //EXPRESIONES LAMBDA:funciones anonimas que permiten filtrar los datos de una tabla
            //FirstOrDefault: devuelve el primer elemento que cumpla con la condicion de la expresion lambda
            Infraccion inf = DBTransito.Infraccions.FirstOrDefault(e => e.PlacaVehiculo == PlacaVehiculo);//consulta la infraccion por PlacaVehiculo
            return inf;
        }

        public List<Infraccion> ConsultarTodos()
        {
            return DBTransito.Infraccions
                .OrderBy(e => e.FechaInfraccion)
                .ToList();//consulta todos los empleados por orden de fecha
        }

        public String Eliminar()
        {
            try
            {
                //consultamos el empleado
                Infraccion inf = Consultar(infraccion.PlacaVehiculo);
                if (inf == null)
                {
                    return "no existen infracciones relacionadas con la placa "+ infraccion.PlacaVehiculo;
                }
                DBTransito.Infraccions.Remove(inf);//actualiza el empleado de la tabla empleadoes
                DBTransito.SaveChanges();
                return "se ha eliminado el empleado correctamente";

            }
            catch (Exception ex)
            {
                return ex.Message;// MENSAJE DE ERROR
            }
        }

    }
}