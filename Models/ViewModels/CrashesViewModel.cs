using System;
using System.Linq;

namespace UTCrashes.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> crashes { get; set; }
        public IQueryable<County> Counties { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
