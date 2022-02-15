using AutoMapper;
using Todo.Web.ViewModels;
using TodoDataLibrary.Models;

namespace Todo.Web
{
    public class BasicTodoProfile : Profile
    {
        public BasicTodoProfile()
        {
            CreateMap<BasicTodoModel, BasicTodoViewModel>().ReverseMap();
        }
    }
}
