using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Personas;
using AutenticacionBlazor.Shared.Modelos.Personas.Juridica;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Personas
{
    public class SPersonas : IPersonas
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private string _iDiDentity { get; set; }
        public SPersonas(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<MRespuestaBoolMensaje> InsertDireccionesPersonas(MInsertUpdateDirecciones _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Insert_persona_direccion""('" + _v.Id_persona + "'," +
                                                                        "'" + _v.Id_tipo_direccion + "'," +
                                                                        "'" + _v.Id_pais + "'," +
                                                                        "'" + _v.Id_provincia + "'," +
                                                                        "'" + _v.Id_localidad + "'," +
                                                                        "'" + _v.Codigo_postal + "'," +
                                                                        "'" + _v.Nombre_calle + "'," +
                                                                        "'" + _v.Numero + "'," +
                                                                        "'" + _v.Piso + "'," +
                                                                        "'" + _v.Departamento + "'," +
                                                                        "'" + _v.Sector + "'," +
                                                                        "'" + _v.Manzana + "'," +
                                                                        "'" + _v.Lote + "'," +
                                                                        "'" + _v.Descripcion + "'," +
                                                                        "'" + _iDiDentity + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdateDireccionesPersonas(MInsertUpdateDirecciones _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Update_persona_direccion""('" + _v.Id_direccion_persona + "'," +
                                                                        "'" + _v.Id_direccion + "'," +
                                                                        "'" + _v.Id_tipo_direccion + "'," +
                                                                        "'" + _v.Id_pais + "'," +
                                                                        "'" + _v.Id_provincia + "'," +
                                                                        "'" + _v.Id_localidad + "'," +
                                                                        "'" + _v.Codigo_postal + "'," +
                                                                        "'" + _v.Nombre_calle + "'," +
                                                                        "'" + _v.Numero + "'," +
                                                                        "'" + _v.Piso + "'," +
                                                                        "'" + _v.Departamento + "'," +
                                                                        "'" + _v.Sector + "'," +
                                                                        "'" + _v.Manzana + "'," +
                                                                        "'" + _v.Lote + "'," +
                                                                        "'" + _v.Descripcion + "'," +
                                                                        "'" + _iDiDentity + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<IEnumerable<MMisDirecciones>> ListaDireccionesPersonas()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_personas_direcciones"" ('" + _iDiDentity + "')";
            return await db.QueryAsync<MMisDirecciones>(sql);
        }

        public async Task<IEnumerable<MMisDirecciones>> ListaDireccionesPersonasJuridicas()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_personas_juridicas_direcciones"" ('" + _iDiDentity + "')";
            return await db.QueryAsync<MMisDirecciones>(sql);
        }

        //Falta Corregir
        public async Task<IEnumerable<MPersonaJuridicaGet>> PersonasJuridicasByIdUser()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_juridica_lista""";
            return await db.QueryAsync<MPersonaJuridicaGet>(sql);
        }
        public async Task<MRespuestaBoolMensaje> DeleteDireccionesPersonas(MEliminarDireccion _id)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Delete_direccion""('" + _id.Id + "'," +
                                                                   "'" + _id.Motivo + "'," +
                                                                   "'" + _iDiDentity + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }
    }
}
