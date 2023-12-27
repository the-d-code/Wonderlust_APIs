namespace WONDERLUST_PROJECT_APIs.Helpers
{
    using AutoMapper;
    using WONDERLUST_PROJECT_APIs.Models.DbModels;
    using WONDERLUST_PROJECT_APIs.Models.InputModels;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LoginInputModel, User>().ReverseMap();
            CreateMap<RegisterInputModel, User>().ReverseMap();
            //CreateMap<EventInputModel, Event>().ReverseMap();
            //CreateMap<UserEventInputModel, UserEvent>().ReverseMap();
        }
    }
}
