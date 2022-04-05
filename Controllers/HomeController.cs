using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UTCrashes.Models;
using UTCrashes.Models.ViewModels;

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

        public IActionResult AllCrashes(string county, int pageNum=1)
        {
            int pageSize = 30;

            ViewBag.Counties = _repo.Counties.OrderBy(x => x.COUNTY_NAME).ToList();

            var x = new CrashesViewModel
            {
                crashes = _repo.crashes
                .Where(x => x.COUNTY.COUNTY_NAME == county || county == null)
                .OrderBy(x => x.CRASH_ID)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                        (county == null
                            ? _repo.crashes.Count()
                            : _repo.crashes.Where(x => x.COUNTY.COUNTY_NAME == county).Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            //var crashes = _repo.crashes
            //    .Where(x => x.COUNTY.COUNTY_NAME == county || county == null)
            //    .Include(x => x.COUNTY)
            //    .ToList();

            return View(x);
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
