using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.DocTree.DocumentTypes
{
    public class Article : DocumentBase
    {
        #region Public Methods
        public Article(int fontSize, LayoutFlags layoutFlags) : base(fontSize, layoutFlags)
        {

        }
        #endregion
    }
}
