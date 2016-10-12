using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Packages
{
    class BasePackage : PackageBase
    {
        #region Public Methods
        public BasePackage()
        {
            commands = new Dictionary<string, CommandPair>();

            commands["documentstyle"] = new CommandPair(ParseDocumentStyle, null);
            commands["documentclass"] = new CommandPair(ParseDocumentClass, null);
        }
        #endregion

        #region Private Methods
        private void ParseDocumentStyle(ParserState state, string parameters)
        {
        }

        private void ParseDocumentClass(ParserState state, string parameters)
        {
            string[] options = GetOptions(parameters);
            string parameter = GetParameter(ref parameters);

            if (parameter == "article")
                state.Document.PushElement(new DocTree.DocumentTypes.Article());
        }
        #endregion
    }
}
