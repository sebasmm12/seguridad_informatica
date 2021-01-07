using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Orden
    {
        public Int32 OrdenId { get; set; }
        public Double PrecioTotal { get; set; }
        public Int32 ClienteTarjetaId { get; set; }
    }
}
