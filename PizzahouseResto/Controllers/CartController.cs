using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzahouseResto.Data;
using PizzahouseResto.Models;
using PizzahouseResto.Utility;
using PizzahouseResto.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PizzahouseResto.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMailingService _mailingService;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IMailingService mailingService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _mailingService = mailingService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList(); 
          
            IEnumerable<Meal> prodList = _db.Meals.Where(u => prodInCart.Contains(u.MealId));

            List<Meal> prodlist = prodList.ToList();

            for (int i = 0; i < prodlist.Count; i++)
            {
                for (int j = 0; j < shoppingCartList.Count; j++)
                {
                    if (prodlist[i].MealId == shoppingCartList[j].ProductId)
                    { prodlist[i].Quantity = shoppingCartList[j].Quantity; }
                }

            }

            prodlist.AsEnumerable();

                return View(prodlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userId = User.FindFirstValue(ClaimTypes.Name);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Meal> prodList = _db.Meals.Where(u => prodInCart.Contains(u.MealId));

            List<Meal> prodlist = prodList.ToList();

            double total = 0;

            for (int i = 0; i < prodlist.Count; i++)
            {
                for (int j = 0; j < shoppingCartList.Count; j++)
                {
                    if (prodlist[i].MealId == shoppingCartList[j].ProductId)
                    { prodlist[i].Quantity = shoppingCartList[j].Quantity;
                        total += prodlist[i].Quantity * prodlist[i].Price;
                    }
                }

            }


           
            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _db.Users.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = prodlist.ToList(),
                TotalPrice = total
            };


            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "templates" + Path.DirectorySeparatorChar.ToString() +
                "Inquiry.html";

            var subject = "New Inquiry";
            string HtmlBody = "";
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }
            //Name: { 0}
            //Email: { 1}
            //Phone: { 2}
            //Products: {3}

            StringBuilder productListSB = new StringBuilder();
            foreach (var prod in ProductUserVM.ProductList)
            {
                productListSB.Append($" - Name: { prod.Name} <span style='font-size:14px;'> </span><br />");
            }

            string messageBody = string.Format(HtmlBody,
                ProductUserVM.ApplicationUser.FirstName,
                ProductUserVM.ApplicationUser.Email,
                ProductUserVM.ApplicationUser.PhoneNumber,
                productListSB.ToString());

            await _mailingService.SendEmailAsync(ProductUserVM.ApplicationUser.Email, subject, messageBody);

            var user = await _userManager.FindByIdAsync(ProductUserVM.ApplicationUser.Id);

            List<int> prodInVM = ProductUserVM.ProductList.Select(i => i.MealId).ToList();

            IEnumerable<Meal> prodList = _db.Meals.Where(u => prodInVM.Contains(u.MealId));

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<Meal> prodlist = prodList.ToList();

            double total = 0;

            for (int i = 0; i < prodlist.Count; i++)
            {
                for (int j = 0; j < shoppingCartList.Count; j++)
                {
                    if (prodlist[i].MealId == shoppingCartList[j].ProductId)
                    {
                        prodlist[i].Quantity = shoppingCartList[j].Quantity;
                        total += prodlist[i].Quantity * prodlist[i].Price;
                    }
                }

            }

            Order obj = new Order {
                ApplicationUser = user,
                Meals = prodList.ToList(),
                TotalPrice = total+8,
                OrderDate = DateTime.Now,
                Status = false


        };

           

           

           
            _db.Orders.Add(obj);
           
            _db.SaveChanges();

            for (int i = 0; i < prodlist.Count; i++)
            {
               

                var dbMealOrder = _db.OrderMeal.Where(o => o.MealId == prodlist[i].MealId && o.OrderId == obj.OrderId).Single();

                dbMealOrder.Quantity = GetQuantity(prodlist[i].MealId);
                _db.OrderMeal.Update(dbMealOrder);
                _db.SaveChanges();


            }

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Remove(int id)
        {

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }


        public int GetQuantity(int id)
        {
            var qte = 0;
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            for (int j = 0; j < shoppingCartList.Count; j++)
            {
                if (shoppingCartList[j].ProductId == id)
                {
                    qte= shoppingCartList[j].Quantity;
                   
                }
            }

            return qte;

        }

        [ActionName("NbInCart")]
        public string GetNumberProductsInCart()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();

            return prodInCart.Count().ToString();
        }

    }
}
