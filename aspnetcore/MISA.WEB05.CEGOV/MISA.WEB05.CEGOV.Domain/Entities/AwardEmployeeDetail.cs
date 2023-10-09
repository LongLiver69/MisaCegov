using System.ComponentModel.DataAnnotations;

namespace MISA.WEB05.CEGOV.Domain
{
    public class AwardEmployeeDetail : BaseAuditEntity, IHasKey
    {
        [Required]
        public Guid AwardEmployeeDetailId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid AwardId { get; set; }

        /// <summary>
        /// Lấy ra giá trị của key AwardEmployeeDetailId
        /// </summary>
        /// <returns>Giá trị của AwardEmployeeDetailId</returns>
        /// Created by: ntlong ( 21/07/2023 )
        public Guid GetKey() => AwardEmployeeDetailId;
    }
}
