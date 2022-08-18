using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Siro
{
    public class MSiroLoginRespuesta
    {
        public bool success { get; set; }
        public string access_token { get; set; }
        public string Message { get; set; }
    }

    public class MSiroIntencionDePagoBody
    {
        public string nro_cliente_empresa { get; set; }
        public string nro_comprobante { get; set; }
        public string Concepto { get; set; }
        public decimal Importe { get; set; }
        public string URL_OK { get; set; }
        public string URL_ERROR { get; set; }
        public string IdReferenciaOperacion { get; set; }
        public List<MSiroIntencionDePagoDetalle> Detalle { get; set; }
    }

    public class MSiroIntencionDePagoDetalle
    {
        public string descripcion { get; set; }
        public decimal importe { get; set; }
    }

    public class MSiroIntencionDePagoRespuesta
    {
        public bool success { get; set; }
        public string Url { get; set; }
        public string Hash { get; set; }
        public string Message { get; set; }
    }
}
