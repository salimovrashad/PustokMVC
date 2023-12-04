using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
