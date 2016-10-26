using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Tokens
{
    class IdentifierToken : ContentToken
    {
        #region Public Methods
        public IdentifierToken(string content) : base(content)
        {
        }
        #endregion
    }
}
