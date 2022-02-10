using TodoDataLibrary.Models;

namespace TodoDataLibrary.Data
{
    public interface IDatabaseData
    {
        void CreateTodo(string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories);
        void DeleteTodo(int id);
        List<CategoryModel> GetAllCategories();
        List<BasicTodoModel> GetAllTodos();
        FullTodoModel GetTodo(int id);
        void UpdateTodo(int id, string title, string description, DateTime startDate, DateTime endDate);
    }
}