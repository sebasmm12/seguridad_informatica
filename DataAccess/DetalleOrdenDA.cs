using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace DataAccess
{
    public class DetalleOrdenDA: BaseDA
    {
        public Int32 Registrar(DetalleOrden detalleOrden)
        {
            Int32 detalleOrdenId = 0;
 
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_DetalleOrden_Registrar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ProductoId", detalleOrden.ProductoId);
                    cmd.Parameters.AddWithValue("@Cantidad", detalleOrden.Cantidad);
                    cmd.Parameters.AddWithValue("@OrdenId", detalleOrden.OrdenId);

                    cnx.Open();

                    detalleOrdenId = Int32.Parse(cmd.ExecuteScalar().ToString());

                    if(detalleOrdenId <= 0)
                    {
                        detalleOrdenId = -1;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return detalleOrdenId;
        }
    }
}
