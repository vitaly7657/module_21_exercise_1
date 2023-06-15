using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace module_21.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Subscriber> Subscribers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //создание БД, если её нет
        }
    }

}
