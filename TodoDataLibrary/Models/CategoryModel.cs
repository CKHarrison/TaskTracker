using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoDataLibrary.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage ="Minimum length of category must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length of category must be at most 50 characters")]
        public string Name { get; set; }
    }
}
