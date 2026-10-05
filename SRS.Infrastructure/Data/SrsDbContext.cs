using Microsoft.EntityFrameworkCore;

namespace SRS.Infrastructure.Data
{
    public class SrsDbContext : DbContext
    {
        public SrsDbContext(DbContextOptions<SrsDbContext> options)
            : base(options)
        {
        }
    }
}