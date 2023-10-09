using MISA.WEB05.CEGOV.Domain;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MISA.WEB05.CEGOV.Infrastructure
{
    public class ExcelWorker : IExcelWorker
    {
        public ExcelWorker()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="tableFormat">Định dạng cho bảng trong excel</param>
        /// <param name="records">Các bản ghi trong file excel được xuất ra</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task<byte[]> ExportToExcel(TableFormat tableFormat, IList<IDictionary<string, object?>> records)
        {
            // Tạo một package Excel mới
            using var package = new ExcelPackage();

            // Tạo một worksheet mới
            var worksheet = package.Workbook.Worksheets.Add(tableFormat.SheetName);

            // Thiết lập tiêu đề cho worksheet
            worksheet.Cells["A1"].Value = tableFormat.TableTitle;
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.Font.Size = 18;
            var cell = worksheet.Cells[1, 1, 1, tableFormat.Columns.Count];
            cell.Merge = true;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Đặt tiêu đề cho các cột
            for (var i = 0; i < tableFormat.Columns.Count; i++)
            {
                // Thêm từ dòng thứ 2, cột thứ 1
                cell = worksheet.Cells[2, i + 1];
                cell.Value = tableFormat.Columns[i].DisplayName;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(1, 73, 204, 144));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 12;
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // Thiết lập căn lề cho cột
                var col = tableFormat.Columns[i];
                var colStyle = worksheet.Column(i + 1).Style;

                if (col.Align.HasFlag(AlignEnum.Left))
                    colStyle.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                else if (col.Align.HasFlag(AlignEnum.Right))
                    colStyle.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                else if (col.Align.HasFlag(AlignEnum.Center))
                    colStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                if (col.Align.HasFlag(AlignEnum.Top))
                    colStyle.VerticalAlignment = ExcelVerticalAlignment.Top;
                else if (col.Align.HasFlag(AlignEnum.Bottom))
                    colStyle.VerticalAlignment = ExcelVerticalAlignment.Bottom;
                else if (col.Align.HasFlag(AlignEnum.Middle))
                    colStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            // Điền dữ liệu từ danh sách bản ghi vào worksheet
            for (var i = 0; i < records.Count; i++)
            {
                // Thêm số thứ tự cho các dòng ở cột đầu tiên
                worksheet.Cells[i + 3, 1].Value = i + 1;

                var record = records[i];

                // Thêm dữ liệu bỏ qua trường đầu tiên ( số thứ tự )
                for (var j = 1; j < tableFormat.Columns.Count; j++)
                {
                    var key = tableFormat.Columns[j].FieldName;

                    // Bắt đầu thêm từ dòng thứ 3, cột thứ 2
                    worksheet.Cells[i + 3, j + 1].Value = record[key];
                }
            }

            worksheet.Cells.AutoFitColumns();

            var file = await package.GetAsByteArrayAsync();

            return file;

        }

        /// <summary>
        /// Lấy ra Lấy thông tin và kiểm tra tính hợp lệ của file
        /// </summary>
        /// <param name="file">File được gửi từ client</param>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task<ImportFileInfo> GetFileInfo(IFormFile file)
        {
            // Tính toán kích thước của file ở dạng có đơn vị
            // Mảng các đơn vị
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB" };
            // Biến lưu chỉ số của đơn vị
            int index = 0;
            // Biến lưu kích thước của File dưới dạng số thực
            double size = (double)file.Length;
            // Lặp cho đến khi kích thước nhỏ hơn 1024 hoặc đã đến đơn vị cuối cùng
            while (size >= 1024 && index < units.Length - 1)
            {
                // Chia kích thước cho 1024 để chuyển sang đơn vị cao hơn
                size /= 1024;
                // Tăng chỉ số của đơn vị lên 1
                index++;
            }
            // Kích thước của File dưới dạng chuỗi có định dạng là số thập phân có 2 chữ số sau dấu phẩy và đơn vị tương ứng
            var fileSize = string.Format("{0:0.00} {1}", size, units[index]);

            // Khởi tạo một đối tượng ImportFileInfo
            var fileInfo = new ImportFileInfo
            {
                // Lấy tên file và kích cỡ file từ IFormFile
                FileName = file.FileName,
                FileSize = fileSize,
                TitleLine = 2,
                IsValid = true
            };

            // Sử dụng thư viện EPPlus để đọc và lấy tên các Sheet
            using (var package = new ExcelPackage())
            {
                // Tải file Excel từ IFormFile vào bộ nhớ
                await package.LoadAsync(file.OpenReadStream());

                // Lấy danh sách các sheet trong file Excel
                var sheets = package.Workbook.Worksheets;

                // Duyệt qua từng sheet và lấy tên của nó
                foreach (var sheet in sheets)
                {
                    fileInfo.Sheets.Add(sheet.Name);
                }
            }

            return fileInfo;
        }

        /// <summary>
        /// Đọc file và map các bản ghi thành List trả về service
        /// </summary>
        /// <param name="file">File được upload</param>
        /// <param name="sheetIndex">Thứ tự của sheet cần check</param>
        /// <param name="titleLine">Thứ tự của dòng tiêu đề</param>
        /// <param name="fieldNames">Các trường của cần import</param>
        /// <returns></returns>
        /// Created by: ntlong ( 15/08/2023 )
        public async Task<List<Dictionary<string, object?>>> ReadImportFile(IFormFile file, int sheetIndex, int titleLine, List<string> fieldNames)
        {
            // Sử dụng thư viện EPPlus để đọc và xử lý file Excel
            using var package = new ExcelPackage();

            // Tải file Excel từ IFormFile vào bộ nhớ
            await package.LoadAsync(file.OpenReadStream());

            // Lấy ra sheet cần nhập khẩu
            var worksheet = package.Workbook.Worksheets[sheetIndex];

            // Lấy số hàng và số cột của file
            int rowCount = worksheet.Dimension.End.Row;
            int columnCount = worksheet.Dimension.End.Column;

            var list = new List<Dictionary<string, object?>>();
            for (int i = titleLine + 1; i <= rowCount; i++)
            {
                var dic = new Dictionary<string, object?>();
                for (int j = 2; j <= columnCount; j++)
                {
                    // Bắt đầu từ dưới dòng tiêu đề, cột thì bỏ qua cột STT đầu tiên
                    var content = worksheet.Cells[i, j].Value;
                    {
                        dic.Add(fieldNames[j - 2], content);
                    };
                }
                list.Add(dic);
            }

            return list;
        }
    }
}
