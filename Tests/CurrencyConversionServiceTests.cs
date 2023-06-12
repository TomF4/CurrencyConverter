using Xunit;
using test.Services;
using test.Models;
using Microsoft.EntityFrameworkCore;

namespace test.Tests
{
    public class CurrencyConversionServiceTests
    {
        private ConversionAppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ConversionAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new ConversionAppDbContext(options);
        }

        [Fact]
        public void AddConversion_IsAnyConversions()
        {
            var context = GetInMemoryDbContext();
            var service = new CurrencyConversionService(context);
            var conversion = new CurrencyConversion
            {
                FromCurrency = "GBP",
                ToCurrency = "EUR",
                ConversionRate = 1.0M,
                UserEnteredAmount = 100,
                Result = 0,
                ConversionDate = DateTime.Now
            };

            service.AddConversion(conversion);
            Assert.True(context.CurrencyConversions.Any());
        }
    }
}
