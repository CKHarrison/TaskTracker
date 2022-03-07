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
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseData _db;
        private readonly IMapper _mapper;
        public List<BasicTodoViewModel> Todos;

        public IndexModel(ILogger<IndexModel> logger, IDatabaseData db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        public void OnGet()
        {
            
            Todos = _mapper.Map<List<BasicTodoViewModel>>(_db.GetAllTodos());
        }
    }
}