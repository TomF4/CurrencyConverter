using Microsoft.AspNetCore.Mvc;
using test.Models;
using test.Services;

namespace test.Controllers
{
    public class CurrencyConversionController : Controller
    {
        private readonly ILogger<CurrencyConversionController> _logger;
        private readonly ICurrencyConversionService _currencyConversionService;

        public CurrencyConversionController(ILogger<CurrencyConversionController> logger, ICurrencyConversionService currencyConversionService)
        {
            _logger = logger;
            _currencyConversionService = currencyConversionService;
        }

        public ActionResult Conversion()
        {
            ViewBag.FromCurrencies = GetFromCurrencies();
            ViewBag.ToCurrencies = GetToCurrencies();
            return View();
        }

        [HttpPost]
        public ActionResult Convert(CurrencyConversion conversion)
        {
            ViewBag.FromCurrencies = GetFromCurrencies();
            ViewBag.ToCurrencies = GetToCurrencies();
            string conversionKey = $"{conversion.FromCurrency}_{conversion.ToCurrency}";

            var conversionRatios = _currencyConversionService.ConversionRates;

            if (conversion.UserEnteredAmount <= 0) { return View("Conversion", conversion); }

            if (conversionRatios.TryGetValue(conversionKey, out decimal conversionRate))
            {
                conversion.Result = conversionRate * conversion.UserEnteredAmount;
                conversion.ConversionRate = conversionRate;
                conversion.ConversionDate = DateTimeOffset.UtcNow;
                _currencyConversionService.AddConversion(conversion);
            }
            else
            {
                ModelState.AddModelError("ConversionNotFound", $"Conversion from '{conversion.FromCurrency}' to '{conversion.ToCurrency}' not found.");
            }

            return View("Conversion", conversion);
        }

        public IActionResult Conversions()
        {
            var conversions = _currencyConversionService.GetConversions();
            return View(conversions);
        }

        private List<string> GetFromCurrencies() => _currencyConversionService.ConversionRates.Keys.Select(x => x.Split("_")[0]).Distinct().ToList();
        private List<string> GetToCurrencies() => _currencyConversionService.ConversionRates.Keys.Select(x => x.Split("_")[1]).Distinct().ToList();
    }
}