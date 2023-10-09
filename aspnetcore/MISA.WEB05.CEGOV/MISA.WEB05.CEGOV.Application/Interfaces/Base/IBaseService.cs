namespace MISA.WEB05.CEGOV.Application
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi cần thêm</param>
        /// <returns>Bản ghi vừa thêm mới</returns>
        /// Created by: ntlong ( 19/07/2023 )
        Task InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="entityCreateDtos">Dữ liệu các bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task InsertManyAsync(List<TEntityCreateDto> entityCreateDtos);

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Bản ghi sau khi sửa</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task UpdateAsync(TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng các id của các bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task DeleteManyAsync(List<Guid> ids);
    }
}
