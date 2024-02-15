using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASPNET_Sample.Models;

namespace ASPNET_Sample.Data
{
    public class ASPNET_SampleContext : DbContext
    {
        public ASPNET_SampleContext (DbContextOptions<ASPNET_SampleContext> options)
            : base(options)
        {
        }

        public DbSet<ASPNET_Sample.Models.Movie> Movie { get; set; } = default!;
    }
}
