using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Entity;

namespace DataAccess
{
    public class CarritoDA: BaseDA
    {
        public Int32 Registrar(Int32 ClienteId)
        {
            Int32 CarritoId = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Carrito_Registrar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ClienteId", ClienteId);

                    cnx.Open();

                    CarritoId = Int32.Parse(cmd.ExecuteScalar().ToString());

                    if(CarritoId <= 0)
                    {
                        CarritoId = -1;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CarritoId;
        }

        public Boolean Actualizar(Carrito Carrito)
        {
            Boolean actualizado = false;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Carrito_Actualizar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ClienteId", Carrito.ClienteId);
                    cmd.Parameters.AddWithValue("@FechaUltimaActualizacion", Carrito.FechaUltimaActualizacion);

                    cnx.Open();

                    if(Int32.Parse(cmd.ExecuteScalar().ToString()) > 0)
                    {
                        actualizado = true;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return actualizado;
        }
    }
}
