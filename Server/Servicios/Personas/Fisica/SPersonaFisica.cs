using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Personas.Fisica;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Personas.Fisica
{
    public class SPersonaFisica : IPersonaFisica
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private string _iDiDentity { get; set; }
        public SPersonaFisica(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;
            _iDiDentity = _uLogeado.IdUsuarioIdentity();
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<MRespuestaBoolMensaje> InsertPersonaFisica(MPersonaFisicaInsert _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_persona_fisica""('" + _v.Id_tipo_persona + "'," +
                                                                        "'" + _v.Apellido + "'," +
                                                                        "'" + _v.Nombre + "'," +
                                                                        "'" + _v.Dni + "'," +
                                                                        "'" + _v.Cuit + "'," +
                                                                        "'" + _v.Fecha_nacimiento + "'," +
                                                                        "'" + _v.Id_genero + "'," +
                                                                        "'" + _v.Id_pais + "'," +
                                                                        "'" + _v.Id_estado_civil + "'," +
                                                                        "'" + _v.Id_tipo_dni + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MPersonaFisicaGet> GetPersonaFisicaById(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_fisica_by_id"" ('" + id + "')";
            return await db.QueryFirstOrDefaultAsync<MPersonaFisicaGet>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdatePersonaFisica(MPersonaFisicaLista _v)
        {
            
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM personas.""Update_persona_fisica""('" + _v.Uid_persona + "'," +
                                                                            "'" + _v.Apellido + "'," +
                                                                            "'" + _v.Nombre + "'," +
                                                                            "'" + _v.Dni + "'," +
                                                                            "'" + _v.Cuit + "'," +
                                                                            "'" + _v.Fecha_nacimiento + "'," +
                                                                            "'" + _v.Id_genero + "'," +
                                                                            "'" + _v.Id_pais + "'," +
                                                                            "'" + _v.Id_estado_civil + "'," +
                                                                            "'" + _v.Id_tipo_dni + "')";
                return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);

            } catch (Exception e)
            {
                MRespuestaBoolMensaje mRespuestaBoolMensaje = new MRespuestaBoolMensaje();
                mRespuestaBoolMensaje.mensaje = e.Message;
                mRespuestaBoolMensaje.resultado = false;
                return mRespuestaBoolMensaje;
            }
        }

        public async Task<MRespuestaBoolMensaje> InsertPersonaFisicaExterna(MPersonaFisicaExterna _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_persona_fisica_externa""('" + _v.Id_tipo_persona + "'," +
                                                                                "'" + _v.Apellido + "'," +
                                                                                "'" + _v.Nombre + "'," +
                                                                                "'" + _v.Dni + "'," +
                                                                                "'" + _v.Cuit + "'," +
                                                                                "'" + _v.Fecha_nacimiento + "'," +
                                                                                "'" + _v.Id_genero + "'," +
                                                                                "'" + _v.Id_pais + "'," +
                                                                                "'" + _v.Id_estado_civil + "'," +
                                                                                "'" + _v.Id_tipo_dni + "'," +
                                                                                "'" + _v.Id_identity + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MPersonaFisicaGet> GetPersonaFisica()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_fisica"" ('"+ _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MPersonaFisicaGet>(sql);
        }

        public async Task<IEnumerable<MPersonaFisicaLista>> ListaPersonaFisica()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_fisica_lista"" ()";
            return await db.QueryAsync<MPersonaFisicaLista>(sql);
        }
        public async Task<IEnumerable<MPersonaFisicaLista>> GetPersonasFisicas()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_personas_fisicas"" ()";
            return await db.QueryAsync<MPersonaFisicaLista>(sql);
        }

        public async Task<MObtenerUidPersona> GetUid()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_uid_persona""('" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MObtenerUidPersona>(sql);
        }

        public async Task<IEnumerable<MPersonaFisicaLista>> SearchPersonaFisica(string term)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Search_persona_fisica"" ('" + term + "')";
            return await db.QueryAsync<MPersonaFisicaLista>(sql);
        }
    }
}
