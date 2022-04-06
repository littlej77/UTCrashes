using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCrashes.Models
{
    public class CrashesDbContext : DbContext
    {
        public CrashesDbContext(DbContextOptions<CrashesDbContext> options): base(options)
        {
        }
        
        public DbSet<Crash> crashes { get; set; }
        public DbSet<County> Counties { get; set; }
    }
}
