using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Exceptions
{
    [Serializable()]
    abstract class LaTexException : Exception
    {
        #region Public Properties
        public bool FatalException
        {
            get { return fatalException; }
        }
        #endregion

        #region Public Methods
        public LaTexException(bool fatalException) : base() { this.fatalException = fatalException; }
        public LaTexException(string message, bool fatalException) : base(message) { this.fatalException = fatalException; }
        public LaTexException(string message, bool fatalException, Exception innerException) : base(message, innerException) { this.fatalException = fatalException; }
        #endregion

        #region Protected Fields
        protected bool fatalException;
        #endregion

        #region Protected Methods
        protected LaTexException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        #endregion
    }
}
