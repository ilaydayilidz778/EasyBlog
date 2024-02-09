using EasyBlog.Areas.Admin.Models;
using EasyBlog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly Post _post;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostsController(ApplicationDbContext db, Post post, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _post = post;
            _hostEnvironment = hostEnvironment;
        }

        // GET: PostsController
        public ActionResult Index()
        {
            return View(_db.Posts.Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToList());
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            //ViewBag.Category = new SelectList(_db.Categories.ToList(), "Id", "Name");
            LoadSelectCategories();
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _post.Title = vm.Title;
                    _post.CategoryId = vm.CategoryId;   
                    _post.Content = vm.Content;
                    _post.Description = vm.Description;

                    if (vm.Image != null)
                    {
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", vm.Image.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                             vm.Image.CopyTo(stream);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            LoadSelectCategories();
            return View(vm);
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void LoadSelectCategories()
        {
            ViewBag.Categories = _db.Categories.OrderBy(c => c.Name)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .ToList();
        }
    }
}
