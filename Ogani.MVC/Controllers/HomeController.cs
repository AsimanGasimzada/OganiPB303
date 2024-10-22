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

namespace Ogani.MVC.Controllers;

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
            List<BasketItemViewModel> basket = _getBasketFromCookie();

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
            _appendBasketInCookie(basket);
        }
        else
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);



            if (userId is null)
                return BadRequest();

            var existItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == product.Id && x.AppUserId == userId);

            if (existItem is not null)
            {
                existItem.Count++;
                _context.Update(existItem);
                await _context.SaveChangesAsync();

                if (returnUrl is not null)
                    return Redirect(returnUrl);

                return RedirectToAction(nameof(Index));
            }

            BasketItem basketItem = new()
            {
                AppUserId = userId,
                ProductId = product.Id,
                Count = 1
            };

            await _context.BasketItems.AddAsync(basketItem);
            await _context.SaveChangesAsync();


        }
        if (returnUrl is not null)
            return Redirect(returnUrl);

        return RedirectToAction(nameof(Index));

    }


    public async Task<IActionResult> Decrease(int id, string? returnUrl)
    {
        var product = await _productService.GetAsync(id);

        if (product is null)
            return NotFound();

        if (!User.Identity?.IsAuthenticated ?? true)
        {

            List<BasketItemViewModel> basket = _getBasketFromCookie();

            var existItem = basket.FirstOrDefault(x => x.ProductId == id);

            if (existItem is { })
                existItem.Count--;
            else
            {
                return NotFound();
            }


            _appendBasketInCookie(basket);
        }
        else
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return BadRequest();

            var existItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == product.Id && x.AppUserId == userId);

            if (existItem is null)
                return NotFound();

            existItem.Count--;
            _context.Update(existItem);
            await _context.SaveChangesAsync();

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

            var basketItemList = await _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == userId).ToListAsync();


            foreach (var basketItem in basketItemList)
            {
                GetBasketViewModel vm = new()
                {
                    Id = basketItem.Id,
                    Count = basketItem.Count,
                    Price = basketItem.Product.Price,
                    ImagePath = basketItem.Product.ProductImages?.FirstOrDefault()?.ImageUrl ?? "",
                    Name = basketItem.Product.Name,
                    ProductId = basketItem.ProductId,
                };

                basket.Add(vm);
            }


            return View(basket);

        }


        List<BasketItemViewModel> basketItems = _getBasketFromCookie();

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


    private void _appendBasketInCookie(List<BasketItemViewModel> basket)
    {
        var newJson = JsonConvert.SerializeObject(basket);

        Response.Cookies.Append(BASKET_KEY, newJson);
    }

    private List<BasketItemViewModel> _getBasketFromCookie()
    {
        string? json = Request.Cookies[BASKET_KEY];

        List<BasketItemViewModel> basket = new();

        if (!string.IsNullOrEmpty(json))
            basket = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json!) ?? new();
        return basket;
    }

}
