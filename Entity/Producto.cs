using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ProductoEN
    {
        public Int32 ProductoId { get; set; }
        public String Nombre { get; set; }
        public String DescripcionCorta { get; set; }
        public String DescripcionLarga { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public String Compania { get; set; }
        public Double Precio { get; set; }
        public String EdadMinima { get; set; }
        public String DireccionUrl { get; set; }
        public Int32 Rating { get; set; }
    }

    public class ProductoAux : ProductoEN
    {
        public Genero Genero { get; set; }
        public Requisito Requisito { get; set; }
        public ProductoFoto ProductoFoto { get; set; }
    }

    public class Producto : ProductoEN
    {
        public String Generos { get; set; }
        public List<Requisito> Requisitos { get; set; }
        public List<ProductoFoto> Fotos { get; set; }
        public ProductoFoto Portada { get; set; }
    }

    public class ProductoPlataformaAux
    {
        public Int32 ProductoId { get; set; }
        public String Nombre { get; set; }
        public String Url { get; set; }
    }

    public class ProductoCarousel
    {
        public Int32 ProductoId { get; set; }
        public String Compania { get; set; }
        public String Nombre { get; set; }
        public String EdadMinima { get; set; }
        public Int32 Lanzamiento { get; set; }
        public String Url { get; set; }
        public String GeneroNombre { get; set; }
    }
}
