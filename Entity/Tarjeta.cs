using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Tarjeta
    {
        public String NumeroTarjeta { get; set; }
        public String Propietario { get; set; }
        public String CVC { get; set; }
        public String FechaExpiracion { get; set; }
    }

    public class TarjetaPago : Tarjeta
    {
        public Int32 ClienteTarjetaId { get; set; }
        public Int32 ClienteId { get; set; }
    }
}
