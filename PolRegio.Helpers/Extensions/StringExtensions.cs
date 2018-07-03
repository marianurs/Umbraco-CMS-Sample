using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PolRegio.Helpers.Extensions
{
    /// <summary>
    /// Klasa zawierająća dodatkowe metody do klasy string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Metoda usuwająca specjalne znaki ze string
        /// </summary>
        /// <param name="item">obiekt string</param>
        /// <param name="allowDot">Określa czy w kropka jest dozwolona w nazwie pliku</param>
        /// <returns>string bez znaków specjalnych</returns>
        public static string ToSafeFilenameString(this string item, bool allowDot = true)
        {
            string regex = allowDot
                ? "(?:[^\\w\\p{L}-.]|(?<=[\'\"])s)+"
                : "(?:[^\\w\\p{L}-]|(?<=[\'\"])s)+"; //dot less option of above regex

            Regex r = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(item, "-");
        }

        /// <summary>
        /// Metoda usuwająca specjalne znaki ze string
        /// </summary>
        /// <param name="item">obiekt string</param>
        /// <returns>string bez znaków specjalnych</returns>
        public static string RemoveSpecialChar(this string item)
        {
            Regex r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(item, "");
        }
        /// <summary>
        /// Metoda zmieniająca ciąg znaków w adres url
        /// </summary>
        /// <param name="item">obiekt string</param>
        /// <param name="textToReplace">ciąg znaków, który ma zostać zamieniony</param>
        /// <param name="destinationUrl">docelowy adres url</param>
        /// <returns>string z zamienionym ciągiem znaków na docelowy adres url</returns>
        public static string ReplaceStringToUrl(this string item, string textToReplace, string destinationUrl)
        {
            return Regex.Replace(item, Regex.Escape(textToReplace), string.Format("<a href=\"{0}\">{1}</a>", destinationUrl, textToReplace).Replace("$", "$$"), RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// Metoda zwracająca klasę css dla box elementu na liście
        /// </summary>
        /// <param name="elementsCount">ilość elementów na liście</param>
        /// <param name="elementIndexInList">index elementu na liście</param>
        /// <returns>klasa css dla boxa</returns>
        public static string GetClassForArticlesList(int elementsCount, int elementIndexInList)
        {
            switch (elementsCount)
            {
                case 1:
                    return "col-sm-6 col-md-4 col-sm-offset-3 col-md-offset-4";
                case 2:
                    return "col-sm-6 col-md-3 col-md-offset-3";
                case 3:
                    return "col-sm-6 col-md-4";
                default:
                    return "col-sm-6 col-md-3";
            }
        }
        /// <summary>
        /// Return Base64String
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var _plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(_plainTextBytes);
        }
        /// <summary>
        /// retirns decoded string from Base64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var _base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(_base64EncodedBytes);
        }

        public static List<int> MapToIdList(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return new List<int>();

            return s.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .MapToInt()
                .ToList();
        }
    }
}
