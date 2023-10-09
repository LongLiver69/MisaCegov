using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Domain
{
    public class Employee : BaseAuditEntity, IHasKey
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
        /// Lấy ra giá trị của EmployeeId
        /// </summary>
        /// <returns>Giá trị của EmployeeId</returns>
        /// Created by: ntlong ( 21/07/2023 )
        public Guid GetKey() => EmployeeId;
    }
}
