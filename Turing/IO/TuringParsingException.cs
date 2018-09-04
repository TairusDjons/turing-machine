using System;
using System.Runtime.Serialization;

namespace Turing.IO
{
    public class TuringParsingException : Exception
    {
        public TuringParsingException()
        {
        }

        protected TuringParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TuringParsingException(string message) : base(message)
        {
        }

        public TuringParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
