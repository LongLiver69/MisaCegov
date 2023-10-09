using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyService<TEntity, TEntityDto>, IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region Constructor
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task InsertAsync(TEntityCreateDto entityCreateDto)
        {
            // Validate nghiệp vụ
            var entity = await MapEntityCreateDtoToEntity(entityCreateDto);
            // Insert vào db
            await _baseRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="entityCreateDtos">Dữ liệu các bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task InsertManyAsync(List<TEntityCreateDto> entityCreateDtos)
        {
            var entities = new List<TEntity>();

            foreach (var createDto in entityCreateDtos)
            {
                // Validate nghiệp vụ
                var entity = await MapEntityCreateDtoToEntity(createDto);
                entities.Add(entity);
            }
            // Insert vào db
            await _baseRepository.InsertManyAsync(entities);
        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Dữ liệu bản ghi cần sửa</param>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            // Validate nghiệp vụ
            var entity = await MapEntityUpdateDtoToEntity(entityUpdateDto);
            // Insert vào db
            await _baseRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task DeleteAsync(Guid id)
        {
            // Check xem bản ghi có tồn tại không
            await _baseRepository.GetByIdAsync(id);
            // Xóa trong db
            await _baseRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            // Truyền list rỗng
            if (ids.Count == 0)
            {
                throw new Exception("Không được truyền danh sách rỗng");
            }
            // Check xem các bản ghi cần xóa có đủ trong db không
            var awards = await _baseRepository.GetListByIdsAsync(ids);
            if (awards.ToList().Count < ids.Count)
            {
                throw new Exception("Không thể xóa");
            }
            // Xóa trong db
            await _baseRepository.DeleteManyAsync(ids);
        }

        /// <summary>
        /// Ánh xạ EntityCreateDto sang Entity
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng EntityCreateDto</param>
        /// <returns>Đối tượng Entity</returns>
        /// Created by: ntlong ( 23/07/2023 )
        public abstract Task<TEntity> MapEntityCreateDtoToEntity(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Ánh xạ EntityUpdateDto sang Entity
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng EntityUpdateDto</param>
        /// <returns>Đối tượng Entity</returns>
        /// Created by: ntlong ( 23/07/2023 )
        public abstract Task<TEntity> MapEntityUpdateDtoToEntity(TEntityUpdateDto entityUpdateDto);

        #endregion
    }
}
