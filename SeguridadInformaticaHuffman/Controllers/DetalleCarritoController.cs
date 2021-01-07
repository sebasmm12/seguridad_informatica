using BusinessLogic;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeguridadInformaticaHuffman.Models;
using SeguridadInformaticaHuffman.Models.DetalleCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Controllers
{
    [Route("detalle/carrito")]
    [Authorize]
    public class DetalleCarritoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Registrar([FromBody] RegistroModel registroModel) 
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if(registroModel == null)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                Tuple<Boolean, String> Valid = registroModel.Validate();

                if(!Valid.Item1)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = Valid.Item2;

                    return BadRequest(responseModel);
                }

                DateTime FechaActual = DateTime.UtcNow.AddHours(-5);
                DetalleCarritoBL detalleCarritoBL = new DetalleCarritoBL();

                String carritoId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                Int32 CarritoId = Int32.Parse(carritoId);

                DetalleCarrito detalleCarrito = new DetalleCarrito
                {
                    CarritoId = CarritoId,
                    Cantidad = registroModel.Cantidad,
                    ProductoId = registroModel.ProductoId
                };

                Int32 DetalleCarritoId = detalleCarritoBL.Registrar(detalleCarrito);

                if(DetalleCarritoId > 0)
                {

                    String clienteId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    Int32 ClienteId = Int32.Parse(clienteId);

                    CarritoBL carritoBL = new CarritoBL();

                    Carrito carrito = new Carrito
                    {
                        ClienteId = ClienteId,
                        FechaUltimaActualizacion = FechaActual
                    };

                    carritoBL.Actualizar(carrito);

                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se agregó el producto al carrito de manera satisfactoria";


                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo agregar el producto al carrito";

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

        [HttpGet]
        public IActionResult ListarPorCliente()
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                String clienteId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                
                if(String.IsNullOrEmpty(clienteId))
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Sesión expirada";

                    return Unauthorized(responseModel);
                }

                Int32 ClienteId = Int32.Parse(clienteId);

                DetalleCarritoBL detalleCarritoBL = new DetalleCarritoBL();


                List<DetalleCarritoProducto> lstDetalleCarritoProductos = detalleCarritoBL.ListarPorCliente(ClienteId);

                if(lstDetalleCarritoProductos != null)
                {
                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se listo los productos guardados en el carrito de manera satisfactoria";
                    responseModel.Data = lstDetalleCarritoProductos;

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo listar los productos guardados en el carrito";

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

        [HttpDelete("{DetalleCarritoId}")]
        public IActionResult Eliminar(Int32? DetalleCarritoId)
        {
            ResponseModel responseModel= new ResponseModel();

            try
            {
                if(DetalleCarritoId == null || DetalleCarritoId <= 0)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                DetalleCarritoBL detalleCarritoBL = new DetalleCarritoBL();
                DateTime FechaActual = DateTime.UtcNow.AddHours(-5);

                if (detalleCarritoBL.Eliminar(DetalleCarritoId.GetValueOrDefault()))
                {
                    String clienteId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    Int32 ClienteId = Int32.Parse(clienteId);

                    CarritoBL carritoBL = new CarritoBL();

                    Carrito carrito = new Carrito
                    {
                        ClienteId = ClienteId,
                        FechaUltimaActualizacion = FechaActual
                    };

                    carritoBL.Actualizar(carrito);

                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se eliminó el producto de su carrito de manera satisfactoria";

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo eliminar el producto de su carrito";

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
