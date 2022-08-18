using Amazon.S3;
using Amazon.S3.Model;
using AutenticacionBlazor.Server.Servicios.ArchivosS3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MimeTypes;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos;
using System.Net;

namespace AutenticacionBlazor.Server.Controllers.ArchivoS3
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly IArchivos _archivos;
        private readonly IAmazonS3 _amazonS3;
        private readonly IServiciosPersonaArchivos _serviciosArchivos;
        private List<MArchivosUploadRespuesta> _respuestaupload = new List<MArchivosUploadRespuesta>();
        
        public ArchivosController(IAmazonS3 amazonS3, IServiciosPersonaArchivos serv, IArchivos archivos)
        {
            this._amazonS3 = amazonS3;
            this._serviciosArchivos = serv;
            this._archivos = archivos;
            
    }

        [Authorize(Roles = "general,super")]
        [HttpPost("Upload")]
        public async Task<List<MArchivosUploadRespuesta>> Upload(List<UploadedFile> uploadedFile)
        {
            foreach (var archivo in uploadedFile)
            {
                MArchivosUpload _upload = new MArchivosUpload();
                Guid g = Guid.NewGuid();

                string _extension = Path.GetExtension(archivo.FileName);
                string _mimeType = MimeTypeMap.GetMimeType(_extension);
                string _nombre = g.ToString() + "-" + archivo.FileName;
                MemoryStream memStream = new MemoryStream();
                memStream.Write(archivo.FileContent, 0, archivo.FileContent.Length);

                var putRequest = new PutObjectRequest()
                {
                    BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                    Key = _nombre,
                    InputStream = memStream,
                    ContentType = _mimeType,
                };
                var rr = await this._amazonS3.PutObjectAsync(putRequest);
                var test = rr.HttpStatusCode;
                Console.WriteLine($"Codigo : {test}");

                _upload.Estado = 1;
                _upload.Nombre_original = archivo.FileName;
                _upload.Nombre_subida = _nombre;
                _upload.Mime_type = _mimeType;
                var respuesta1 = await this._archivos.SubirArchivos(_upload);
                _respuestaupload.Add(new MArchivosUploadRespuesta() { Id = respuesta1.Id, Nombre_archivo = respuesta1.Nombre_archivo });
            }
            return _respuestaupload;
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Donwload(string id)
        {
            var respuesta1 = await this._archivos.BajarArchivos(id);
            var putRequest = new GetObjectRequest()
            {
                BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                Key = respuesta1.Nombre_subida,

            };
            using GetObjectResponse reponse = await this._amazonS3.GetObjectAsync(putRequest);
            using Stream stream = reponse.ResponseStream;

            var image = new MemoryStream();
            await reponse.ResponseStream.CopyToAsync(image);
            image.Position = 0;

            return new FileStreamResult(image, reponse.Headers["Content-Type"])
            {
                FileDownloadName = respuesta1.Nombre_original
            };
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("Descargar/{id}")]
        public async Task<IActionResult> GetDocumentFromS3Async(string id)
        {
            try
            {
                var respuesta1 = await this._archivos.BajarArchivos(id);


                var document = _archivos.DownloadFileAsync(respuesta1.Nombre_subida).Result;

                return File(document, "application/octet-stream", respuesta1.Nombre_original);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
