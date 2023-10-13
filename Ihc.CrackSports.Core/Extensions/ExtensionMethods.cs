using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Extensions
{
    public static class ExtensionMethods
    {

        public static string FormataCPF(this long cpf)
        {
            return cpf.ToString(@"000\.000\.000\-00");
        }

        public static string FormataCNPJ(this long cnpj)
        {
            return cnpj.ToString(@"00\.000\.000\/0000\-00");
        }

        public static string SomenteNumeros(this string value)
        {
            return string.Join("", System.Text.RegularExpressions.Regex.Split(value, @"[^\d]"));
        }

        public static string SomenteLetras(this string value)
        {
            return string.Join("", System.Text.RegularExpressions.Regex.Split(value, @"[^a-zA-Z]+$"));
        }


        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static List<string> GetValuesEnum(this Type tipo)
        {
            var valores = Enum.GetValues(tipo);

            List<string> result;

            if (valores.Length > 0)
            {
                result = new List<string>();
                foreach (Enum x in valores)
                {
                    result.Add(x.GetEnumDescription());
                }
                return result;
            }
            return null;
        }

        public static byte[]? GetBytesFromBase64(this string base64)
        {
            byte[]? result = null;

            try
            {
                if (!string.IsNullOrEmpty(base64))
                {
                    result = Convert.FromBase64String(base64);
                }
            }
            catch
            { }

            return result;
        }


        public static string ObterDataEscrita(this DateTime data)
        {
            string result = $"{data.ToString("dddd", new CultureInfo("pt-BR"))} {data.Day} de {data.ToString("MMMM")} de {data.Year}";
            
            return $"{result[0].ToString().ToUpper()}{result.Substring(1)}";
        }

        public static string FormatarHoraEvento(this TimeSpan time)
        {
            string result = $"{time.Hours}h{time.Minutes}";

            return result;
        }



    }
}
