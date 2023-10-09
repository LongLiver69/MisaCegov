namespace MISA.WEB05.CEGOV.Domain
{
    public interface IAwardManager
    {
        /// <summary>
        /// Check xem id truyền vào đã tồn tại chưa
        /// </summary>
        /// <param name="id">Id danh hiệu cần check</param>
        /// Created by: ntlong ( 21/07/2023 )
        Task CheckAwardExistById(Guid id);

        /// <summary>
        /// Check xem mã danh hiệu truyền vào đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã danh hiệu cần check</param>
        /// Created by: ntlong ( 21/07/2023 )
        Task CheckAwardExistByCode(string code, string? oldCode = null);

        /// <summary>
        /// Kiểm tra tính hợp lệ của bản ghi nhập khẩu
        /// </summary>
        /// <param name="record">Bản ghi cần kiểm tra</param>
        /// <returns>True/False</returns>
        /// Created by: ntlong ( 06/08/2023 )
        Task<bool> CheckImportRecordValidity(Dictionary<string, object?> record);

        /// <summary>
        /// Convert dữ liệu từ dạng Text sang Enum qua attribute Description
        /// </summary>
        /// <param name="text">Text cần chuyển đổi</param>
        /// <returns>Giá trị Enum tương ứng</returns>
        /// Created by: ntlong ( 06/08/2023 )
        object? ConvertTextToEnum(string enumType, string? text);
    }
}
