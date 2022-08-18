using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AutenticacionBlazor.Server.Data.Entidades;
using AutenticacionBlazor.Server.Servicios.ArchivosS3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MimeTypes;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos;

namespace AutenticacionBlazor.Server.Controllers.ArchivoS3
{
    [Route("api/[controller]")]
    [ApiController]
    public class Archivos_PersonasController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IServiciosPersonaArchivos _serviciosArchivos;
        public Archivos_PersonasController(IAmazonS3 amazonS3, IServiciosPersonaArchivos serv)
        {
            this._amazonS3 = amazonS3;
            this._serviciosArchivos = serv;
        }

        [Authorize(Roles = "general,super")]
        [HttpPost("PostPersonaArchivo")]
        public async Task<IActionResult> Post([FromForm] IFormFile files, [FromQuery] int id)
        {
            Guid g = Guid.NewGuid();
            string extension = Path.GetExtension(files.FileName);
            string mimeType = MimeTypeMap.GetMimeType(extension);
            var putRequest = new PutObjectRequest()
            {
                BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                Key = g.ToString() + "-" + files.FileName,
                InputStream = files.OpenReadStream(),
                ContentType = mimeType,
            };

            var result = await this._amazonS3.PutObjectAsync(putRequest);
            var doc = new Documento
            {
                Extencion = putRequest.ContentType,
                NombreArchivo = putRequest.Key
            };
            _serviciosArchivos.InsertDocument(doc, id);
            return Ok(result);
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("GetPersonaArchivo")]
        public async Task<IActionResult> Get([FromQuery] string filename)
        {
            var putRequest = new GetObjectRequest()
            {
                BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                Key = filename,

            };
            using GetObjectResponse reponse = await this._amazonS3.GetObjectAsync(putRequest);
            using Stream stream = reponse.ResponseStream;

            var image = new MemoryStream();
            await reponse.ResponseStream.CopyToAsync(image);
            image.Position = 0;

            return new FileStreamResult(image, reponse.Headers["Content-Type"])
            {
                FileDownloadName = filename
            };
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("GetListDocumentPersona")]
        public async Task<IActionResult> GetDocumentByIdPersona(int IdPersona)
        {
            var documents = _serviciosArchivos.GetListDocumentByPersonId(IdPersona);
            if (documents.Count() > 0)
            {
                return Ok(documents);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("GetDocumentPersonaByIdDocument")]
        public async Task<IActionResult> GetDocumentPersonaByIdDocument([FromQuery] int IdDocument)
        {
            var fileName = _serviciosArchivos.GetDocumentById(IdDocument).NombreArchivo;
            var putRequest = new GetObjectRequest()
            {
                BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                Key = fileName,

            };
            using GetObjectResponse reponse = await this._amazonS3.GetObjectAsync(putRequest);
            using Stream stream = reponse.ResponseStream;

            var image = new MemoryStream();
            await reponse.ResponseStream.CopyToAsync(image);
            image.Position = 0;

            return new FileStreamResult(image, reponse.Headers["Content-Type"])
            {
                FileDownloadName = fileName
            };
        }
    }
}
