// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;

namespace Softeq.NetKit.Payments.Service.Exceptions
{
    public interface IServiceException
    {
        List<ErrorDto> Errors { get; set; }
    }
}