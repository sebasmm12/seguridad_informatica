using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class CarritoBL
    {
        public Int32 Registrar(Int32 ClienteId)
        {
            Int32 CarritoId;

            try
            {
                CarritoDA carritoDA = new CarritoDA();
                CarritoId = carritoDA.Registrar(ClienteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CarritoId;
        }

        public Boolean Actualizar(Carrito Carrito)
        {
            Boolean updated;

            try
            {
                CarritoDA carritoDA = new CarritoDA();
                updated = carritoDA.Actualizar(Carrito);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return updated;
        }
    }
}
