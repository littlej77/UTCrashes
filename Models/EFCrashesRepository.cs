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

        public void AddCrash(Crash c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public void EditCrash(Crash c)
        {
            _context.Update(c);
            _context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }

    }
}
