using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTexParser.Tokens
{
    enum ParameterTokenType
    {
        Begin,
        End
    }

    class ParameterToken : Token
    {
        #region Public Properties
        public ParameterTokenType ParameterType
        {
            get { return parameterType; }
        }

        public bool Optional
        {
            get { return optional; }
        }
        #endregion

        #region Public Methods
        public ParameterToken(ParameterTokenType type, bool isOptional)
        {
            parameterType = type;
            optional = isOptional;
        }
        #endregion

        #region Protected Fields
        protected ParameterTokenType parameterType;
        protected bool optional;
        #endregion
    }
}
