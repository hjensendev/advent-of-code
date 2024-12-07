namespace Aoc.Core.Extensions;

public static class StringExtensions
{
    public static string Backwards(this string s)
    {
        var c = s.ToCharArray();
        Array.Reverse(c);
        return new string(c);
    }     
}