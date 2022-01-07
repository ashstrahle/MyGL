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

        public DbSet<MyGL.Models.Account> Account { get; set; }
        public DbSet<MyGL.Models.Category> Category { get; set; }
        public DbSet<MyGL.Models.CategoryCondition> CategoryCondition { get; set; }
        public DbSet<MyGL.Models.LoadTable> LoadTable { get; set; }
        public DbSet<MyGL.Models.Transaction> Transactions { get; set; }
    }
}
