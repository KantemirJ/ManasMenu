using Microsoft.EntityFrameworkCore;

namespace ManasMenuTest.Data
{
    public class ManasMenuContext : DbContext
    {
        public ManasMenuContext(DbContextOptions<ManasMenuContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menu { get; set; } = default!;

        public DbSet<Canteen> Canteen { get; set; }
        public DbSet<OneDayMenu> OneDayMenu { get; set; }
    }
}
