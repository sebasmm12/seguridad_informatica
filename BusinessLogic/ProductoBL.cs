using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ProductoBL
    {
        public List<ProductoAux> Consultar(Int32 ProductoId)
        {
            List<ProductoAux> lstproductoAuxes;

            try
            {
                ProductoDA productoDA = new ProductoDA();
                lstproductoAuxes = productoDA.Consultar(ProductoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstproductoAuxes;
        }

        public List<ProductoPlataformaAux> ListarPorPlataforma(Int32 PlataformaId)
        {
            List<ProductoPlataformaAux> lstproductoPlataformaAuxes;

            try
            {
                ProductoDA productoDA = new ProductoDA();
                lstproductoPlataformaAuxes = productoDA.ListarPorPlataforma(PlataformaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstproductoPlataformaAuxes;
        }

        public List<ProductoCarousel> ListarCarousel()
        {
            List<ProductoCarousel> lstProductoCarouseles = new List<ProductoCarousel>();

            try
            {
                ProductoDA productoDA = new ProductoDA();
                lstProductoCarouseles = productoDA.ListarCarousel();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstProductoCarouseles;
        }
    }
}
