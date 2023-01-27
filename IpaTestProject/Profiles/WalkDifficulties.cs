using AutoMapper;

namespace IpaTestProject.Profiles
{
    public class WalkDifficulties : Profile
    {
        public WalkDifficulties()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
                .ReverseMap();
        }
    }
}
