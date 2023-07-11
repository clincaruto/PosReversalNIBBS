using Microsoft.EntityFrameworkCore;
using PosReversalNIBBS_API.Models.Domain;

namespace PosReversalNIBBS_API.Data
{
	public class PosNibbsDbContext :DbContext
	{
		public PosNibbsDbContext(DbContextOptions<PosNibbsDbContext> options): base(options)
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
