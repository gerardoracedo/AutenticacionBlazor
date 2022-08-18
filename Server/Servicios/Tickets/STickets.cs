using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Tickets;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Tickets
{
    public class STickets : ITickets
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private string _iDiDentity { get; set; }
        private int _UserAdmin { get; set; }
        private int _UserSuper { get; set; }
        private int _UserEmpleado { get; set; } 
        public STickets(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
            _UserAdmin = _uLogeado.CheckUserRol("admin");
            _UserSuper = _uLogeado.CheckUserRol("super");
            _UserEmpleado = _uLogeado.CheckUserRol("empleado");
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<MRespuestaBoolMensaje> InsertTicket(MTicketNuevo _v)
        {
            MRespuestaBoolMensaje _respuesta = new MRespuestaBoolMensaje();
            var db = dbConnection();
            var sql1 = @"SELECT * FROM tickets.""Insert_ticket""('" + _v.Estado + "'," +
                                                                "'" + _v.Id_tipo_ticket + "'," +
                                                                "'" + _iDiDentity + "')";
            var respuesta1 = await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql1);

            var sql2 = @"SELECT * FROM tickets.""Insert_ticket_detalle""('" + _v.Estado + "'," +
                                                                        "'" + respuesta1.id + "'," +
                                                                        "'" + _v.Mensaje + "'," +
                                                                        "'" + _iDiDentity + "'," +
                                                                        "'" + _v.Envia + "')";
            var respuesta2 = await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql2);

            foreach (var _a in _v.Archivos)
            {
                var sql3 = @"SELECT * FROM tickets.""Insert_ticket_detalle_archivos""('" + _v.Estado + "'," +
                                                                                     "'" + respuesta2.id + "'," +
                                                                                     "'" + _a.Id + "'," +
                                                                                     "'" + _iDiDentity + "')";
                await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql3);
            }

            if (respuesta1.resultado && respuesta2.resultado)
            {
                var _habilitar = await this.HabilitaTicket(respuesta1.id, respuesta2.id);
                return _habilitar;
            }
            else
            {
                _respuesta.resultado = false;
                _respuesta.mensaje = "No se agrego el ticket";
                var _habilitar = _respuesta;
                return _habilitar;
            }
        }

        public async Task<MRespuestaBoolMensaje> HabilitaTicket(int id_ticket, int id_ticket_detalle)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM tickets.""Habilitar_tickets""('" + id_ticket + "'," + "'" + id_ticket_detalle + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> InsertTicketDetalle(MMsjNuevoUploadFiles _v)
        {
            int _envia;
            MRespuestaBoolMensaje _respuesta = new MRespuestaBoolMensaje();
            if (_UserSuper == 1 || _UserAdmin == 1)
            {
                _envia = 0;
            }
            else if(_UserEmpleado == 1)
            {
                
                _envia = 1;

            }
            else
            {
                _envia = 2;
            }
            var db = dbConnection();
            var sql1 = @"SELECT * FROM tickets.""Insert_ticket_detalle""('" + _v.Estado + "'," +
                                                                        "'" + _v.Id_ticket + "'," +
                                                                        "'" + _v.Mensaje + "'," +
                                                                        "'" + _iDiDentity + "'," +
                                                                        "'" + _envia + "')";
            var respuesta1 = await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql1);



            foreach (var _a in _v.ArchivosRespuesta)
            {
                var sql2 = @"SELECT * FROM tickets.""Insert_ticket_detalle_archivos""('" + _v.Estado + "'," +
                                                                                     "'" + respuesta1.id + "'," +
                                                                                     "'" + _a.Id + "'," +
                                                                                     "'" + _iDiDentity + "')";
                await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql2);
            }

            if (respuesta1.resultado)
            {
                _respuesta.resultado = true;
                _respuesta.mensaje = "Respuesta enviada";
                var _habilitar = _respuesta;
                return _habilitar;
            }
            else
            {
                _respuesta.resultado = false;
                _respuesta.mensaje = "No se puso agregar su respuesta";
                var _habilitar = _respuesta;
                return _habilitar;
            }
        }

        public async Task<MRespuestaBoolMensaje> InsertTicketPersonaJuridica(int id_ticket, int id_persona_juridica)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM tickets.""Insert_ticket_persona_juridica""('" + id_ticket + "'," + "'" + id_persona_juridica + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }
        public async Task<MRespuestaBoolMensaje> InsertTicketAltaActividad(int id_ticket, int id_persona_actividad)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM tickets.""Insert_ticket_alta_actividad""('" + id_ticket + "'," + "'" + id_persona_actividad + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> Ticket_Acceso(int id_ticket)
        {
            var db = dbConnection();
            int _admin;
            if(_UserSuper == 1 || _UserAdmin == 1)
            {
                _admin = 1;
            }
            else
            {
                _admin = 0;
            }
            var sql = @"SELECT * FROM tickets.""Tramite_verificar_acceso""('" + id_ticket + "'," + "'" + _iDiDentity + "'," + "'" + _admin + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<IEnumerable<MTicketChat>> Ticket_Chat(int id_ticket)
        {
            var db = dbConnection();
            int _admin;
            if (_UserSuper == 1 || _UserAdmin == 1)
            {
                _admin = 1;
            }
            else
            {
                _admin = 0;
            }
            var sql = @"SELECT * FROM tickets.""Tramite_ver_chat""('" + id_ticket + "'," + "'" + _iDiDentity + "'," + "'" + _admin + "')";
            var resultado = await db.QueryAsync<MTicketChat>(sql);
            return resultado;
        }

        public async Task<IEnumerable<MTicketArchivos>> Ticket_Archivos(int id_ticket)
        {
            var db = dbConnection();
            int _admin;
            if (_UserSuper == 1 || _UserAdmin == 1)
            {
                _admin = 1;
            }
            else
            {
                _admin = 0;
            }
            var sql = @"SELECT * FROM tickets.""Tramite_ver_archivos""('" + id_ticket + "'," + "'" + _iDiDentity + "'," + "'" + _admin + "')";
            return await db.QueryAsync<MTicketArchivos>(sql);
        }
    }
}
