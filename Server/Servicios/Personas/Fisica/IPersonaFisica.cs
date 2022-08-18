using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Personas.Fisica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Personas.Fisica
{
    public interface IPersonaFisica
    {
        Task<MRespuestaBoolMensaje> InsertPersonaFisica(MPersonaFisicaInsert _v);
        Task<MRespuestaBoolMensaje> UpdatePersonaFisica(MPersonaFisicaLista _v);
        Task<MRespuestaBoolMensaje> InsertPersonaFisicaExterna(MPersonaFisicaExterna _v);
        Task<MPersonaFisicaGet> GetPersonaFisica();
        Task<IEnumerable<MPersonaFisicaLista>> ListaPersonaFisica();
        Task<IEnumerable<MPersonaFisicaLista>> GetPersonasFisicas();
        Task<MObtenerUidPersona> GetUid();
        Task<IEnumerable<MPersonaFisicaLista>> SearchPersonaFisica(string term);
        Task<MPersonaFisicaGet> GetPersonaFisicaById(int id);
    }
}
