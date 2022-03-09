using TodoDataLibrary.Models;

namespace TodoDataLibrary.Data
{
    public interface IDatabaseData
    {
        void CreateTodo(string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories, string userId);
        void DeleteTodo(FullTodoModel todo, string userId);
        List<CategoryModel> GetAllCategories();
        List<BasicTodoModel> GetAllTodos();
        List<BasicTodoModel> GetAllUserTodos(string userId);
        CategoryModel GetCategory(int id);
        FullTodoModel GetTodo(int tod);
        List<BasicTodoModel> GetTodoByCategory(int categoryId);
        void UpdateTodo(int id, string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories);
    }
}