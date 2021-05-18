using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CPF
{
    public static class CPFValidator
    {

        public static string Format(this string cpf)
        {
            if (IsValid(cpf))
            {
                return string.Format("{0}{1}{2}.{3}{4}{5}.{6}{7}{8}-{9}{10}",
                                cpf[0], cpf[1], cpf[2],
                                cpf[3], cpf[4], cpf[5],
                                cpf[6], cpf[7], cpf[8],
                                cpf[9], cpf[10]);
            }

            return null;
        }

        public static bool IsValid(this string cpf)
        {
            cpf = cpf.ReplaceNonNumericCharacters();
            
            if (cpf.Length != 11)
                return false;

            if (cpf.HasEqualNumbers() || cpf.IsSequence())
                return false;

            int[] numbers = cpf.ParseToArray();

            int resultado = numbers.CalculateFirstVerifyDigit();

            if (resultado == 1 || resultado == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - resultado)
            { 
                return false;
            }
            
            resultado = numbers.CalculateSecondVerifyDigit();

            if (resultado == 1 || resultado == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else if (numbers[10] != 11 - resultado)
            { 
                return false;
            }
            
            return true;
        }

        public static string ReplaceNonNumericCharacters(this string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "").Trim();
        }

        public static bool HasEqualNumbers(this string cpf)
        {
            for (int i = 1; i < 11; i++)
            {
                if (cpf[i] != cpf[0])
                    return false;
            }

            return true;
        }

        public static bool IsSequence(this string cpf)
        {
            return cpf.Equals("12345678909");
        }

        public static int[] ParseToArray(this string cpf)
        {
            int[] numbers = new int[11];
            for (int i = 0; i < 11; i++)
            {
                numbers[i] = int.Parse(cpf[i].ToString());
            }

            return numbers;
        }

        public static int CalculateFirstVerifyDigit(this int[] numbers)
        {
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numbers[i];
            }

            return soma % 11;
        }

        public static int CalculateSecondVerifyDigit(this int[] numbers)
        {
            int soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numbers[i];
            }

            return soma % 11;
        }
    }
}
