using AutoMapper;
using Todo.Web.ViewModels;
using TodoDataLibrary.Models;

namespace Todo.Web
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<FullTodoModel, TodoViewModel>()
                .ForMember(dest => dest.Id,
                            opts => opts.MapFrom(src => src.BasicInfo.Id))
                .ForMember(dest => dest.Title,
                            opts => opts.MapFrom(src => src.BasicInfo.Title))
                .ForMember(dest => dest.Description,
                            opts => opts.MapFrom(src => src.BasicInfo.Description))
                .ForMember(dest => dest.StartDate,
                            opts => opts.MapFrom(src => src.BasicInfo.StartDate))
                .ForMember(dest => dest.EndDate,
                            opts => opts.MapFrom(src => src.BasicInfo.EndDate))
                .ForMember(dest => dest.Categories,
                            opts => opts.MapFrom(src => src.Categories))
                .ReverseMap();
        }
    }
}
