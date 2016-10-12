using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Packages
{
    delegate void CommandParser(ParserState state, string input);

    struct CommandPair
    {
        public CommandParser Parser;
        public Dictionary<string, CommandPair> ContextCommands;

        public CommandPair(CommandParser parser, Dictionary<string, CommandPair> contextCommands)
        {
            Parser = parser;
            ContextCommands = contextCommands;
        }
    }

    abstract class PackageBase
    {
        #region Internal Types
        protected enum CommandState
        {
            First,
            Scope,
            Top
        }
        #endregion

        #region Public Methods
        public bool Parse(ParserState state, string text)
        {
            Dictionary<string, CommandPair> commandList = commands;
            CommandState commandState = CommandState.First; // This is our first scope level, failure to find a command means it's probably just in another package.
            string lastCommand = null;
            int scopeCount = 1;
            foreach(string command in state.CommandScopeStack)
            {
                if (scopeCount++ == state.CommandScopeStack.Count) // We're the top level scope, we should execute this command.
                    commandState = CommandState.Top;
                try
                {
                    if (!TryParse(ref commandList, state, command, text, commandState))
                        return false;

                    lastCommand = command;
                    commandState = CommandState.Scope; // We're moving up through the scope, failure to find a command now is an error.
                }
                catch(Exceptions.InvalidLaTexCommandException exception)
                {
                    if(lastCommand != null)
                    {
                        throw new Exceptions.InvalidLaTexSyntaxException(exception.Message + " is not a valid sub-command of " + lastCommand);
                    }
                }
                catch(Exceptions.LaTexException exception)
                {
                    // Do something with this.
                }
            }
            return true;
        }

        
        #endregion

        #region Protected Fields
        protected Dictionary<string, CommandPair> commands;
        protected const char kParamStart = '{';
        protected const char kOptionStart = '[';
        protected const char kOptionEnd = ']';
        protected readonly char[] kParamDelimiter = new char[] { '}', '\\' };
        protected readonly char[] kOptionsDelimiter = new char[] { ',' };
        protected readonly string[] kLineBreak = new string[] { "\\\\" };
        #endregion

        #region Protected Methods
        protected bool TryParse(ref Dictionary<string, CommandPair> commandList, ParserState state, string command, string text, CommandState commandState)
        {
            if (commandList.ContainsKey(command))
            {
                if (commandState == CommandState.Top)
                {
                    commandList[command].Parser(state, text);
                }
                else
                {
                    commandList = commandList[command].ContextCommands;
                }
                return true;
            }
            else if(commandState != CommandState.First)
            {
                throw new Exceptions.InvalidLaTexCommandException(command);
            }
            return false;
        }

        protected string[] GetOptions(string text)
        {
            int optionStartIdx = text.IndexOf(kOptionStart);
            if (optionStartIdx == -1)
                return null;
            int optionEndIdx = text.IndexOf(kOptionEnd, kOptionStart);
            if (optionEndIdx == -1)
                throw new Exceptions.InvalidLaTexSyntaxException("Missing ending \']\' in options.");
            if (optionEndIdx - optionStartIdx == 1)
                throw new Exceptions.InvalidLaTexSyntaxException("Options brackets present, but no options specified", false);

            string options = text.Substring(optionStartIdx, optionEndIdx - optionStartIdx);
            return options.Split(kOptionsDelimiter);
        }

        protected string GetParameter(string text)
        {
            int paramStartIdx = text.IndexOf(kParamStart);
            if (paramStartIdx == -1)
                return null;

            int paramDelimIdx = Utilities.FindDelimiterIgnoreLineBreak(text, kParamDelimiter, paramStartIdx);
            
            if (paramStartIdx == -1)
                throw new Exceptions.InvalidLaTexSyntaxException("Missing ending \'}\' in parameters.");

            ++paramStartIdx; // Remove leading open-brace.
            string parameter = text.Substring(paramStartIdx, paramDelimIdx - paramStartIdx);
            return parameter;
        }
        #endregion
    }
}
