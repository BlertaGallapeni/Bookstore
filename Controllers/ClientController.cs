using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Crypto;
using System.Collections.Generic;
using System.Data;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Repository.IRepository;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<ApplicationUser> clients = _unitOfWork.ApplicationUser.GetAll().ToList();
            return View(clients);
        }

        public IActionResult Edit(string? id)
        {
            if (id == "" || id == null)
            {
                return NotFound();
            }
            List<SelectListItem> gender = new List<SelectListItem>();
            gender.Add(new SelectListItem() { Text = "Female", Value = "F" });
            gender.Add(new SelectListItem() { Text = "Male", Value = "M" });
            ViewBag.Gender = gender;
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            var model = new ApplicationUserVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Address = user.Address,
                Gender = user.Gender,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Edit(ApplicationUserVM model)
        {
            if (ModelState.IsValid)
            {
                var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == model.Id);
                obj.PhoneNumber = model.PhoneNumber;
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.Birthday = model.Birthday;
                obj.Address = model.Address;
                _unitOfWork.ApplicationUser.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var usersList = _unitOfWork.ApplicationUser.GetAll(x=>x.Deleted!=true);
            return Json(new { data = usersList });
        }

        //POST
        [HttpDelete]
		public IActionResult Delete(string? id)
		{
			var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			_unitOfWork.ApplicationUser.Delete(obj);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Delete Successful" });

		}
    #endregion
    }
}
