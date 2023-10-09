using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Infrastructure
{
    public class AwardEmployeeDetailRepository : BaseRepository<AwardEmployeeDetail>, IAwardEmployeeDetailRepository
    {
        #region Constructor
        public AwardEmployeeDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

    }
}
