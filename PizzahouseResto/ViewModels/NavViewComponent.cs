using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzahouseResto.Models;
using PizzahouseResto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.ViewModels
{
    public class NavViewComponent : ViewComponent
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public NavViewComponent(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public IViewComponentResult Invoke()
        {

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();

            //return prodInCart.Count().ToString();

            NavCart navCart = new NavCart
            {
                nb = prodInCart.Count().ToString()
            };

            return View("_SideNav", navCart);
        }
    }
}
