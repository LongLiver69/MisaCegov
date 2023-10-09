using Microsoft.AspNetCore.Http;

namespace MISA.WEB05.CEGOV.Domain
{
    public interface IExcelWorker
    {
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="tableFormat">Định dạng cho bảng trong excel</param>
        /// <param name="records">Các bản ghi trong file excel được xuất ra</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        Task<byte[]> ExportToExcel(TableFormat tableFormat, IList<IDictionary<string, object?>> records);

        /// <summary>
        /// Lấy ra Lấy thông tin và kiểm tra tính hợp lệ của file
        /// </summary>
        /// <param name="file">File được gửi từ client</param>
        /// Created by: ntlong ( 15/08/2023 )
        Task<ImportFileInfo> GetFileInfo(IFormFile file);

        /// <summary>
        /// Đọc file và map các bản ghi thành List trả về service
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <param name="fieldName">Các trường của cần import</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        Task<List<Dictionary<string, object?>>> ReadImportFile(IFormFile file, int sheetIndex, int titleLine, List<string> fieldNames);
    }
}
