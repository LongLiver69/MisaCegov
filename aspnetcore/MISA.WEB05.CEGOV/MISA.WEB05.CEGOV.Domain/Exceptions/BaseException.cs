using System.Text.Json;

namespace MISA.WEB05.CEGOV.Domain
{
    public class BaseException
    {
        #region Properties
        public int ErrorCode { get; set; }
        public string? DevMessage { get; set; }
        public string? UserMessage { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }
        public object? Errors { get; set; }
        #endregion

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
