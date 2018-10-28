using Microsoft.EntityFrameworkCore;
using myCoreMvc.Models;
using PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    public class EfCtx : DbContext
    {
        /*==================================  Methods =================================*/

        public EfCtx() { } // Only to get Get-DbContext command of EF to work

        public EfCtx(DbContextOptions options) : base(options) { } // Necessary to be able to configure EF

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("SERVER=.\\sqlexpress; Database=myCoreMvc; MultipleActiveResultSets=True; Integrated Security=SSPI;");
        }

        /*==================================  Properties =================================*/

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkPlan> WorkPlans { get; set; }
    }
}
