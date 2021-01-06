using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ClienteTarjetaBL
    {
        public Int32 Registrar(ClienteTarjeta clienteTarjeta)
        {
            Int32 ClienteTarjetaId;

            try
            {
                ClienteTarjetaDA clienteTarjetaDA = new ClienteTarjetaDA();
                ClienteTarjetaId = clienteTarjetaDA.Registrar(clienteTarjeta);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ClienteTarjetaId;
        }

        public List<ClienteTarjeta> Listar(Int32 ClienteId)
        {
            List<ClienteTarjeta> lstclienteTarjetas;

            try
            {
                ClienteTarjetaDA clienteTarjetaDA = new ClienteTarjetaDA();
                lstclienteTarjetas = clienteTarjetaDA.Listar(ClienteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstclienteTarjetas;
        }

        public Boolean Eliminar(Int32 ClienteTarjetaId)
        {
            Boolean eliminado;

            try
            {
                ClienteTarjetaDA clienteTarjetaDA = new ClienteTarjetaDA();
                eliminado = clienteTarjetaDA.Eliminar(ClienteTarjetaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return eliminado;
        }
    }
}
