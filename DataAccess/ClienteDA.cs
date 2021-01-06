using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class ClienteDA : BaseDA
    {
        public Cliente Consultar(String Cuenta)
        {
            Cliente cliente = null;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Cliente_Consultar_PorCuenta", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@Cuenta", Cuenta);

                    cnx.Open();

                    IDataReader dr = cmd.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {
                            cliente = new Cliente();

                            cliente.ClienteId = dr.GetInt32(dr.GetOrdinal("ClienteId"));

                            if (!dr.IsDBNull(dr.GetOrdinal("Nombre")))
                            {
                                cliente.Nombre = dr.GetString(dr.GetOrdinal("Nombre"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Apellido")))
                            {
                                cliente.Apellido = dr.GetString(dr.GetOrdinal("Apellido"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Cuenta")))
                            {
                                cliente.Cuenta = dr.GetString(dr.GetOrdinal("Cuenta"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Contrasena")))
                            {
                                cliente.Contrasena = dr.GetString(dr.GetOrdinal("Contrasena"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("NroIntento")))
                            {
                                cliente.NroIntento = dr.GetInt32(dr.GetOrdinal("NroIntento"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("FechaUltimoIntento")))
                            {
                                cliente.FechaUltimoIntento = dr.GetDateTime(dr.GetOrdinal("FechaUltimoIntento"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Estado")))
                            {
                                cliente.Estado = dr.GetString(dr.GetOrdinal("Estado"));
                            }
                        }
                    }

                    cnx.Close();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return cliente;
        }

        public Boolean ActualizarIntentos(Int32 ClienteId, String Estado, Int32? NroIntento, DateTime? FechaUltimoIntento)
        {
            Boolean actualizado = false;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Cliente_Actualizar_Intentos", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@ClienteId", ClienteId);
                    cmd.Parameters.AddWithValue("@NroIntento", NroIntento);
                    cmd.Parameters.AddWithValue("@FechaUltimoIntento", FechaUltimoIntento);
                    cmd.Parameters.AddWithValue("@Estado", Estado);


                    cnx.Open();

                    if (Int32.Parse(cmd.ExecuteScalar().ToString()) > 0)
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

        public Int32 Registrar(Cliente cliente)
        {
            Int32 ClienteId = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.USP_Cliente_Registrar", cnx)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Contrasena", cliente.Contrasena);
                    cmd.Parameters.AddWithValue("@Cuenta", cliente.Cuenta);
                    cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

                    cnx.Open();

                    ClienteId = Int32.Parse(cmd.ExecuteScalar().ToString());

                    if (ClienteId <= 0)
                    {
                        ClienteId = -1;
                    }


                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ClienteId;
        }
    }
}
