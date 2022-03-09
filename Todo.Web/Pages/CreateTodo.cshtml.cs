using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;
using TodoDataLibrary.Models;

namespace Todo.Web.Pages
{
    [Authorize]
    public class CreateTodoModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public string UserId { get; set; }

        [BindProperty]
        public TodoViewModel Todo { get; set; } = new TodoViewModel();
        [BindProperty]
        public CategoryViewModel Categories { get; set; }
        public List<CategoryViewModel> InitialCategories { get; set; } = new List<CategoryViewModel>();
        public List<CategoryModel> ProcessedCategories { get; set; } = new List<CategoryModel>();

        public CreateTodoModel(IDatabaseData db, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            List<string> data = Categories.Name.Split(',').ToList();
            foreach (var category in data)
            {
               InitialCategories.Add(new CategoryViewModel {  Name = category.Trim() });
            }
            foreach (var category in InitialCategories)
            {
                var CategoryModel = _mapper.Map<CategoryModel>(category);
                ProcessedCategories.Add(CategoryModel);
            }
            if(Todo.EndDate < Todo.StartDate)
            {
                ModelState.AddModelError("Invalid End Date", "End Date cannot be before Start Date");
            }
            
            if(ModelState.IsValid == false)
            {
                return Page();
            } else
            {
                UserId = _userManager.GetUserId(User);
                _db.CreateTodo(Todo.Title, Todo.Description, Todo.StartDate, Todo.EndDate, ProcessedCategories, UserId);
                return RedirectToPage("/Index");
            }
        }
    }
}
