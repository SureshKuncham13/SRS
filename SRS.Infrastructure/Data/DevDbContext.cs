using Microsoft.EntityFrameworkCore;

namespace SRS.Infrastructure.Data
{
    public class DevDbContext : DbContext
    {
        public DevDbContext(DbContextOptions<DevDbContext> options)
            : base(options)
        {
        }
    }
}