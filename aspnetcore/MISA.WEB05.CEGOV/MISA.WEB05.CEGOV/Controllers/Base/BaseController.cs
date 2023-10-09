using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB05.CEGOV.Application;

namespace MISA.WEB05.CEGOV
{
    public class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyController<TEntityDto>
    {
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi được thêm mới</param>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpPost]
        public virtual async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            await _baseService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Thêm mới nhiều bản ghi
        /// </summary>
        /// <param name="entityCreateDtos">Bản ghi được thêm mới</param>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpPost("Many")]
        public virtual async Task<IActionResult> InsertManyAsync([FromBody] List<TEntityCreateDto> entityCreateDtos)
        {
            await _baseService.InsertManyAsync(entityCreateDtos);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi được sửa</param>
        /// <param name="entityUpdatedto">đối tượng chứa các giá trị cần sửa</param>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] TEntityUpdateDto entityUpdatedto)
        {
            await _baseService.UpdateAsync(entityUpdatedto);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo
        /// </summary>
        /// <param name="ids">mảng các id của các bản ghi cần xóa</param>
        /// Created by: ntlong ( 19/07/2023 )
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteManyAsync([FromBody] List<Guid> ids)
        {
            await _baseService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
