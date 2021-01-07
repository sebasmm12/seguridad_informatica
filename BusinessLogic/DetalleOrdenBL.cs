using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DetalleOrdenBL
    {
        public Int32 Registrar(DetalleOrden detalleOrden)
        {
            Int32 DetalleOrdenId;

            try
            {
                DetalleOrdenDA detalleOrdenDA = new DetalleOrdenDA();
                DetalleOrdenId = detalleOrdenDA.Registrar(detalleOrden);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return DetalleOrdenId;
        }
    }
}
