using System;
using System.Collections.Generic;

namespace CoordinateSearchApi
{
    public class LowerCaseComparer : IEqualityComparer<char>
    {
        public bool Equals(char x, char y)
        {
            return char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
        }

        public int GetHashCode(char obj)
        {
            return char.ToUpperInvariant(obj).GetHashCode();
        }
    }
}