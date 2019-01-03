// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Runtime.Serialization;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;

namespace Softeq.NetKit.Payments.Service.Exceptions
{
    [Serializable]
    public class DuplicateUserException : ServiceException
    {
        public DuplicateUserException(params ErrorDto[] errors)
        {
            InitializeErorrs(errors);
        }

        public DuplicateUserException(string message) : base(message, new ErrorDto(ErrorCode.DuplicateError, message))
        {
        }

        public DuplicateUserException(Exception innerException) : base("See inner exception.", innerException, new ErrorDto(ErrorCode.DuplicateError, innerException.Message))
        {
        }

        public DuplicateUserException(string message, Exception innerException) : base(message, innerException, new ErrorDto(ErrorCode.DuplicateError, message))
        {
        }

        protected DuplicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}