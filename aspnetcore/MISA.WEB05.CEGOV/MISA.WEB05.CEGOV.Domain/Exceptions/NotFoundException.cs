using Microsoft.AspNetCore.Http;

namespace MISA.WEB05.CEGOV.Domain
{
    public class NotFoundException : Exception
    {
        public int ErrorCode { get; set; } = StatusCodes.Status404NotFound;
        public NotFoundException() { }
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
