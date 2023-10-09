using System.Resources;
using MISA.WEB05.CEGOV.Domain.Resources;

namespace MISA.WEB05.CEGOV.Domain
{
    public class AwardManager : IAwardManager
    {
        #region Fields
        private readonly IAwardRepository _awardRepository;
        #endregion

        #region Constructor
        public AwardManager(IAwardRepository awardRepository)
        {
            _awardRepository = awardRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check xem id truyền vào đã tồn tại chưa
        /// </summary>
        /// <param name="id">Id danh hiệu cần check</param>
        /// <returns>Trả về true nếu id đã tồn tại và ngược lại</returns>
        /// Created by: ntlong ( 21/07/2023 )
        public async Task CheckAwardExistById(Guid id)
        {
            var awardExist = await _awardRepository.FindByIdAsync(id);
            if (awardExist != null)
            {
                throw new ConflictException("Id đã tồn tại");
            }
        }

        /// <summary>
        /// Check xem mã danh hiệu truyền vào đã tồn tại chưa
        /// Trường hợp update thì check xem mã mới có bị trùng với các mã khác ngoài mã cũ không
        /// </summary>
        /// <param name="code">Mã danh hiệu cần check</param>
        /// <param name="oldCode">Mã danh hiệu cũ ( trong trường hợp update )</param>
        /// <exception cref="ConflictException">Throw ra khi mã đã tồn tại</exception>
        /// Created by: ntlong ( 21/07/2023 )
        public async Task CheckAwardExistByCode(string code, string? oldCode = null)
        {
            var awardExist = await _awardRepository.FindByCodeAsync(code);
            if (awardExist != null && code != oldCode)
            {
                throw new ConflictException("Mã danh hiệu đã tồn tại");
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của bản ghi nhập khẩu
        /// </summary>
        /// <param name="record">Bản ghi cần kiểm tra</param>
        /// <returns>True/False</returns>
        /// Created by: ntlong ( 06/08/2023 )
        public async Task<bool> CheckImportRecordValidity(Dictionary<string, object?> record)
        {
            var resource = new ResourceManager(typeof(AwardResource));
            foreach (var key in record.Keys)
            {
                // Các giá trị không được null hoặc là chuỗi rỗng
                var value = record[key] as string;
                if (string.IsNullOrEmpty(value))
                {
                    return false;
                }
                if (key == "AwardCode")
                {
                    var awardExist = await _awardRepository.FindByCodeAsync(value);
                    if (awardExist != null) // Đã tồn tại
                    {
                        return false;
                    }
                }
                else if (key == "AwardObject")
                {
                    if (value != resource.GetString("Individual") && value != resource.GetString("Group") && value != resource.GetString("IndividualAndGroup") && value != resource.GetString("Family"))
                    {
                        return false;
                    }
                }
                else if (key == "AwardLevel")
                {
                    if (value != resource.GetString("National") && value != resource.GetString("Province") && value != resource.GetString("District") && value != resource.GetString("Commune"))
                    {
                        return false;
                    }
                }
                else if (key == "AwardType")
                {
                    if (value != resource.GetString("Frequent") && value != resource.GetString("Periodic") && value != resource.GetString("FrequentAndPeriodic"))
                    {
                        return false;
                    }
                }
                else if (key == "AwardStatus")
                {
                    if (value != resource.GetString("Active") && value != resource.GetString("Inactive"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Convert dữ liệu từ dạng Text sang Enum qua attribute Description
        /// </summary>
        /// <param name="text">Text cần chuyển đổi</param>
        /// <returns>Giá trị Enum tương ứng</returns>
        /// Created by: ntlong ( 06/08/2023 )
        public object? ConvertTextToEnum(string enumType, string? text)
        {
            var resource = new ResourceManager(typeof(AwardResource));

            if (enumType == "AwardObjectEnum")
            {
                if (text == resource.GetString("Individual"))
                {
                    return AwardObjectEnum.Individual;
                }
                else if (text == resource.GetString("Group"))
                {
                    return AwardObjectEnum.Group;
                }
                else if (text == resource.GetString("IndividualAndGroup"))
                {
                    return AwardObjectEnum.IndividualAndGroup;
                }
                else if (text == resource.GetString("Family"))
                {
                    return AwardObjectEnum.Family;
                }
                else { return null; }

            }
            else if (enumType == "AwardLevelEnum")
            {
                if (text == resource.GetString("National"))
                {
                    return AwardLevelEnum.National;
                }
                else if (text == resource.GetString("Province"))
                {
                    return AwardLevelEnum.Province;
                }
                else if (text == resource.GetString("District"))
                {
                    return AwardLevelEnum.District;
                }
                else if (text == resource.GetString("Commune"))
                {
                    return AwardLevelEnum.Commune;
                }
                else { return null; }
            }
            else if (enumType == "AwardTypeEnum")
            {
                if (text == resource.GetString("Frequent"))
                {
                    return AwardTypeEnum.Frequent;
                }
                else if (text == resource.GetString("Periodic"))
                {
                    return AwardTypeEnum.Periodic;
                }
                else if (text == resource.GetString("FrequentAndPeriodic"))
                {
                    return AwardTypeEnum.FrequentAndPeriodic;
                }
                else { return null; }
            }
            else if (enumType == "AwardStatusEnum")
            {
                if (text == resource.GetString("Active"))
                {
                    return AwardStatusEnum.Active;
                }
                else if (text == resource.GetString("Inactive"))
                {
                    return AwardStatusEnum.Inactive;
                }
                else { return null; }
            }
            else
            {
                return text;
            }
        }

        #endregion
    }
}
