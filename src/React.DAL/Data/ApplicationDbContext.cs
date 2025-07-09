using Microsoft.EntityFrameworkCore;
using React.Domain.Models.AppUser;
using React.Domain.Models.Employee;
using React.Domain.Models.User;
using React.Domain.Models.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}   
