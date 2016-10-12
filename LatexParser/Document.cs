using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser
{
    public class Document
    {
        #region Public Fields
        public DocTree.DocElement BaseElement
        {
            get { return baseElement; }
        }
        #endregion

        #region Public Methods
        public Document()
        {
            baseElement = null;
            topElement = null;
        }

        public void PushElement(DocTree.DocElement element)
        {
            if(baseElement == null)
            {
                baseElement = element;
                topElement = element;
            }
            else
            {
                topElement.AddChild(element);
                topElement = element;
            }
        }

        public DocTree.DocElement PeekElement()
        {
            return topElement;
        }

        public void PopElement()
        {
            if(topElement != null)
            {
                topElement = topElement.Parent;
            }
        }
        #endregion

        #region Private Fields
        private DocTree.DocElement baseElement;
        private DocTree.DocElement topElement;
        #endregion
    }
}
