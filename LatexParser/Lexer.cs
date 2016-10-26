using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTexParser.Tokens;

namespace LaTexParser
{
    public class Lexer
    {
        #region Public Methods
        public Lexer(string inputString)
        {
            scanner = new Scanner(inputString);
        }

        public Token NextToken()
        {
            char curChar = scanner.NextChar();
            while (curChar == '\r' || curChar == '\n')
                curChar = scanner.NextChar();

            switch(curChar)
            {
                case '\\':
                    return ReadIdentifierToken();

                default:
                    throw new Exception("Error parsing token at (Line: " + scanner.LineNumber + ", Character: " + scanner.CharNumber + ")");
            }
        }
        #endregion

        #region Protected Fields
        protected Scanner scanner;
        #endregion

        #region Protected Methods
        public Token ReadIdentifierToken()
        {
            string identifierString = "";
            char nextChar = scanner.NextChar();
            
        }
        #endregion
    }
}
