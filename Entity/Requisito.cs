using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class RequisitoEN
    {
        public Int32 RequisitoId { get; set; }
        public String Tipo { get; set; }
    }
    public class Requisito : RequisitoEN
    {
        public String Descripcion { get; set; }
    }
}
