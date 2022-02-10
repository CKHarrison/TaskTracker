using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDataLibrary.Database;
using TodoDataLibrary.Models;

namespace TodoDataLibrary.Data
{
    public class SqlData : IDatabaseData
    {
        private readonly ISqlDataAccess _db;
        private readonly string connectionString = "PgSqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public List<BasicTodoModel> GetAllTodos()
        {
            string sql = "select Id, Title, Description, StartDate, EndDate from Todos;";
            return _db.LoadData<BasicTodoModel, dynamic>(sql, new { }, connectionString);
        }

        public FullTodoModel GetTodo(int id)
        {
            FullTodoModel todo = new FullTodoModel();
            string sql = @"select Id, Title, Description, StartDate, EndDate
                            from Todos
                            where Id = @Id;";
            todo.BasicInfo = _db.LoadData<BasicTodoModel, dynamic>(sql, new { Id = id }, connectionString).ToList().First();

            sql = @"select  c.*
                    from todos t
                    inner join TodoCategories tc on t.Id = tc.todoId
                    inner join Categories c on c.id = tc.categoryId
                    where t.id = @Id;";

            List<CategoryModel> categories = _db.LoadData<CategoryModel, dynamic>(sql, new { Id = id }, connectionString);
            foreach (var category in categories)
            {
                todo.Categories.Add(category);
            }
            return todo;
        }

        public void CreateTodo(string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories)
        {
            string sql = @"insert into Todos(Title, Description, StartDate, EndDate)
                           values(@Title, @Description, @StartDate, @EndDate);";
            _db.SaveData(sql, new { Title = title, Description = description, StartDate = startDate, EndDate = endDate }, connectionString);
            sql = @"select Id
                    from Todos
                    where Title = @Title and Description = @Description and StartDate = @StartDate and EndDate = @EndDate;";
            int todoId = _db.LoadData<IdLookupModel, dynamic>(sql,
                                                              new { Title = title, Description = description, StartDate = startDate, EndDate = endDate },
                                                              connectionString).First().Id;
            CreateCategory(categories, todoId);
        }

        public void UpdateTodo(int id, string title, string description, DateTime startDate, DateTime endDate)
        {
            string sql = @"update Todos
                            set Title = @Title, Description = @Description, StartDate = @StartDate, EndDate = @EndDate
                            where id = @Id;";
            _db.SaveData(sql, new { Title = title, Description = description, startDate = startDate, endDate = endDate, Id = id }, connectionString);
        }

        public void DeleteTodo(int id)
        {
            string sql = "delete from Todos where Id = @Id;";
            _db.SaveData(sql, new { Id = id }, connectionString);
        }

        public List<CategoryModel> GetAllCategories()
        {
            string sql = "select * from Categories;";
            return _db.LoadData<CategoryModel, dynamic>(sql, new { }, connectionString);
        }

        private void CreateCategory(List<CategoryModel> categories, int todoId)
        {
            string sql = @"insert into Categories(Name) 
                        values (@Name);";


            foreach (var category in categories)
            {
                _db.SaveData(sql, new { Name = category.Name }, connectionString);
                category.Id = LookupCategoryId(category.Name);
                ConnectCategoryTodo(todoId, category.Id);
            }
        }
        private int LookupCategoryId(string name)
        {
            string sqlId = @"select Id
                            from Categories
                            where Name = @Name;";
            return _db.LoadData<IdLookupModel, dynamic>(sqlId, new { Name = name }, connectionString).First().Id;
        }
        private void ConnectCategoryTodo(int todoId, int categoryId)
        {
            string sql = @"insert into TodoCategories(TodoId, CategoryId)
                            values (@TodoId, @CategoryId);";
            _db.SaveData(sql, new { TodoId = todoId, CategoryId = categoryId }, connectionString);
        }
    }
}
