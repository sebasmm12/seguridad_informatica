using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Entity;

namespace DataAccess
{
    public class OrdenDA: BaseDA
    {
        public Int32 Registrar(Orden Orden)
        {
            Int32 OrdenId = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Orden_Registrar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@PrecioTotal", Orden.PrecioTotal);
                    cmd.Parameters.AddWithValue("@ClienteTarjetaId", Orden.ClienteTarjetaId);


                    cnx.Open();

                    OrdenId = Int32.Parse(cmd.ExecuteScalar().ToString());

                    if(OrdenId <= 0)
                    {
                        OrdenId = -1;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return OrdenId;
        }
    }
}
