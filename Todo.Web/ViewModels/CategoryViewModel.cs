using System.ComponentModel.DataAnnotations;

namespace Todo.Web.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name ="Category Name(s)")]
        public string Name { get; set; }
    }
}