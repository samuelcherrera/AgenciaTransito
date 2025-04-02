using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgenciaTransito.Models;
namespace AgenciaTransito.Classes
{
    public class clsFotoInfraccion
    {


        private DBTransitoEntities1 DBSuper = new DBTransitoEntities1();// objeto de la bd que permite manipular el CRUD de los objetos generados por el entityFramework

        public string idInfraccion { get; set; }
        public List<string> Archivos { get; set; }
        public string GrabarImagenes()
        {

            try
            {
                if (Archivos.Count > 0)
                {
                    foreach (string Archivo in Archivos)
                    {
                        FotoInfraccion Imagen = new FotoInfraccion();
                        Imagen.idInfraccion = Convert.ToInt32(idInfraccion);
                        Imagen.NombreFoto = Archivo;
                        DBSuper.FotoInfraccions.Add(Imagen);
                        DBSuper.SaveChanges();
                    }
                    return "Imagenes grabadas correctamente";
                }
                else
                {
                    return "No hay archivos para subir";
                }


            }
            catch (Exception ex)
            {
                return "Error al grabar las imagenes " + ex.Message;
            }
        }

    }
}