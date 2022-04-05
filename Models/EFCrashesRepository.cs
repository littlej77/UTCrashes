using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCrashes.Models
{
    public class EFCrashesRepository : ICrashesRepository
    {
        private CrashesDbContext _context { get; set; }
        public EFCrashesRepository (CrashesDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Crash> crashes =>_context.crashes;

        public IQueryable<County> Counties => _context.Counties;


    }
}
