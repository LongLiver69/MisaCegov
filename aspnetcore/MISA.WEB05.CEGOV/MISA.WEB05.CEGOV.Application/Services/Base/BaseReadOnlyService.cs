using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public abstract class BaseReadOnlyService<TEntity, TEntityDto> : IBaseReadOnlyService<TEntityDto>
    {
        #region Fields
        protected readonly IBaseReadOnlyRepository<TEntity> _baseReadOnlyRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        protected BaseReadOnlyService(IBaseReadOnlyRepository<TEntity> baseReadOnlyRepository, IMapper mapper)
        {
            _baseReadOnlyRepository = baseReadOnlyRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = await _baseReadOnlyRepository.GetAllAsync();
            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);
            return entityDtos;
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// <returns>1 bản ghi có Id bằng id truyền vào</returns>
        /// Created by: ntlong ( 19/07/2023 )
        public virtual async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            var entity = await _baseReadOnlyRepository.GetByIdAsync(id);
            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        /// <summary>
        /// Lấy ra các bản ghi theo list các id
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần lấy</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        public virtual async Task<IEnumerable<TEntityDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var entities = await _baseReadOnlyRepository.GetListByIdsAsync(ids);
            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);
            return entityDtos;
        }
        #endregion
    }
}
