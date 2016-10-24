using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser
{
    public class Lexer
    {
        #region Public Methods
        public Lexer(string inputString)
        {
            scanner = new Scanner(inputString);
        }
        #endregion

        #region Protected Methods
        protected Scanner scanner;
        #endregion
    }
}
