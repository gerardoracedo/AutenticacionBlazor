using AutenticacionBlazor.Shared.Modelos.Siro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Xml.Linq;
using System.Net;
using System.Collections.Generic;
using System;
using AutenticacionBlazor.Server.Servicios.Tesoreria;
using AutenticacionBlazor.Shared.Modelos.Global;

namespace AutenticacionBlazor.Server.Controllers.Siro
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SiroController : Controller
    {
        private readonly ITesoreria _tesoreria;

        [Authorize(Roles = "general")]
        [HttpGet("Loginsiro")]
        public async Task<MSiroLoginRespuesta> LoginSiro()
        {
            MSiroLoginRespuesta RespuestaLogin = new MSiroLoginRespuesta();
            var client = new RestClient("https://www.bancoroela.com.ar:8081");
            var request = new RestRequest("api/Sesion", Method.Post);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Accept-Charset", "utf-8");
            var body = new
            {
                Usuario = "MUNIYERBABUENA",
                Password = "Aconquija1991!"
            };
            request.AddJsonBody(body);
            var response = await client.ExecuteAsync(request);
            HttpStatusCode statusCode = response.StatusCode;
            MSiroLoginRespuesta res = Newtonsoft.Json.JsonConvert.DeserializeObject<MSiroLoginRespuesta>(response.Content);

            if ((int)statusCode == 200 || (int)statusCode == 201)
            {
                RespuestaLogin.success = true;
                RespuestaLogin.access_token = res.access_token;
                RespuestaLogin.Message = "Login exitoso";
            }
            else
            {
                RespuestaLogin.success = false;
                RespuestaLogin.access_token = "";
                RespuestaLogin.Message = res.Message;
            }

            return RespuestaLogin;

        }

        //[Authorize(Roles = "general")]
        [AllowAnonymous]
        [HttpPost("Intenciondepago")]
        public async Task<MSiroIntencionDePagoRespuesta> IntencionDePago(List<MSiroIntencionDePagoDetalle> _detalle)
        {
            //Detalle.Add(new MSiroIntencionDePagoDetalle { descripcion = "Test pago 1", importe = "1000" });
            // Importe maximo
            decimal Total_max = 99999;

            // Obtengo el token del login
            MSiroLoginRespuesta Token = await LoginSiro();

            // Resultado
            MSiroIntencionDePagoRespuesta Resultado = new MSiroIntencionDePagoRespuesta();

            // Body
            MSiroIntencionDePagoBody Body = new MSiroIntencionDePagoBody();

            // Genero intencion de pago en siro
            var client = new RestClient("https://www.bancoroela.com.ar:8081");
            var request = new RestRequest("api/Pago", Method.Post);

            // Header
            request.AddHeader("Authorization", "Bearer " + Token.access_token);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Accept-Charset", "utf-8");

            // Sumo los importes
            decimal Total = 0;
            foreach (var item in _detalle)
            {
                Total += item.importe;
            }

            // Datos para armar el body
            var CodigoEmpresa = "5120119308";

            // Id cliente


            // Acomodo el body
            Body.nro_cliente_empresa = "0000000455120119308";
            Body.nro_comprobante = "rOpmic2DAldAG6zWa500";
            Body.Concepto = "Pago de boletas MYB";
            Body.Importe = Total;
            Body.URL_OK = "http://192.168.40.182:3050/online/siro/verificar";
            Body.URL_ERROR = "http://192.168.40.182:3050/online/siro/verificar";
            Body.IdReferenciaOperacion = "rOpmic2DAldAG6zWa500";
            Body.Detalle = _detalle;

            // Agrego el body como json
            request.AddJsonBody(Body);

            // Ejecuto el post a siro
            if (Total > Total_max)
            {
                Resultado.success = false;
                Resultado.Url = "";
                Resultado.Hash = "";
                Resultado.Message = "Importe maximo excedido";
            }
            else
            {
                var response = await client.ExecuteAsync(request);
                HttpStatusCode statusCode = response.StatusCode;
                MSiroIntencionDePagoRespuesta res = Newtonsoft.Json.JsonConvert.DeserializeObject<MSiroIntencionDePagoRespuesta>(response.Content);

                if ((int)statusCode == 200 || (int)statusCode == 201)
                {
                    Resultado.success = true;
                    Resultado.Url = res.Url;
                    Resultado.Hash = res.Hash;
                    Resultado.Message = "";
                }
                else
                {
                    Resultado.success = false;
                    Resultado.Url = "";
                    Resultado.Hash = "";
                    Resultado.Message = res.Message;
                }
            }

            return Resultado;

        }
    }
}
