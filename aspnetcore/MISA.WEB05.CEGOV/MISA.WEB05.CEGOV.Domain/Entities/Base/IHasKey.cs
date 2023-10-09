namespace MISA.WEB05.CEGOV.Domain
{
    public interface IHasKey
    {
        /// <summary>
        /// Lấy ra key (Id) của entity
        /// </summary>
        /// Created by: ntlong ( 21/07/2023 )
        Guid GetKey();
    }
}
