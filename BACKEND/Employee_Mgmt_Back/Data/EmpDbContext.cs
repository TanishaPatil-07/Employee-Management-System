using Employee_Mgmt_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Mgmt_Back.Data
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions <EmpDbContext> options) : base(options) { }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; } 
    }
}
