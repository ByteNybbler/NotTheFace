// Author(s): R. Martinho Fernandes (via StackExchange)
// https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
// Functions for reversing strings in a way that is language-independent.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class UtilGrapheme
{
    public static IEnumerable<string> GraphemeClusters(string s)
    {
        TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(s);
        while (enumerator.MoveNext())
        {
            yield return (string)enumerator.Current;
        }
    }

    public static string ReverseGraphemeClusters(string s)
    {
        return string.Join("", GraphemeClusters(s).Reverse().ToArray());
    }
}