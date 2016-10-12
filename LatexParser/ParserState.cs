using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser
{
    class ParserState
    {
        #region Public Properties
        public Document Document
        {
            get { return document; }
            set { document = value; }
        }

        public List<Packages.PackageBase> LoadedPackages
        {
            get { return loadedPackages; }
        }

        public List<string> CommandScopeStack
        {
            get { return commandScopeStack; }
        }
        #endregion

        #region Public Methods
        public ParserState()
        {
            document = new Document();
            loadedPackages = new List<Packages.PackageBase>();
            loadedPackages.Add(new Packages.BasePackage());
            commandScopeStack = new List<string>();
        }

        public void AddPackage(Packages.PackageBase newPackage)
        {
            if(newPackage != null)
                loadedPackages.Add(newPackage);
        }

        public void PushCommandScope(string command)
        {
            if(command != null)
                commandScopeStack.Add(command);
        }

        public void PopCommandScope()
        {
            if(commandScopeStack.Count > 0)
                commandScopeStack.RemoveAt(commandScopeStack.Count - 1);
        }
        #endregion

        #region Private Fields
        private Document document;
        private List<Packages.PackageBase> loadedPackages;
        private List<string> commandScopeStack;
        #endregion
    }
}
