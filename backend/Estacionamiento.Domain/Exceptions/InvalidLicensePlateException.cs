using System;
using System.Runtime.Serialization;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class InvalidLicensePlateException : AppException
    {
        public InvalidLicensePlateException() : this("Invalid license plate")
        {
        }

        public InvalidLicensePlateException(string message) : base(message)
        {
        }

        public InvalidLicensePlateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidLicensePlateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
