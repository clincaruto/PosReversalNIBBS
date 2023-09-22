using Microsoft.EntityFrameworkCore;
using POSReversalNIBBSBackground.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSReversalNIBBSBackground.Data
{
    public class PosNibbsLogDbContext : DbContext
    {
        public PosNibbsLogDbContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<CC>(null);
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<ExcelResponse> ExcelResponses { get; set; }
        public DbSet<UploadedExcelDetail> UploadedExcelDetails { get; set; }
    }
}
