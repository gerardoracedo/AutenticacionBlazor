using AutenticacionBlazor.Server.Models;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Rentas.Comercio;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Global
{
    public class SGlobal : IGlobal
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private readonly SignInManager<ApplicationUser> _signInManager;
        int uid;
        private string _iDiDentity { get; set; }
        public SGlobal(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
            uid = DarValorUid(_uLogeado.IdUsuarioIdentity());
           
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<MListaGeneros>> ListaGeneros()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_generos""()";
            return await db.QueryAsync<MListaGeneros>(sql);
        }

        public async Task<IEnumerable<MListaEstadoCivil>> ListaEstadoCivil()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_estados_civiles""()";
            return await db.QueryAsync<MListaEstadoCivil>(sql);
        }

        public async Task<IEnumerable<MListaTipoDni>> ListaTipoDni()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_tipos_dni""()";
            return await db.QueryAsync<MListaTipoDni>(sql);
        }

        public async Task<IEnumerable<MListaTipoSociedad>> ListaTipoSociedad()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_tipo_sociedad""()";
            return await db.QueryAsync<MListaTipoSociedad>(sql);
        }

        public async Task<IEnumerable<MListaPaises>> ListaPaises()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_direcciones_lista_paises""()";
            return await db.QueryAsync<MListaPaises>(sql);
        }

        public async Task<IEnumerable<MListaProvincias>> ListaProvincias()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_direcciones_lista_provincias""()";
            return await db.QueryAsync<MListaProvincias>(sql);
        }

        public async Task<IEnumerable<MListaProvincias>> ListaProvinciasByIdPais(int id_pais)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_direcciones_lista_provincia_por_idpais"" ('" + id_pais + "')";
            return await db.QueryAsync<MListaProvincias>(sql);
        }

        public async Task<IEnumerable<MListaLocalidades>> ListaLocalidades()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_direcciones_lista_localidades""()";
            return await db.QueryAsync<MListaLocalidades>(sql);
        }

        public async Task<IEnumerable<MListaLocalidades>> ListaLocalidadesByIdProvincia(int id_provincia)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_direcciones_lista_localidad_por_idprovincia"" ('" + id_provincia + "')";
            return await db.QueryAsync<MListaLocalidades>(sql);
        }

        public async Task<IEnumerable<MOficina>> GetOficinas()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_oficinas""()";
            return await db.QueryAsync<MOficina>(sql);
        }

        public async Task<MRespuestaBoolMensaje> AssignOficinaToUser(int id_oficina, MAspNetUsers aspnetuser)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Assign_oficina_to_user""('" + aspnetuser.Id + "'," +
                                                                    "'" + id_oficina + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> RemoveOficinaFromUser(int id_oficina, MAspNetUsers aspnetuser)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Remove_oficina_from_user""('" + aspnetuser.Id + "'," +
                                                                    "'" + id_oficina + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<IEnumerable<MOficina>> GetUserOficinas(string ididentity)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_user_oficinas""('" + ididentity + "')";
            return await db.QueryAsync<MOficina>(sql);
        }

        public async Task<IEnumerable<MOficina>> GetNotUserOficinas(string ididentity)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_not_user_oficinas""('" + ididentity + "')";
            return await db.QueryAsync<MOficina>(sql);
        }

        public async Task<IEnumerable<MTelefono>> GetMisTelefonos()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_mis_telefonos""('" + _iDiDentity + "')";
            return await db.QueryAsync<MTelefono>(sql);
        }

        public async Task<IEnumerable<MTelefono>> GetTelefonosByUid(int uid)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_telefonos_by_uid""('" + uid + "')";
            return await db.QueryAsync<MTelefono>(sql);
        }

        public async Task<MTelefono> GetTelefono(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_telefono""('" + id + "')";
            return await db.QueryFirstOrDefaultAsync<MTelefono>(sql);
        }

        public async Task<MRespuestaBoolMensaje> InsertTelefono(MTelefono telefono)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_telefono""('" + uid + "'," +
                                                                  "'" + telefono.Descripcion + "'," +
                                                                  "'" + telefono.Telefono + "'," +
                                                                  "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> InsertTelefono2(MTelefono telefono)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_telefono""('" + telefono.Uid_persona + "'," +
                                                                  "'" + telefono.Descripcion + "'," +
                                                                  "'" + telefono.Telefono + "'," +
                                                                  "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdateTelefono(MTelefono telefono)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Update_telefono""('" + uid + "'," +
                                                                  "'" + telefono.Id + "'," +
                                                                  "'" + telefono.Descripcion + "'," +
                                                                  "'" + telefono.Telefono + "'," +
                                                                  "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdateTelefono2(MTelefono telefono)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Update_telefono""('" + telefono.Uid_persona + "'," +
                                                                  "'" + telefono.Id + "'," +
                                                                  "'" + telefono.Descripcion + "'," +
                                                                  "'" + telefono.Telefono + "'," +
                                                                  "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> DeleteTelefono(string motivobaja, MTelefono telefono)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Delete_telefono""('" + telefono.Id + "'," +
                                                                  "'" + motivobaja + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<IEnumerable<MEmail>> GetMisMails()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_mis_mails""('" + _iDiDentity + "')";
            return await db.QueryAsync<MEmail>(sql);
        }

        public async Task<IEnumerable<MEmail>> GetMailsByUid(int uid)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_mails_by_uid""('" + uid + "')";
            return await db.QueryAsync<MEmail>(sql);
        }

        public async Task<MEmail> GetEmail(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_mail""('" + id + "')";
            return await db.QueryFirstOrDefaultAsync<MEmail>(sql);
        }

        public async Task<MRespuestaBoolMensaje> InsertMail(MEmail mail)
        {
            var db = dbConnection();
            try
            {
                var sql = @"SELECT * FROM personas.""Insert_mail""('" + uid + "'," +
                                                                  "'" + mail.Descripcion + "'," +
                                                                  "'" + mail.Email + "'," +
                                                                  "'" + _iDiDentity + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
            }
            catch (System.Exception e)
            {
                var mensaje = new MRespuestaBoolMensaje { mensaje = e.Message, resultado = false };
                return mensaje;
                
            }
            finally
            {
                db.Close();
            }


        }

        public async Task<MRespuestaBoolMensaje> InsertMail2(MEmail mail)
        {

            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_mail""('" + mail.Uid_persona + "'," +
                                                              "'" + mail.Descripcion + "'," +
                                                              "'" + mail.Email + "'," +
                                                              "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdateMail(MEmail mail)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Update_mail""('" + uid + "'," +
                                                                  "'" + mail.Id + "'," +
                                                                  "'" + mail.Descripcion + "'," +
                                                                  "'" + mail.Email + "'," +
                                                                  "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdateMail2(MEmail mail)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Update_mail""('" + mail.Uid_persona + "'," +
                                                                  "'" + mail.Id + "'," +
                                                                  "'" + mail.Descripcion + "'," +
                                                                  "'" + mail.Email + "'," +
                                                                  "'" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> DeleteMail(string motivobaja, MEmail mail)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Delete_mail""('" + mail.Id + "'," +
                                                               "'" + motivobaja + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<IEnumerable<MListaTipoDirecciones>> ListaTipoDirecciones()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Get_direcciones_tipos""()";
            return await db.QueryAsync<MListaTipoDirecciones>(sql);
        }

        public async Task<MVerificarLoginExterno> VerificarLoginExterno(string cuit)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""f_verificar_login_externo""('" + cuit + "')";
            return await db.QueryFirstOrDefaultAsync<MVerificarLoginExterno>(sql);
        }
        public int DarValorUid(string _idIdentity)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_uid_persona""('" + _idIdentity + "')";
            return db.QueryFirstOrDefault<int>(sql);
        }
        public async Task<MVerificarLoginProvider> VerificarLoginProvider(string cuit)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""f_verificar_login_externo""('" + cuit + "')";
            return await db.QueryFirstOrDefaultAsync<MVerificarLoginProvider>(sql);
        }
        public async Task<IEnumerable<MListaPeriodosFiscales>> ListaPeriodoFiscal(MBuscarPeriodoFiscal _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Lista_periodos_fiscales""('" + _v.Tipo_periodo + "')";
            return await db.QueryAsync<MListaPeriodosFiscales>(sql);
        }
        public async Task<MBuscaPeriodoFiscalResultado> BuscarPeriodoFiscal(MBuscarPeriodoFiscal _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM global.""Buscar_periodo_fiscal""('" + _v.Periodo + "','" + _v.Tipo_periodo + "')";
            return await db.QueryFirstOrDefaultAsync<MBuscaPeriodoFiscalResultado>(sql);
        }
        public async Task<SignInResult> NuevoLoginTest(string user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            return result;
        }
    }
}
