using PizzahouseResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.ViewModels
{

    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList = new List<Meal>();
        }

        public ApplicationUser ApplicationUser { get; set; }
        public IList<Meal> ProductList { get; set; }

        public double TotalPrice { get; set; }
    }
}
