using AgenciaTransito.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AgenciaTransito.Controllers
{

    [RoutePrefix("api/uploadfiles")]
    public class UploadFilesController : ApiController
    {

        [HttpPost]
        [Route("Insertar")]
        public async Task<HttpResponseMessage> GrabarArchivo(HttpRequestMessage Request, String Datos, String Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.request = Request;
            UploadFiles.Datos = Datos;
            UploadFiles.Proceso = Proceso;
            return await UploadFiles.GrabaArchivo(false);
        }

        [HttpGet]
        [Route("Consultar")]
        public HttpResponseMessage Get(string NombreImagen)
        {
            clsUpload UploadFiles = new clsUpload();
            return UploadFiles.ConsultarArchivo(NombreImagen);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage Request, String Datos, String Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.request = Request;
            UploadFiles.Datos = Datos;
            UploadFiles.Proceso = Proceso;
            return await UploadFiles.GrabaArchivo(true);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public HttpResponseMessage EliminarArchivo(string NombreImagen)
        {
            clsUpload UploadFiles = new clsUpload();
            return UploadFiles.EliminarArchivo(NombreImagen);
        }

    }

}
