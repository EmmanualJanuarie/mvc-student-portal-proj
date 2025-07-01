using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace WILWebApp.Entities
{
    public class DB_Context : DbContext
    {
        public DB_Context(DbContextOptions<DB_Context> options) 
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SpaceCadet;Initial Catalog=StudentPortal_Db;Integrated Security=True;Trust Server Certificate=True;");
            }
        }

        // ADDING A DBContext Property - for table migration (table data)
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<StudentReport> StudentReports { get; set; }


    }
}
