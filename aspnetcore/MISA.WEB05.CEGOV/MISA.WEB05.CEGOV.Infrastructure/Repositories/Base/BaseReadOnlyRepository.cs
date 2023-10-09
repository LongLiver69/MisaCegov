using Dapper;
using MISA.WEB05.CEGOV.Domain;
using System.Data;

namespace MISA.WEB05.CEGOV.Infrastructure
{
    public abstract class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        #endregion

        #region Properties
        protected virtual string TableName { get; set; } = typeof(TEntity).Name;
        protected virtual string ColumnIdName { get; set; } = typeof(TEntity).Name + "Id";
        #endregion

        #region Constructor
        protected BaseReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Mảng tất cả bản ghi</returns>
        /// Created by: ntlong ( 13/07/2023 )
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}";
            var entities = await _uow.Connection.QueryAsync<TEntity>(sql, transaction: _uow.Transaction);
            return entities;
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Không tìm thấy theo id " + id);
            }
            return entity;
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy ( có thể trả về null )</returns>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task<TEntity?> FindByIdAsync(Guid id)
        {
            var query = $"SELECT * FROM {TableName} WHERE {ColumnIdName} = @Id;";
            var param = new DynamicParameters();
            param.Add("Id", id);

            var entity = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(query, param, transaction: _uow.Transaction);
            return entity;
        }

        /// <summary>
        /// Lấy nhiều bản ghi theo danh sách các id
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần lấy</param>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids)
        {
            var query = $"SELECT * FROM {TableName} WHERE {ColumnIdName} IN @Ids;";
            var param = new DynamicParameters();
            param.Add("Ids", ids);

            var entities = await _uow.Connection.QueryAsync<TEntity>(query, param, transaction: _uow.Transaction);
            return entities;
        }

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
        public async Task<(int Total, IEnumerable<TEntity> Data)> GetListFPSSAsync(int? page = null, int? size = null, string? search = null, List<string>? searchFields = null, Dictionary<string, object?>? filter = null, string? sortField = null, bool? sortType = null)
        {
            // Tạo truy vấn
            var sql = "";
            var param = new DynamicParameters();

            // Thêm điều kiện tìm kiếm nếu có            
            if (!string.IsNullOrEmpty(search) && searchFields != null && searchFields.Count > 0)
            {
                sql += " WHERE (";

                // Duyệt qua từng trường trong searchFields
                foreach (var field in searchFields)
                {
                    // Thêm điều kiện LIKE cho mỗi trường
                    sql += $" {field} LIKE @search OR";
                    param.Add("search", $"%{search}%");
                }

                // Xóa đi ký tự " OR" cuối cùng
                sql = sql.Remove(sql.Length - 3);

                // Thêm đóng ngoặc đơn
                sql += ")";
            }
            // Thêm điều kiện lọc nếu có
            if (filter != null && filter?.Count > 0)
            {
                // Nếu chưa có điều kiện WHERE thì thêm vào
                if (!sql.Contains("WHERE"))
                {
                    sql += " WHERE";
                }
                else
                {
                    sql += " AND"; // Ngược lại: Thêm AND cho điều kiện tiếp theo
                }

                // Duyệt qua từng cặp key-value trong filter
                foreach (var pair in filter)
                {
                    // Nếu value là null thì thêm điều kiện IS NULL
                    if (pair.Value != null)
                    {
                        sql += $" {pair.Key} = @{pair.Key} AND";
                        param.Add(pair.Key, pair.Value);
                    }
                }
                // Xóa chuỗi "AND" ở cuối nếu có
                sql = sql.Remove(sql.Length - 4);
            }

            // Lấy tổng số bản ghi thỏa mãn điều kiện
            var totalSql = $"SELECT COUNT(*) FROM {TableName}" + sql;
            var total = await _uow.Connection.ExecuteScalarAsync<int>(totalSql, param: param, transaction: _uow.Transaction);

            // Thêm điều kiện sắp xếp nếu có
            if (sortField != null && sortType != null)
            {
                sql += $" ORDER BY {sortField}";
                if (sortType == true)
                {
                    sql += " ASC";
                }
                else
                {
                    sql += " DESC";
                }
            }

            // Thêm điều kiện phân trang
            if (page != null && size != null)
            {
                sql += " LIMIT @offset, @size";
                var offset = size * (page - 1); // offset: vị trí bắt đầu cần lấy
                param.Add("offset", offset, DbType.Int32);
                param.Add("size", size, DbType.Int32);
            }

            // Lấy danh sách bản ghi theo phân trang, lọc, tìm kiếm, sắp xếp
            var dataSql = $"SELECT * FROM {TableName}" + sql;
            var data = await _uow.Connection.QueryAsync<TEntity>(dataSql, param: param, transaction: _uow.Transaction);
            // Trả về kết quả
            return (total, data);
        }
        #endregion
    }
}
