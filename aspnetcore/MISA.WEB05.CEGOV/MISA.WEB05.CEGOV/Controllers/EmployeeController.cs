using Microsoft.AspNetCore.Mvc;
using MISA.WEB05.CEGOV.Application;

namespace MISA.WEB05.CEGOV
{
    [Route("api/v1/Employees")]
    [ApiController]
    public class EmployeeController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        #region Field
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion
    }
}
