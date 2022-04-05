#nullable disable
using Microsoft.EntityFrameworkCore;

namespace MyGL.Data
{
    public class MyGLContext : DbContext
    {
        public MyGLContext(DbContextOptions<MyGLContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.CategoryCondition> CategoryConditions { get; set; }
        public DbSet<Models.LoadTable> LoadTable { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }
        public DbSet<Models.DimDate> DimDates { get; set; }
        public DbSet<Models.PivotData> view_PivotData { get; set; }
    }
}
