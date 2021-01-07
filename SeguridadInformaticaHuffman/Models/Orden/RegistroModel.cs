using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Models.Orden
{
    public class RegistroModel
    {
        public Int32 ClienteTarjetaId { get; set; }

        public Tuple<Boolean, String> Validate()
        {
            Boolean Valid = true;
            String Mensaje = "";

            if(ClienteTarjetaId <= 0)
            {
                Valid = false;
                Mensaje = "Ingrese su tarjeta para el pago";
            }

            return Tuple.Create(Valid, Mensaje);
        }
    }
}
