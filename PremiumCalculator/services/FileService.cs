using PremiumCalculator.Models;
using System.Globalization;
using System.Numerics;
using System.Text.Json;

namespace PremiumCalculator.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private List<PremiumRecord> _premiumRecords;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._premiumRecords = new List<PremiumRecord>();
        }

        public IEnumerable<PremiumResult> GetPremiums(PremiumParameters premiumParameters)
        {
            CultureInfo culture = new CultureInfo("en-EN");
            var premiumResults = this._premiumRecords.Where(e => e.Plan.Contains(premiumParameters.Plan) && (e.MonthOfBirth.Equals("*") || e.MonthOfBirth.Equals(premiumParameters.DateOfBirth.ToString("MMMM", culture))) && (e.State.Equals("*") || e.State.Equals(premiumParameters.State)) && e.InitialAgeRange <= premiumParameters.Age && premiumParameters.Age <= e.FinalAgeRange)
                .Select(e => new PremiumResult { Carrier = e.Carrier, Premium = e.Premium });

            return premiumResults;
        }

        public FileService SavePremium(PremiumRecord premiumRecord)
        {
            this._premiumRecords.Add(premiumRecord);
            return this;
        }

        public FileService SavePremium(IEnumerable<PremiumRecord> premiumRecords)
        {
            this._premiumRecords.AddRange(premiumRecords);
            return this;
        }

        public FileService InitializeRecords()
        {
            if (this._premiumRecords == null || this._premiumRecords.Count() == 0)
            {
                string pathFile = this._webHostEnvironment.ContentRootPath + "\\Files\\PremiumRecords.json";
                string jsonString = System.IO.File.ReadAllText(pathFile);

                this._premiumRecords = JsonSerializer.Deserialize<List<PremiumRecord>>(jsonString);
            }

            return this;
        }
    }
}
