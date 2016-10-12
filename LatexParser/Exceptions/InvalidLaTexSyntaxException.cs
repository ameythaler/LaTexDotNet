using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Exceptions
{
    [Serializable()]
    class InvalidLaTexSyntaxException : LaTexException
    {
        #region Public Methods
        public InvalidLaTexSyntaxException() : base(true) { }
        public InvalidLaTexSyntaxException(string message) : base(message, true) { }
        public InvalidLaTexSyntaxException(string message, bool fatalException) : base(message, fatalException) { }
        public InvalidLaTexSyntaxException(string message, Exception innerException) : base(message, true, innerException) { }
        public InvalidLaTexSyntaxException(string message, bool fatalException, Exception innerException) : base(message, fatalException, innerException) { }
        #endregion

        #region Protected Methods
        protected InvalidLaTexSyntaxException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(true) { }
        #endregion
    }
}
