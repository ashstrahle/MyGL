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

        public DbSet<MyGL.Models.Account> Accounts { get; set; }
        public DbSet<MyGL.Models.Category> Categories { get; set; }
        public DbSet<MyGL.Models.CategoryCondition> CategoryConditions { get; set; }
        public DbSet<MyGL.Models.LoadTable> LoadTable { get; set; }
        public DbSet<MyGL.Models.Transaction> Transactions { get; set; }
        public DbSet<MyGL.Models.DimDate> DimDates { get; set; }
        public DbSet<MyGL.Models.PivotData> View_PivotData { get; set; }
    }
}
