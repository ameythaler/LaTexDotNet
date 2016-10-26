using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Tokens
{
    class TextToken : ContentToken
    {
        #region Public Methods
        public TextToken(string content) : base(content)
        {
        }
        #endregion
    }
}
