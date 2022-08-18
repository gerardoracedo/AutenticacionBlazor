using AutenticacionBlazor.Server.Servicios.Tesoreria;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Rentas.Comercio;
using Dapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Rentas.Comercio
{
    public class SComercioActividades : IComercioActividades
    {
        private PostgreSQLConfiguration _connectionString;
        private readonly UsuarioLogeado _uLogeado;
        private readonly ITesoreria _teso;
        private string _iDiDentity { get; set; }
        public SComercioActividades(PostgreSQLConfiguration connectionString, UsuarioLogeado uLogeado, ITesoreria teso)
        {
            _connectionString = connectionString;
            _uLogeado = uLogeado;
            _teso = teso;

            _iDiDentity = _uLogeado.IdUsuarioIdentity();
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<MRespuestaBoolMensaje> AgregarActividadesAPersona(MAgregarActividadesAPersonas _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Agregar_actividades_a_personas""('2'," +
                                                                                        "'" + _iDiDentity + "'," +
                                                                                        "'" + _v.Uid_persona + "'," +
                                                                                        "'" + _v.Codigo_actividad + "'," +
                                                                                        "'" + _v.Inicio_actividad + "'," +
                                                                                        "'" + _v.Id_direccion + "'," +
                                                                                        "'" + _v.Id_titulo + "'," +
                                                                                        "'" + _v.Id_subtitulo + "'," +
                                                                                        "'" + _v.Local + "'," +
                                                                                        "'" + _v.Nombre_local + "'," +
                                                                                        "'" + _v.Padron_cisi + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }
        public async Task<IEnumerable<MListaActividades>> ListaActividadesActivas()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Get_lista_actividades""()";

            return await db.QueryAsync<MListaActividades>(sql);
        }

        public async Task<IEnumerable<MMisActividades>> MisActividades()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Get_mis_actividades_persona_fisica"" ('" + _iDiDentity + "')";
            var persona_fisica = await db.QueryAsync<MMisActividades>(sql);

            var sql2 = @"SELECT * FROM comercio.""Get_mis_actividades_persona_juridica"" ('" + _iDiDentity + "')";
            var persona_juridica = await db.QueryAsync<MMisActividades>(sql2);

            var resultado = persona_fisica.Concat(persona_juridica);

            return resultado;
        }

        public async Task<MDdjj_InformacionPersona> ComercioPersonaInformacion()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Get_persona_informacion"" ('" + _iDiDentity + "')";
            return await db.QueryFirstOrDefaultAsync<MDdjj_InformacionPersona>(sql);
        }

        public async Task<IEnumerable<MDdjj_actividades>> DdjjMisActividades(int periodo)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Get_mis_actividades_persona"" ('" + _iDiDentity + "','" + periodo + "')";
            return await db.QueryAsync<MDdjj_actividades>(sql);
        }

        public async Task<IEnumerable<MListaGrupoActividades>> ListaActividadesGrupo()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Get_lista_titulos""()";

            return await db.QueryAsync<MListaGrupoActividades>(sql);
        }

        public async Task<IEnumerable<MListaSubGrupoActividades>> ListaActividadesSubGrupo()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Get_lista_subtitulos""()";

            return await db.QueryAsync<MListaSubGrupoActividades>(sql);
        }

        public async Task<MRespuestaBoolMensaje> EditarActividadesAPersona(MActividadesEditar _v)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM comercio.""Editar_actividades""('" + _iDiDentity + "'," +
                                                                            "'" + _v.Id + "'," +
                                                                            "'" + _v.Codigo + "'," +
                                                                            "'" + _v.Descripcion + "'," +
                                                                            "'" + _v.Alicuota + "'," +
                                                                            "'" + _v.Fecha_inicio + "'," +
                                                                            "'" + _v.Exento + "'," +
                                                                            "'" + _v.Fecha_fin + "')";

            return await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(sql);
        }
        public async Task<IEnumerable<MListaSubGrupoActividades>> ListaSubGrupoByIdgrupo(int id_titulo)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM comercio.""Get_subtitulo_lista_titulos_por_idtitulo"" ('" + id_titulo + "')";
                return await db.QueryAsync<MListaSubGrupoActividades>(sql);
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
              
                return null;
            }
        }

        public async Task<MRespuestaBoolMensaje> Actividad_alta(MActividadesAlta actividad)
        {
            try
            {
                actividad.Estado = 1;
                var db = dbConnection();
                var sql = @"SELECT * FROM comercio.""Insert_actividad""('" + actividad.Estado + "'," +
                                                                  "'" + actividad.Id_subtitulo + "'," +
                                                                  "'" + actividad.Codigo + "'," +
                                                                  "'" + actividad.Descripcion + "'," +
                                                                  "'" + actividad.Observacion + "'," +
                                                                  "'" + actividad.Alicuota + "'," +
                                                                  "'" + actividad.Fecha_inicio + "'," +
                                                                  "'" + actividad.Exento + "'," +
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
        public async Task<MRespuestaBoolMensaje> BajaActividades(MActividadesBaja _v)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT * FROM comercio.""Eliminar_actividades""('" + _iDiDentity + "'," +
                                                                                "'" + _v.Id + "'," +
                                                                                "'" + _v.Fecha_fin + "')";

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
        public async Task<MRespuestaBoolMensaje> Ddjj_agregar_ver(MDdjj_ver_carga _v)
        {
            try
            {
                // Conexion a la db
                var db = dbConnection();
                var query = @"SELECT * FROM ""comercio"".""Ver_ddjj""('" + _v.Persona + "'," + "'" + _v.Periodo + "')";
                var doquery = await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(query);
                if (doquery.resultado == true)
                {
                    return doquery;
                }
                else
                {
                    throw new Exception(doquery.mensaje);
                }
            }
            catch (Exception ex)
            {
                MRespuestaBoolMensaje respuesta = new MRespuestaBoolMensaje();
                respuesta.resultado = false;
                respuesta.mensaje = ex.Message;
                return respuesta;
            }

        }
        public async Task<MRespuestaBoolMensaje> Agregarddjj(MDdjj_agregar _v)
        {
            try
            {
                // Conexion a la db
                var db = dbConnection();
                var Agregarddjj = @"SELECT * FROM ""comercio"".""Agregar_ddjj""('" + _iDiDentity + "'," + "'" + System.Text.Json.JsonSerializer.Serialize(_v) + "')";
                var doAgregarddjj = await db.QueryFirstOrDefaultAsync<MRespuestaBoolMensaje>(Agregarddjj);
                if (doAgregarddjj.resultado == true)
                {
                    return doAgregarddjj;
                }
                else
                {
                    throw new Exception(doAgregarddjj.mensaje);
                }
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
