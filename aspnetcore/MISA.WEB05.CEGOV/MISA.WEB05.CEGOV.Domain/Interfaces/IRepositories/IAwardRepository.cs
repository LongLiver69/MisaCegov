namespace MISA.WEB05.CEGOV.Domain
{
    public interface IAwardRepository : IBaseRepository<Award>
    {
        /// <summary>
        /// Lấy 1 danh hiệu theo mã danh hiệu
        /// </summary>
        /// <param name="code">Mã danh hiệu cần lấy</param>
        /// <returns>Danh hiệu có mã bằng code truyền vào ( có thể null )</returns>
        /// CreatedBy: ntlong ( 19/07/2023 )
        Task<Award?> FindByCodeAsync(string code);

        /// <summary>
        /// Sửa trường AwardStatus ở nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng các id của các bản ghi cần sửa</param>
        /// <param name="newStatus">Giá trị mới của AwardStatus</param>
        /// Created by: ntlong ( 19/07/2023 )
        Task UpdateManyStatusAsync(List<Guid> ids, int newStatus);

    }
}
