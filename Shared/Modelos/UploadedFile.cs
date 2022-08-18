using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string FileType { get; set; }
    }
}
