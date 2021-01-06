using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class ClienteTarjetaDA : BaseDA
    {
        public Int32 Registrar(ClienteTarjeta clienteTarjeta)
        {
            Int32 ClienteTarjetaId = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_ClienteTarjeta_Registrar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ClienteId", clienteTarjeta.ClienteId);
                    cmd.Parameters.AddWithValue("@Descripcion", clienteTarjeta.Descripcion);

                    cnx.Open();

                    ClienteTarjetaId = Int32.Parse(cmd.ExecuteScalar().ToString());

                    if (ClienteTarjetaId <= 0)
                    {
                        ClienteTarjetaId = -1;
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ClienteTarjetaId;
        }

        public List<ClienteTarjeta> Listar(Int32 ClienteId)
        {
            List<ClienteTarjeta> lstClienteTarjetas = null;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_ClienteTarjeta_Listar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ClienteId", ClienteId);

                    cnx.Open();

                    IDataReader dr = cmd.ExecuteReader();

                    using (dr)
                    {
                        lstClienteTarjetas = new List<ClienteTarjeta>();

                        while (dr.Read())
                        {
                            ClienteTarjeta clienteTarjeta = new ClienteTarjeta();

                            clienteTarjeta.ClienteTarjetaId = dr.GetInt32(dr.GetOrdinal("ClienteTarjetaId"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ClienteId")))
                            {
                                clienteTarjeta.ClienteId = dr.GetInt32(dr.GetOrdinal("ClienteId"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Descripcion")))
                            {
                                clienteTarjeta.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                            }

                            lstClienteTarjetas.Add(clienteTarjeta);
                        }
                    }

                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstClienteTarjetas;
        }

        public Boolean Eliminar(Int32 ClienteTarjetaId)
        {
            Boolean eliminado = false;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_ClienteTarjeta_Eliminar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ClienteTarjetaId", ClienteTarjetaId);

                    cnx.Open();

                    if (Int32.Parse(cmd.ExecuteScalar().ToString()) > 0)
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
