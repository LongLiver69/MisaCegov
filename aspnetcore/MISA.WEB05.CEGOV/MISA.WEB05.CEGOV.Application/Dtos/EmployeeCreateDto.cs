using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Application
{
    public class EmployeeCreateDto
    {
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
        /// Danh sách detail thêm mới
        /// </summary>
        public IEnumerable<AwardEmployeeDetailCreateDto> AwardEmployeeDetails { get; set; } = Enumerable.Empty<AwardEmployeeDetailCreateDto>();
    }
}
