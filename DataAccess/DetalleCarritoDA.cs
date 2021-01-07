using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace DataAccess
{
    public class DetalleCarritoDA: BaseDA
    {
       public Int32 Registrar(DetalleCarrito detalleCarrito)
       {
            Int32 DetalleCarritoID = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_DetalleCarrito_Registrar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@CarritoId", detalleCarrito.CarritoId);
                    cmd.Parameters.AddWithValue("@Cantidad", detalleCarrito.Cantidad);
                    cmd.Parameters.AddWithValue("@ProductoId", detalleCarrito.ProductoId);

                    cnx.Open();

                    DetalleCarritoID = Int32.Parse(cmd.ExecuteScalar().ToString());

                    if(DetalleCarritoID <= 0)
                    {
                        DetalleCarritoID = -1;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return DetalleCarritoID;
       }

        public Boolean Eliminar(Int32 DetalleCarritoId)
        {
            Boolean eliminado = false;

            try
            {
                using (SqlConnection  cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_DetalleCarrito_Eliminar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@DetalleCarritoId", DetalleCarritoId);

                    cnx.Open();

                    if(Int32.Parse(cmd.ExecuteScalar().ToString()) > 0)
                    {
                        eliminado = true;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return eliminado;
        }

        public List<DetalleCarritoProducto> ListarPorCliente(Int32 CarritoId)
        {
            List<DetalleCarritoProducto> lstDetalleCarritoProductos = null;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_DetalleCarrito_Listar_PorCliente", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@CarritoId", CarritoId);

                    cnx.Open();
                    IDataReader dr = cmd.ExecuteReader();

                    using (dr)
                    {
                        lstDetalleCarritoProductos = new List<DetalleCarritoProducto>();

                        while (dr.Read())
                        {
                            DetalleCarritoProducto detalleCarritoProducto = new DetalleCarritoProducto();

                            if(!dr.IsDBNull(dr.GetOrdinal("DetalleCarritoId")))
                            {
                                detalleCarritoProducto.DetalleCarritoId = dr.GetInt32(dr.GetOrdinal("DetalleCarritoId"));
                            }
                            if(!dr.IsDBNull(dr.GetOrdinal("ProductoId")))
                            {
                                detalleCarritoProducto.ProductoId = dr.GetInt32(dr.GetOrdinal("ProductoId"));
                            }
                            if(!dr.IsDBNull(dr.GetOrdinal("Compania")))
                            {
                                detalleCarritoProducto.Compania = dr.GetString(dr.GetOrdinal("Compania"));
                            }
                            if(!dr.IsDBNull(dr.GetOrdinal("PrecioTotal")))
                            {
                                detalleCarritoProducto.PrecioTotal = dr.GetDouble(dr.GetOrdinal("PrecioTotal"));
                            }
                            if(!dr.IsDBNull(dr.GetOrdinal("Nombre"))) 
                            {
                                detalleCarritoProducto.Nombre = dr.GetString(dr.GetOrdinal("Nombre"));
                            }
                            if(!dr.IsDBNull(dr.GetOrdinal("Url"))) 
                            {
                                detalleCarritoProducto.Foto = dr.GetString(dr.GetOrdinal("Url"));
                            }
                            if(!dr.IsDBNull(dr.GetOrdinal("Cantidad"))
                            {
                                detalleCarritoProducto.Cantidad = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                            }

                            lstDetalleCarritoProductos.Add(detalleCarritoProducto);
                        }
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstDetalleCarritoProductos;
        }

        public Boolean EliminarPorCarrito(Int32 CarritoId)
        {
            Boolean eliminado = false;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_DetalleCarrito_Eliminar_PorCarrito", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@CarritoId", CarritoId);

                    cnx.Open();

                    if(Int32.Parse(cmd.ExecuteScalar().ToString()) > 0)
                    {
                        eliminado = true;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return eliminado;
        }
    }
}
