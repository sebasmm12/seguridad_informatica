using SeguridadInformaticaHuffman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Models.Cliente
{
    public class LoginModel
    {
        public String Cuenta { get; set; }
        public String Contrasena { get; set; }

        public Tuple<Boolean, String> Validate()
        {
            Boolean Valid = true;
            String Mensaje = "";

            if (String.IsNullOrEmpty(Cuenta))
            {
                Valid = false;
                Mensaje = "Ingrese la cuenta";
            }
            else if (!InputValidation.IsEmail(Cuenta))
            {
                Valid = false;
                Mensaje = "Formato de correo para la cuenta inválido";
            }
            else if (Cuenta.Length > 100)
            {
                Valid = false;
                Mensaje = "La cuenta solo admite 100 caracteres como máximo";
            }
            else if (String.IsNullOrEmpty(Contrasena))
            {
                Valid = false;
                Mensaje = "Ingrese la contraseña";
            }
            else if (InputValidation.HasSpecialCharacters(Contrasena))
            {
                Valid = false;
                Mensaje = "La contraseña no admite caracteres especiales";
            }
            else if (Contrasena.Length > 100)
            {
                Valid = false;
                Mensaje = "La contraseña solo admite 100 caracteres como máximo";
            }

            return Tuple.Create(Valid, Mensaje);
        }
    }
}
