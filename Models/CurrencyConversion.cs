using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class CurrencyConversion
    {
        public int Id { get; set; }  // Primary key
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid number.")]
        public decimal UserEnteredAmount { get; set; } 
        public decimal ConversionRate { get; set; }
        public decimal Result { get; set; }
        public DateTimeOffset ConversionDate { get; set; } 
    }
}