using System.ComponentModel.DataAnnotations;

namespace Todo.Web.ViewModels
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        [Required]
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    }
}
