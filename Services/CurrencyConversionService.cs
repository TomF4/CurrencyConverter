using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Services
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly ConversionAppDbContext _context;

        public CurrencyConversionService(ConversionAppDbContext context)
        {
            _context = context;
        }

        // this could be added to the in-memory database
        public Dictionary<string, decimal> ConversionRates { get; private set; } = new Dictionary<string, decimal>
        {
            { "GBP_USD", 1.39M },
            { "GBP_AUD", 1.83M },
            { "GBP_EUR", 1.18M }
        };

        public List<CurrencyConversion> GetConversions() => _context.CurrencyConversions.ToList();

        public void AddConversion(CurrencyConversion conversion)
        {
            _context.CurrencyConversions.Add(conversion);
            _context.SaveChanges();
        }
    }
}