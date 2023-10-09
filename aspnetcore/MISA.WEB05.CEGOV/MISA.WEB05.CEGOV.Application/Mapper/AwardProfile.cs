using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application
{
    public class AwardProfile : Profile
    {
        /// <summary>
        /// Set ánh xạ Dto cho Award
        /// </summary>
        /// Created by: ntlong (22/07/2023)
        public AwardProfile()
        {
            CreateMap<Award, AwardDto>();
            CreateMap<AwardCreateDto, Award>();
            CreateMap<AwardUpdateDto, Award>();
        }
    }
}
