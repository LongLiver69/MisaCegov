using MISA.WEB05.CEGOV.Domain;
using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Application
{
    public class AwardCreateDto
    {
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
        public AwardLevelEnum AwardLevel { get; set; }

        /// <summary>
        /// Loại danh hiệu
        /// </summary>
        [Required]
        public AwardTypeEnum AwardType { get; set; }

        /// <summary>
        /// Trạng thái danh hiệu
        /// </summary>
        [Required]
        public AwardStatusEnum AwardStatus { get; set; }
    }
}
