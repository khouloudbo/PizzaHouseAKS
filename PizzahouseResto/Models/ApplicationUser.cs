using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

       
        public City City { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public virtual IList<Order> OrderList { get; set; }
    }

    public enum City
    {
        Ariana,
        BenArous,
        Tunis
    }
}
