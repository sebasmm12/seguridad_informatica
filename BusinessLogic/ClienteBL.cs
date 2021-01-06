using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ClienteBL
    {
        public Cliente Consultar(String Cuenta)
        {
            Cliente cliente;

            try
            {
                ClienteDA clienteDA = new ClienteDA();
                cliente = clienteDA.Consultar(Cuenta);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return cliente;
        }

        public Boolean ActualizarIntentos(Int32 ClienteId, String Estado, Int32? NroIntento, DateTime? FechaUltimoIntento)
        {
            Boolean actualizado;

            try
            {
                ClienteDA clienteDA = new ClienteDA();
                actualizado = clienteDA.ActualizarIntentos(ClienteId, Estado, NroIntento, FechaUltimoIntento);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return actualizado;
        }

        public Int32 Registrar(Cliente cliente)
        {
            Int32 ClienteId;

            try
            {
                ClienteDA clienteDA = new ClienteDA();
                ClienteId = clienteDA.Registrar(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ClienteId;
        }

    }
}
