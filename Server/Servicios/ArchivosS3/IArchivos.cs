using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutenticacionBlazor.Shared.Modelos;
using AutenticacionBlazor.Shared.Modelos.Global;

namespace AutenticacionBlazor.Server.Servicios.ArchivosS3
{
    public interface IArchivos
    {
        Task<MArchivosUploadRespuesta> SubirArchivos(MArchivosUpload _v);
        Task<MArchivosDownload> BajarArchivos(string getid);
        Task<List<MArchivosUploadRespuesta>> SubirArchivos2(List<UploadedFile> _v);
        Task<byte[]> DownloadFileAsync(string file);
    }
}
