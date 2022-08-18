using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Personas.Juridica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Personas.Juridica
{
    public interface IPersonaJuridica
    {
        Task<MRespuestaBoolMensaje> InsertPersonaJuridica(MPersonaJuridicaInsert _v);
        Task<MRespuestaBoolMensaje> InsertPersonaJuridicaExterna(MPersonaJuridicaExterna _v);
        Task<MPersonaJuridicaGet> GetListaPersonaJuridica();
        Task<IEnumerable<MPersonaJuridicaGet>> GetGetListaPersonaJuridicaPersonaJuridica();
        Task<IEnumerable<MPersonaJuridicaGet>> ListaPersonaJuridica();
        Task<IEnumerable<MRelacion>> ListaRelacion();
        Task<MRespuestaBoolMensaje> UpdatePersonaJuridica(MPersonaJuridicaUpdate _v);
    }
}
