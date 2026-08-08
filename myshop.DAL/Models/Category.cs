using myshop.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace myshop.Entities.Models
{
    public class Category : ISoftDelete , IAuditable
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public List<Product> Products { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
