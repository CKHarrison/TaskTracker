using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoDataLibrary.Models
{
    public class FullTodoModel
    {
        public BasicTodoModel BasicInfo { get; set; }
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    }
}
