using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UTCrashes.Models;

namespace UTCrashes.Controllers
{
    public class HomeController : Controller
    {
        private ICrashesRepository _repo { get; set; }
        //private CrashesDbContext _context { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICrashesRepository temp)
        {
            _logger = logger;
            _repo = temp;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllCrashes(string county)
        {
            var crashes = _repo.crashes
                .Where(x => x.COUNTY.COUNTY_NAME == county || county == null)
                .Include(x => x.COUNTY)
                .ToList();

            return View(crashes);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
