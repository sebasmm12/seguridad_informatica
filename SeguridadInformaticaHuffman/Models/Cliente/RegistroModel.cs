using SeguridadInformaticaHuffman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Models.Cliente
{
    public class RegistroModel
    {
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Contrasena { get; set; }
        public String Cuenta { get; set; }

        public Tuple<Boolean, String> Validate()
        {
            Boolean Valid = true;
            String Message = "";

            if (String.IsNullOrEmpty(Nombre))
            {
                Valid = false;
                Message = "Ingrese el nombre";
            }
            else if (Nombre.Length > 100)
            {
                Valid = false;
                Message = "El nombre no debe tener más de 100 caracteres";
            }
            else if (String.IsNullOrEmpty(Apellido))
            {
                Valid = false;
                Message = "Ingrese el apellido";
            }
            else if (Apellido.Length > 100)
            {
                Valid = false;
                Message = "El apellido no debe tener más de 100 caracteres";
            }
            else if (String.IsNullOrEmpty(Contrasena))
            {
                Valid = false;
                Message = "Ingrese la contraseña";
            }
            else if (InputValidation.HasSpecialCharacters(Contrasena))
            {
                Valid = false;
                Message = "La contraseña no debe tener caracteres especiales";
            }
            else if (Contrasena.Length > 100)
            {
                Valid = false;
                Message = "La contraseña no debe tener más de 100 caracteres";
            }
            else if (String.IsNullOrEmpty(Cuenta))
            {
                Valid = false;
                Message = "Ingrese la cuenta";
            }
            else if (Cuenta.Length > 100)
            {
                Valid = false;
                Message = "La cuenta debe tener más de 100 caracteres";
            }

            return Tuple.Create(Valid, Message);
        }
    }
}
