using Microsoft.AspNetCore.Http;
using System.Collections;

namespace MISA.WEB05.CEGOV.Domain
{
    public class ValidateException : Exception
    {
        public int ErrorCode { get; set; } = StatusCodes.Status400BadRequest;
        public ValidateException() { }
        public ValidateException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public ValidateException(string message) : base(message) { }
        public ValidateException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
