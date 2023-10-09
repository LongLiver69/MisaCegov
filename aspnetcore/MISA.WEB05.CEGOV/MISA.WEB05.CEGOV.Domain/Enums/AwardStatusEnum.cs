using MISA.WEB05.CEGOV.Domain.Resources;
using System.ComponentModel;

namespace MISA.WEB05.CEGOV.Domain
{
    /// <summary>
    /// Trạng thái
    /// </summary>
    public enum AwardStatusEnum
    {
        /// <summary>
        /// Sử dụng
        /// </summary>
        [LocalizedDescription("Active", typeof(AwardResource))]
        Active = 1,

        /// <summary>
        /// Ngừng sử dụng
        /// </summary>
        [LocalizedDescription("Inactive", typeof(AwardResource))]
        Inactive = 2
    }
}
