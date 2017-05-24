using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatexDotNet.Parser
{
    public struct Token
    {
        public string Str;
        public int LineNumber;
        public int ColumnNumber;

        public override string ToString()
        {
            return "(" + LineNumber + "," + ColumnNumber + ")" + Str;
        }
    }

    public class Tokenizer
    {
        #region Public Methods
        public Tokenizer(TextReader reader)
        {
            mReader = reader;
        }

        public Token? GetNextToken()
        {
            int curLineNumber = mLineNumber;
            int curColumnNumber = mColumnNumber;
            string token = "";
            bool finishedToken = false;
            bool haveBackslash = false;
            bool shouldRead = true;
            while(!finishedToken)
            {
                int nextVal = mReader.Peek();
                char nextChar = '\0';
                if(nextVal > -1)
                {
                    nextChar = (char)nextVal;
                    if(nextChar == '\\')
                    {
                        if(!haveBackslash)
                        {
                            token += nextChar;
                            haveBackslash = true;
                        }
                        else
                        {
                            if(token.Length > 1) // backslash is the start of a new token
                            {
                                finishedToken = true;
                                shouldRead = false; // leave the backslash for next time
                            }
                            else // The backslash is the escaped character
                            {
                                token += nextChar;
                                finishedToken = true;
                            }
                        }
                    }
                    else
                    {
                        if(nextChar == '\r')
                        {
                            if(token.Length > 0)
                            {
                                finishedToken = true;
                                shouldRead = false;
                            }
                            else
                            {
                                ++curLineNumber;
                                curColumnNumber = 0;
                                token += '\n';
                                finishedToken = true;
                            }
                        }
                        else if(nextChar == '\n')
                        {
                            if(token.Length > 0)
                            {
                                finishedToken = true;
                                shouldRead = false;
                            }
                            else
                            {
                                ++curLineNumber;
                                curColumnNumber = 0;
                                token += nextChar;
                                finishedToken = true;
                                mReader.Read();
                                nextVal = mReader.Peek();
                                if(nextVal > -1)
                                {
                                    nextChar = (char)nextVal;
                                    if(nextChar != '\r')
                                    {
                                        shouldRead = false;
                                    }
                                }
                                else
                                {
                                    shouldRead = false;
                                }
                            }
                        }
                        else if(IsEscapeChar(nextChar))
                        {
                            if(token.Length > 0)
                            {
                                finishedToken = true;
                                shouldRead = false;
                            }
                            else
                            {
                                finishedToken = true;
                                token += nextChar;
                            }
                        }
                        else
                        {
                            System.Globalization.UnicodeCategory category = Char.GetUnicodeCategory(nextChar);
                            if(category == System.Globalization.UnicodeCategory.LowercaseLetter || category == System.Globalization.UnicodeCategory.UppercaseLetter)
                            {
                                token += nextChar;
                            }
                            else
                            {
                                if (token.Length > 0)
                                {
                                    finishedToken = true;
                                    shouldRead = false;
                                }
                                else
                                {
                                    finishedToken = true;
                                    token += nextChar;
                                }
                            }
                        }
                    }
                }
                else
                {
                    finishedToken = true;
                }

                if(shouldRead)
                {
                    if (nextChar != '\n' && nextChar != '\r')
                    {
                        ++curColumnNumber;
                    }
                    mReader.Read();
                }

            }

            if (token.Length > 0)
            {
                Token newToken = new Token {
                    Str = token,
                    LineNumber = mLineNumber,
                    ColumnNumber = mColumnNumber
                };
                mLineNumber = curLineNumber;
                mColumnNumber = curColumnNumber;
                return newToken;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Protected Fields
        protected TextReader mReader;
        protected readonly char[] kEscapeChars = new char[] { ' ', '{', '}', '&', '%', '_', '#', '^', '$', '\t' };
        protected int mLineNumber = 0;
        protected int mColumnNumber = 0;
        #endregion

        #region Protected Methods
        protected bool IsEscapeChar(char nextChar)
        {
            foreach(var escapeChar in kEscapeChars)
            {
                if(nextChar == escapeChar)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
