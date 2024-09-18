using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoekhoudAssistent.Models;

namespace BoekhoudAssistent.Data
{
    public class BoekhoudAssistentContext : DbContext
    {
		public DbSet<BoekhoudAssistent.Models.BKFP> BKFP { get; set; }
		public DbSet<BoekhoudAssistent.Models.BSEG> BSEG { get; set; }

		public BoekhoudAssistentContext (DbContextOptions<BoekhoudAssistentContext> options)
            : base(options)
        {
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BKFP>()
				.HasKey(b => new { b.BUKRS, b.BELNR, b.GJAHR });

            modelBuilder.Entity<BSEG>()
				.HasKey(b => new { b.BUKRS, b.BELNR, b.GJAHR,b.BUZEI });

		}
	}
}
