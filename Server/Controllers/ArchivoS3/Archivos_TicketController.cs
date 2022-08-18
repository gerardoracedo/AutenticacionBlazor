using Amazon.S3;
using Amazon.S3.Model;
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

namespace AutenticacionBlazor.Server.Controllers.ArchivoS3
{
    [Route("api/[controller]")]
    [ApiController]
    public class Archivos_TicketController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IServicioTicketArchivos _serviciosTicketPersona;

        public Archivos_TicketController(IAmazonS3 amazonS3, IServicioTicketArchivos _serv)
        {
            this._amazonS3 = amazonS3;
            _serviciosTicketPersona = _serv;

        }

        [Authorize(Roles = "general,super")]
        [HttpPost("PostTicketArchivo")]
        public async Task<IActionResult> Post([FromForm] IFormFile file2, [FromQuery] int TicketId)
        {
            Guid g = Guid.NewGuid();
            var putRequest = new PutObjectRequest()
            {
                BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                Key = g.ToString() + "-" + file2.Name,
                InputStream = file2.OpenReadStream(),
                ContentType = file2.ContentType,
            };

            var result = await this._amazonS3.PutObjectAsync(putRequest);
            var doc = new Documento
            {
                Extencion = putRequest.ContentType,
                NombreArchivo = putRequest.Key
            };
            _serviciosTicketPersona.InsertDocument(doc, TicketId);

            return Ok(result);
        }
        [Authorize(Roles = "general,super")]
        [HttpGet("GetTicketArchivo")]
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
        public async Task<IActionResult> GetDocumentByIdPersona(int IdTicket)
        {
            var documents = _serviciosTicketPersona.GetListDocumentByTicketId(IdTicket);
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
            var fileName = _serviciosTicketPersona.GetDocumentById(IdDocument).NombreArchivo;
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
