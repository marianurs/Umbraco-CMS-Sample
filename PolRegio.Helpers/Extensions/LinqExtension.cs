using System.Collections.Generic;

namespace PolRegio.Helpers.Extensions
{
    public static class LinqExtension
    {
        public static IEnumerable<int> MapToInt(this IEnumerable<string> enumerable)
        {
            foreach (var str in enumerable)
            {
                int i;
                if (int.TryParse(str, out i))
                {
                    yield return i;
                }
            }
        }
    }
}