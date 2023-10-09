using MISA.WEB05.CEGOV.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Application
{
    public class AwardDisplayDto
    {
        /// <summary>
        /// ID của danh hiệu
        /// </summary>
        [Key]
        [Required]
        public Guid AwardId { get; set; }

        /// <summary>
        /// Tên của danh hiệu
        /// </summary>
        [Required]
        public string AwardName { get; set; } = string.Empty;

        /// <summary>
        /// Mã danh hiệu
        /// </summary>
        [Required]
        public string AwardCode { get; set; } = string.Empty;

        /// <summary>
        /// Đối tượng khen thưởng
        /// </summary>
        [Required]
        public string AwardObject { get; set; } = string.Empty;

        /// <summary>
        /// Cấp khen thưởng
        /// </summary>
        [Required]
        public string AwardLevel { get; set; } = string.Empty;

        /// <summary>
        /// Loại danh hiệu
        /// </summary>
        [Required]
        public string AwardType { get; set; } = string.Empty;

        /// <summary>
        /// Trạng thái danh hiệu
        /// </summary>
        [Required]
        public string AwardStatus { get; set; } = string.Empty;

        /// <summary>
        /// Thể hiện có phải dữ liệu hệ thống không 
        /// ( Không được phép sửa xóa )
        /// </summary>
        [Required]
        public bool IsSystemData { get; set; }
    }
}
