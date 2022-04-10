using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repro.Data;
using Repro.Models;

namespace Repro.Controllers;

public class Result
{
    public long Id { get; set; }

    public Result(long id)
    {
        Id = id;
    }

    public Result()
    {
        
    }
}
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // This Does Not Work
        var x = _context.Products
            .GroupBy(x => x.Id)
            .Select(x => new Result(x.Key))
            .Count();
        
        // This Works. Using Object Initializer
        // var x = _context.Products
        //     .GroupBy(x => x.Id)
        //     .Select(x => new Result
        //     {
        //         Id = x.Key
        //     })
        //     .Count();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}