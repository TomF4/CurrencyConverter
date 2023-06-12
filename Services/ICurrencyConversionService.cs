using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Services
{
    public interface ICurrencyConversionService
    {
        Dictionary<string, decimal> ConversionRates { get; }
        List<CurrencyConversion> GetConversions();
        void AddConversion(CurrencyConversion conversion);
    }
}