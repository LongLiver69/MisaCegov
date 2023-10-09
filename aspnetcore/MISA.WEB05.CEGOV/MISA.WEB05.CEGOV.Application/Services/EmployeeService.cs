using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public class EmployeeService : DetailService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        #region Fields
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAwardEmployeeDetailService _detailService;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IAwardEmployeeDetailService detailService, IMapper mapper, IUnitOfWork uow) : base(employeeRepository, mapper, uow)
        {
            _employeeRepository = employeeRepository;
            _detailService = detailService;
        }
        #endregion

        #region Mapping
        public override async Task<Employee> MapEntityCreateDtoToEntity(EmployeeCreateDto employeeCreateDto)
        {
            // Map sang Employee
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeName = employeeCreateDto.EmployeeName,
                EmployeeCode = employeeCreateDto.EmployeeCode,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                ModifiedDate = null,
                ModifiedBy = null,
            };
            return employee;
        }

        public override async Task<Employee> MapEntityUpdateDtoToEntity(EmployeeUpdateDto employeeUpdateDto)
        {
            // Map sang Employee
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeName = employeeUpdateDto.EmployeeName,
                EmployeeCode = employeeUpdateDto.EmployeeCode,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
            };
            return employee;
        }
        #endregion

        #region LoadDetail
        protected override async Task LoadDetail(EmployeeDto entityDto)
        {
            var details = await _detailService.GetAllAsync();
            var key = entityDto.EmployeeId;
            entityDto.AwardEmployeeDetails = details
                .Where(x => x.EmployeeId == key);
        }

        protected override async Task LoadDetail(IEnumerable<EmployeeDto> entityDtos)
        {
            var details = await _detailService.GetAllAsync();
            foreach (var entityDto in entityDtos)
            {
                var key = entityDto.EmployeeId;
                entityDto.AwardEmployeeDetails = details
                    .Where(x => x.EmployeeId == key);
            }
        }
        #endregion

        #region SaveDetail Methods

        /// <summary>
        /// Thêm detail vào db sau khi tạo master từ createDto (Thêm bảng quan hệ)
        /// </summary>
        /// <param name="createDto">Dto chứa dữ liệu tạo master và detail</param>
        /// <param name="master">Entity master vừa thêm</param>
        /// Created by: ntlong ( 12/09/2023 )
        public override async Task InsertDetailAsync(EmployeeCreateDto createDto, Employee entity)
        {
            foreach (var detail in createDto.AwardEmployeeDetails)
            {
                detail.EmployeeId = entity.EmployeeId;
            }

            await _detailService.InsertManyAsync(createDto.AwardEmployeeDetails.ToList());
        }

        /// <summary>
        /// Thêm detail vào db sau khi tạo nhiều master từ createDtos
        /// </summary>
        /// <param name="data">Dữ liệu vừa thêm master theo cặp entity và createDto</param>
        /// Created by: ntlong ( 12/09/2023 )
        public override async Task InsertManyDetailAsync(List<(EmployeeCreateDto CreateDto, Employee Entity)> data)
        {
            foreach (var (createDto, entity) in data)
            {
                foreach (var detail in createDto.AwardEmployeeDetails)
                {
                    detail.EmployeeId = entity.EmployeeId;
                }
            }
            var detailDtos = data
                .Select(x => x.CreateDto)
                .SelectMany(x => x.AwardEmployeeDetails);
            await _detailService.InsertManyAsync(detailDtos.ToList());
        }

        /// <summary>
        /// Cập nhật detail sau khi cập nhật master
        /// </summary>
        /// <param name="updateDto">Dto chứa master và detail cần update</param>
        /// <param name="oldMaster">Dữ liệu master cũ</param>
        /// <param name="newMaster">Dữ liệu master mới</param>
        /// Created by: ntlong ( 12/09/2023 )
        public override async Task UpdateDetailAsync(EmployeeUpdateDto updateDto, Employee oldMaster, Employee newMaster)
        {

        }

        /// <summary>
        /// Cập nhật nhiều detail sau khi cập nhật master
        /// </summary>
        /// <param name="updateDtos">Dto chứa dữ liệu cập nhật master và detail</param>
        /// Created by: ntlong ( 12/09/2023 )
        public override async Task UpdateManyDetailAsync(List<EmployeeUpdateDto> updateDtos)
        {

        }

        /// <summary>
        /// Xóa detail trước khi xóa master
        /// </summary>
        /// <param name="masterKey">Khóa của master</param>
        /// Created by: ntlong ( 12/09/2023 )
        public override async Task DeleteDetailAsync(Guid masterKey)
        {
            var dto = await GetByIdAsync(masterKey);

            var detailKeys = dto.AwardEmployeeDetails
            .Select(x => x.AwardEmployeeDetailId);
            await _detailService.DeleteManyAsync(detailKeys.ToList());
        }

        /// <summary>
        /// Thực hiện xóa detail trước khi xóa nhiều master
        /// </summary>
        /// <param name="masterKeys">Khóa chính của các master</param>
        /// Created by: ntlong ( 12/09/2023 )
        public override async Task DeleteManyDetailAsync(List<Guid> masterKeys)
        {
            var dtos = await GetListByIdsAsync(masterKeys);

            var detailKeys = dtos.SelectMany(x => x.AwardEmployeeDetails).Select(x => x.AwardEmployeeDetailId);
            await _detailService.DeleteManyAsync(detailKeys.ToList());
        }

        #endregion
    }
}
