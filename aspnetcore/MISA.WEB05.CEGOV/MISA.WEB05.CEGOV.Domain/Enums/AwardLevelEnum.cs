using MISA.WEB05.CEGOV.Domain.Resources;
using System.ComponentModel;

namespace MISA.WEB05.CEGOV.Domain
{
    /// <summary>
    /// Cấp khen thưởng
    /// </summary>
    public enum AwardLevelEnum
    {
        /// <summary>
        /// Cấp Nhà nước
        /// </summary>
        [LocalizedDescription("National", typeof(AwardResource))]
        National = 1,

        /// <summary>
        /// Cấp Tỉnh/tương đương 
        /// </summary>
        [LocalizedDescription("Province", typeof(AwardResource))]
        Province = 2,

        /// <summary>
        /// Cấp Huyện/tương đương 
        /// </summary>
        [LocalizedDescription("District", typeof(AwardResource))]
        District = 3,

        /// <summary>
        /// Cấp Xã/tương đương 
        /// </summary>
        [LocalizedDescription("Commune", typeof(AwardResource))]
        Commune = 4

    }
}
