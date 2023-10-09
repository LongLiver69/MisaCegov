
namespace MISA.WEB05.CEGOV.Domain
{
    public class ImportFileInfo
    {
        // Tên file
        public string FileName { get; set; } = string.Empty;

        // Các sheet có trong file
        public List<string> Sheets { get; set; } = new List<string>();

        // Số thứ tự dòng của dòng tiêu đề
        public int TitleLine { get; set; }

        // Kích cỡ của file
        public string FileSize { get; set; } = string.Empty;

        // Tính hợp lệ của file
        public bool IsValid { get; set; }
    }
}
