using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Helpers.Huffman
{
    public class Nodo
    {
        public Int32 Cantidad { get; set; }
        public Char Letra { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(Char Letra, Int32 Cantidad)
        {
            this.Cantidad = Cantidad;
            this.Letra = Letra;
        }
    }
}
