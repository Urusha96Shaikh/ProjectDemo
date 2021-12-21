using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using online_shopping_app.Data;
using online_shopping_app.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace online_shopping_app.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private ApplicationDbContext _DB_Context;

        public HomeController(ApplicationDbContext dbContext)
        {
            _DB_Context = dbContext;
        }
        public IActionResult Index()
        {
            return View(_DB_Context.Products.Include(p => p.ProductTypes).Include(t => t.TagNames).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public ActionResult Details(int? Id)
        {
            var products = _DB_Context.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == Id);
            return View(products);
        }
    }
}
