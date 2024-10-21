using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.UI.Services.Contracts;
using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.MVC.Models;
using Ogani.MVC.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace Ogani.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService _homeService;
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private const string BASKET_KEY = "OGANI_BASKET_KEY";
        public HomeController(IHomeService homeService, IProductService productService, AppDbContext context)
        {
            _homeService = homeService;
            _productService = productService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = await _homeService.GetHomeViewModelAsync();

            return View(homeViewModel);


        }

        public async Task<IActionResult> AddToBasket(int id, string? returnUrl)
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
                return NotFound();

            if (!User.Identity?.IsAuthenticated ?? true)
            {
                string? json = Request.Cookies[BASKET_KEY];

                List<BasketItemViewModel> basket = new();

                if (!string.IsNullOrEmpty(json))
                    basket = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json!) ?? new();

                var existItem = basket.FirstOrDefault(x => x.ProductId == id);

                if (existItem is { })
                    existItem.Count++;
                else
                {
                    BasketItemViewModel newItem = new()
                    {
                        ProductId = id,
                        Count = 1
                    };

                    basket.Add(newItem);
                }


                var newJson = JsonConvert.SerializeObject(basket);

                Response.Cookies.Append(BASKET_KEY, newJson);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);



                if (userId is null)
                    return BadRequest();

                var existItem = await _context.BasketIems.FirstOrDefaultAsync(x => x.ProductId == product.Id && x.AppUserId == userId);

                if (existItem is not null)
                {
                    existItem.Count++;
                    _context.Update(existItem);
                    await _context.SaveChangesAsync();

                    if (returnUrl is not null)
                        return Redirect(returnUrl);

                    return RedirectToAction(nameof(Index));
                }

                BasketIem basketItem = new()
                {
                    AppUserId = userId,
                    ProductId = product.Id,
                    Count = 1
                };

                await _context.BasketIems.AddAsync(basketItem);
                await _context.SaveChangesAsync();


            }
            if (returnUrl is not null)
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Decrease(int id,string? returnUrl)
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
                return NotFound();

            if (!User.Identity?.IsAuthenticated ?? true)
            {
                string? json = Request.Cookies[BASKET_KEY];

                List<BasketItemViewModel> basket = new();

                if (!string.IsNullOrEmpty(json))
                    basket = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json!) ?? new();

                var existItem = basket.FirstOrDefault(x => x.ProductId == id);

                if (existItem is { })
                    existItem.Count--;
                else
                {
                    return NotFound();
                }


                var newJson = JsonConvert.SerializeObject(basket);

                Response.Cookies.Append(BASKET_KEY, newJson);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);



                if (userId is null)
                    return BadRequest();

                var existItem = await _context.BasketIems.FirstOrDefaultAsync(x => x.ProductId == product.Id && x.AppUserId == userId);

                if (existItem is not null)
                {
                    existItem.Count--;
                    _context.Update(existItem);
                    await _context.SaveChangesAsync();

                    if (returnUrl is not null)
                        return Redirect(returnUrl);

                    return RedirectToAction(nameof(Index));
                }

                return NotFound();


            }
            if (returnUrl is not null)
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShoppingCard()
        {
            List<GetBasketViewModel> basket = new();

            if (User.Identity?.IsAuthenticated ?? false)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId is null)
                    return BadRequest();

                var basketItems2 = await _context.BasketIems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == userId).ToListAsync();


                foreach (var basketIem in basketItems2)
                {
                    GetBasketViewModel vm = new()
                    {
                        Id = basketIem.Id,
                        Count = basketIem.Count,
                        Price = basketIem.Product.Price,
                        ImagePath = basketIem.Product.ProductImages?.FirstOrDefault()?.ImageUrl ?? "",
                        Name = basketIem.Product.Name,
                        ProductId = basketIem.ProductId,
                    };

                    basket.Add(vm);
                }


                return View(basket);

            }


            List<BasketItemViewModel> basketItems = new();

            var json = Request.Cookies[BASKET_KEY];


            if (!string.IsNullOrEmpty(json))
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json) ?? new();

            foreach (var basketIem in basketItems)
            {

                var product = await _productService.GetAsync(basketIem.ProductId);

                if (product is null)
                    continue;

                GetBasketViewModel vm = new()
                {
                    Count = basketIem.Count,
                    Price = product.Price,
                    ImagePath = product.ProductImages?.FirstOrDefault()?.ImageUrl ?? "",
                    Name = product.Name,
                    ProductId = product.Id
                };

                basket.Add(vm);
            }


            return View(basket);
        }
    }
}
