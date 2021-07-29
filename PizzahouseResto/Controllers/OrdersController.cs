using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzahouseResto.Data;
using PizzahouseResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzahouseResto.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           
            IEnumerable<Order> objList = _db.Orders.Include(c => c.ApplicationUser).Include(c => c.Meals);
            List<Order> ordersList = objList.ToList();

            for (int i = 0; i < ordersList.Count; i++)
            {
                for (int j = 0; j < ordersList[i].Meals.Count; j++)
                {
                    var dbMealOrder = _db.OrderMeal.Where(o => o.MealId == ordersList[i].Meals[j].MealId && o.OrderId == ordersList[i].OrderId).Single();
                    ordersList[i].Meals[j].Quantity = dbMealOrder.Quantity;
                }
            }

                return View(ordersList.AsEnumerable());
        }


        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(obj);
            _db.SaveChanges();
            return Ok();
        }


        public IActionResult History()
        {
           
            IEnumerable<Order> objList = _db.Orders.Include(c => c.ApplicationUser).Include(c => c.Meals);
            List<Order> ordersList = objList.ToList();

            for (int i = 0; i < ordersList.Count; i++)
            {
                for (int j = 0; j < ordersList[i].Meals.Count; j++)
                {
                    var dbMealOrder = _db.OrderMeal.Where(o => o.MealId == ordersList[i].Meals[j].MealId && o.OrderId == ordersList[i].OrderId).Single();
                    ordersList[i].Meals[j].Quantity = dbMealOrder.Quantity;
                }
            }

            return View(ordersList.AsEnumerable());
        }

         public IActionResult MarkCompleted(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.Status = true;
            _db.Orders.Update(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
