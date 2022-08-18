using AutenticacionBlazor.Shared.Modelos.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Tickets
{
    public class MMsjNuevoUploadFiles
    {
        public int Id_ticket { get; set; }
        public int Estado { get; set; }
        public int Id_tipo_ticket { get; set; }
        public string Mensaje { get; set; }
        public int Envia { get; set; }
        public List<UploadedFile> ArchivosGuardar { get; set; } = new List<UploadedFile>();
        public List<MArchivosUploadRespuesta> ArchivosRespuesta { get; set; } = new List<MArchivosUploadRespuesta>();
    }
}
