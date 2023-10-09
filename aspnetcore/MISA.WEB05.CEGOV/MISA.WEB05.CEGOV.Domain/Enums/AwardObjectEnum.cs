using MISA.WEB05.CEGOV.Domain.Resources;
using System.ComponentModel;

namespace MISA.WEB05.CEGOV.Domain
{
    /// <summary>
    /// Đối tượng khen thưởng
    /// </summary>
    public enum AwardObjectEnum
    {
        /// <summary>
        /// Cá nhân
        /// </summary>
        [LocalizedDescription("Individual", typeof(AwardResource))]
        Individual = 1,

        /// <summary>
        /// Tập thể
        /// </summary>
        [LocalizedDescription("Group", typeof(AwardResource))]
        Group = 2,

        /// <summary>
        /// Cá nhân và tập thể 
        /// </summary>        
        [LocalizedDescription("IndividualAndGroup", typeof(AwardResource))]
        IndividualAndGroup = 3,

        /// <summary>
        /// Hộ gia đình
        /// </summary>        
        [LocalizedDescription("Family", typeof(AwardResource))]
        Family = 4
    }
}
