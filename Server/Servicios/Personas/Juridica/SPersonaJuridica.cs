using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Personas.Juridica;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Personas.Juridica
{
    public class SPersonaJuridica : IPersonaJuridica
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private string _iDiDentity { get; set; }
        public SPersonaJuridica(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        
        public Task<MRespuestaBoolMensaje> InsertPersonaJuridica(MPersonaJuridicaInsert _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_persona_juridica""('2'," +
                                                                          "'" + _v.Nombre_fantasia + "'," +
                                                                          "'" + _v.Nombre_legal + "'," +
                                                                          "'" + _v.Cuit + "'," +
                                                                          "'" + _v.Id_tipo_sociedad + "'," +
                                                                          "'" + _v.Id_pais + "'," +
                                                                          "'" + _v.Uid_persona_fisica + "'," +
                                                                          "'" + _v.Rol + "'," +
                                                                          "'" + _iDiDentity + "'," +
                                                                          "'" + _v.Estado + "')";

            return db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> InsertPersonaJuridicaExterna(MPersonaJuridicaExterna _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_persona_juridica_externa""('" + _v.Id_tipo_persona + "'," +
                                                                                  "'" + _v.Nombre_fantasia + "'," +
                                                                                  "'" + _v.Nombre_legal + "'," +
                                                                                  "'" + _v.Cuit + "'," +
                                                                                  "'" + _v.Id_tipo_sociedad + "'," +
                                                                                  "'" + _v.Id_pais + "'," +
                                                                                  "'" + _v.Id_identity + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MPersonaJuridicaGet> GetListaPersonaJuridica()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_juridica"" ('" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MPersonaJuridicaGet>(sql);
        }

        public async Task<IEnumerable<MPersonaJuridicaGet>> GetGetListaPersonaJuridicaPersonaJuridica()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_juridica"" ('" + _iDiDentity + "')";
            return await db.QueryAsync<MPersonaJuridicaGet>(sql);
        }

        public async Task<IEnumerable<MPersonaJuridicaGet>> ListaPersonaJuridica()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_juridica_lista"" ()";
            return await db.QueryAsync<MPersonaJuridicaGet>(sql);
        }
        public async Task<IEnumerable<MRelacion>> ListaRelacion()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_relacion""()";
            return await db.QueryAsync<MRelacion>(sql);
        }
        public async Task<MRespuestaBoolMensaje> UpdatePersonaJuridica(MPersonaJuridicaUpdate _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Update_persona_juridica""('" + _v.Id + "'," +
                                                                          "'" + _v.Uid_persona_fisica + "'," +
                                                                          "'" + _v.Uid_persona_juridica + "'," +
                                                                          "'" + _v.Nombre_fantasia + "'," +
                                                                          "'" + _v.Nombre_legal + "'," +
                                                                           "'" + _v.Id_tipo_sociedad + "'," +
                                                                          "'" + _v.Id_pais + "'," +
                                                                          "'" + _v.Rol + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }
    }
}
