﻿using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool? isDeleted { get; set; }
    }
}
