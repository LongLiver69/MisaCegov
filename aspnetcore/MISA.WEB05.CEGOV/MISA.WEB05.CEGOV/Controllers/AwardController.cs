using Microsoft.AspNetCore.Mvc;
using MISA.WEB05.CEGOV.Application;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV
{
    [Route("api/v1/Awards")]
    [ApiController]
    public class AwardController : BaseController<AwardDto, AwardCreateDto, AwardUpdateDto>
    {
        #region Field
        private readonly IAwardService _awardService;
        #endregion


        #region Constructor
        public AwardController(IAwardService awardService) : base(awardService)
        {
            _awardService = awardService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sửa AwardStatus của nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng các id của các bản ghi được sửa</param>
        /// <param name="newStatus">AwardStatus mới</param>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpPut("Status")]
        public async Task<IActionResult> UpdateManyStatusAsync([FromBody] List<Guid> ids, [FromQuery] int newStatus)
        {
            await _awardService.UpdateManyStatusAsync(ids, newStatus);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Lấy danh sách bản ghi bao gồm phân trang, lọc, tìm kiếm, sắp xếp
        /// Ý nghĩa "FPSS": Filter-Paging-Search-Sort
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
        [HttpGet("ListFPSS")]
        public async Task<IActionResult> GetListFPSSAsync([FromQuery] int? page = null, [FromQuery] int? size = null, [FromQuery] string? search = null, [FromQuery] List<string>? searchFields = null, AwardObjectEnum? filteredObject = null, AwardLevelEnum? filteredLevel = null, AwardTypeEnum? filteredType = null, AwardStatusEnum? filteredStatus = null, [FromQuery] string? sortField = null, [FromQuery] bool? sortType = null)
        {
            var (total, data) = await _awardService.GetListFPSSAsync(page, size, search, searchFields, filteredObject, filteredLevel, filteredType, filteredStatus, sortField, sortType);

            var result = new
            {
                total,
                data
            };
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="tableFormat">Cấu trúc bảng được xuất khẩu</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        [HttpPost("Exportation")]
        public async Task<IActionResult> ExportToExcel([FromBody] TableFormat tableFormat)
        {
            // Thiết lập các thông tin phản hồi
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = await _awardService.ExportToExcel(tableFormat);

            return File(file, contentType, "xxx.xlsx");
        }

        /// <summary>
        /// Lấy ra file nhập khẩu mẫu
        /// </summary>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        [HttpGet("SampleImportFile")]
        public IActionResult GetSampleImportFile()
        {
            // Lấy đường dẫn đến file trong wwwroot
            var filePath = "~/Excel/SampleImportFile.xlsx";
            // Thiết lập các thông tin phản hồi
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(filePath, contentType, "xxx.xlsx");
        }

        /// <summary>
        /// Lấy thông tin và kiểm tra tính hợp lệ của file
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <returns></returns>
        /// Created by: ntlong ( 23/08/2023 )
        [HttpPost("FileInfo")]
        public async Task<IActionResult> GetFileInfo([FromForm] IFormFile file)
        {

            var fileInfo = await _awardService.GetFileInfo(file);

            return Ok(fileInfo);
        }

        /// <summary>
        /// Lấy ra số bản ghi hợp lệ và không hợp lệ của file được upload
        /// </summary>
        /// Các param bên dưới được truyền bên trong 1 FormData gửi từ bên client
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        [HttpPost("FileChecking")]
        public async Task<IActionResult> CheckImportFile([FromForm] IFormFile file, [FromForm] int sheetIndex, [FromForm] int titleLine)
        {
            var result = await _awardService.CheckImportFile(file, sheetIndex, titleLine);
            return Ok(result.DataStatistics);
        }

        /// <summary>
        /// Nhập dữ liệu từ file vào database
        /// </summary>
        /// Các param bên dưới được truyền bên trong 1 FormData gửi từ bên client
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        [HttpPost("Importation")]
        public async Task<IActionResult> ImportExcel([FromForm] IFormFile file, [FromForm] int sheetIndex, [FromForm] int titleLine)
        {
            await _awardService.ImportExcel(file, sheetIndex, titleLine);
            return Ok();
        }
        #endregion
    }
}
