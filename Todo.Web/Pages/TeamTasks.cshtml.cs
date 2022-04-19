using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Web.ViewModels;
using TodoDataLibrary.Data;

namespace Todo.Web.Pages
{
    public class TeamTasksModel : PageModel
    {
        private readonly ILogger<TeamTasksModel> _logger;
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public List<BasicTodoViewModel> Todos;
        public string UserId { get; set; }

        public TeamTasksModel(ILogger<TeamTasksModel> logger, IDatabaseData db, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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
            Todos = _mapper.Map<List<BasicTodoViewModel>>(_db.GetAllTodos());
        }
    }
}
