using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTexParser.DocTree.DocumentTypes;

namespace LaTexParser.Packages
{
    class BasePackage : PackageBase
    {
        #region Internal Types
        enum DocumentClass
        {
            article
        }
        #endregion

        #region Public Methods
        public BasePackage()
        {
            commands = new Dictionary<string, CommandPair>();

            commands["documentstyle"] = new CommandPair(ParseDocumentStyle, null);
            commands["documentclass"] = new CommandPair(ParseDocumentClass, null);
            commands["usepackage"] = new CommandPair(ParseUsePackage, null);
            commands["title"] = new CommandPair(ParseTitle, null);
        }
        #endregion

        #region Private Fields

        #endregion

        #region Private Methods
        private void ParseDocumentStyle(ParserState state, string parameters)
        {
        }

        private void ParseDocumentClass(ParserState state, string parameters)
        {
            string[] options = GetOptions(parameters);
            string parameter = GetParameter(parameters);

            DocumentClass paramVal;
            if(!Enum.TryParse(parameter, out paramVal))
            {
                throw new Exceptions.InvalidLaTexDataException(parameter + " is not a valid parameter for \\documentclass", true);
            }

            int fontSize = 10;
            DocumentBase.LayoutFlags layoutFlags = DocumentBase.LayoutFlags.none;
            if (options != null)
            {
                foreach (string option in options)
                {
                    if (option.EndsWith("pt"))
                    {
                        string fontSizeStr = option.Substring(0, option.Length - 2);
                        int desiredFontSize;
                        if (Int32.TryParse(fontSizeStr, out desiredFontSize))
                            fontSize = desiredFontSize;
                        else
                            throw new Exceptions.InvalidLaTexDataException("\\documentclass font size options must take the form of 12pt, 14pt, etc");
                    }
                    else
                    {
                        DocumentBase.LayoutFlags layoutFlag;

                        if (Enum.TryParse(option, out layoutFlag))
                            layoutFlags |= layoutFlag;
                    }
                }
            }

            switch(paramVal)
            {
                case DocumentClass.article:
                    state.Document.PushElement(new Article(fontSize, layoutFlags));
                    break;
            }
        }

        private void ParseUsePackage(ParserState state, string parameters)
        {

        }

        private void ParseTitle(ParserState state, string parameters)
        {
            DocTree.DocElement topElement = state.Document.PeekElement();
            while (topElement != null && !(topElement is DocumentBase))
                topElement = topElement.Parent;

            if(topElement != null)
            {
                string title = GetParameter(parameters);
                DocumentBase doc = topElement as DocumentBase;
                string[] titleLines = title.Split(kLineBreak, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in titleLines)
                    doc.AddTitleLine(line);
            }
        }
        #endregion
    }
}
