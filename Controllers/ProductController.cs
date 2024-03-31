using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Repository.IRepository;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new();
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update product
                var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
                productVM.Author = product.Author;
                productVM.Title = product.Title;
                productVM.Description = product.Description;
                productVM.ISBN = product.ISBN;
                productVM.ImageUrl = product.ImageUrl;
                productVM.Id = product.Id;
                return View(productVM);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ImageUrl = @"\images\products\" + fileName + extension;

                }
                var newProduct = new Product()
                {
                    Title = obj.Title,
                    Author = obj.Author,
                    Description = obj.Description,
                    Id = obj.Id,
                    ImageUrl = obj.ImageUrl,
                    ISBN = obj.ISBN
                };
                
                if (obj.Id == 0)
                {
                    _unitOfWork.Product.Add(newProduct);
                    TempData["success"] = "Product added successfully!";
                }
                else
                {
                    newProduct.ISBN = obj.ISBN;
                    newProduct.Author = obj.Author;
                    newProduct.Description = obj.Description;
                    newProduct.Id = obj.Id;
                    newProduct.Title = obj.Title;
                    newProduct.ImageUrl = obj.ImageUrl;
                    _unitOfWork.Product.Update(newProduct);
                    TempData["success"] = "Product updated successfully!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll();
            return Json(new { data = productList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product deleted successfully!" });

        }
        #endregion
    }
}
