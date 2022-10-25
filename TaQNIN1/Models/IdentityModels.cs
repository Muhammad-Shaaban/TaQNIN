using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TaQNIN1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string userrole { get; set; }
        public int isactivated { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection"){}

        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<TaqninData> TaqninData { get; set; }
        public virtual DbSet<TaqninMetadata> TaqninMetadata { get; set; }
        public virtual DbSet<Income_noData> Income_noData { get; set; }
        public virtual DbSet<LogTable> LogTable { get; set; }
      }
       
    }
