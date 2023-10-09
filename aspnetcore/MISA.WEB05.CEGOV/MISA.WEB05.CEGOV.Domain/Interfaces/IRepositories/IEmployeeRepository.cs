namespace MISA.WEB05.CEGOV.Domain
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy 1 cá nhân theo mã cá nhân
        /// </summary>
        /// <param name="code">Mã cá nhân cần lấy</param>
        /// <returns>Danh hiệu có mã bằng code truyền vào ( có thể null )</returns>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task<Employee?> FindByCodeAsync(string code);
    }
}
