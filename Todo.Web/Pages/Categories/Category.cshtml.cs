using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;

namespace Todo.Web.Pages.Categories
{
    public class CategoryModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;

        public CategoryViewModel Category { get; set; }
        public List<BasicTodoViewModel> Todos { get; set; } = new List<BasicTodoViewModel>();
        public CategoryModel(IDatabaseData db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void OnGet(int id)
        {
            Todos = _mapper.Map<List<BasicTodoViewModel>>(_db.GetTodoByCategory(id));
            Category = _mapper.Map<CategoryViewModel>(_db.GetCategory(id));
        }
    }
}
