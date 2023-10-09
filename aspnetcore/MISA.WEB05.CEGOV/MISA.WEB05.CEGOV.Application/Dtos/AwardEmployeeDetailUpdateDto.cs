namespace MISA.WEB05.CEGOV.Application
{
    public class AwardEmployeeDetailUpdateDto
    {
        /// <summary>
        /// ID của quan hệ giữa danh hiệu và cá nhân
        /// </summary>
        public Guid AwardEmployeedetailId { get; set; }

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
