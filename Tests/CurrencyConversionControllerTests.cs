using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using test.Models;
using test.Services;
using test.Controllers;

namespace test.Tests
{
    public class CurrencyConversionControllerTests
    {
        [Fact]
        public void Convert_ShouldBe50()
        {
            var mockLogger = new Mock<ILogger<CurrencyConversionController>>();
            var mockService = new Mock<ICurrencyConversionService>();
            mockService.Setup(service => service.ConversionRates)
                .Returns(new Dictionary<string, decimal>
                {
                    { "GBD_EUR", 0.5M }
                });

            var controller = new CurrencyConversionController(mockLogger.Object, mockService.Object);
            var conversion = new CurrencyConversion
            {
                FromCurrency = "GBD",
                ToCurrency = "EUR",
                UserEnteredAmount = 100
            };

            var result = controller.Convert(conversion);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CurrencyConversion>(viewResult.ViewData.Model);
            Assert.Equal(50, model.Result);
        }

        [Fact]
        public void Convert_ConversionDoesntExist_ModelStateContainsError()
        {
            var mockLogger = new Mock<ILogger<CurrencyConversionController>>();
            var mockService = new Mock<ICurrencyConversionService>();
            mockService.Setup(service => service.ConversionRates)
                .Returns(new Dictionary<string, decimal>
                {
                    { "USD_USD", 0.5M }
                });

            var controller = new CurrencyConversionController(mockLogger.Object, mockService.Object);
            var conversion = new CurrencyConversion
            {
                FromCurrency = "GBP",
                ToCurrency = "GBP",
                UserEnteredAmount = 100
            };

            var result = controller.Convert(conversion);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CurrencyConversion>(viewResult.ViewData.Model);

            Assert.True(controller.ModelState.ContainsKey("ConversionNotFound"));
        }
    }
}