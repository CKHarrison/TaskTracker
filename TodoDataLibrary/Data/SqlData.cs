using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDataLibrary.Database;
using TodoDataLibrary.Models;
using TodoDataLibrary.Utilities;

namespace TodoDataLibrary.Data
{
    public class SqlData : IDatabaseData
    {
        private readonly ISqlDataAccess _db;
        private readonly string connectionString = string.Empty;

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
            connectionString = ConnectionStringUtil.GetConnectionString();
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

        public void UpdateTodo(int id, string title, string description, DateTime startDate, DateTime endDate, List<CategoryModel> categories)
        {
            string sql = @"update Todos
                            set Title = @Title, Description = @Description, StartDate = @StartDate, EndDate = @EndDate
                            where id = @Id;";
            _db.SaveData(sql, new { Title = title, Description = description, startDate = startDate, endDate = endDate, Id = id }, connectionString);
            // Need to update the categories
            foreach (var category in categories)
            {
                // need to fix bug where moving from 2 categories to  1 category, still leaves deleted category attached.
                sql = "select Id from categories where Name = @Name;";
                int? categoryId;

                try
                {
                    categoryId = _db.LoadData<IdLookupModel, dynamic>(sql, new { Name = category.Name }, connectionString).First().Id;
                }
                catch (InvalidOperationException)
                {
                    categoryId = null;
                }

                if (categoryId != null)
                {

                    sql = "select Id, TodoId, CategoryId from todoCategories where CategoryId = @CategoryId;";
                    var links = _db.LoadData<CategoryModel, dynamic>(sql, new { CategoryId = categoryId }, connectionString);

                    if (links.Count == 1)
                    {
                        sql = "Update categories set Name = @Name Where Id = @Id;";
                        _db.SaveData(sql, new { Id = links[0].Id, Name = category.Name }, connectionString);
                    }
                }

                else
                {
                    sql = "insert into categories(Name) values(@Name);";
                    _db.SaveData(sql, new { Name = category.Name }, connectionString);
                    sql = "select Id from categories where Name=@Name;";
                    categoryId = _db.LoadData<IdLookupModel, dynamic>(sql, new { Name = category.Name }, connectionString).First().Id;
                    sql = "insert into todoCategories(TodoId, CategoryId) values(@TodoId, @CategoryId);";
                    _db.SaveData(sql, new { TodoId = id, CategoryId = categoryId }, connectionString);
                }
            }
        }

        public void DeleteTodo(FullTodoModel todo)
        {
            string sql = "";
            foreach (var category in todo.Categories)
            {
                sql = "select Id, TodoId, CategoryId from todoCategories where CategoryId = @CategoryId;";
                var links = _db.LoadData<CategoryModel, dynamic>(sql, new { CategoryId = category.Id }, connectionString);

                sql = "delete from todoCategories where CategoryId = @CategoryId and TodoId=@TodoId;";
                _db.SaveData(sql, new { CategoryId = category.Id, TodoId = todo.BasicInfo.Id }, connectionString);


                if (links.Count == 1)
                {
                    sql = "delete from categories where id = @Id;";
                    _db.SaveData(sql, new { Id = category.Id }, connectionString);
                }
            }

            sql = "delete from Todos where Id = @Id;";
            _db.SaveData(sql, new { Id = todo.BasicInfo.Id }, connectionString);

        }

        public List<CategoryModel> GetAllCategories()
        {
            string sql = "select * from Categories;";
            return _db.LoadData<CategoryModel, dynamic>(sql, new { }, connectionString);
        }

        public CategoryModel GetCategory(int id)
        {
            string sql = @"select Id, Name
                            from categories
                            where Id = Id;";
            var output = _db.LoadData<CategoryModel, dynamic>(sql, new { Id = id }, connectionString).First();
            return output;
        }

        public List<BasicTodoModel> GetTodoByCategory(int categoryId)
        {
            string sql = @"select t.Id, t.Title, T.Description, t.StartDate, t.EndDate, c.Name
                        from Categories c
                        inner join todoCategories tc on c.Id = tc.CategoryId
                        inner join Todos t on tc.TodoId = t.Id
                        where c.id = @CategoryId;";
            var output = _db.LoadData<BasicTodoModel, dynamic>(sql, new { CategoryId = categoryId }, connectionString);
            return output;
        }

        private void CreateCategory(List<CategoryModel> categories, int todoId)
        {
            foreach (var category in categories)
            {
                string sql = "select Name from categories where Name = @Name;";
                string? name = _db.LoadData<string, dynamic>(sql, new { Name = category.Name }, connectionString).FirstOrDefault();
                if (string.IsNullOrEmpty(name))
                {
                    sql = @"insert into Categories(Name) 
                        values (@Name);";
                    _db.SaveData(sql, new { Name = category.Name }, connectionString);
                    category.Id = LookupCategoryId(category.Name);
                }

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
