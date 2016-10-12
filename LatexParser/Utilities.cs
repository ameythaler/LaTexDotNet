using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser
{
    class Utilities
    {
        public static int FindDelimiterIgnoreLineBreak(string text, char[] delimiters, int startIdx)
        {
            int delimIdx = startIdx;
            bool foundDelim = false;
            while (!foundDelim)
            {
                delimIdx = text.IndexOfAny(delimiters, delimIdx);
                if (delimIdx == -1 || text[delimIdx] != '\\')
                    foundDelim = true;
                if (text.Length >= delimIdx + 2)
                {
                    if (text[delimIdx + 1] == '\\')
                        delimIdx += 2;
                    else
                        foundDelim = true;
                }
            }
            return delimIdx;
        }

        public static T FindTop<T>(DocTree.DocElement startElement) where T : class
        {
            while(startElement != null && !(startElement is T))
            {
                startElement = startElement.Parent;
            }

            if (startElement != null)
                return startElement as T;
            else
                return null;
        }
    }
}
