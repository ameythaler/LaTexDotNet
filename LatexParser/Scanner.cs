using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser
{
    public class Scanner
    {
        #region Public Internal Types
        public delegate char GetChar();
        #endregion

        #region Public Properties
        public int Index
        {
            get { return index; }
        }

        public bool LastChar
        {
            get { return index == (textString.Length - 1); }
        }

        public bool FirstChar
        {
            get { return index == 0; }
        }

        public int Length
        {
            get { return textString.Length; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
        }

        public int CharNumber
        {
            get { return charNumber; }
        }
        #endregion

        #region Public Methods
        public Scanner(string inputString)
        {
            textString = inputString.Replace("\n\r", "\n");
            index = 0;
            charNumber = 1;
            lineNumber = 1;
            NextChar = GetThisChar;
            PrevChar = GetThisChar;
        }

        public GetChar NextChar;
        public GetChar PrevChar;

        #endregion

        #region Protected Fields
        protected string textString;
        protected int index;
        protected int charNumber;
        protected int lineNumber;
        protected static readonly char[] kNewLine = new char[] { '\n', '\r' };
        #endregion

        #region Protected Methods
        protected char GetNextChar()
        {
            if (index < textString.Length - 1)
            {
                if (textString[index] == '\n' || textString[index] == '\r')
                {
                    ++lineNumber;
                    charNumber = 1;
                }
                else
                {
                    ++charNumber;
                }
                return textString[++index];
            }
            else
            {
                return '\0';
            }
        }

        protected char GetPrevChar()
        {
            if (index > 0)
            {
                int prevIdx = index - 1; // The newline character should go on the previous line.
                if(textString[prevIdx] == '\n' || textString[prevIdx] == '\r')
                {
                    --lineNumber;
                    if (index > 0)
                        charNumber = prevIdx - textString.LastIndexOfAny(kNewLine, index - 2);
                }
                else
                {
                    --charNumber;
                }
                return textString[--index];
            }
            else
            {
                return '\0';
            }
        }

        protected char GetThisChar()
        {
            NextChar = GetNextChar;
            PrevChar = GetPrevChar;

            return textString[index];
        }

        protected void HandleLine(int dir)
        {
            if (textString[index] == '\n' || textString[index] == '\r')
            {
                lineNumber += dir;
                if (dir == 1)
                    charNumber = 1;
                else if(index > 0)
                    charNumber = index - textString.LastIndexOfAny(kNewLine, index - 1);
            }
            else
            {
                charNumber += dir;
            }
        }
        #endregion
    }
}
