namespace MISA.WEB05.CEGOV.Application
{
    public class AwardEmployeeDetailDto
    {
        /// <summary>
        /// ID của quan hệ giữa danh hiệu và cá nhân
        /// </summary>
        public Guid AwardEmployeeDetailId { get; set; }

        /// <summary>
        /// ID của danh hiệu
        /// </summary>
        public Guid AwardId { get; set; }

        /// <summary>
        /// ID của cá nhân
        /// </summary>
        public Guid EmployeeId { get; set; }
    }
}
