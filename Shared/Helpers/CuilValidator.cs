using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using System.Collections;

namespace AutenticacionBlazor.Shared.Helpers
{
    class CuilValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cuit = value.ToString();

            if (cuit.Length != 11) return new ValidationResult("Cuil inválido, debe contener 11 dígitos, sin guiones.", new[] { validationContext.MemberName });

            if (ValidarCuit(cuit))
            {
                return null;
            }
            else
                return new ValidationResult("Cuil inválido, revise los primeros 2 y último dígito e intentelo nuevamente.",
                                             new[] { validationContext.MemberName });
        }

        public static int CalcularDigitoCuit(string cuit)
        {
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            var resto = total % 11;
            int oncemenos = 11 - resto;
            return oncemenos == 11 ? 0 : oncemenos == 10 ? CalcularDigitoCuit(CorregirCuit(cuit)) : oncemenos;
        }
        private static String CorregirCuit(string cuit)
        {
            string nc = cuit;
            string pd = (nc.Remove(2));
            string ul = nc.Substring(10);

            int _pd = int.Parse(pd);
            int _ul = int.Parse(ul);

            if (_pd == 20)
            {
                return String.Concat((String.Concat("23", cuit.Substring(2))).Remove(10), "9");
            }
            if (_pd == 27)
            {
                return String.Concat((String.Concat("23", cuit.Substring(2))).Remove(10), "4");
            }
            if (_pd == 24)
            {
                return String.Concat((String.Concat("23", cuit.Substring(2))).Remove(10), "3");
            }
            if (_pd == 30)
            {
                return String.Concat((String.Concat("33", cuit.Substring(2))).Remove(10), "9");
            }
            if (_pd == 34)
            {
                return String.Concat((String.Concat("33", cuit.Substring(2))).Remove(10), "3");
            }
            else
            {
                return null;
            }
        }

        public static bool ValidarCuit(string cuit)
        {
            if (cuit == null)
            {
                return false;
            }
            //cuit = cuit.Replace("-", string.Empty);
            if (cuit.Length != 11)
            {
                return false;
            }
            else
            {
                int calculado = CalcularDigitoCuit(cuit);
                int digito = int.Parse(cuit.Substring(10));
                return calculado == digito;
            }
        }

    }


}





