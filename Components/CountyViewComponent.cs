using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UTCrashes.Models;

namespace UTCrashes.Components
{
    public class CountyViewComponent : ViewComponent
    {
        private ICrashesRepository repo { get; set; }
        public CountyViewComponent(ICrashesRepository temp)
        {
            repo = temp;
        }

        // when county view component is called, build all county names together
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCounty = RouteData?.Values["county"];

            var counties = repo.crashes
                .Select(x => x.COUNTY.COUNTY_NAME)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return View(counties);
        }

    }
}
