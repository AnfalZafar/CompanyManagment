using CompanyManagmentApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;

namespace CompanyManagmentApplication.Database
{
    public class Data : DbContext
    {
        public Data(DbContextOptions options) : base (options) { }

        public DbSet<Users> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Messages> Messages { get; set; }

    }
}
