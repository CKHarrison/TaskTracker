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
        public void OnGet(int id)
        {

            var todo = _db.GetTodo(id);
            Todo = _mapper.Map<TodoViewModel>(todo);
        }
    }
}
