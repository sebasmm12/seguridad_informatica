using SeguridadInformaticaHuffman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Models.ClienteTarjeta
{
    public class RegistroModel
    {
        public String NumeroTarjeta { get; set; }
        public String Propietario { get; set; }
        public String CVC { get; set; }
        public String FechaExpiracion { get; set; }

        public Tuple<Boolean, String> Validate()
        {
            Boolean Valid = true;
            String Mensaje = "";

            if (String.IsNullOrEmpty(NumeroTarjeta))
            {
                Valid = false;
                Mensaje = "Ingrese el número de tarjeta";
            }
            else if (NumeroTarjeta.Trim().Split(" ").Length != 4)
            {
                Valid = false;
                Mensaje = "El número de tarjeta no tiene el formato válido";
            }
            else if (NumeroTarjeta.Trim().Split(" ").Any(x => !InputValidation.IsOnlyNumbers(x)))
            {
                Valid = false;
                Mensaje = "El número de tarjeta contiene caracteres que no son dígitos";
            }
            else if (NumeroTarjeta.Trim().Split(" ").Any(x => x.Length != 4))
            {
                Valid = false;
                Mensaje = "El número de tarjeta no cuenta con los 16 dígitos";
            }
            else if (String.IsNullOrEmpty(Propietario))
            {
                Valid = false;
                Mensaje = "Ingrese el propietario";
            }
            else if (String.IsNullOrEmpty(CVC))
            {
                Valid = false;
                Mensaje = "Ingrese el código de seguridad";
            }
            else if (!InputValidation.IsOnlyNumbers(CVC))
            {
                Valid = false;
                Mensaje = "El código de seguridad debe contener solamente dígitos";
            }
            else if (String.IsNullOrEmpty(FechaExpiracion))
            {
                Valid = false;
                Mensaje = "Ingrese la fecha de expiración";
            }
            else if (FechaExpiracion.Split("/").Length != 2)
            {
                Valid = false;
                Mensaje = "Fecha de expiración con formato inválido";
            }
            else if (FechaExpiracion.Split("/").Any(x => !InputValidation.IsOnlyNumbers(x)))
            {
                Valid = false;
                Mensaje = "Fecha de expiración contiene caracteres no numéricos";
            }
            else if (Convert.ToInt32(FechaExpiracion.Split("/")[0]) <= 0 || Convert.ToInt32(FechaExpiracion.Split("/")[0]) > 12)
            {
                Valid = false;
                Mensaje = "El mes de la fecha de expiración debe estar entre 01 a 12";
            }
            else if ((Convert.ToInt32(FechaExpiracion.Split("/")[1]) < Convert.ToInt32(DateTime.UtcNow.AddHours(-5).Year.ToString().Substring(2, 2))) ||
                     Convert.ToInt32(FechaExpiracion.Split("/")[1]) > Convert.ToInt32(DateTime.UtcNow.AddHours(-5).AddYears(5).Year.ToString().Substring(2, 2)))
            {
                Valid = false;
                Mensaje = $"El año de la fecha de expiración debe entre entre {DateTime.UtcNow.AddHours(-5).Year} y {DateTime.UtcNow.AddHours(-5).AddYears(5).Year}";
            }

            return Tuple.Create(Valid, Mensaje);
        }
    }
}
