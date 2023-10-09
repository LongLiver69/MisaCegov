namespace MISA.WEB05.CEGOV.Application
{
    public class AwardEmployeeDetailCreateDto
    {
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
