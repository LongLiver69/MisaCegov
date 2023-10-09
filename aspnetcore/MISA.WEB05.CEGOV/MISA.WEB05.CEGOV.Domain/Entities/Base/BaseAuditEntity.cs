namespace MISA.WEB05.CEGOV.Domain
{
    public abstract class BaseAuditEntity
    {
        /// <summary>
        /// Ngày được thêm
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Được thêm bởi ai
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày được sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Được sửa bởi ai
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
