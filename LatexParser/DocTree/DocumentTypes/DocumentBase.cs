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

        public struct AuthorEntry
        {
            public string Author;
            public string Thanks;
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

        public List<AuthorEntry> Authors
        {
            get { return authors; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        #endregion

        #region Public Methods
        public DocumentBase(int fontSize, LayoutFlags layoutFlags)
        {
            this.fontSize = fontSize;
            this.layoutFlags = layoutFlags;
            titleLines = new List<string>();
            authors = new List<AuthorEntry>();
        }

        public void AddTitleLine(string newLine)
        {
            titleLines.Add(newLine);
        }

        public void AddAuthor(string author)
        {
            AuthorEntry entry;
            entry.Author = author;
            entry.Thanks = null;
            authors.Add(entry);
        }

        public void AddAuthorThanks(string thanks)
        {

        }
        #endregion

        #region Protected Fields
        int fontSize;
        LayoutFlags layoutFlags;
        List<string> titleLines;
        List<AuthorEntry> authors;
        string date;
        #endregion
    }
}
