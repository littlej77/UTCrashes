using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCrashes.Models
{
    public interface ICrashesRepository
    {
        IQueryable<Crash> crashes { get; }
        IQueryable<County> Counties { get; }
    }
}
 