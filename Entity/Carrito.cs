using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Carrito
    {
        public Int32 CarritoId { get; set; }
        public Int32 ClienteId { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
    }
}
