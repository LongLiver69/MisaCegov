using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public class AwardEmployeeDetailProfile : Profile
    {
        /// <summary>
        /// Set ánh xạ Dto cho AwardEmployeeDetail
        /// </summary>
        /// Created by: ntlong (22/07/2023)
        public AwardEmployeeDetailProfile()
        {
            CreateMap<AwardEmployeeDetail, AwardEmployeeDetailDto>();
            CreateMap<AwardEmployeeDetailCreateDto, AwardEmployeeDetail>();
            CreateMap<AwardEmployeeDetailUpdateDto, AwardEmployeeDetail>();
        }
    }
}
