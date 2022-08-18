using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Rentas.Comercio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Rentas.Comercio
{
    public interface IComercioActividades
    {
        Task<MRespuestaBoolMensaje> AgregarActividadesAPersona(MAgregarActividadesAPersonas _v);
        Task<IEnumerable<MListaActividades>> ListaActividadesActivas();
        Task<IEnumerable<MListaGrupoActividades>> ListaActividadesGrupo();
        Task<IEnumerable<MListaSubGrupoActividades>> ListaActividadesSubGrupo();
        Task<IEnumerable<MMisActividades>> MisActividades();
        Task<MDdjj_InformacionPersona> ComercioPersonaInformacion();
        Task<IEnumerable<MDdjj_actividades>> DdjjMisActividades(int periodo);
        Task<MRespuestaBoolMensaje> EditarActividadesAPersona(MActividadesEditar _v);
        Task<IEnumerable<MListaSubGrupoActividades>> ListaSubGrupoByIdgrupo(int id_titulo);
        Task<MRespuestaBoolMensaje> Actividad_alta(MActividadesAlta actividad);
        Task<MRespuestaBoolMensaje> BajaActividades(MActividadesBaja _v);
        Task<MRespuestaBoolMensaje> Ddjj_agregar_ver(MDdjj_ver_carga _v);
        Task<MRespuestaBoolMensaje> Agregarddjj(MDdjj_agregar _v);
    }
}
