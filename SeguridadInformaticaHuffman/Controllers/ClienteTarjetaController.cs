using BusinessLogic;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeguridadInformaticaHuffman.Models;
using SeguridadInformaticaHuffman.Models.ClienteTarjeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Controllers
{
    [Authorize]
    [Route("cliente/tarjeta")]
    public class ClienteTarjetaController : ControllerBase
    {
        private readonly IDataProtector dataProtector;

        public ClienteTarjetaController(IDataProtectionProvider dataProtector)
        {
            this.dataProtector = dataProtector.CreateProtector("SecretKeyTarjeta");
        }

        [HttpPost]
        public IActionResult Registrar([FromBody] RegistroModel registroModel)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if (registroModel == null)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                Tuple<Boolean, String> Valid = registroModel.Validate();

                if (!Valid.Item1)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = Valid.Item2;

                    return BadRequest(responseModel);
                }

                ClienteTarjetaBL clienteTarjetaBL = new ClienteTarjetaBL();

                Tarjeta tarjeta = new Tarjeta
                {
                    NumeroTarjeta = registroModel.NumeroTarjeta,
                    CVC = registroModel.CVC,
                    Propietario = registroModel.Propietario,
                    FechaExpiracion = registroModel.FechaExpiracion
                };

                String datosTarjeta = JsonSerializer.Serialize(tarjeta);

                String encriptacionTarjeta = dataProtector.Protect(datosTarjeta);

                String clienteId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if(String.IsNullOrEmpty(clienteId))
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "Sesión expirada";

                    return Unauthorized(responseModel);
                }

                Int32 ClienteId = Int32.Parse(clienteId);

                ClienteTarjeta clienteTarjeta = new ClienteTarjeta
                {
                    ClienteId = ClienteId,
                    Descripcion = encriptacionTarjeta
                };

                Int32 ClienteTarjetaId = clienteTarjetaBL.Registrar(clienteTarjeta);

                if (ClienteTarjetaId > 0)
                {
                    clienteTarjeta.ClienteId = ClienteTarjetaId;

                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se registro la tarjeta de manera satisfactoria";
                    responseModel.Data = clienteTarjeta;

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo registrar la tarjeta";
                    responseModel.Data = null;

                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
            catch (Exception ex)
            {

                responseModel.Codigo = CodeEN.Exception;
                responseModel.Mensaje = "Ocurrió una excepción";

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [HttpGet("{ClienteId}")]
        public IActionResult ListarPorCliente(Int32? ClienteId)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if (ClienteId == null || ClienteId <= 0)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                ClienteTarjetaBL clienteTarjetaBL = new ClienteTarjetaBL();
                List<ClienteTarjeta> lstclienteTarjetas = clienteTarjetaBL.Listar(ClienteId.GetValueOrDefault());

                if (lstclienteTarjetas != null)
                {
                    List<TarjetaPago> lstTarjetaPagos = new List<TarjetaPago>();

                    if (lstclienteTarjetas.Count > 0)
                    {
                        lstclienteTarjetas.ForEach(clienteTarjeta =>
                        {
                            TarjetaPago tarjetaPago = new TarjetaPago();

                            tarjetaPago.ClienteTarjetaId = clienteTarjeta.ClienteTarjetaId;
                            tarjetaPago.ClienteId = clienteTarjeta.ClienteId;

                            String tarjetaDesencriptado = dataProtector.Unprotect(clienteTarjeta.Descripcion);

                            Tarjeta tarjeta = JsonSerializer.Deserialize<Tarjeta>(tarjetaDesencriptado);

                            tarjetaPago.NumeroTarjeta = tarjeta.NumeroTarjeta;
                            tarjetaPago.Propietario = tarjeta.Propietario;
                            tarjetaPago.CVC = tarjeta.CVC;
                            tarjetaPago.FechaExpiracion = tarjeta.FechaExpiracion;

                            lstTarjetaPagos.Add(tarjetaPago);
                        });
                    }

                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se listó las tarjetas de pago con éxito";
                    responseModel.Data = lstTarjetaPagos;

                    return Ok(responseModel);

                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo listar sus tarjetas";

                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }

            }
            catch (Exception ex)
            {

                responseModel.Codigo = CodeEN.Exception;
                responseModel.Mensaje = "Ocurrió una excepción";

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }


        [HttpDelete("{ClienteTarjetaId}")]
        public IActionResult Eliminar(Int32? ClienteTarjetaId)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if (ClienteTarjetaId == null || ClienteTarjetaId <= 0)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                ClienteTarjetaBL clienteTarjetaBL = new ClienteTarjetaBL();

                if (clienteTarjetaBL.Eliminar(ClienteTarjetaId.GetValueOrDefault()))
                {
                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se eliminó su tarjeta de manera satisfactoria";

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo eliminar su tarjeta de manera satisfactoria";

                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }

            }
            catch (Exception ex)
            {

                responseModel.Codigo = CodeEN.Exception;
                responseModel.Mensaje = "Ocurrió una excepción";

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }


    }
}
