using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Exceptions
{
    class InvalidLaTexCommandException : LaTexException
    {
        #region Public Methods
        public InvalidLaTexCommandException() : base(true) { }
        public InvalidLaTexCommandException(string command) : base(command, true) { }
        public InvalidLaTexCommandException(string command, Exception innerException) : base(command, true, innerException) { }
        #endregion

        #region Protected Methods
        protected InvalidLaTexCommandException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(true) { }
        #endregion
    }
}
