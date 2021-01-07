using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class OrdenBL
    {
        public Int32 Registrar(Orden Orden)
        {
            Int32 OrdenId;

            try
            {
                OrdenDA ordenDA = new OrdenDA();
                OrdenId = ordenDA.Registrar(Orden);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return OrdenId;
        }
    }
}
