using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ProductoFoto
    {
        public Int32 ProductoFotoId { get; set; }
        public String Url { get; set; }
        public String Tipo { get; set; }
    }

    public class ProductoFotoEN : ProductoFoto
    {
        public Int32 ProductoId { get; set; }
    }
}
