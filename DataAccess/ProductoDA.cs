using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class ProductoDA : BaseDA
    {
        public List<ProductoAux> Consultar(Int32 ProductoId)
        {
            List<ProductoAux> lstproductoAuxes = null;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Producto_Consultar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ProductoId", ProductoId);

                    cnx.Open();

                    IDataReader dr = cmd.ExecuteReader();

                    using (dr)
                    {
                        lstproductoAuxes = new List<ProductoAux>();

                        while (dr.Read())
                        {
                            ProductoAux productoAux = new ProductoAux();
                            productoAux.Genero = new Genero();
                            productoAux.Requisito = new Requisito();
                            productoAux.ProductoFoto = new ProductoFoto();

                            if (!dr.IsDBNull(dr.GetOrdinal("ProductoId")))
                            {
                                productoAux.ProductoId = dr.GetInt32(dr.GetOrdinal("ProductoId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ProductoNombre")))
                            {
                                productoAux.Nombre = dr.GetString(dr.GetOrdinal("ProductoNombre"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("DescripcionCorta")))
                            {
                                productoAux.DescripcionCorta = dr.GetString(dr.GetOrdinal("DescripcionCorta"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("DescripcionLarga")))
                            {
                                productoAux.DescripcionLarga = dr.GetString(dr.GetOrdinal("DescripcionLarga"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("FechaLanzamiento")))
                            {
                                productoAux.FechaLanzamiento = dr.GetDateTime(dr.GetOrdinal("FechaLanzamiento"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Compania")))
                            {
                                productoAux.Compania = dr.GetString(dr.GetOrdinal("Compania"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Precio")))
                            {
                                productoAux.Precio = dr.GetDouble(dr.GetOrdinal("Precio"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("EdadMinima")))
                            {
                                productoAux.EdadMinima = dr.GetString(dr.GetOrdinal("EdadMinima"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("GeneroId")))
                            {
                                productoAux.Genero.GeneroId = dr.GetInt32(dr.GetOrdinal("GeneroId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("GeneroNombre")))
                            {
                                productoAux.Genero.Nombre = dr.GetString(dr.GetOrdinal("GeneroNombre"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("RequisitoId")))
                            {
                                productoAux.Requisito.RequisitoId = dr.GetInt32(dr.GetOrdinal("RequisitoId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TipoRequisito")))
                            {
                                productoAux.Requisito.Tipo = dr.GetString(dr.GetOrdinal("TipoRequisito"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Descripcion")))
                            {
                                productoAux.Requisito.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ProductoFotoId")))
                            {
                                productoAux.ProductoFoto.ProductoFotoId = dr.GetInt32(dr.GetOrdinal("ProductoFotoId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Url")))
                            {
                                productoAux.ProductoFoto.Url = dr.GetString(dr.GetOrdinal("Url"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TipoFoto")))
                            {
                                productoAux.ProductoFoto.Tipo = dr.GetString(dr.GetOrdinal("TipoFoto"));
                            }

                            lstproductoAuxes.Add(productoAux);

                        }
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstproductoAuxes;
        }

        public List<ProductoPlataformaAux> ListarPorPlataforma(Int32 PlataformaId)
        {
            List<ProductoPlataformaAux> lstProductoPlataformaAuxes;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Producto_Listar_PorPlataforma", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@PlataformaId", PlataformaId);

                    cnx.Open();
                    IDataReader dr = cmd.ExecuteReader();

                    using (dr)
                    {
                        lstProductoPlataformaAuxes = new List<ProductoPlataformaAux>();

                        while (dr.Read())
                        {
                            ProductoPlataformaAux productoPlataformaAux = new ProductoPlataformaAux();

                            if (!dr.IsDBNull(dr.GetOrdinal("ProductoId")))
                            {
                                productoPlataformaAux.ProductoId = dr.GetInt32(dr.GetOrdinal("ProductoId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Nombre")))
                            {
                                productoPlataformaAux.Nombre = dr.GetString(dr.GetOrdinal("Nombre"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Url")))
                            {
                                productoPlataformaAux.Url = dr.GetString(dr.GetOrdinal("Url"));
                            }

                            lstProductoPlataformaAuxes.Add(productoPlataformaAux);
                        }
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstProductoPlataformaAuxes;
        }

        public List<ProductoCarousel> ListarCarousel()
        {
            List<ProductoCarousel> lstProductoCarousels;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Producto_Listar_Carousel", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cnx.Open();
                    IDataReader dr = cmd.ExecuteReader();

                    using (dr)
                    {
                        lstProductoCarousels = new List<ProductoCarousel>();

                        while (dr.Read())
                        {
                            ProductoCarousel productoCarousel = new ProductoCarousel();

                            if (!dr.IsDBNull(dr.GetOrdinal("ProductoId")))
                            {
                                productoCarousel.ProductoId = dr.GetInt32(dr.GetOrdinal("ProductoId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ProductoNombre")))
                            {
                                productoCarousel.Nombre = dr.GetString(dr.GetOrdinal("ProductoNombre"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("EdadMinima")))
                            {
                                productoCarousel.EdadMinima = dr.GetString(dr.GetOrdinal("EdadMinima"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("FechaLanzamiento")))
                            {
                                productoCarousel.Lanzamiento = dr.GetDateTime(dr.GetOrdinal("FechaLanzamiento")).Year;
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Url")))
                            {
                                productoCarousel.Url = dr.GetString(dr.GetOrdinal("Url"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("GeneroNombre")))
                            {
                                productoCarousel.GeneroNombre = dr.GetString(dr.GetOrdinal("GeneroNombre"));
                            }

                            lstProductoCarousels.Add(productoCarousel);
                        }
                    }


                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstProductoCarousels;
        }
    }
}
