using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Models.DetalleCarrito
{
    public class RegistroModel
    {

        public Int32 Cantidad { get; set; }
        public Int32 ProductoId { get; set; }

        public Tuple<Boolean, String> Validate()
        {
            Boolean Valid = true;
            String Mensaje = "";

            if (Cantidad <= 0)
            {
                Valid = false;
                Mensaje = "La cantidad no puede ser menor a 0";
            }
            else if (ProductoId <= 0)
            {
                Valid = false;
                Mensaje = "Selecciona un producto";
            }

            return Tuple.Create(Valid, Mensaje);
        }
    }
}