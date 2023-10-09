using Microsoft.AspNetCore.Http;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public interface IAwardService : IBaseService<AwardDto, AwardCreateDto, AwardUpdateDto>
    {
        /// <summary>
        /// Ánh xạ AwardDto sang AwardDisplayDto
        /// </summary>
        /// <param name="awardDto">Đối tượng AwardUpdateDto</param>
        /// <returns>Đối tượng AwardDisplayDto</returns>
        /// Created by: ntlong ( 23/07/2023 )
        AwardDisplayDto MapEntityDtoToEntityDisplayDto(AwardDto awardDto);

        /// <summary>
        /// Sửa trường AwardStatus ở nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng các id của các bản ghi cần sửa</param>
        /// <param name="newStatus">Giá trị mới của AwardStatus</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task UpdateManyStatusAsync(List<Guid> ids, int newStatus);

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
        Task<(int Total, List<AwardDto> Data)> GetListFPSSAsync(int? page = null, int? size = null, string? search = null, List<string>? searchFields = null, AwardObjectEnum? filteredObject = null, AwardLevelEnum? filteredLevel = null, AwardTypeEnum? filteredType = null, AwardStatusEnum? filteredStatus = null, string? sortField = null, bool? sortType = null);

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        Task<byte[]> ExportToExcel(TableFormat tableFormat);

        /// <summary>
        /// Lấy ra Lấy thông tin và kiểm tra tính hợp lệ của file
        /// </summary>
        /// <param name="file">File được gửi từ client</param>
        /// Created by: ntlong ( 15/08/2023 )
        Task<ImportFileInfo> GetFileInfo(IFormFile file);

        /// <summary>
        /// Lấy ra số bản ghi hợp lệ và không hợp lệ của file được upload
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        Task<(ImportDataStatistics DataStatistics, List<Dictionary<string, object?>> ValidList)> CheckImportFile(IFormFile file, int sheetIndex, int titleLine);

        /// <summary>
        /// Nhập dữ liệu từ file vào database
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        Task ImportExcel(IFormFile file, int sheetIndex, int titleLine);
    }
}
