using BusinessLogic;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeguridadInformaticaHuffman.Helpers.Huffman;
using SeguridadInformaticaHuffman.Models;
using SeguridadInformaticaHuffman.Models.Cliente;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Controllers
{
    [Authorize]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ClienteController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult LogIn([FromBody] LoginModel loginModel)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if (loginModel == null)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                Tuple<Boolean, String> Valid = loginModel.Validate();

                if (!Valid.Item1)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = Valid.Item2;

                    return BadRequest(responseModel);
                }

                ClienteBL clienteBL = new ClienteBL();

                Cliente cliente = clienteBL.Consultar(loginModel.Cuenta);

                if (cliente == null)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Cuenta no encontrada";

                    return BadRequest(responseModel);
                }

                Int32 MaxIntentosPermitidos = configuration.GetValue<Int32>("Variables:Seguridad:NroIntentosPermitidos");
                Double MaxTiempoEspera = configuration.GetValue<Double>("Variables:Seguridad:TiempoEsperaIntentos");

                if (cliente.Estado != "3" ||
                   (cliente.Estado == "3" &&
                   (DateTime.UtcNow.AddHours(-5) - cliente.FechaUltimoIntento.GetValueOrDefault()).TotalHours > MaxTiempoEspera))
                {

                    DateTime fechaActual = DateTime.UtcNow.AddHours(-5);
                    String codigoResultado = GenerarCodigoHuffman(loginModel.Contrasena);

                    if (codigoResultado == cliente.Contrasena)
                    {
                        cliente.NroIntento = null;
                        cliente.FechaUltimoIntento = null;
                        cliente.Estado = "1";

                        clienteBL.ActualizarIntentos(cliente.ClienteId, cliente.Estado, cliente.NroIntento, cliente.FechaUltimoIntento);

                        String Token = GenerarToken(cliente);

                        var clienteLogueado = new
                        {
                            cliente.ClienteId,
                            cliente.Cuenta,
                            NombreCompleto = cliente.Nombre + " " + cliente.Apellido,
                            Token
                        };

                        responseModel.Codigo = CodeEN.Success;
                        responseModel.Mensaje = "El cliente ha accedido a la aplicación de manera satisfactoria";
                        responseModel.Data = clienteLogueado;

                        return Ok(responseModel);
                    }
                    else
                    {
                        if (cliente.Estado == "3")
                        {
                            cliente.NroIntento = 1;
                            cliente.FechaUltimoIntento = fechaActual;
                            cliente.Estado = "1";
                        }
                        else if ((DateTime.UtcNow.AddHours(-5) - cliente.FechaUltimoIntento.GetValueOrDefault()).TotalHours > MaxTiempoEspera)
                        {
                            cliente.NroIntento = 1;
                            cliente.FechaUltimoIntento = fechaActual;
                        }
                        else
                        {
                            cliente.NroIntento = cliente.NroIntento == null ? 1 : cliente.NroIntento + 1;
                            cliente.FechaUltimoIntento = fechaActual;
                            cliente.Estado = cliente.NroIntento == 5 ? "3" : cliente.Estado;
                        }

                        clienteBL.ActualizarIntentos(cliente.ClienteId, cliente.Estado, cliente.NroIntento, cliente.FechaUltimoIntento);

                        responseModel.Codigo = CodeEN.Error;
                        responseModel.Mensaje = "Usuario y/o contraseña inválidos";
                        responseModel.Data = null;

                        return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                    }
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "Máximo número de intentos permitidos";
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

        [HttpGet("huffman/{palabra}")]
        [AllowAnonymous]
        public IActionResult ObtenerCodigoHuffman(String palabra)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                Huffman huffman = new Huffman(palabra);

                huffman.EncontrarCaminoCaracteres(huffman.JuntaNodo(huffman.CuentaCaracteres(huffman.InsertarCaracteresPalabra())).First().raiz, "");

                responseModel.Codigo = CodeEN.Success;
                responseModel.Mensaje = "Se obtuvo el código huffman de manera exitosa";
                responseModel.Data = huffman.Convertir(huffman.caminos);

                return Ok(responseModel);
            }
            catch (Exception)
            {

                responseModel.Codigo = CodeEN.Exception;
                responseModel.Mensaje = "Ocurrió una excepción";

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [HttpPost("")]
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

                String contrasenaCodificada = GenerarCodigoHuffman(registroModel.Contrasena.Trim());

                ClienteBL clienteBL = new ClienteBL();
                Cliente cliente = new Cliente
                {
                    Cuenta = registroModel.Cuenta.Trim(),
                    Nombre = registroModel.Nombre.Trim(),
                    Apellido = registroModel.Apellido.Trim(),
                    Contrasena = contrasenaCodificada,
                    Estado = "1"
                };

                Int32 ClienteId = clienteBL.Registrar(cliente);

                if (ClienteId <= 0)
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo registrar su cuenta";
                    responseModel.Data = null;

                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }

                responseModel.Codigo = CodeEN.Success;
                responseModel.Mensaje = "Se registró su cuenta de manera satisfactoria";

                return Ok(responseModel);
            }
            catch (Exception)
            {

                responseModel.Codigo = CodeEN.Exception;
                responseModel.Mensaje = "Ocurrió una excepción";

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        private String GenerarCodigoHuffman(String palabra)
        {
            Huffman huffman = new Huffman(palabra);

            List<Arbol> lstCaracteresContados = huffman.CuentaCaracteres(huffman.InsertarCaracteresPalabra());
            List<Arbol> arbolJuntado = huffman.JuntaNodo(lstCaracteresContados);

            huffman.EncontrarCaminoCaracteres(arbolJuntado.First().raiz, "");

            return huffman.Convertir(huffman.caminos) + huffman.ConvertPalabraASCII();
        }

        private string GenerarToken(Cliente cliente)
        {
            var llaveSecreta = configuration.GetValue<String>("JWT:SecretKey");
            var llave = Encoding.ASCII.GetBytes(llaveSecreta);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, cliente.ClienteId.ToString()),
                new Claim(ClaimTypes.Email, cliente.Cuenta)
            };

            var descripcionToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var manejadorToken = new JwtSecurityTokenHandler();

            var creadorToken = manejadorToken.CreateToken(descripcionToken);

            return manejadorToken.WriteToken(creadorToken);

        }
    }
}
