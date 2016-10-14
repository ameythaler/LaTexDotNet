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

        public string Title
        {
            get { return title; }
            set { title = value; }
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
            authors = new List<AuthorEntry>();
        }

        public void AddAuthor(string author)
        {
            AuthorEntry entry;
            entry.Author = author;
            entry.Thanks = null;
            authors.Add(entry);
        }

        public void AddAuthorLine(string author)
        {
            AuthorEntry currentEntry = authors[authors.Count - 1];
            currentEntry.Author += '\n' + author;
            authors[authors.Count - 1] = currentEntry;
        }

        public void AddAuthorThanks(string thanks)
        {
            AuthorEntry currentEntry = authors[authors.Count - 1];
            currentEntry.Thanks = thanks;
            authors[authors.Count - 1] = currentEntry;
        }
        #endregion

        #region Protected Fields
        int fontSize;
        LayoutFlags layoutFlags;
        string title;
        List<AuthorEntry> authors;
        string date;
        #endregion
    }
}
