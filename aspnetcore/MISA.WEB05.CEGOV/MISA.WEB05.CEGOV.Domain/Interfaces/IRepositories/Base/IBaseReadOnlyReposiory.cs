namespace MISA.WEB05.CEGOV.Domain
{
    public interface IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// Created by: ntlong ( 13/07/2023 )
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy danh sách bản ghi bao gồm phân trang, lọc, tìm kiếm, sắp xếp
        /// </summary>
        /// <param name="page">Số thứ tự của trang cần lấy</param>
        /// <param name="size">Số lượng tối đa một trang</param>
        /// <param name="search">Chuỗi tìm kiếm</param>
        /// <param name="searchFields">Danh sách các trường cần tìm kiếm</param>
        /// <param name="filter">Bộ lọc: Key là trường cần lọc, Value là giá trị lọc</param>
        /// <param name="sortField">Trường được sắp xếp</param>
        /// <param name="sortType">Kiểu sắp xếp: true là tăng dần, false là giảm dần</param>
        /// <returns>Tổng số và danh sách bản ghi theo trang cần hiển thị</returns>
        /// Created by: ntlong ( 25/07/2023 )
        Task<(int Total, IEnumerable<TEntity> Data)> GetListFPSSAsync(int? page = null, int? size = null, string? search = null, List<string>? searchFields = null, Dictionary<string, object?>? filter = null, string? sortField = null, bool? sortType = null);

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy nhiều bản ghi theo danh sách các id
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần lấy</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids);

        /// <summary>
        /// Lấy 1 bản ghi theo id ( có thể trả về null )
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task<TEntity?> FindByIdAsync(Guid id);
    }
}
