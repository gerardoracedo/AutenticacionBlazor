using AutenticacionBlazor.Server.Models;
using AutenticacionBlazor.Server.Servicios.Global;
using AutenticacionBlazor.Server.Servicios.Tickets;
using AutenticacionBlazor.Shared.Modelos;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Tickets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AutenticacionBlazor.Server.Controllers.Global
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GlobalController : Controller
    {
        private readonly IGlobal _global;
        private readonly ITickets _tickets;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public GlobalController(IGlobal global, ITickets tickets, SignInManager<ApplicationUser> signInManager)
        {
            _global = global;
            _tickets = tickets;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpGet("GetOficinas")]
        public async Task<IEnumerable<MOficina>> GetOficinas()
        {
            return await _global.GetOficinas();
        }

        [Authorize(Roles = "admin, super, AltaBajaOficinaUsuario")]
        [HttpPost("AssignOficinaToUser/{id_oficna}")]
        public async Task<MRespuestaBoolMensaje> AssignOficinaToUser(int id_oficna, MAspNetUsers aspnetuser)
        {
            return await _global.AssignOficinaToUser(id_oficna, aspnetuser);
        }

        [Authorize(Roles = "admin, super, AltaBajaOficinaUsuario")]
        [HttpPost("RemoveOficinaFromUser/{id_oficna}")]
        public async Task<MRespuestaBoolMensaje> RemoveOficinaFromUser(int id_oficna, MAspNetUsers aspnetuser)
        {
            return await _global.RemoveOficinaFromUser(id_oficna, aspnetuser);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetUserOficinas/{ididentity}")]
        public async Task<IEnumerable<MOficina>> GetUserOficinas(string ididentity)
        {
            return await _global.GetUserOficinas(ididentity);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetNotUserOficinas/{ididentity}")]
        public async Task<IEnumerable<MOficina>> GetNotUserOficinas(string ididentity)
        {
            return await _global.GetNotUserOficinas(ididentity);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpGet("GetMisTelefonos")]
        public async Task<IEnumerable<MTelefono>> GetMisTelefonos()
        {
            return await _global.GetMisTelefonos();
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpGet("GetTelefono/{id}")]
        public async Task<MTelefono> GetTelefono(int id)
        {
            return await _global.GetTelefono(id);
        }

        [Authorize(Roles = "admin, super, EditarPersonaFisica")]
        [HttpGet("GetTelefonosByUid/{uid}")]
        public async Task<IEnumerable<MTelefono>> GetTelefonosByUid(int uid)
        {
            return await _global.GetTelefonosByUid(uid);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("InsertTelefono")]
        public async Task<MRespuestaBoolMensaje> InsertTelefono(MTelefono telefono)
        {
            return await _global.InsertTelefono(telefono);
        }

        [Authorize(Roles = "admin, super, general, EditarPersonaFisica")]
        [HttpPost("InsertTelefono2")]
        public async Task<MRespuestaBoolMensaje> InsertTelefono2(MTelefono telefono)
        {
            return await _global.InsertTelefono2(telefono);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("UpdateTelefono")]
        public async Task<MRespuestaBoolMensaje> UpdateTelefono(MTelefono telefono)
        {
            return await _global.UpdateTelefono(telefono);
        }

        [Authorize(Roles = "admin, super, general, EditarPersonaFisica")]
        [HttpPost("UpdateTelefono2")]
        public async Task<MRespuestaBoolMensaje> UpdateTelefono2(MTelefono telefono)
        {
            return await _global.UpdateTelefono2(telefono);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("DeleteTelefono/{motivobaja}")]
        public async Task<MRespuestaBoolMensaje> DeleteTelefono(string motivobaja, MTelefono telefono)
        {
            return await _global.DeleteTelefono(motivobaja, telefono);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpGet("GetMisMails")]
        public async Task<IEnumerable<MEmail>> GetMisMails()
        {
            return await _global.GetMisMails();
        }

        [Authorize(Roles = "admin, super, EditarPersonaFisica")]
        [HttpGet("GetMailsByUid/{uid}")]
        public async Task<IEnumerable<MEmail>> GetMailsByUid(int uid)
        {
            return await _global.GetMailsByUid(uid);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpGet("GetEmail/{id}")]
        public async Task<MEmail> GetEmail(int id)
        {
            return await _global.GetEmail(id);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("InsertMail")]
        public async Task<MRespuestaBoolMensaje> InsertMail(MEmail mail)
        {
            return await _global.InsertMail(mail);
        }

        [Authorize(Roles = "admin, super, general, EditarPersonaFisica")]
        [HttpPost("InsertMail2")]
        public async Task<MRespuestaBoolMensaje> InsertMail2(MEmail mail)
        {
            return await _global.InsertMail2(mail);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("UpdateMail")]
        public async Task<MRespuestaBoolMensaje> UpdateMail(MEmail mail)
        {
            return await _global.UpdateMail(mail);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("UpdateMail2")]
        public async Task<MRespuestaBoolMensaje> UpdateMail2(MEmail mail)
        {
            return await _global.UpdateMail2(mail);
        }

        [Authorize(Roles = "admin, super, general")]
        [HttpPost("DeleteMail/{motivobaja}")]
        public async Task<MRespuestaBoolMensaje> DeleteMail(string motivobaja, MEmail mail)
        {
            return await _global.DeleteMail(motivobaja, mail);
        }

        /// <summary>
        /// Lista de paises
        /// </summary>
        [Authorize(Roles = "general, super")]
        [HttpGet("Listadepaises")]
        public async Task<IEnumerable<MListaPaises>> ListaDePaises()
        {
            return await _global.ListaPaises();
        }

        /// <summary>
        /// Lista de provincias
        /// </summary>
        [Authorize(Roles = "general, super")]
        [HttpGet("Listadeprovincias")]
        public async Task<IEnumerable<MListaProvincias>> ListaDeProvincias()
        {
            return await _global.ListaProvincias();
        }

        /// <summary>
        /// Lista de provincias filtrado por id_pais 5
        /// </summary>
        /// /// <remarks>
        /// Ejemplo :
        ///
        ///     id_pais = 5
        ///
        /// </remarks>
        [Authorize(Roles = "general, super")]
        [HttpGet("Listadeprovincias/Pais/{id_pais}")]
        public async Task<IEnumerable<MListaProvincias>> ListaDeProvinciasPorIdPais(int id_pais)
        {
            return await _global.ListaProvinciasByIdPais(id_pais);
        }

        /// <summary>
        /// Lista de localidades
        /// </summary>
        [Authorize(Roles = "general, super")]
        [HttpGet("Listadelocalidades")]
        public async Task<IEnumerable<MListaLocalidades>> ListaDeLocalidades()
        {
            return await _global.ListaLocalidades();
        }

        /// <summary>
        /// Lista de localidades filtrado por id_provincia
        /// </summary>
        /// /// <remarks>
        /// Ejemplo :
        ///
        ///     id_provincia = 1841
        ///
        /// </remarks>
        [Authorize(Roles = "general, super")]
        [HttpGet("Listadelocalidades/Provincia/{id_provincia}")]
        public async Task<IEnumerable<MListaLocalidades>> ListaDeLocalidadesPorIdProvincia(int id_provincia)
        {
            return await _global.ListaLocalidadesByIdProvincia(id_provincia);
        }

        /// <summary>
        /// Lista de tipo de direcciones
        /// </summary>
        [Authorize(Roles = "general, super")]
        [HttpGet("Tipodedireciones")]
        public async Task<IEnumerable<MListaTipoDirecciones>> ListaTipoDirecciones()
        {
            return await _global.ListaTipoDirecciones();
        }

        /// <summary>
        /// Lista de sociedades
        /// </summary>
        [Authorize(Roles = "general, super")]
        [HttpGet("Listadesociedades")]
        public async Task<IEnumerable<MListaTipoSociedad>> ListaDeSociedades()
        {
            return await _global.ListaTipoSociedad();
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("ListaGeneros")]
        public async Task<IEnumerable<MListaGeneros>> ListaGeneros()
        {
            return await _global.ListaGeneros();
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("ListaTipoDni")]
        public async Task<IEnumerable<MListaTipoDni>> ListaTipoDni()
        {
            return await _global.ListaTipoDni();
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("ListaEstadoCivil")]
        public async Task<IEnumerable<MListaEstadoCivil>> ListaEstadoCivil()
        {
            return await _global.ListaEstadoCivil();
        }
        [Authorize(Roles = "general, super, admin")]
        [HttpGet("Tramite/AgregarDetalle")]
        public async Task<MRespuestaBoolMensaje> TicketAgregarDetalle(MMsjNuevoUploadFiles _v)
        {
            return await _tickets.InsertTicketDetalle(_v);
        }
        [Authorize(Roles = "general")]
        [HttpGet("Verificarlogin/{cuit}")]
        public async Task<MVerificarLoginProvider> VerificarLoginProvider(string cuit)
        {
            return await _global.VerificarLoginProvider(cuit);
        }

        [Authorize(Roles = "general")]
        [HttpPost("ListaPeriodoFiscal")]
        public async Task<IEnumerable<MListaPeriodosFiscales>> ListaPeriodoFiscal(MBuscarPeriodoFiscal _v)
        {
            return await _global.ListaPeriodoFiscal(_v);
        }

        [Authorize(Roles = "general")]
        [HttpPost("BuscarPeriodoFiscal")]
        public async Task<MBuscaPeriodoFiscalResultado> BuscarPeriodoFiscal(MBuscarPeriodoFiscal _v)
        {
            return await _global.BuscarPeriodoFiscal(_v);
        }

        [AllowAnonymous]
        [HttpPost("NuevoLogin")]
        public IActionResult NuevoLoginx()
        {
            /*
            var result = await _signInManager.PasswordSignInAsync("super@super.com", "super123", isPersistent:false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
            */



            MNuevo _re = new MNuevo();
            MNuevoPersona _pe = new MNuevoPersona();
            if (User.Identity.IsAuthenticated)
            {
                var _d = 1; // Sin datos = 0, Con datos = 1
                if (_d == 0)
                {
                    _re.Resultado = false;
                    _re.Mensaje = "No se encontro";
                    _re.Respuesta = null;
                    return NotFound(_re);
                }
                else
                {
                    _pe.Dni = "12345678";
                    _pe.Apellido = "Apellido";
                    _pe.Nombre = "Nombre";

                    _re.Resultado = true;
                    _re.Mensaje = "Se encontro";
                    _re.Respuesta = _pe;
                    return Ok(_re);
                }
            }
            else
            {
                _re.Resultado = false;
                _re.Mensaje = "No autorizado";
                _re.Respuesta = null;
                return Unauthorized(_re);
            }

        }
    }
}
