using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Repository.IRepository;

namespace WebApp.Controllers;
	[Authorize(Roles = "Admin,Client")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
		{
        var user = await _userManager.GetUserAsync(User);
        var selectedIds = _unitOfWork.Product.GetProductsByClient(user?.Id).Select(x => x.Id);
        IEnumerable<ProductVM> productList = _unitOfWork.Product.GetAll().Select(x=>new ProductVM()
		{
			Id=x.Id,
			ISBN=x.ISBN,
			Author=x.Author,
			Description=x.Description,
			ImageUrl=x.ImageUrl,
			IsFavorite= selectedIds.Contains(x.Id)
        });
		
		
			return View(productList);
		}
	public async Task<IActionResult> AddToFavorite(int productId) 
		{
		var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == productId);
		var user =await _userManager.GetUserAsync(User);
		
		if (product != null && user != null)
		{
			var userProduct = new UserProduct()
			{
				ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == user.Id),
				Product = product,
			};
			_unitOfWork.UserProduct.Add(userProduct);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Added Successfuly" });
		}
		return Json(new { success = false, message = "Not Found" });
	}

	public async Task<IActionResult> RemoveFromFavorite(int productId)
	{
		
		var user = await _userManager.GetUserAsync(User);
		var userProduct = _unitOfWork.UserProduct.GetFirstOrDefault(x=>x.ProductId == productId && x.UserId==user.Id);
		if (userProduct != null)
		{
			_unitOfWork.UserProduct.Remove(userProduct);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Removed Successfuly" });
		}
		return Json(new { success = false, message = "Not Found" });
		
	}



	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}

