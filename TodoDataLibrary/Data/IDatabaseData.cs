using TodoDataLibrary.Models;

namespace TodoDataLibrary.Data
{
    public interface IDatabaseData
    {
        void CreateTodo(string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories);
        void DeleteTodo(FullTodoModel todo);
        List<CategoryModel> GetAllCategories();
        List<BasicTodoModel> GetAllTodos();
        CategoryModel GetCategory(int id);
        FullTodoModel GetTodo(int tod);
        List<BasicTodoModel> GetTodoByCategory(int categoryId);
        void UpdateTodo(int id, string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories);
    }
}