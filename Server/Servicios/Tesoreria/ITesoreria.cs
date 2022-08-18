using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Rentas.Tesoreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Tesoreria
{
    public interface ITesoreria
    {
        Task<MRespuestaBoolMensaje> CuentaCorriente_alta(MCuentaCorriente cuentaCorriente);
        Task<MRespuestaBoolMensaje> AgregarDeuda(MAgregarDeuda agregarDeuda);
        Task<MRespuestaBoolMensaje> AgregarBoleta(MBoletas agregarBoleta);
        Task<MRespuestaBoolMensaje> AgregarDetalleBoleta(MBoletasDetalle agregarDetalleBoleta);
        Task<MRespuestaBoolMensaje> AgregarCodigoBarra(MBoletasCodigosBarras agregarCodigoBarra);
        Task<MRespuestaBoolMensaje> HabilitarBoleta(string Id_boleta);
    }
}
