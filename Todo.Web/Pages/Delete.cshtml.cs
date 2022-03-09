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
    public class DeleteModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public string UserId { get; set; }

        public TodoViewModel Todo { get; set; }
        public DeleteModel(IDatabaseData db, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }
        public void OnGet(int id)
        {
            Todo = _mapper.Map<TodoViewModel>(_db.GetTodo(id));
        }

        public IActionResult OnPost(int id)
        {
            Todo = _mapper.Map<TodoViewModel>(_db.GetTodo(id));
            UserId = _userManager.GetUserId(User);
            _db.DeleteTodo(_mapper.Map<FullTodoModel>(Todo), UserId);
            return RedirectToPage("./Index");
        }
    }
}
