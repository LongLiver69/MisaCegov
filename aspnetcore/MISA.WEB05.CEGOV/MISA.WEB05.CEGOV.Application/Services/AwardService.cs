using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public class AwardService : BaseService<Award, AwardDto, AwardCreateDto, AwardUpdateDto>, IAwardService
    {
        #region Fields
        private readonly IAwardRepository _awardRepository;
        private readonly IAwardManager _awardManager;
        private readonly IExcelWorker _excelWorker;
        #endregion

        #region Constructor
        public AwardService(IAwardRepository awardRepository, IMapper mapper, IAwardManager awardManager, IExcelWorker excelWorker) : base(awardRepository, mapper)
        {
            _awardRepository = awardRepository;
            _awardManager = awardManager;
            _excelWorker = excelWorker;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ánh xạ AwardCreateDto sang Award
        /// </summary>
        /// <param name="awardCreateDto">Đối tượng AwardCreateDto</param>
        /// <returns>Đối tượng Award</returns>
        /// Created by: ntlong ( 23/07/2023 )
        public override async Task<Award> MapEntityCreateDtoToEntity(AwardCreateDto awardCreateDto)
        {
            // Check mã danh hiệu đã tồn tại chưa
            await _awardManager.CheckAwardExistByCode(awardCreateDto.AwardCode);
            // Map sang Award
            var award = new Award()
            {
                AwardId = Guid.NewGuid(),
                AwardName = awardCreateDto.AwardName,
                AwardCode = awardCreateDto.AwardCode,
                AwardObject = awardCreateDto.AwardObject,
                AwardLevel = awardCreateDto.AwardLevel,
                AwardType = awardCreateDto.AwardType,
                AwardStatus = awardCreateDto.AwardStatus,
                IsSystemData = false,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                ModifiedDate = null,
                ModifiedBy = null,
            };
            return award;
        }

        /// <summary>
        /// Ánh xạ AwardUpdateDto sang Award
        /// </summary>
        /// <param name="awardUpdateDto">Đối tượng AwardUpdateDto</param>
        /// <returns>Đối tượng Award</returns>
        /// Created by: ntlong ( 23/07/2023 )
        public override async Task<Award> MapEntityUpdateDtoToEntity(AwardUpdateDto awardUpdateDto)
        {
            // Lấy ra mã cũ để check xem mã mới có bị trùng với các mã khác ngoài mã cũ không
            var oldAward = await _awardRepository.GetByIdAsync(awardUpdateDto.AwardId);
            var oldCode = oldAward.AwardCode;

            // Check mã danh hiệu đã tồn tại chưa
            await _awardManager.CheckAwardExistByCode(awardUpdateDto.AwardCode, oldCode);

            // Map sang Award
            var award = new Award()
            {
                AwardId = awardUpdateDto.AwardId,
                AwardName = awardUpdateDto.AwardName,
                AwardCode = awardUpdateDto.AwardCode,
                AwardObject = awardUpdateDto.AwardObject,
                AwardLevel = awardUpdateDto.AwardLevel,
                AwardType = awardUpdateDto.AwardType,
                AwardStatus = awardUpdateDto.AwardStatus,
                IsSystemData = false,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
            };
            return award;
        }

        /// <summary>
        /// Ánh xạ AwardDto sang AwardDisplayDto
        /// </summary>
        /// <param name="awardDto">Đối tượng AwardUpdateDto</param>
        /// <returns>Đối tượng AwardDisplayDto</returns>
        /// Created by: ntlong ( 23/07/2023 )
        public AwardDisplayDto MapEntityDtoToEntityDisplayDto(AwardDto awardDto)
        {
            // Lấy các prop của 2 đối tượng để map giá trị
            var props = typeof(AwardDto).GetProperties();
            var displayProps = typeof(AwardDisplayDto).GetProperties();

            var displayDto = new AwardDisplayDto();

            for (int i = 0; i < props.Length; i++)
            {
                // Lấy ra từng giá trị của đối tượng AwardDto
                var value = props[i].GetValue(awardDto);

                // Nếu giá trị của prop nào của AwardDto có kiểu Enum thì map sang String
                if (value != null && value is Enum enumValue)
                {
                    value = enumValue.GetDescription();
                }
                // Lấy giá trị kiểu String vừa map để set cho đối tượng hiển thị
                displayProps[i].SetValue(displayDto, value);
            }

            return displayDto;
        }

        /// <summary>
        /// Sửa trường AwardStatus ở nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng các id của các bản ghi cần sửa</param>
        /// <param name="newStatus">Giá trị mới của AwardStatus</param>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task UpdateManyStatusAsync(List<Guid> ids, int newStatus)
        {
            await _awardRepository.UpdateManyStatusAsync(ids, newStatus);
        }


        /// <summary>
        /// Lấy danh sách bản ghi bao gồm phân trang, lọc, tìm kiếm, sắp xếp
        /// </summary>
        /// <param name="page">Số thứ tự của trang cần lấy</param>
        /// <param name="size">Số lượng tối đa một trang</param>
        /// <param name="search">Chuỗi tìm kiếm</param>
        /// <param name="searchFields">Danh sách các trường cần tìm kiếm</param>
        /// <param name="filteredObject">Giá trị lọc của đối tượng khen thưởng</param>
        /// <param name="filteredLevel">Giá trị lọc của cấp khen thưởng</param>
        /// <param name="filteredType">Giá trị lọc của loại khen thưởng</param>
        /// <param name="filteredStatus">Giá trị lọc của trạng thái</param>
        /// <param name="sortField">Trường được sắp xếp</param>
        /// <param name="sortType">Kiểu sắp xếp: true là tăng dần, false là giảm dần</param>
        /// <returns>Tổng số và danh sách bản ghi theo trang cần hiển thị</returns>
        /// Created by: ntlong ( 25/07/2023 )
        public async Task<(int Total, List<AwardDto> Data)> GetListFPSSAsync(int? page = null, int? size = null, string? search = null, List<string>? searchFields = null, AwardObjectEnum? filteredObject = null, AwardLevelEnum? filteredLevel = null, AwardTypeEnum? filteredType = null, AwardStatusEnum? filteredStatus = null, string? sortField = null, bool? sortType = null)
        {
            // Kiểm tra tính hợp lệ của page và size
            if (page <= 0 || size <= 0)
            {
                throw new ValidateException("Page hoặc PageSize nhỏ hơn 1");
            }

            // Nếu các trường lọc đều null thì filter = null
            Dictionary<string, object?>? filter;
            if (filteredObject == null && filteredLevel == null && filteredType == null && filteredStatus == null)
            {
                filter = null;
            }
            else // Ngược lại: Tạo một dictionary là các trường được lọc               
            {
                filter = new Dictionary<string, object?>
                {
                    { "AwardObject", filteredObject},
                    { "AwardLevel", filteredLevel},
                    { "AwardType", filteredType},
                    { "AwardStatus", filteredStatus}
                };
            }

            var (total, entities) = await _awardRepository.GetListFPSSAsync(page, size, search, searchFields, filter, sortField, sortType);
            var entityDtos = _mapper.Map<List<AwardDto>>(entities);

            //var data = new List<AwardDisplayDto>();
            //foreach ( var dto in entityDtos )
            //{
            //    var displayDto = MapEntityDtoToEntityDisplayDto(dto);
            //    data.Add(displayDto );
            //}

            //return (total, data);

            return (total, entityDtos);
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task<byte[]> ExportToExcel(TableFormat tableFormat)
        {
            // Lấy dữ liệu trong database
            var awards = await _awardRepository.GetAllAsync();

            // Chuyển đổi thành Dto ẩn các trường dữ liệu thừa
            var dtos = _mapper.Map<IEnumerable<AwardDto>>(awards);

            var list = new List<IDictionary<string, object?>>();

            // Mapping dữ liệu
            foreach (var dto in dtos)
            {
                var type = dto.GetType();
                var props = type.GetProperties();
                var dic = props.ToDictionary(p => p.Name, p => p.GetValue(dto));
                foreach (var key in dic.Keys)
                {
                    var value = dic[key];
                    if (value != null && value is Enum enumValue)
                    {
                        var content = enumValue.GetDescription();
                        dic[key] = content;
                    }
                }
                list.Add(dic);
            }

            var file = await _excelWorker.ExportToExcel(tableFormat, list);
            return file;
        }

        /// <summary>
        /// Lấy ra Lấy thông tin và kiểm tra tính hợp lệ của file
        /// </summary>
        /// <param name="file">File được gửi từ client</param>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task<ImportFileInfo> GetFileInfo(IFormFile file)
        {
            var fileInfo = await _excelWorker.GetFileInfo(file);
            return fileInfo;
        }

        /// <summary>
        /// Lấy ra số bản ghi hợp lệ và không hợp lệ của file được upload
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task<(ImportDataStatistics DataStatistics, List<Dictionary<string, object?>> ValidList)> CheckImportFile(IFormFile file, int sheetIndex, int titleLine)
        {
            var fieldNames = new List<string>()
            {
                "AwardName", "AwardCode", "AwardObject", "AwardLevel", "AwardType", "AwardStatus"
            };
            var list = await _excelWorker.ReadImportFile(file, sheetIndex, titleLine, fieldNames);

            // Biến đếm số lượng bản ghi hợp lệ
            int validCount = 0;

            // Tạo list lưu những dictionary tương ứng bản ghi hợp lệ
            var validList = new List<Dictionary<string, object?>>();

            foreach (var dic in list)
            {
                // Kiểm tra xem bản ghi có hợp lệ không
                bool isValidRecord = await _awardManager.CheckImportRecordValidity(dic);
                if (isValidRecord) // Hợp lệ
                {
                    validCount++;
                    validList.Add(dic);
                }
            }

            var obj = new ImportDataStatistics
            {
                Total = list.Count,
                ValidNumber = validCount,
                InvalidNumber = list.Count - validCount
            };

            return (obj, validList);
        }

        /// <summary>
        /// Nhập dữ liệu từ file vào database
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task ImportExcel(IFormFile file, int sheetIndex, int titleLine)
        {
            // Lấy ra list các bản ghi hợp lệ đã được kiểm tra
            var (_, validList) = await CheckImportFile(file, sheetIndex, titleLine);

            // Tạo list để map sang kiểu Award để insert vào db
            var dtos = new List<Award>();

            foreach (var dic in validList)
            {
                var createDto = new AwardCreateDto();
                var props = typeof(AwardCreateDto).GetProperties();
                foreach (var prop in props)
                {
                    if (dic.TryGetValue(prop.Name, out object? value))
                    {
                        if (prop.PropertyType.IsAssignableTo(typeof(Enum)))
                        {
                            var enumType = prop.PropertyType.Name;

                            value = _awardManager.ConvertTextToEnum(enumType, value as string);
                        }
                        prop.SetValue(createDto, value);
                    }
                }
                var dto = await MapEntityCreateDtoToEntity(createDto);
                dtos.Add(dto);
            }

            await _awardRepository.InsertManyAsync(dtos);
        }
        #endregion
    }
}
