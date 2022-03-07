using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;
using TodoDataLibrary.Models;

namespace Todo.Web.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public TodoViewModel Todo { get; set; }
        [BindProperty]
        public CategoryViewModel Category { get; set; } = new CategoryViewModel();
        public List<CategoryViewModel> InitialCategories = new List<CategoryViewModel>();
        public List<CategoryModel> ProcessedCategories = new List<CategoryModel>();


        public EditModel(IDatabaseData db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void OnGet(int id)
        {
            Category.Name = String.Empty;
            Todo = _mapper.Map<TodoViewModel>(_db.GetTodo(id));
            int total = 0;
            foreach (var category in Todo.Categories)
            {
                total++;
                if (total == Todo.Categories.Count)
                {
                    Category.Name += category.Name;
                }
                else
                {
                    Category.Name += category.Name + ", ";
                }
            }
        }

        public IActionResult OnPost()
        {
            List<string> data = Category.Name.Split(',').ToList();
            foreach (var category in data)
            {
                InitialCategories.Add(new CategoryViewModel { Name = category.Trim() });
            }
            foreach (var category in InitialCategories)
            {
                var CategoryModel = _mapper.Map<CategoryModel>(category);
                ProcessedCategories.Add(CategoryModel);
            }
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            else
            {
                _db.UpdateTodo(Todo.Id, Todo.Title, Todo.Description, Todo.StartDate, Todo.EndDate, ProcessedCategories);
                return RedirectToPage("/Index");
            }
        }
    }
}
