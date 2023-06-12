using Microsoft.EntityFrameworkCore;

namespace test.Models
{
    public class ConversionAppDbContext : DbContext
    {
        public ConversionAppDbContext(DbContextOptions<ConversionAppDbContext> options)
        : base(options)
        {
        }
        public DbSet<CurrencyConversion> CurrencyConversions { get; set; }

    }
}
