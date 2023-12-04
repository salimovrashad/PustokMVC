using PustokMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ViewModels.ProductVM
{
    public class ProductListItemVM
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        public Category? Category { get; set; }
        public bool? isDeleted { get; set; }
    }
}
