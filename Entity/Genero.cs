using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Genero
    {
        public Int32 GeneroId { get; set; }
        public String Nombre { get; set; }

    }

    public class GeneroEN : Genero
    {
        public String Descripcion { get; set; }
    }
}
