using AutoMapper;
using Todo.Web.ViewModels;
using TodoDataLibrary.Models;

namespace Todo.Web
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, CategoryModel>();
        }
    }
}
