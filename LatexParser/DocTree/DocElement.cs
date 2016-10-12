using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.DocTree
{
    public abstract class DocElement
    {
        #region Public Properties
        public List<DocElement> Children
        {
            get { return children; }
        }

        public DocElement Parent
        {
            get { return parent; }
        }
        #endregion

        #region Public Methods
        public DocElement()
        {
            children = new List<DocElement>();
            parent = null;
        }

        public void AddChild(DocElement child)
        {
            if (child != null)
            {
                children.Add(child);
                child.parent = this;
            }
        }
        #endregion

        #region Protected Fields
        protected List<DocElement> children;
        protected DocElement parent;
        #endregion
    }
}
