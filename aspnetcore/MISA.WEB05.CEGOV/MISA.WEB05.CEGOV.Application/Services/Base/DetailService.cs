using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public abstract class DetailService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IUnitOfWork _uow;
        #endregion
        #region Constructor
        protected DetailService(IBaseRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper)
        {
            _repository = repository;
            _uow = unitOfWork;
        }
        #endregion

        #region LoadDetail Methods
        /// <summary>
        /// Thực hiện load detail của một entity master
        /// </summary>
        /// <param name="entityDto">Bản ghi master</param>
        protected abstract Task LoadDetail(TEntityDto entityDto);
        /// <summary>
        /// Thực hiện load detail cho một danh sách master
        /// </summary>
        /// <param name="entityDtos">Danh sách bản ghi master</param>
        protected abstract Task LoadDetail(IEnumerable<TEntityDto> entityDtos);
        #endregion

        #region Override ReadOnly Methods
        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: ntlong ( 19/07/2023 )
        public override async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entityDtos = await base.GetAllAsync();
            await LoadDetail(entityDtos);
            return entityDtos;
        }

        /// <summary>
        /// Lấy 1 bản ghi 
        /// </summary>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: ntlong ( 19/07/2023 )
        public override async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            var entityDto = await base.GetByIdAsync(id);
            await LoadDetail(entityDto);
            return entityDto;
        }

        /// <summary>
        /// Lấy ra các bản ghi theo list các id
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần lấy</param>
        /// CreatedBy: ntlong ( 19/07/2023 )
        public override async Task<IEnumerable<TEntityDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var entityDtos = await base.GetListByIdsAsync(ids);
            await LoadDetail(entityDtos);
            return entityDtos;
        }
        #endregion

        #region Override Save Methods

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        public override async Task DeleteAsync(Guid id)
        {
            var check = false;
            try
            {
                if (_uow.Transaction == null)
                {
                    check = true;
                    await _uow.BeginTransactionAsync();
                }
                // Xóa detail
                await DeleteDetailAsync(id);
                // Xóa master
                await base.DeleteAsync(id);

                if (check) await _uow.CommitAsync();
            }
            catch (Exception)
            {
                if (check) await _uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của các bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        public override async Task DeleteManyAsync(List<Guid> ids)
        {
            var check = false;
            try
            {
                if (_uow.Transaction == null)
                {
                    check = true;
                    await _uow.BeginTransactionAsync();
                }
                // Xóa detail
                await DeleteManyDetailAsync(ids);
                // Xóa master
                await base.DeleteManyAsync(ids);

                if (check) await _uow.CommitAsync();
            }
            catch (Exception)
            {
                if (check) await _uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        public override async Task InsertAsync(TEntityCreateDto createDto)
        {
            var check = false;
            try
            {
                if (_uow.Transaction == null)
                {
                    check = true;
                    await _uow.BeginTransactionAsync();
                }
                // Map từ createDto sang entity
                var entity = await MapEntityCreateDtoToEntity(createDto);

                // Thêm master trước
                await _repository.InsertAsync(entity);
                // Thêm detail sau
                await InsertDetailAsync(createDto, entity);

                if (check) await _uow.CommitAsync();
            }
            catch (Exception)
            {
                if (check) await _uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="entityCreateDtos">Dữ liệu các bản ghi cần thêm</param>
        /// Created by: ntlong ( 19/07/2023 )
        public override async Task InsertManyAsync(List<TEntityCreateDto> createDtos)
        {
            var check = false;
            try
            {
                if (_uow.Transaction == null)
                {
                    check = true;
                    await _uow.BeginTransactionAsync();
                }
                var data = new List<(TEntityCreateDto, TEntity)>();
                var entities = new List<TEntity>();

                foreach (var createDto in createDtos)
                {
                    // Map từ createDto sang entity
                    var entity = await MapEntityCreateDtoToEntity(createDto);
                    entities.Add(entity);
                    data.Add((createDto, entity));
                }

                // Thêm master trước
                await _repository.InsertManyAsync(entities);
                // Thêm detail sau
                await InsertManyDetailAsync(data);

                if (check) await _uow.CommitAsync();
            }
            catch (Exception)
            {
                if (check) await _uow.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region SaveDetail Methods
        /// <summary>
        /// Thêm detail vào db sau khi tạo master từ createDto
        /// </summary>
        /// <param name="createDto">Dto chứa dữ liệu tạo master và detail</param>
        /// <param name="master">Entity master vừa thêm</param>
        /// Created by: ntlong ( 12/09/2023 )
        public abstract Task InsertDetailAsync(TEntityCreateDto createDto, TEntity master);

        /// <summary>
        /// Thêm detail vào db sau khi tạo nhiều master từ createDtos
        /// </summary>
        /// <param name="data">Dữ liệu vừa thêm master theo cặp entity và createDto</param>
        /// Created by: ntlong ( 12/09/2023 )
        public abstract Task InsertManyDetailAsync(List<(TEntityCreateDto CreateDto, TEntity Entity)> data);

        /// <summary>
        /// Cập nhật detail sau khi cập nhật master
        /// </summary>
        /// <param name="updateDto">Dto chứa master và detail cần update</param>
        /// <param name="oldMaster">Dữ liệu master cũ</param>
        /// <param name="newMaster">Dữ liệu master mới</param>
        /// Created by: ntlong ( 12/09/2023 )
        public abstract Task UpdateDetailAsync(TEntityUpdateDto updateDto, TEntity oldMaster, TEntity newMaster);

        /// <summary>
        /// Cập nhật nhiều detail sau khi cập nhật master
        /// </summary>
        /// <param name="updateDtos">Dto chứa dữ liệu cập nhật master và detail</param>
        /// Created by: ntlong ( 12/09/2023 )
        public abstract Task UpdateManyDetailAsync(List<TEntityUpdateDto> updateDtos);

        /// <summary>
        /// Xóa detail trước khi xóa master
        /// </summary>
        /// <param name="masterKey">Khóa của master</param>
        /// Created by: ntlong ( 12/09/2023 )
        public abstract Task DeleteDetailAsync(Guid masterKey);

        /// <summary>
        /// Thực hiện xóa detail trước khi xóa nhiều master
        /// </summary>
        /// <param name="masterKeys">Khóa chính của các master</param>
        /// Created by: ntlong ( 12/09/2023 )
        public abstract Task DeleteManyDetailAsync(List<Guid> masterKeys);
        #endregion        
    }
}
