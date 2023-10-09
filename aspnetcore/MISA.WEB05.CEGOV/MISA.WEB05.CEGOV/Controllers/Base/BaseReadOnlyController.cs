using Microsoft.AspNetCore.Mvc;
using MISA.WEB05.CEGOV.Application;

namespace MISA.WEB05.CEGOV
{
    public abstract class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        #region Fields
        private readonly IBaseReadOnlyService<TEntityDto> _baseReadOnlyService;
        #endregion

        #region Constructor
        protected BaseReadOnlyController(IBaseReadOnlyService<TEntityDto> baseReadOnlyService)
        {
            _baseReadOnlyService = baseReadOnlyService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <param name="limit">Giới hạn bản ghi lấy ra</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <returns>Các bản ghi thỏa mãn</returns>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            var entityDtos = await _baseReadOnlyService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, entityDtos);
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// <returns>Bản ghi có id bằng id truyền vào</returns>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var entityDto = await _baseReadOnlyService.GetByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, entityDto);
        }

        /// <summary>
        /// Lấy các bản ghi theo list các id
        /// </summary>
        /// <param name="ids">List các id của bản ghi cần lấy</param>
        /// <returns>Các bản ghi có id bằng các id truyền vào</returns>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpGet("Many")]
        public virtual async Task<IActionResult> GetListByIdsAsync([FromQuery] List<Guid> ids)
        {
            var entityDtos = await _baseReadOnlyService.GetListByIdsAsync(ids);
            return StatusCode(StatusCodes.Status200OK, entityDtos);
        }

        #endregion
    }
}
