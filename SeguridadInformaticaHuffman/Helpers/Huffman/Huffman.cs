using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Helpers.Huffman
{
    public class Huffman
    {
        public String palabra { get; set; }
        public String resultado { get; set; }
        public List<Camino> caminos { get; set; }

        public Huffman(String palabra)
        {
            this.palabra = palabra;
            caminos = new List<Camino>();
            resultado = "";
        }

        public List<Char> InsertarCaracteresPalabra()
        {
            List<Char> lstCaracteres = palabra.Select(x => x).ToList();

            return lstCaracteres;
        }

        public List<Arbol> CuentaCaracteres(List<Char> lstCaracteres)
        {
            List<Arbol> lstArboles = (from caracter in lstCaracteres
                                      group caracter by caracter into c
                                      orderby c.Count() ascending
                                      select new Arbol(new Nodo(c.Key, c.Count()))).ToList();

            return lstArboles;

        }

        public List<Arbol> JuntaNodo(List<Arbol> lstArbolCaracteres)
        {
            Arbol arbolAux;

            while (lstArbolCaracteres.Count > 1)
            {
                arbolAux = lstArbolCaracteres.ElementAt(1);

                Arbol arbolUnido = new Arbol(UneNodos(lstArbolCaracteres.First().raiz, arbolAux.raiz));

                lstArbolCaracteres.Add(arbolUnido);
                lstArbolCaracteres.RemoveRange(0, 2);

            }

            return lstArbolCaracteres;
        }

        public Nodo UneNodos(Nodo primerNodo, Nodo segundoNodo)
        {
            Nodo nodoUnido = new Nodo('\u0000', primerNodo.Cantidad + segundoNodo.Cantidad)
            {
                Izquierdo = primerNodo,
                Derecho = segundoNodo
            };

            return nodoUnido;
        }

        public void EncontrarCaminoCaracteres(Nodo nodoArbol, String camino)
        {
            resultado += camino;

            if (nodoArbol != null)
            {
                if (nodoArbol.Letra != '\u0000')
                {
                    caminos.Add(new Camino
                    {
                        Letra = nodoArbol.Letra,
                        Valor = resultado
                    });

                }

                EncontrarCaminoCaracteres(nodoArbol.Izquierdo, "0");

                if (nodoArbol.Izquierdo != null)
                {
                    resultado = resultado.Substring(0, resultado.Length - 1);
                }

                EncontrarCaminoCaracteres(nodoArbol.Derecho, "1");

                if (nodoArbol.Derecho != null)
                {
                    resultado = resultado.Substring(0, resultado.Length - 1);
                }
            }
            else
            {
                resultado = resultado.Substring(0, resultado.Length - 1);
            }

        }

        public String Convertir(List<Camino> caminos)
        {
            String caminoConvertido = "";

            List<Char> lstCaracteres = InsertarCaracteresPalabra();

            lstCaracteres.ForEach(x =>
            {
                caminoConvertido += caminos.Find(y => y.Letra == x).Valor;
            });

            return caminoConvertido;
        }

        public String ConvertPalabraASCII()
        {
            var bytes = Encoding.ASCII.GetBytes(palabra);

            return Convert.ToBase64String(bytes);
        }
    }
}
