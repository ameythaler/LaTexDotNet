using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.DocTree.DocumentTypes
{
    public abstract class DocumentBase : DocElement
    {
        #region Public Types
        [Flags]
        public enum LayoutFlags
        {
            none = 0,
            twosided = 1,
            twocolumn = 2,
            leqno = 4,
            fleqn = 8
        }
        #endregion

        #region Public Properties
        public int FontSize
        {
            get { return fontSize; }
        }

        public List<string> Title
        {
            get { return titleLines; }
        }
        #endregion

        #region Public Methods
        public DocumentBase(int fontSize, LayoutFlags layoutFlags)
        {
            this.fontSize = fontSize;
            this.layoutFlags = layoutFlags;
            titleLines = new List<string>();
        }

        public void AddTitleLine(string newLine)
        {
            titleLines.Add(newLine);
        }
        #endregion

        #region Protected Fields
        int fontSize;
        LayoutFlags layoutFlags;
        List<string> titleLines;
        #endregion
    }
}
