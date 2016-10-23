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
        #endregion

        #region Public Methods
        public Scanner(string inputString)
        {
            textString = inputString;
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
        #endregion

        #region Protected Methods
        protected char GetNextChar()
        {
            if (index < textString.Length - 1)
            {
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
        #endregion
    }
}
