using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using truYum_asp.Models;

namespace truYum_asp.Controllers
{
    public class MenuItemsController : Controller
    {
        // GET: MenuItems
        public ActionResult Index(bool isAdmin=false)
        {
            TruYumContext ds = new TruYumContext();
            List<Category> Categories = ds.Categories.ToList();
            List<MenuItem> MenuItems = ds.MenuItems.ToList();
            var MenuRecord = from e in Categories
                             join d in MenuItems on e.Id equals d.categoryId into table1
                             from d in table1.ToList()
                             select new ViewModel
                             {
                                 menu = d,
                                 category = e,
                             };
            if (isAdmin == true)
            {
                
                return View("MenuItemAdmin",MenuRecord);
            }
            else
            {
                
                List<ViewModel> menuRecord = MenuRecord.Where(m => m.menu.Active == true && m.menu.dateOfLaunch < DateTime.Now).ToList();
                return View("MenuItemCustomer",menuRecord);
            }
        }
        public ActionResult AddMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMenu(MenuItem m)
        {
            if (ModelState.IsValid)
            {
                TruYumContext ds = new TruYumContext();
                ds.MenuItems.Add(m);
                ds.SaveChanges();
                ModelState.Clear();
                ViewBag.message = "Item Added Successfully";
                return View();
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TruYumContext context = new TruYumContext();
            var m = context.MenuItems.Where(i=>i.Id==id).FirstOrDefault();
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(MenuItem m)
        {
            if (ModelState.IsValid)
            {
                TruYumContext context = new TruYumContext();
                context.MenuItems.AddOrUpdate(m);
                context.SaveChanges();
                ViewBag.message = "Item Edited Successfully";
                return View();
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult AddToCart(int id)
        {
            TruYumContext context = new TruYumContext();            
            Cart cart = new Cart();
            cart.userId = 1;
            cart.menuItemId = id;

            context.Carts.Add(cart);
            context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            TruYumContext ds = new TruYumContext();
            List<Cart> carts = ds.Carts.ToList();
            List<MenuItem> menuItems = ds.MenuItems.ToList();
            var viewcart = from c in carts
                             join m in menuItems on c.menuItemId equals m.Id into table1
                             from m in table1.ToList()
                             select new ViewModel1
                             {
                                 menu = m,
                                 cart = c
                             };
            return View(viewcart);
        }

        public ActionResult Delete(int id)
        {
            TruYumContext t = new TruYumContext();
            Cart c = t.Carts.Where(i=>i.menuItemId==id).FirstOrDefault();
            t.Carts.Remove(c);
            t.SaveChanges();
            return RedirectToAction("ViewCart");
        }
    }
}