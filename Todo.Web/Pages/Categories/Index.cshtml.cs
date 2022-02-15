using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;

namespace Todo.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public IndexModel(IDatabaseData db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void OnGet()
        {
            Categories = _mapper.Map<List<CategoryViewModel>>(_db.GetAllCategories());
        }
    }
}
