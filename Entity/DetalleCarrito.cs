using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class DetalleCarrito
    {
        public Int32 DetalleCarritoId { get; set; }
        public Int32 CarritoId { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public Int32 Cantidad { get; set; }
        public Int32 ProductoId { get; set; }

    }

    public class DetalleCarritoProducto
    {
        public Int32 DetalleCarritoId { get; set; }
        public Int32 ProductoId { get; set; }
        public String Nombre { get; set; }
        public String Compania { get; set; }
        public Double PrecioTotal { get; set; }
        public String Foto { get; set; }
        public Int32 Cantidad { get; set; }
    }
}
