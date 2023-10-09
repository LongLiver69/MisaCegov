using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Set ánh xạ Dto cho Employee
        /// </summary>
        /// Created by: ntlong (22/07/2023)
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}
