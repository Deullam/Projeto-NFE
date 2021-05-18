using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CNPJ
{
    public static class CNPJValidator
    {

        private static readonly int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };


        public static string Format(this string cnpj)
        {
            if (IsValid(cnpj))
            {
                return string.Format("{0}{1}.{2}{3}{4}.{5}{6}{7}/{8}{9}{10}{11}-{12}{13}",
                                cnpj[0], cnpj[1], cnpj[2],
                                cnpj[3], cnpj[4], cnpj[5],
                                cnpj[6], cnpj[7], cnpj[8],
                                cnpj[9], cnpj[10], cnpj[11], 
                                cnpj[12], cnpj[13]);
            }

            return null;
        }

        public static bool IsValid(this string cnpj)
        {
            int resto;

            cnpj = cnpj.ReplaceNonNumericCharacters();

            if (cnpj.Length != 14)
                return false;
            
            int[] numbers = cnpj.ParseToArray();

            resto = numbers.CalculateFirstVerifyingDigit();
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            if (resto != numbers[12])
            {
                return false;
            }
            
            resto = numbers.CalculateSecondVerifyingDigit();
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            if (resto != numbers[13])
            {
                return false;
            }

            return true;
        }

        public static string ReplaceNonNumericCharacters(this string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }

        public static int[] ParseToArray(this string cnpj)
        {
            int[] numbers = new int[14];
            for (int i = 0; i < 14; i++)
            {
                numbers[i] = int.Parse(cnpj[i].ToString());
            }

            return numbers;
        }

        public static int CalculateFirstVerifyingDigit(this int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += numbers[i] * multiplier1[i];
            }

            return (sum % 11);
        }

        public static int CalculateSecondVerifyingDigit(this int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                sum += numbers[i] * multiplier2[i];
            }

            return (sum % 11);
        }
    }
}
