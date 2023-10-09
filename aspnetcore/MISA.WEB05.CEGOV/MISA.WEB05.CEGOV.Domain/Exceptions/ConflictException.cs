using Microsoft.AspNetCore.Http;

namespace MISA.WEB05.CEGOV.Domain
{
    public class ConflictException : Exception
    {
        public int ErrorCode { get; set; } = StatusCodes.Status409Conflict;
        public ConflictException() { }
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
