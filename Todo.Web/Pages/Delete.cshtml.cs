using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;
using TodoDataLibrary.Models;

namespace Todo.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;

        public TodoViewModel Todo { get; set; }
        public DeleteModel(IDatabaseData db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void OnGet(int id)
        {
            Todo = _mapper.Map<TodoViewModel>(_db.GetTodo(id));
        }

        public IActionResult OnPost(int id)
        {
            Todo = _mapper.Map<TodoViewModel>(_db.GetTodo(id));
            _db.DeleteTodo(_mapper.Map<FullTodoModel>(Todo));
            return RedirectToPage("./Index");
        }
    }
}
