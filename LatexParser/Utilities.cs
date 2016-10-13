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

        public static int FindDelimiter(ref string text, char[] delimiters, int startIdx)
        {
            int delimIdx = startIdx;
            bool foundDelim = false;
            while(!foundDelim)
            {
                delimIdx = text.IndexOfAny(delimiters, delimIdx);
                if (delimIdx == -1 || !IsEscapeCharacter(text[delimIdx]))
                {
                    foundDelim = true;
                }
                else if(text.Length >= delimIdx + 2)
                {
                    if (!ProcessEscapeCharacters(ref text, delimIdx))
                        foundDelim = true;
                    else
                        ++delimIdx;
                }
                else
                {
                    delimIdx = -1;
                    foundDelim = true;
                }
            }
            return delimIdx;
        }

        public static bool ProcessEscapeCharacters(ref string text, int escapeIdx)
        {
            char escapeChar = text[escapeIdx];
            bool retVal = false;
            if (text.Length >= escapeIdx + 2)
            {
                if (escapeChar == kEscape)
                {
                    char nextChar = text[escapeIdx + 1];
                    switch(nextChar)
                    {
                        case '#':
                            ReplaceChars(ref text, escapeIdx, 2, '#');
                            retVal = true;
                            break;
                        case '$':
                            ReplaceChars(ref text, escapeIdx, 2, '$');
                            retVal = true;
                            break;
                        case '%':
                            ReplaceChars(ref text, escapeIdx, 2, '%');
                            retVal = true;
                            break;
                        case '&':
                            ReplaceChars(ref text, escapeIdx, 2, '&');
                            retVal = true;
                            break;
                        case '_':
                            ReplaceChars(ref text, escapeIdx, 2, '_');
                            retVal = true;
                            break;
                        case '{':
                            ReplaceChars(ref text, escapeIdx, 2, '{');
                            retVal = true;
                            break;
                        case '}':
                            ReplaceChars(ref text, escapeIdx, 2, '}');
                            retVal = true;
                            break;
                        default:
                            break;
                    }
                }
                else if (escapeChar == kMathSym)
                {

                }
            }
            return retVal;
        }

        public static void ReplaceChars(ref string text, int startIdx, int count, char newChar)
        {
            string leftText = text.Substring(0, startIdx);
            string rightText = text.Substring(startIdx + count);
            text = leftText + newChar + rightText;
        }

        public static bool IsEscapeCharacter(char charToCheck)
        {
            foreach(char escapeChar in kEscapeChars)
            {
                if (charToCheck == escapeChar)
                    return true;
            }
            return false;
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

        private static readonly char[] kEscapeChars = new char[] { '\\', '$' };
        private const char kEscape = '\\';
        private const char kMathSym = '$';
    }
}
