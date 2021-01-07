using BusinessLogic;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeguridadInformaticaHuffman.Models;
using SeguridadInformaticaHuffman.Models.Orden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Controllers
{
    [Route("orden")]
    [Authorize]
    public class OrdenController: ControllerBase
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

                OrdenBL ordenBL = new OrdenBL();
                DetalleOrdenBL detalleOrdenBL = new DetalleOrdenBL();
                DetalleCarritoBL detalleCarritoBL = new DetalleCarritoBL();

                String carritoId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

                if(String.IsNullOrEmpty(carritoId))
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "Sesión expirada";

                    return Unauthorized(responseModel);
                }

                Int32 CarritoId = Int32.Parse(carritoId);

                List<DetalleCarritoProducto> lstDetalleCarritoProductos = detalleCarritoBL.ListarPorCliente(CarritoId);

                if(lstDetalleCarritoProductos != null && lstDetalleCarritoProductos.Count > 0)
                {
                    Orden orden = new Orden
                    {
                        ClienteTarjetaId = registroModel.ClienteTarjetaId,
                        PrecioTotal = lstDetalleCarritoProductos.Sum(x => x.PrecioTotal)
                    };

                    Int32 OrdenId = ordenBL.Registrar(orden);

                    if(OrdenId > 0)
                    {
                        foreach (var detalleCarritoProducto in lstDetalleCarritoProductos)
                        {
                            DetalleOrden detalleOrden = new DetalleOrden
                            {
                                OrdenId = OrdenId,
                                ProductoId = detalleCarritoProducto.ProductoId,
                                Cantidad = detalleCarritoProducto.Cantidad
                            };

                            detalleOrdenBL.Registrar(detalleOrden);
                        }

                        detalleCarritoBL.EliminarPorCarrito(CarritoId);

                        responseModel.Codigo = CodeEN.Success;
                        responseModel.Mensaje = "Se realizó la compra de los productos con éxito";

                        return Ok(responseModel);
                    }
                    else
                    {
                        responseModel.Codigo = CodeEN.Error;
                        responseModel.Mensaje = "No se pudo realizar la compra de los productos";

                        return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                    }

                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No existe ningún producto en el carrito de compras";

                    return NotFound(responseModel);
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
