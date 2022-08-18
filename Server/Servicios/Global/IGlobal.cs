using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutenticacionBlazor.Shared.Modelos.Rentas.Comercio;
using Microsoft.AspNetCore.Identity;

namespace AutenticacionBlazor.Server.Servicios.Global
{
    public interface IGlobal
    {
        Task<IEnumerable<MListaPaises>> ListaPaises();
        Task<IEnumerable<MListaProvincias>> ListaProvincias();
        Task<IEnumerable<MListaProvincias>> ListaProvinciasByIdPais(int id_pais);
        Task<IEnumerable<MListaLocalidades>> ListaLocalidades();
        Task<IEnumerable<MListaLocalidades>> ListaLocalidadesByIdProvincia(int id_provincia);
        Task<IEnumerable<MListaTipoDirecciones>> ListaTipoDirecciones();
        Task<IEnumerable<MListaGeneros>> ListaGeneros();
        Task<IEnumerable<MListaEstadoCivil>> ListaEstadoCivil();
        Task<IEnumerable<MListaTipoDni>> ListaTipoDni();
        Task<IEnumerable<MListaTipoSociedad>> ListaTipoSociedad();
        Task<IEnumerable<MTelefono>> GetMisTelefonos();
        Task<IEnumerable<MTelefono>> GetTelefonosByUid(int uid);
        Task<MTelefono> GetTelefono(int id);
        Task<MRespuestaBoolMensaje> InsertTelefono(MTelefono telefono);
        Task<MRespuestaBoolMensaje> InsertTelefono2(MTelefono telefono);
        Task<MRespuestaBoolMensaje> UpdateTelefono(MTelefono telefono);
        Task<MRespuestaBoolMensaje> UpdateTelefono2(MTelefono telefono);
        Task<MRespuestaBoolMensaje> DeleteTelefono(string motivobaja, MTelefono telefono);
        Task<IEnumerable<MEmail>> GetMisMails();
        Task<IEnumerable<MEmail>> GetMailsByUid(int uid);
        Task<MEmail> GetEmail(int id);
        Task<MRespuestaBoolMensaje> InsertMail(MEmail mail);
        Task<MRespuestaBoolMensaje> InsertMail2(MEmail mail);
        Task<MRespuestaBoolMensaje> UpdateMail(MEmail mail);
        Task<MRespuestaBoolMensaje> UpdateMail2(MEmail mail);
        Task<MRespuestaBoolMensaje> DeleteMail(string motivobaja, MEmail mail);
        Task<IEnumerable<MOficina>> GetOficinas();
        Task<MRespuestaBoolMensaje> AssignOficinaToUser(int id_oficina, MAspNetUsers aspnetuser);
        Task<MRespuestaBoolMensaje> RemoveOficinaFromUser(int id_oficina, MAspNetUsers aspnetuser);
        Task<IEnumerable<MOficina>> GetUserOficinas(string ididentity);
        Task<IEnumerable<MOficina>> GetNotUserOficinas(string ididentity);
        Task<MVerificarLoginExterno> VerificarLoginExterno(string cuit);
        Task<MVerificarLoginProvider> VerificarLoginProvider(string cuit);
        Task<IEnumerable<MListaPeriodosFiscales>> ListaPeriodoFiscal(MBuscarPeriodoFiscal _v);
        Task<MBuscaPeriodoFiscalResultado> BuscarPeriodoFiscal(MBuscarPeriodoFiscal _v);
        Task<SignInResult> NuevoLoginTest(string user, string password);
    }
}
