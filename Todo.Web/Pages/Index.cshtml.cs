using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.Utilities;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;
using TodoDataLibrary.Models;

namespace Todo.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public List<BasicTodoViewModel> Todos;
        public string UserId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDatabaseData db, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            
            UserId = _userManager.GetUserId(User);
            Todos = _mapper.Map<List<BasicTodoViewModel>>(_db.GetAllUserTodos(UserId));
        }
    }
}