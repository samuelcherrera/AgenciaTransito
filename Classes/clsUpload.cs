using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AgenciaTransito.Classes
{
    public class clsUpload
    {

        private string RptaError;

        public HttpRequestMessage request { get; set; }

        public String Datos { get; set; }

        public String Proceso { get; set; }

        public async Task<HttpResponseMessage> GrabaArchivo(bool Actualizar)
        {
            if (!request.Content.IsMimeMultipartContent())
            {

                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);

            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await request.Content.ReadAsMultipartAsync(provider);
                List<string> Archivos = new List<string>();

                foreach (MultipartFileData file in provider.FileData)
                {
                    String fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    if (File.Exists(Path.Combine(root, fileName)))
                    {
                        if (Actualizar)
                        {
                            //se borra el original 
                            File.Delete(Path.Combine(root, fileName));
                            //se crea el nuevo archivo con el mismo nombre
                            File.Move(file.LocalFileName, Path.Combine(root, fileName));
                            //  no se debe agregar en la base de datos porque ya existe

                        }
                        else
                        {
                            // no se pueden tener archivos con el mismo nombre,  deben ser nombres unicos
                            //si el archivo existe, se borra el archivo temporal que se subió
                            File.Delete(file.LocalFileName);
                            //se da una respuesta de error 
                            RptaError += "el archivo " + fileName + " ya existe";
                            //return request.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo ya existe");
                        }
                    }
                    else
                    {
                        Archivos.Add(fileName);
                        // se renombra el archivo
                        File.Move(file.LocalFileName, Path.Combine(root, fileName));
                    }

                }
                if (Archivos.Count > 0)
                {
                    //envia a grabar la informacion de las imagenes
                    string Respuesta = ProcesarArchivos(Archivos);
                    //se da una respuesta de exito 
                    return request.CreateResponse(HttpStatusCode.OK, "archivo subido con exito");
                }
                else
                {
                    if (Actualizar)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, "archivo actualizado con exito");

                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.Conflict, "el(los) archivo(s) ya existe(n)");

                    }
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "Error al cargar el archivo: " + ex.Message);
            }


        }

        public HttpResponseMessage ConsultarArchivo(string NombreArchivo)
        {
            try
            {

                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, NombreArchivo);
                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open, FileAccess.Read);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = NombreArchivo;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Archivo no encontrado");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "Error al consultar el archivo: " + ex.Message);
            }
        }

        private string ProcesarArchivos(List<string> Archivos)
        {
            switch (Proceso.ToUpper())
            {
                case "INFRACCIÓN":
                    clsFotoInfraccion fotoInfraccion = new clsFotoInfraccion();
                    fotoInfraccion.idInfracción = Datos;//Debe venir la informacion que se procesa en la bd (idInfracción)
                    fotoInfraccion.Archivos = Archivos;
                    return fotoInfraccion.GrabarImagenes();
                default:
                    return "Proceso no válido";
            }

        }

    }
}