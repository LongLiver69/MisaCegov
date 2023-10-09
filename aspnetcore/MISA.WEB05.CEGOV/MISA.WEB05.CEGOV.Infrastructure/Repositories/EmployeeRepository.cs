using Dapper;
using MISA.WEB05.CEGOV.Domain;
using static Dapper.SqlMapper;

namespace MISA.WEB05.CEGOV.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods

        /// <summary>
        /// Tìm 1 cá nhân theo mã cá nhân
        /// </summary>
        /// <param name="code">mã của cá nhân cần lấy</param>
        /// <returns>1 cá nhân có mã bằng mã truyền vào ( có thể null )</returns>
        /// CreatedBy: ntlong ( 19/07/2023 )
        public async Task<Employee?> FindByCodeAsync(string code)
        {
            var sql = "SELECT * FROM award WHERE EmployeeCode = @Code;";
            var param = new DynamicParameters();
            param.Add("Code", code);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<Employee>(sql, param, transaction: _uow.Transaction);
            return result;
        }

        #endregion
    }
}
