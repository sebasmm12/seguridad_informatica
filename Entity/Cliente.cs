using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Cliente
    {
        public Int32 ClienteId { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Contrasena { get; set; }
        public Int32? NroIntento { get; set; }
        public String Cuenta { get; set; }
        public DateTime? FechaUltimoIntento { get; set; }
        public String Estado { get; set; }

        //
        public Carrito Carrito { get; set; }
    }
}
