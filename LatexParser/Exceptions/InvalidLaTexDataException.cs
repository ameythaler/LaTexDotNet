using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Exceptions
{
    [Serializable()]
    class InvalidLaTexDataException : LaTexException
    {
        #region Public Methods
        public InvalidLaTexDataException() : base(false) { }
        public InvalidLaTexDataException(string message) : base(message, false) { }
        public InvalidLaTexDataException(string message, bool fatalException) : base(message, fatalException) { }
        public InvalidLaTexDataException(string message, Exception innerException) : base(message, false, innerException) { }
        public InvalidLaTexDataException(string message, bool fatalException, Exception innerException) : base(message, fatalException, innerException) { }
        #endregion

        #region Protected Methods
        protected InvalidLaTexDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(false) { }
        #endregion
    }
}
