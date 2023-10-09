using MISA.WEB05.CEGOV.Domain.Resources;
using System.ComponentModel;

namespace MISA.WEB05.CEGOV.Domain
{
    /// <summary>
    /// Loại khen thưởng
    /// </summary>
    public enum AwardTypeEnum
    {
        /// <summary>
        /// Thường xuyên 
        /// </summary>
        [LocalizedDescription("Frequent", typeof(AwardResource))]
        Frequent = 1,

        /// <summary>
        /// Theo đợt (chuyên đề)
        /// </summary>
        [LocalizedDescription("Periodic", typeof(AwardResource))]
        Periodic = 2,

        /// <summary>
        /// Thường xuyên; Theo đợt (chuyên đề)
        /// </summary>
        [LocalizedDescription("FrequentAndPeriodic", typeof(AwardResource))]
        FrequentAndPeriodic = 3

    }
}
