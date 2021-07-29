using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PizzahouseResto.ViewModels;

namespace PizzahouseResto.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        
      
        public virtual IList<Meal> Meals { get; set; }


        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
    }
}
