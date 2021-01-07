using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DetalleCarritoBL: BaseDA
    {
        public Int32 Registrar(DetalleCarrito detalleCarrito)
        {
            Int32 DetalleCarritoID;

            try
            {
                DetalleCarritoDA detalleCarritoDA = new DetalleCarritoDA();
                DetalleCarritoID = detalleCarritoDA.Registrar(detalleCarrito);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return DetalleCarritoID;
        }

        public Boolean Eliminar(Int32 DetalleCarritoId)
        {
            Boolean deleted;

            try
            {
                DetalleCarritoDA detalleCarritoDA = new DetalleCarritoDA();
                deleted = detalleCarritoDA.Eliminar(DetalleCarritoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return deleted;
        }

        public List<DetalleCarritoProducto> ListarPorCliente(Int32 CarritoId)
        {
            List<DetalleCarritoProducto> lstDetalleCarritoProductos;

            try
            {
                DetalleCarritoDA detalleCarritoDA = new DetalleCarritoDA();
                lstDetalleCarritoProductos = detalleCarritoDA.ListarPorCliente(CarritoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstDetalleCarritoProductos;
        }

        public Boolean EliminarPorCarrito(Int32 CarritoId)
        {
            Boolean eliminado;

            try
            {
                DetalleCarritoDA detalleCarritoDA = new DetalleCarritoDA();
                eliminado = detalleCarritoDA.EliminarPorCarrito(CarritoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return eliminado;
        }
    }
}
