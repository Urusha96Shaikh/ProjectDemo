using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using online_shopping_app.Data;
using online_shopping_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using online_shopping_app.ViewModels;

namespace online_shopping_app.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _DB_Context;
        private readonly IWebHostEnvironment _hE;
        public ProductController(ApplicationDbContext DB_Context, IWebHostEnvironment hE)
        {
            _DB_Context = DB_Context;
            _hE = hE;
        }
        // GET: ProductListController
        public ActionResult Index()
        {
            return View(_DB_Context.Products.Include(p => p.ProductTypes).Include(t => t.TagNames).ToList());
        }
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _DB_Context.Products.Include(p => p.ProductTypes).Include(t => t.TagNames)
                                               .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if(lowAmount == null || largeAmount == null)
            {
                products = _DB_Context.Products.Include(p => p.ProductTypes).Include(t => t.TagNames).ToList();
            }
            return View(products);
        }

        // GET: ProductListController/Details/5
        public ActionResult Details(int id)
        {
            ViewData["productTypeId"] = new SelectList(_DB_Context.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(_DB_Context.TagNames.ToList(), "Id", "TagName");
            var productDetails = _DB_Context.Products.Include(p => p.ProductTypes).Include(t => t.TagNames).FirstOrDefault(c => c.Id == id);
            return View(productDetails);
        }

        // GET: ProductListController/Create
        public ActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_DB_Context.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(_DB_Context.TagNames.ToList(), "Id", "TagName");
            return View();
        }

        // POST: ProductListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productVM)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(productVM);
                    Products product = new Products();
                    {
                        product.Name = productVM.Name;
                        product.Price = productVM.Price;
                        product.IsAvailable = productVM.IsAvailable;
                        product.ProductTypeId = productVM.ProductTypeId;
                        product.TagNameId = productVM.TagNameId;
                        product.Image = uniqueFileName;
                    }
                    if (uniqueFileName == null)
                    {
                        product.Image = "/Images/noImage.png";
                    }
                    _DB_Context.Products.Add(product);
                    await _DB_Context.SaveChangesAsync();
                    TempData["save"] = "Product Added Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        private string UploadedFile(ProductViewModel productVM)
        {
            string uniqueFileName = null;
            if(productVM.Image != null)
            {
                string uploadsFolder = Path.Combine(_hE.WebRootPath + "/Images");
                uniqueFileName = productVM.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productVM.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: ProductListController/Edit/5
        public ActionResult Update(int id)
        {
            ViewData["productTypeId"] = new SelectList(_DB_Context.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(_DB_Context.TagNames.ToList(), "Id", "TagName");
            var updateProduct = _DB_Context.Products.Include(p => p.ProductTypes).Include(t => t.TagNames).FirstOrDefault(c => c.Id == id);
            ProductViewModel updateProductVM = new ProductViewModel(updateProduct);
            return View(updateProductVM);
        }

        // POST: ProductListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductViewModel updateProductVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(updateProductVM);
                    Products updateProduct = _DB_Context.Products.SingleOrDefault(item => item.Id == id);
                    {
                        updateProduct.Name = updateProductVM.Name;
                        updateProduct.Price = updateProductVM.Price;
                        updateProduct.IsAvailable = updateProductVM.IsAvailable;
                        updateProduct.ProductTypeId = updateProductVM.ProductTypeId;
                        updateProduct.TagNameId = updateProductVM.TagNameId;
                        updateProduct.Image = uniqueFileName;
                    }
                    if (uniqueFileName == null)
                    {
                        updateProduct.Image = "/Images/noImage.png";
                    }
                    _DB_Context.Update(updateProduct);
                    await _DB_Context.SaveChangesAsync();
                    TempData["save"] = "Product Updated Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductListController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewData["productTypeId"] = new SelectList(_DB_Context.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(_DB_Context.TagNames.ToList(), "Id", "TagName");
            var removeProduct = _DB_Context.Products.Include(t => t.TagNames).Include(p => p.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            return View(removeProduct);
        }

        // POST: ProductListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Products removeProduct)
        {
            try
            {
                _DB_Context.Products.Remove(removeProduct);
                _DB_Context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
