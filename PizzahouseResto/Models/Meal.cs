using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.Models
{
    public class Meal
    {
        public int MealId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [Required]
        public Categories Category { get; set; }

        [NotMapped]
        [Display( Name ="Upload File")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

      
        public virtual IList<Order> Orders { get; set; }

    }

    public enum Categories
    {
        Salads,
        Pizzas,
        Burgers,
        Desserts,
        Drinks,
        Seafood
      
    }

 
}
