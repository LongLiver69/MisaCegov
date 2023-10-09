namespace MISA.WEB05.CEGOV.Application
{
    /// <summary>
    /// Đối tượng chứa thông tin để save detail của một master
    /// </summary>
    /// <typeparam name="TCreateDto">Kiểu dữ liệu Create DTO</typeparam>
    /// <typeparam name="TUpdateDto">Kiểu dữ liệu Update DTO</typeparam>
    /// <typeparam name="TKey">Kiểu khóa chính của bản ghi để xóa</typeparam>
    public class SaveDto<TCreateDto, TUpdateDto, TKey>
    {
        /// <summary>
        /// Danh sách detail để thêm mới
        /// </summary>
        public IEnumerable<TCreateDto> Creates { get; set; } = Enumerable.Empty<TCreateDto>();
        /// <summary>
        /// Danh sách detail để sửa
        /// </summary>
        public IEnumerable<TUpdateDto> Updates { get; set; } = Enumerable.Empty<TUpdateDto>();
        /// <summary>
        /// Danh sách id của detail sẽ bị xóa
        /// </summary>
        public IEnumerable<TKey> Deletes { get; set; } = Enumerable.Empty<TKey>();
    }
}
