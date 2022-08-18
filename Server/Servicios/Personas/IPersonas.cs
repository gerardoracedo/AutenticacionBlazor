using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Personas;
using AutenticacionBlazor.Shared.Modelos.Personas.Juridica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Personas
{
    public interface IPersonas
    {
        Task<MRespuestaBoolMensaje> InsertDireccionesPersonas(MInsertUpdateDirecciones _v);
        Task<MRespuestaBoolMensaje> UpdateDireccionesPersonas(MInsertUpdateDirecciones _v);
        Task<IEnumerable<MMisDirecciones>> ListaDireccionesPersonas();
        Task<IEnumerable<MMisDirecciones>> ListaDireccionesPersonasJuridicas();
        Task<IEnumerable<MPersonaJuridicaGet>> PersonasJuridicasByIdUser();
        Task<MRespuestaBoolMensaje> DeleteDireccionesPersonas(MEliminarDireccion _id);
    }
}
