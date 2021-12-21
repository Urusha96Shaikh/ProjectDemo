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
    public class TagNameController : Controller
    {
        private ApplicationDbContext _DB_Context;
        public TagNameController(ApplicationDbContext Db_Context)
        {
            _DB_Context = Db_Context;
        }
        // GET: TagNameController
        public ActionResult Index()
        {
            return View(_DB_Context.TagNames.ToList());
        }

        // GET: TagNameController/Details/5
        public ActionResult Details(int id)
        {
            var tagList = _DB_Context.TagNames.Find(id);
            return View(tagList);
        }

        // GET: TagNameController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagNameController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagNames addTagName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DB_Context.TagNames.Add(addTagName);
                    _DB_Context.SaveChanges();
                    TempData["save"] = "Tag Name Added Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View(addTagName); 
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: TagNameController/Edit/5
        public ActionResult Update(int id)
        {
            var updateTagName = _DB_Context.TagNames.Find(id);
            return View(updateTagName);
        }

        // POST: TagNameController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, TagNames updateTagName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DB_Context.Update(updateTagName);
                    _DB_Context.SaveChanges();
                    TempData["save"] = "Tag Name Updated Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: TagNameController/Delete/5
        public ActionResult Delete(int id)
        {
           var removeTagName = _DB_Context.TagNames.Find(id);
            return View(removeTagName);
        }

        // POST: TagNameController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TagNames removeTagNames)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DB_Context.Remove(removeTagNames);
                    _DB_Context.SaveChanges();
                    TempData["save"] = "Tag Name Removed Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
