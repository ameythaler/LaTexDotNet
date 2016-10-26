using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Tokens
{
    abstract class ContentToken : Token
    {
        #region Public Properties
        public string Content
        {
            get { return content; }
        }
        #endregion

        #region Public Methods
        public ContentToken(string content)
        {
            this.content = content;
        }
        #endregion

        #region Protected Fields
        protected string content;
        #endregion
    }
}
