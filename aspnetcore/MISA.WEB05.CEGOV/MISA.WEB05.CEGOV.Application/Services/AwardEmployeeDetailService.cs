using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public class AwardEmployeeDetailService : BaseService<AwardEmployeeDetail, AwardEmployeeDetailDto, AwardEmployeeDetailCreateDto, AwardEmployeeDetailUpdateDto>, IAwardEmployeeDetailService
    {
        #region Fields
        private readonly IAwardRepository _awardRepository;
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        public AwardEmployeeDetailService(IAwardEmployeeDetailRepository baseRepository, IMapper mapper, IAwardRepository awardRepository, IEmployeeRepository employeeRepository) : base(baseRepository, mapper)
        {
            _awardRepository = awardRepository;
            _employeeRepository = employeeRepository;
        }

        public override async Task<AwardEmployeeDetail> MapEntityCreateDtoToEntity(AwardEmployeeDetailCreateDto createDto)
        {
            // Map sang AwardEmployeeDetail
            var entity = new AwardEmployeeDetail()
            {
                AwardEmployeeDetailId = Guid.NewGuid(),
                EmployeeId = createDto.EmployeeId,
                AwardId = createDto.AwardId,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                ModifiedDate = null,
                ModifiedBy = null,
            };
            return entity;
        }

        public override async Task<AwardEmployeeDetail> MapEntityUpdateDtoToEntity(AwardEmployeeDetailUpdateDto updateDto)
        {
            // Map sang AwardEmployeeDetail
            var entity = new AwardEmployeeDetail()
            {
                AwardEmployeeDetailId = Guid.NewGuid(),
                AwardId = updateDto.AwardId,
                EmployeeId = updateDto.EmployeeId,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
            };
            return entity;
        }
    }
}
