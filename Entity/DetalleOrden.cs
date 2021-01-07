using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class DetalleOrden
    {
        public Int32 DetalleOrdenId { get; set; }
        public Int32 ProductoId { get; set; }
        public Int32 Cantidad { get; set; }
        public Int32 OrdenId { get; set; }
    }
}
