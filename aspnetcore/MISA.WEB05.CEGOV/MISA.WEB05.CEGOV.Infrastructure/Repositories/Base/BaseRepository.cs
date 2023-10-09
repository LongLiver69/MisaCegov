using Dapper;
using MISA.WEB05.CEGOV.Domain;
using System.Data;

namespace MISA.WEB05.CEGOV.Infrastructure
{
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : IHasKey
    {
        #region Constructor
        public BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task InsertAsync(TEntity entity)
        {
            var procedure = $"Proc_Cegov_InsertOne{TableName}";

            var param = new DynamicParameters();
            var entityProperty = typeof(TEntity).GetProperties();
            // Map các giá trị của newAward vào tên param tương ứng
            foreach (var property in entityProperty)
            {
                var paramName = property.Name;
                var paramValue = property.GetValue(entity);
                param.Add(paramName, paramValue);
            }

            await _uow.Connection.ExecuteAsync(procedure, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="entities">Các bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task InsertManyAsync(List<TEntity> entities)
        {
            // Số lượng bản ghi cần insert
            var recordCount = entities.Count();
            // Số lượng bản ghi insert mỗi lần truy vấn
            int number = 100;
            // Số lượng truy vấn đến db (Nối chuỗi vào câu truy vấn để thêm 100 bản ghi 1 lúc)
            var queryCount = (int)Math.Ceiling((double)recordCount / number);
            var props = typeof(TEntity).GetProperties();

            // Lặp qua từng lần truy vấn
            for (int count = 0; count < queryCount; count++)
            {
                var sql = $"INSERT INTO {TableName} (";

                // Cộng các tên cột vào chuỗi
                for (int c = 0; c < props.Length; c++)
                {
                    var columnName = props[c].Name;
                    sql += $" {columnName},";
                }
                // Xóa ký tự thừa
                sql = sql.Remove(sql.Length - 1) + " ) VALUES";

                var param = new DynamicParameters();

                int recordFrom = count * number;
                int recordTo = (count + 1) * number;
                // Trường hợp lần truy vấn cuối cùng
                if (count == queryCount - 1)
                {
                    recordTo = recordCount;
                }
                // Nối các param vào câu query và add giá trị cho chúng
                for (int i = recordFrom; i < recordTo; i++)
                {
                    sql += " (";
                    for (int j = 0; j < props.Length; j++)
                    {
                        // Lấy giá trị từng trường cần thêm
                        var value = props[j].GetValue(entities[i]);
                        // Tạo tên param thêm index tránh trùng tên
                        var paramName = $"@{props[j].Name}{i}";
                        // Nối vào câu truy vấn
                        sql += $"{paramName}, ";
                        // Add giá trị vào param
                        param.Add(paramName, value);
                    }
                    // Xóa ký tự thừa
                    sql = sql.Remove(sql.Length - 2) + "),";
                }
                // Xóa ký tự thừa
                sql = sql.Remove(sql.Length - 1);

                // Insert nhiều vào db
                await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần sửa</param>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task UpdateAsync(TEntity entity)
        {
            var procedure = $"Proc_Cegov_UpdateOne{TableName}";
            var param = new DynamicParameters();
            var entityProperty = typeof(TEntity).GetProperties();

            // Map các giá trị của newAward vào tên param tương ứng
            foreach (var property in entityProperty)
            {
                var paramName = property.Name;
                var paramValue = property.GetValue(entity);
                param.Add(paramName, paramValue);
            }

            await _uow.Connection.ExecuteAsync(procedure, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">awardId của bản ghi cần xóa</param>
        /// <returns></returns>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task DeleteAsync(Guid id)
        {
            var query = $"DELETE FROM {TableName} WHERE {ColumnIdName} = @Id;";
            var param = new DynamicParameters();
            param.Add("Id", id);

            await _uow.Connection.ExecuteAsync(query, param, transaction: _uow.Transaction);

        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần xóa</param>
        /// <returns></returns>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            var query = $"DELETE FROM {TableName} WHERE {ColumnIdName} IN @Ids;";
            var param = new DynamicParameters();
            param.Add("Ids", ids);

            await _uow.Connection.ExecuteAsync(query, param, transaction: _uow.Transaction);
        }
        #endregion
    }
}
