using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SeguridadInformaticaHuffman.Helpers
{
    public static class InputValidation
    {
        public static Boolean IsOnlyLetters(String Text)
        {
            return Text.All(Char.IsLetter);
        }

        public static Boolean IsOnlyNumbers(String Text)
        {
            return Text.All(Char.IsDigit);
        }

        public static Boolean IsLettersOrNumbers(String Text)
        {
            return Text.All(Char.IsLetterOrDigit);
        }

        public static Boolean HasSpecialCharacters(String Text, String Characters)
        {
            foreach (Char c in Text)
            {
                if (Characters.ToLower().Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean HasSpecialCharacters(String Text)
        {
            String Characters = "@°¡!\"#$%&/()='¿?{}[];._-¨+-/*\\";

            foreach (Char c in Text)
            {
                if (Characters.ToLower().Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean IsEmail(String Text)
        {
            try
            {
                new MailAddress(Text);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
