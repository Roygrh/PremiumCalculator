using Microsoft.AspNetCore.Mvc;
using PremiumCalculator.Models;
using PremiumCalculator.Services;
using System.Globalization;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PremiumCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumCalculatorController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FileService _fileService;

        public PremiumCalculatorController(IWebHostEnvironment webHostEnvironment, FileService fileService)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._fileService = fileService;
            //this._fileService.InitializeRecords();
        }

        [HttpPost("get-premiums")]
        public IActionResult GetPremiums([FromBody] PremiumParameters premiumParameters)
        {
            try
            {
                DateTime today = DateTime.Now;
                int age = today.Year - premiumParameters.DateOfBirth.Year;
                age = premiumParameters.DateOfBirth.DayOfYear > today.DayOfYear ? age - 1: age;

                if (premiumParameters.Age != age)
                {
                    return BadRequest("Incorrect date of birth and age format");
                }

                IEnumerable<PremiumResult> result = this._fileService.GetPremiums(premiumParameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("save-premium")]
        public IActionResult SavePremium([FromBody] PremiumRecord premiumRecord)
        {
            try
            {
                this._fileService.SavePremium(premiumRecord);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("save-premiums")]
        public IActionResult SavePremiums([FromBody] PremiumRecord premiumRecord)
        {
            try
            {
                this._fileService.SavePremium(premiumRecord);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-states")]
        public IActionResult GetStates()
        {
            try
            {
                string pathFile = this._webHostEnvironment.ContentRootPath + "\\Files\\states.json";
                string jsonString = System.IO.File.ReadAllText(pathFile);

                List<string> states = JsonSerializer.Deserialize<List<string>>(jsonString);

                this._fileService.InitializeRecords();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-month-names")]
        public IActionResult GetMonthNames()
        {
            try
            {
                CultureInfo culturaEspañola = new CultureInfo("en-EN");

                IEnumerable<string> monthNames = culturaEspañola.DateTimeFormat.MonthNames.Where(m => !m.Equals(""));
                this._fileService.InitializeRecords();
                return Ok(monthNames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("initialize-test-data")]
        public IActionResult InitializeTestData()
        {
            try
            {
                this._fileService.InitializeRecords();
                return Ok("Successful initialization");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
