using PizzahouseResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Meal = new Meal();
        }

        public Meal Meal { get; set; }
        public bool ExistsInCart { get; set; }

        
    }
}

