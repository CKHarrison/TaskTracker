using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;

namespace Todo.Web.Pages
{
    public class CreateTodoModel : PageModel
    {
        private readonly IDatabaseData _db;
        public TodoViewModel Todo { get; set; } = new TodoViewModel();

        public CreateTodoModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                return RedirectToPage("Index");
            } else
            {
                return Page();
            }
        }
    }
}
