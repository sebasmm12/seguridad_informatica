using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entity
{
    public class ClienteTarjeta
    {
        public Int32 ClienteTarjetaId { get; set; }
        public Int32 ClienteId { get; set; }
        [JsonIgnore]
        public String Descripcion { get; set; }
        public Boolean Activo { get; set; }
    }
}
