using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Rentas.Comercio;
using AutenticacionBlazor.Shared.Modelos.Rentas.Tesoreria;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Tesoreria
{
    public class STesoreria : ITesoreria
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private string _iDiDentity { get; set; }
        public STesoreria(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<MRespuestaBoolMensaje> CuentaCorriente_alta(MCuentaCorriente cuentaCorriente)
        {
            try
            {
                var db = dbConnection(); 
                var sql = @"SELECT * FROM tesoreria.""Agregar_cc""('" + cuentaCorriente.Id_codigo + "'," +
                                                                  "'" + cuentaCorriente.Identificador + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }
        }
        public async Task<MRespuestaBoolMensaje> AgregarDeuda(MAgregarDeuda agregarDeuda)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM tesoreria.""Agregar_deuda""('" + agregarDeuda.Estado + "'," +
                                                                  "'" + agregarDeuda.Id_cuenta_corriente + "'," +
                                                                  "'" + agregarDeuda.Importe + "'," +
                                                                  "'" + agregarDeuda.Id_deuda_tipo + "'," +
                                                                  "'" + agregarDeuda.Id_periodo + "'," +
                                                                  "'" + agregarDeuda.Id_deuda_original + "'," +
                                                                  "'" + agregarDeuda.Id_origen_deuda + "'," +
                                                                  "'" + _iDiDentity + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }
        }

        public async Task<MRespuestaBoolMensaje> AgregarBoleta (MBoletas agregarBoleta)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM tesoreria.""Boletas_agregar""('" + agregarBoleta.Id + "'," +
                                                                  "'" + agregarBoleta.Estado + "'," +
                                                                  "'" + agregarBoleta.Nro_boleta + "'," +
                                                                  "'" + agregarBoleta.Importe + "'," +
                                                                  "'" + agregarBoleta.Fecha_creacion + "'," +
                                                                  "'" + agregarBoleta.Fecha_vencimiento + "'," +
                                                                  "'" + agregarBoleta.Fecha_pago + "'," +
                                                                  "'" + agregarBoleta.Fecha_acreditacion + "'," +
                                                                  "'" + agregarBoleta.Uid_persona + "'," +
                                                                  "'" + _iDiDentity + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }
        }

        public async Task<MRespuestaBoolMensaje> AgregarDetalleBoleta(MBoletasDetalle agregarDetalleBoleta)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM tesoreria.""Boletas_detalle_agregar""('" + agregarDetalleBoleta.Id + "'," +
                                                                  "'" + agregarDetalleBoleta.Estado + "'," +
                                                                  "'" + agregarDetalleBoleta.Id_boleta+ "'," +
                                                                  "'" + agregarDetalleBoleta.Id_deuda + "'," +
                                                                  "'" + agregarDetalleBoleta.Descripcion + "'," +
                                                                  "'" + agregarDetalleBoleta.Importe + "'," +
                                                                  "'" + agregarDetalleBoleta.Id_periodo_fiscal + "'," +
                                                                  "'" + agregarDetalleBoleta.Id_tipo_deuda + "'," +
                                                                  "'" + agregarDetalleBoleta.Fecha_graba + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }
        }

        public async Task<MRespuestaBoolMensaje> AgregarCodigoBarra (MBoletasCodigosBarras agregarCodigoBarra)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM tesoreria.""Boletas_codigo_barra_agregar""('" + agregarCodigoBarra.Id + "'," +
                                                                  "'" + agregarCodigoBarra.Estado + "'," +
                                                                  "'" + agregarCodigoBarra.Id_boleta + "'," +
                                                                  "'" + agregarCodigoBarra.Codigo_barra + "'," +
                                                                  "'" + agregarCodigoBarra.Tipo_codigo_barra + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }
        }

        public async Task<MRespuestaBoolMensaje> HabilitarBoleta (string Id_boleta)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM tesoreria.""Boletas_habilitar""('" + Id_boleta + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }
        }
    }
}
