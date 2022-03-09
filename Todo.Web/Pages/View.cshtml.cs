using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;

namespace Todo.Web.Pages
{
    public class ViewModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;

        public TodoViewModel Todo{ get; set; }

        public ViewModel(IDatabaseData db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IActionResult OnGet(int id)
        {

            var todo = _db.GetTodo(id);
            if(todo.BasicInfo == null)
            {
                return RedirectToPage("/shared/_NotFound");
            }
             Todo = _mapper.Map<TodoViewModel>(todo);
            return Page();
        }
    }
}
