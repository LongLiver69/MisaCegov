using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Domain
{
    public class Award : BaseAuditEntity, IHasKey
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
        public AwardObjectEnum AwardObject { get; set; }

        /// <summary>
        /// Cấp khen thưởng
        /// </summary>
        [Required]
        public AwardLevelEnum  AwardLevel { get; set; }

        /// <summary>
        /// Loại danh hiệu
        /// </summary>
        [Required]
        public AwardTypeEnum  AwardType { get; set; }

        /// <summary>
        /// Trạng thái danh hiệu
        /// </summary>
        [Required]
        public AwardStatusEnum  AwardStatus { get; set; }

        /// <summary>
        /// Thể hiện có phải dữ liệu hệ thống không
        /// ( Không được phép sửa xóa )
        /// </summary>
        [Required]
        public bool IsSystemData { get; set; }

        /// <summary>
        /// Lấy ra giá trị của key AwardId
        /// </summary>
        /// <returns>Giá trị của AwardId</returns>
        /// Created by: ntlong ( 21/07/2023 )
        public Guid GetKey() => AwardId;
    }
}
