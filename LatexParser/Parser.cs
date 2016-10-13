using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser
{
    public struct ParserSettings
    {

    }

    public class Parser
    {
        #region Public Properties
        public Document Document
        {
            get { return parserState.Document; }
        }
        #endregion

        #region Public Methods
        public Parser()
        {
            parserState = new ParserState();
        }

        public Parser(string preamble)
        {
            parserState = new ParserState();
        }

        public Parser(ParserSettings settings)
        {
            parserState = new ParserState();
        }

        public string Parse(string text)
        {
            //int cmdIdx = Utilities.FindDelimiterIgnoreLineBreak(text, kEscapeChars, 0);
            //int cmdIdx = text.IndexOfAny(kEscapeChars);
            int cmdIdx = Utilities.FindDelimiter(ref text, kEscapeChars, 0);
            if (cmdIdx == -1)
                return null;

            char escapeChar = text[cmdIdx];
            ++cmdIdx; // Remove escape character.

            if(escapeChar == kEndScopeChar) // Handle end of scope.
            {
                parserState.PopCommandScope();
                return text.Substring(cmdIdx);
            }
            else if(escapeChar == kCommentChar)
            {
                int newLineIdx = text.IndexOfAny(kNewLineChars, cmdIdx);
                if (newLineIdx == -1)
                    return null; // Last line is a comment.
                return text.Substring(newLineIdx + 1);
            }

            int cmdEndIdx = text.IndexOfAny(kCommandEndChars, cmdIdx);
            if (cmdEndIdx == -1)
                return null;

            string command = text.Substring(cmdIdx, cmdEndIdx - cmdIdx);
            text = text.Substring(cmdEndIdx);
            parserState.PushCommandScope(command);

            foreach(Packages.PackageBase pkg in parserState.LoadedPackages)
            {
                if (pkg.Parse(parserState, text))
                    break;
            }
            parserState.Update();

            return text;
        }
        #endregion

        #region Private Fields
        private ParserState parserState;
        private readonly char[] kEscapeChars = { '\\', '%', '}' };
        private readonly char[] kCommandEndChars = { '{', ' ', '\n', '\r', '[' };
        private readonly char[] kNewLineChars = { '\n', '\r' };
        private const char kEndScopeChar = '}';
        private const char kCommentChar = '%';
        #endregion
    }
}
