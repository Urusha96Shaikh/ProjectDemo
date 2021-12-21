using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_shopping_app.Data;
using online_shopping_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_shopping_app.Controllers
{
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _DB_Context;

        public ProductTypesController(ApplicationDbContext dbContext)
        {
            _DB_Context = dbContext;
        }
        // GET: ProductTypesController
        public ActionResult Index()
        {
            //var productTypesList = _DB_Context.ProductTypes.ToList();
            return View(_DB_Context.ProductTypes.ToList());
        }

        // GET: ProductTypesController/Details/5
        public ActionResult View(int id)
        {
            var productView = _DB_Context.ProductTypes.Find(id);
            return View(productView);
        }

        // GET: ProductTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductTypes productTypes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DB_Context.ProductTypes.Add(productTypes);
                    _DB_Context.SaveChanges();
                    TempData["save"] = "Product Type Saved Successfully!";
                    return RedirectToAction(actionName: nameof(Index));
                }
                return View(productTypes);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductTypesController/Edit/5
        public ActionResult Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var updateProductTypes = _DB_Context.ProductTypes.Find(id);
            if (updateProductTypes == null)
            {
                return NotFound();
            }
            return View(updateProductTypes);
        }

        // POST: ProductTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, ProductTypes updateProductTypes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DB_Context.Update(updateProductTypes);
                    _DB_Context.SaveChanges();
                    TempData["save"] = "Product Type Updated Successfully!";
                    return RedirectToAction(actionName: nameof(Index));
                }
                return View(updateProductTypes);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductTypesController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var removeProductTypes = _DB_Context.ProductTypes.Find(id);
            if (removeProductTypes == null)
            {
                return NotFound();
            }
            return View(removeProductTypes);
        }

        // POST: ProductTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductTypes removeProductTypes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DB_Context.Remove(removeProductTypes);
                    _DB_Context.SaveChanges();
                    TempData["save"] = "Product Type Removed Successfully!";
                    return RedirectToAction(actionName: nameof(Index));
                }
                return View(removeProductTypes);
            }
            catch
            {
                return View();
            }
        }
    }
}
