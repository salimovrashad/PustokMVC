using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool? IsLeft { get; set; }
    }
}
