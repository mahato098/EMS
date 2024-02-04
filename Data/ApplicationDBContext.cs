using EMS.Model;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=;Database=EmpTesting; User Id=; Password=; Trusted_Connection=True; MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        public DbSet<Department> Departments { get; set; }

    }
}
