using AutenticacionBlazor.Server.Servicios.Personas;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Personas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Controllers.Personas
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : Controller
    {
        private readonly IPersonas _personas;
        public PersonasController(IPersonas personaFisica)
        {
            _personas = personaFisica;
        }

        /// <summary>
        /// Agregar direccion a una persona
        /// </summary>
        /// /// <remarks>
        /// Ejemplo:
        ///
        ///     {
        ///        "id_identity": "f110fabe-40e5-4a31-aee1-721147e0027a",
        ///        "id_tipo_direccion": 1,
        ///        "id_pais": 5,
        ///        "id_provincia": 1841,
        ///        "id_localidad": 573416,
        ///        "codigo_postal": "4107",
        ///        "nombre_calle": "Av. Aconquija",
        ///        "numero": "1991",
        ///        "piso": "0",
        ///        "departamento": "0",
        ///        "sector": "0",
        ///        "manzana": "0",
        ///        "lote": "0",
        ///        "descripcion": "Municipalidad de Yerba Buena",
        ///        "usuario_graba": "f110fabe-40e5-4a31-aee1-721147e0027a"
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "general,super")]
        [HttpPost("Direccion/Agregar")]
        public async Task<MRespuestaBoolMensaje> AgregarDireccionPersonas(MInsertUpdateDirecciones _v)
        {
            return await _personas.InsertDireccionesPersonas(_v);
        }

        /// <summary>
        /// Modifica direccion a una persona
        /// </summary>
        /// /// <remarks>
        /// Ejemplo:
        ///
        ///     {
        ///        "id_direccion_persona": 3,
        ///        "Id_direccion": 5,
        ///        "id_tipo_direccion": 1,
        ///        "id_pais": 5,
        ///        "id_provincia": 1841,
        ///        "id_localidad": 573416,
        ///        "codigo_postal": "4107",
        ///        "nombre_calle": "Av. Aconquija",
        ///        "numero": "1991",
        ///        "piso": "0",
        ///        "departamento": "0",
        ///        "sector": "0",
        ///        "manzana": "0",
        ///        "lote": "0",
        ///        "descripcion": "Municipalidad de Yerba Buena",
        ///        "usuario_graba": "f110fabe-40e5-4a31-aee1-721147e0027a"
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "general,super")]
        [HttpPost("Direccion/Modificar")]
        public async Task<MRespuestaBoolMensaje> ModificarDireccionPersonas(MInsertUpdateDirecciones _v)
        {
            return await _personas.UpdateDireccionesPersonas(_v);
        }

        /// <summary>
        /// Lista de personas
        /// </summary>
        /// /// <remarks>
        /// Ejemplo id_identity:
        ///
        ///     f110fabe-40e5-4a31-aee1-721147e0027a
        ///
        /// </remarks>
        [Authorize(Roles = "general,super")]
        [HttpGet("Direccion/Lista")]
        public async Task<IEnumerable<MMisDirecciones>> ListaDireccionPersonas()
        {
            return await _personas.ListaDireccionesPersonas();
        }

        /// <summary>
        /// Borrar direccion
        /// </summary>
        /// /// <remarks>
        /// Ejemplo:
        ///
        ///     
        ///
        /// </remarks>
        [Authorize(Roles = "general,super")]
        [HttpPost("Direccion/Borrar")]
        public async Task<MRespuestaBoolMensaje> DeleteDireccionesPersonas(MEliminarDireccion _id)
        {
            return await _personas.DeleteDireccionesPersonas(_id);
        }
    }
}
