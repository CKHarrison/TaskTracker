using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoDataLibrary.Data;
using TodoDataLibrary.Models;

namespace Todo.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseData _db;
        private List<BasicTodoModel> Todos;

        public IndexModel(ILogger<IndexModel> logger, IDatabaseData db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            Todos = _db.GetAllTodos();
        }
    }
}