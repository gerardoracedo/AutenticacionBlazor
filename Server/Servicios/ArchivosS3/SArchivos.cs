using AutenticacionBlazor.Shared.Modelos.Global;
using System;
using Dapper;
using Npgsql;
using System.Threading.Tasks;
using AutenticacionBlazor.Shared.Modelos;
using System.Collections.Generic;
using MimeTypes;
using System.IO;
using Amazon.S3.Model;
using Amazon.S3;
using System.Net;

namespace AutenticacionBlazor.Server.Servicios.ArchivosS3
{
    public class SArchivos : IArchivos
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private readonly IAmazonS3 _amazonS3;
        private List<MArchivosUploadRespuesta> _respuestaupload = new List<MArchivosUploadRespuesta>();
        private string _iDiDentity { get; set; }

        public SArchivos(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado, IAmazonS3 amazonS3)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;
            _iDiDentity = _uLogeado.IdUsuarioIdentity();
            _amazonS3 = amazonS3;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public Task<MArchivosUploadRespuesta> SubirArchivos(MArchivosUpload _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM ""Archivos"".""Insert_archivos""('" + _v.Estado + "'," +
                                                                      "'" + _v.Nombre_original + "'," +
                                                                      "'" + _v.Nombre_subida + "'," +
                                                                      "'" + _v.Mime_type + "'," +
                                                                       "'" + _iDiDentity + "')";

            return db.QueryFirstOrDefaultAsync<MArchivosUploadRespuesta>(sql);
        }

        public Task<MArchivosDownload> BajarArchivos(string getid)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM ""Archivos"".""Get_archivos""('" + getid + "')";

            return db.QueryFirstOrDefaultAsync<MArchivosDownload>(sql);
        }

        public async Task<List<MArchivosUploadRespuesta>> SubirArchivos2(List<UploadedFile> _v)
        {
            foreach(var archivo in _v)
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
                _upload.Estado = 1;
                _upload.Nombre_original = archivo.FileName;
                _upload.Nombre_subida = _nombre;
                _upload.Mime_type = _mimeType;
                var respuesta1 = await this.SubirArchivos(_upload);
                _respuestaupload.Add(new MArchivosUploadRespuesta() { Id = respuesta1.Id, Nombre_archivo = respuesta1.Nombre_archivo });
            }
            return _respuestaupload;
        }

        public async Task<byte[]> DownloadFileAsync(string file)
        {
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = "archivosmuniybep-95p8317ptiusbbjij5zm5bpifaumyusw2a-s3alias",
                    Key = file
                };

                using (var response = await _amazonS3.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
