using Dapper;
using MISA.WEB05.CEGOV.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Reflection.PortableExecutable;
using static Dapper.SqlMapper;

namespace MISA.WEB05.CEGOV.Infrastructure
{
    public class AwardRepository : BaseRepository<Award>, IAwardRepository
    {
        #region Constructor
        public AwardRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods

        /// <summary>
        /// Tìm 1 danh hiệu theo mã danh hiệu
        /// </summary>
        /// <param name="code">mã của danh hiệu cần lấy</param>
        /// <returns>1 danh hiệu có mã bằng mã truyền vào ( có thể null )</returns>
        /// CreatedBy: ntlong ( 19/07/2023 )
        public async Task<Award?> FindByCodeAsync(string code)
        {
            var sql = "SELECT * FROM award WHERE AwardCode = @Code;";
            var param = new DynamicParameters();
            param.Add("Code", code);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<Award>(sql, param, transaction: _uow.Transaction);
            return result;
        }

        /// <summary>
        /// Sửa trường AwardStatus ở nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng các id của các bản ghi cần sửa</param>
        /// <param name="newStatus">Giá trị mới của AwardStatus</param>
        /// Created by: ntlong ( 19/07/2023 )
        public async Task UpdateManyStatusAsync(List<Guid> ids, int newStatus)
        {
            var sql = "UPDATE award SET AwardStatus = @AwardStatus WHERE AwardId IN @Ids";
            var param = new DynamicParameters();
            param.Add("AwardStatus", newStatus);
            param.Add("Ids", ids);

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
        }

        #endregion
    }
}
