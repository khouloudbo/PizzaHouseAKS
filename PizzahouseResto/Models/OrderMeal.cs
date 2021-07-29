using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.Models
{
    public class OrderMeal
    {
        public int MealId { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public Meal Meal { get; set; }

        public Order Order { get; set; }

    }
}
