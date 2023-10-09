namespace MISA.WEB05.CEGOV.Application
{
    public interface IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy các bản ghi theo list các id
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần lấy</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task<IEnumerable<TEntityDto>> GetListByIdsAsync(List<Guid> ids);

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// <returns>Danh hiệu có id bằng id truyền vào</returns>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task<TEntityDto> GetByIdAsync(Guid id);
    }
}
