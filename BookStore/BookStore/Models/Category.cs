using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

       [Required]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
