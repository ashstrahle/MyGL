#nullable disable
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Data
{
    public class MyGLContext : DbContext
    {
       // private readonly DbSet<PivotData> view_PivotData1;

        public MyGLContext(DbContextOptions<MyGLContext> options)
: base(options)
        {
        }

        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.CategoryRule> CategoryRules { get; set; }
        public DbSet<Models.LoadTable> LoadTable { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }
        public DbSet<Models.DimDate> DimDates { get; set; }
        public virtual DbSet<Models.PivotData> view_PivotData { get; set; }
    }
}