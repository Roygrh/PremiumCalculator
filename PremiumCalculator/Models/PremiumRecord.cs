using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PremiumCalculator.Models
{
    public class PremiumRecord
    {
        public string Carrier { get; set; }
        public string Plan { get; set; }
        public string State { get; set; }
        public string MonthOfBirth { get; set; }
        public int InitialAgeRange { get; set; }
        public int FinalAgeRange { get; set; }
        public decimal Premium { get; set; }
    }
}
