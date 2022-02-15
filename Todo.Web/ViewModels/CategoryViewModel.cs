using System.ComponentModel.DataAnnotations;

namespace Todo.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage ="Max character length cannot exceed 50.")]
        [MinLength(3, ErrorMessage ="Must have a least 3 characters.")]
        [Display(Name ="Category Name(s)")]
        public string Name { get; set; }
    }
}