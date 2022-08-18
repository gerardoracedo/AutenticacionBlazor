using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MArchivosUpload
    {
        public int Estado { get; set; }
        public string Nombre_original { get; set; }
        public string Nombre_subida { get; set; }
        public string Mime_type { get; set; }
    }
}
