using MISA.WEB05.CEGOV.Domain;
using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Application
{
    public class EmployeeUpdateDto
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
        /// Đối tượng chứa thông tin save của detail
        /// </summary>
        public SaveDto<AwardEmployeeDetailCreateDto, AwardEmployeeDetailUpdateDto, Guid> AwardEmployeeDetails { get; set; } = new();
    }
}