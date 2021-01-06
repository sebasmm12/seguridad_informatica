using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Helpers.Huffman
{
    public class Arbol
    {
        public Nodo raiz { get; set; }

        public Arbol()
        {
            raiz = null;
        }

        public Arbol(Nodo raiz)
        {
            this.raiz = raiz;
        }

        public void Insertar(Char letra, Int32 cantidad)
        {
            Nodo nuevo = new Nodo(letra, cantidad);

            if (raiz == null)
            {
                raiz = nuevo;
            }
            else
            {
                Nodo aux = raiz;
                Nodo ant = null;

                while (aux != null)
                {
                    ant = aux;

                    if (cantidad <= aux.Cantidad)
                    {
                        aux = aux.Izquierdo;
                    }
                    else
                    {
                        aux = aux.Derecho;
                    }
                }

                if (cantidad <= ant.Cantidad)
                {
                    ant.Izquierdo = nuevo;
                }
                else
                {
                    ant.Derecho = nuevo;
                }

            }

        }

        public void Ordenar(Nodo nodo)
        {
            if (nodo != null)
            {
                Ordenar(nodo.Izquierdo);
                Ordenar(nodo.Derecho);
            }
        }
    }
}
