using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Models.Identity;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Usuarios
{
    public class SUsuarios : IUsuarios
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private string _iDiDentity { get; set; }
        public SUsuarios(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        protected NpgsqlConnection dbConnection2()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString2);
        }

        public async Task<IEnumerable<MAspNetUsers>> GetAspNetUsers()
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Get_aspnetusers""()";
            return await db.QueryAsync<MAspNetUsers>(sql);
        }

        public async Task<IEnumerable<MAspNetRoles>> GetAspNetRoles()
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Get_aspnetroles""()";
            return await db.QueryAsync<MAspNetRoles>(sql);
        }

        public async Task<MAspNetRoles> GetRole(string roleid)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Get_role""('" + roleid + "')";
            return await db.QueryFirstOrDefaultAsync<MAspNetRoles>(sql);
        }

        public async Task<MRespuestaBoolMensaje> InsertRole(MAspNetRoles aspnetrole)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Insert_role""('" + aspnetrole.RoleName + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> UpdateRole(MAspNetRoles aspnetrole)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Update_role""('" + aspnetrole.RoleId + "'," +
                                                            "'" + aspnetrole.RoleName + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> DeleteRole(MAspNetRoles aspnetrole)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Delete_role""('" + aspnetrole.RoleId + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public void AssignRoleToNewUser(string ididentity)
        {
            var db = dbConnection2();
            string SQL = "public.asignar_rol_usuario_externo";
            db.ExecuteReader(sql: SQL,
                param: new
                {
                    identidad = ididentity
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MAspNetUsers>> SearchUsers(string term)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Search_users""('" + term + "')";
            return await db.QueryAsync<MAspNetUsers>(sql);
        }

        public async Task<IEnumerable<MAspNetRoles>> SearchRoles(string term)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Search_roles""('" + term + "')";
            return await db.QueryAsync<MAspNetRoles>(sql);
        }

        public async Task<MRespuestaBoolMensaje> AssignRoleToUser(string roleid, MAspNetUsers aspnetuser)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Assign_role_to_user""('" + aspnetuser.Id + "'," +
                                                                    "'" + roleid + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<MRespuestaBoolMensaje> RemoveRoleFromUser(string roleid, MAspNetUsers aspnetuser)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Remove_role_from_user""('" + aspnetuser.Id + "'," +
                                                                    "'" + roleid + "')";
            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }

        public async Task<IEnumerable<MAspNetRoles>> GetUserRoles(string ididentity)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Get_user_roles""('" + ididentity + "')";
            return await db.QueryAsync<MAspNetRoles>(sql);
        }

        public async Task<IEnumerable<MAspNetRoles>> GetNotUserRoles(string ididentity)
        {
            var db = dbConnection2();
            var sql = @"SELECT * FROM public.""Get_not_user_roles""('" + ididentity + "')";
            return await db.QueryAsync<MAspNetRoles>(sql);
        }


        public async Task<MDatosUsuarioLogeado> DatosUsuarioLogeado()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM personas.""Get_persona_datos""('" + _iDiDentity + "')";

            return await db.QueryFirstOrDefaultAsync<MDatosUsuarioLogeado>(sql);
        }

        public void AsignarRolAUsuario(string mail)
        {
            var db = dbConnection();
            string SQL = "public.\"Assign_role_to_new_user\"";
            db.ExecuteReader(sql: SQL,
                param: new
                {
                    identidad = mail
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
