using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Application
{
    public class EmployeeDto
    {
        /// <summary>
        /// ID của cá nhân
        /// </summary>
        [Key]
        [Required]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Tên của cá nhân
        /// </summary>
        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// Mã cá nhân
        /// </summary>
        [Required]
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Danh sách thông tin của bảng quan hệ
        /// </summary>
        public IEnumerable<AwardEmployeeDetailDto> AwardEmployeeDetails { get; set; } = Enumerable.Empty<AwardEmployeeDetailDto>();
    }
}
