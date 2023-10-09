namespace MISA.WEB05.CEGOV.Domain
{
    public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="entities">Các bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task InsertManyAsync(List<TEntity> entities);

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần sửa</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi 
        /// </summary>
        /// <param name="listIds">Mảng các id của các bản ghi cần xóa</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task DeleteManyAsync(List<Guid> ids);
    }
}
