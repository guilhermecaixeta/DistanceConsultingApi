using System;
using System.Runtime.Serialization;

namespace Craftable.SharedKernel.exceptions
{
    public class PostalCodeInvalidException : Exception
    {
        public PostalCodeInvalidException() : base("Postal Code is invalid")
        {
        }

        public PostalCodeInvalidException(Exception innerException) : base("Postal Code is invalid", innerException)
        {
        }

        protected PostalCodeInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}