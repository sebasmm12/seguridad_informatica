using BusinessLogic;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeguridadInformaticaHuffman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Controllers
{
    [Route("producto")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        [HttpGet("{ProductoId}")]
        public IActionResult Consultar(Int32? ProductoId)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if (ProductoId == null || ProductoId <= 0)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                ProductoBL productoBL = new ProductoBL();

                List<ProductoAux> lstProductoAuxes = productoBL.Consultar(ProductoId.GetValueOrDefault());


                List<Genero> Generos = new List<Genero>();

                Producto producto = new Producto();
                producto.Requisitos = new List<Requisito>();
                producto.Fotos = new List<ProductoFoto>();
                producto.Portada = new ProductoFoto();

                if (lstProductoAuxes != null)
                {
                    if (lstProductoAuxes.Count > 0)
                    {
                        lstProductoAuxes.ForEach(productoAux =>
                        {
                            producto.ProductoId = productoAux.ProductoId;
                            producto.Nombre = productoAux.Nombre;
                            producto.DescripcionCorta = productoAux.DescripcionCorta;
                            producto.DescripcionLarga = productoAux.DescripcionLarga;
                            producto.FechaLanzamiento = productoAux.FechaLanzamiento;
                            producto.Compania = productoAux.Compania;
                            producto.Precio = productoAux.Precio;
                            producto.EdadMinima = productoAux.EdadMinima;

                            if (Generos.Count > 0)
                            {
                                if (!Generos.Exists(x => x.GeneroId == productoAux.Genero.GeneroId))
                                {
                                    Generos.Add(productoAux.Genero);
                                }
                            }
                            else
                            {
                                Generos.Add(productoAux.Genero);
                            }

                            if (producto.Requisitos.Count > 0)
                            {
                                if (!producto.Requisitos.Exists(x => x.RequisitoId == productoAux.Requisito.RequisitoId))
                                {
                                    producto.Requisitos.Add(productoAux.Requisito);
                                }
                            }
                            else
                            {
                                producto.Requisitos.Add(productoAux.Requisito);
                            }

                            if (producto.Fotos.Count > 0)
                            {
                                if ((!producto.Fotos.Exists(x => x.ProductoFotoId == productoAux.ProductoFoto.ProductoFotoId)) && productoAux.ProductoFoto.Tipo != "2")
                                {
                                    producto.Fotos.Add(productoAux.ProductoFoto);
                                }
                            }
                            else
                            {
                                if (productoAux.ProductoFoto.Tipo != "2")
                                {
                                    producto.Fotos.Add(productoAux.ProductoFoto);
                                }

                            }

                            if (productoAux.ProductoFoto.Tipo == "2")
                            {
                                producto.Portada = productoAux.ProductoFoto;
                            }

                        });
                    }

                    producto.Generos = Generos.Select(x => x.Nombre).ToList().Aggregate((i, j) => i + "," + j);

                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se consulta la información del producto de manera satisfactoria";
                    responseModel.Data = producto;

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo obtener la información del producto";

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

        [HttpGet("plataforma/{PlataformaId}")]
        public IActionResult ListarPorPlataforma(Int32? PlataformaId)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                if (PlataformaId == null || PlataformaId <= 0)
                {
                    responseModel.Codigo = CodeEN.Warning;
                    responseModel.Mensaje = "Datos inválidos";

                    return BadRequest(responseModel);
                }

                ProductoBL productoBL = new ProductoBL();

                List<ProductoPlataformaAux> lstproductoPlataformaAuxes = productoBL.ListarPorPlataforma(PlataformaId.GetValueOrDefault());

                if (lstproductoPlataformaAuxes != null)
                {
                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se listo los productos por plataforma de manera satisfactoria";
                    responseModel.Data = lstproductoPlataformaAuxes;

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo consultar los productos por plataforma";

                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
            catch (Exception)
            {

                responseModel.Codigo = CodeEN.Exception;
                responseModel.Mensaje = "Ocurrió una excepción";

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [HttpGet("carousel")]
        public IActionResult ListarCarousel()
        {

            ResponseModel responseModel = new ResponseModel();

            try
            {
                ProductoBL productoBL = new ProductoBL();

                List<ProductoCarousel> lstproductoCarousels = productoBL.ListarCarousel();

                if (lstproductoCarousels != null)
                {
                    if (lstproductoCarousels.Count > 0)
                    {
                        lstproductoCarousels = lstproductoCarousels.GroupBy(pc => pc.ProductoId)
                                                .Select(x => new ProductoCarousel
                                                {
                                                    ProductoId = x.Key,
                                                    GeneroNombre = String.Join(",", x.Select(gn => gn.GeneroNombre)),
                                                    Compania = x.First().Compania,
                                                    Nombre = x.First().Nombre,
                                                    EdadMinima = x.First().EdadMinima,
                                                    Lanzamiento = x.First().Lanzamiento,
                                                    Url = x.First().Url

                                                }).ToList().Take(5).ToList();
                    }

                    responseModel.Codigo = CodeEN.Success;
                    responseModel.Mensaje = "Se listo los productos para el carousel de manera satisfactoria";
                    responseModel.Data = lstproductoCarousels;

                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Codigo = CodeEN.Error;
                    responseModel.Mensaje = "No se pudo obtener los productos para el carousel";

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
