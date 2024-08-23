using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ApplicationTrackingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
