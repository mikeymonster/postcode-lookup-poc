using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace postcode_lookup_poc.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        [BindProperty]
        [Required(ErrorMessage = "Please enter your company name.")]
        [StringLength(35, ErrorMessage = "Maximum length {1}.")]
        [RegularExpression(@"^[a-zA-Z0-9\u0080-\uFFA7?$@#()&quot;&#39;!,+\-=_:;.&amp;€£*%\s\/]+$",
            ErrorMessage = "Company name contains some invalid characters")]
        public string CompanyName { get; private set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter the first line of your address.")]
        [StringLength(50, ErrorMessage = "First line of address mustn’t exceed {0} characters")]
        [RegularExpression(@"^[a-zA-Z0-9\u0080-\uFFA7?$@#()&quot;&#39;!,+\-=_:;.&amp;€£*%\s\/]+$",
            ErrorMessage = "First line of address contains some invalid characters")]
        public string AddressLine1 { get; private set; }

        [BindProperty]
        [StringLength(50, ErrorMessage = "Second line of address mustn’t exceed {0} characters")]
        [RegularExpression(@"^[a-zA-Z0-9\u0080-\uFFA7?$@#()&quot;&#39;!,+\-=_:;.&amp;€£*%\s\/]+$",
            ErrorMessage = "Second line of address contains some invalid characters")]
        public string AddressLine2 { get; private set; }

        [BindProperty]
        [StringLength(50, ErrorMessage = "Third line of address mustn’t exceed {0} characters")]
        [RegularExpression(@"^[a-zA-Z0-9\u0080-\uFFA7?$@#()&quot;&#39;!,+\-=_:;.&amp;€£*%\s\/]+$",
            ErrorMessage = "Third line of address contains some invalid characters")]
        public string AddressLine3 { get; private set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your city.")]
        [StringLength(50, ErrorMessage = "City mustn’t exceed {0} characters")]
        [RegularExpression(@"^[a-zA-Z0-9\u0080-\uFFA7?$@#()&quot;&#39;!,+\-=_:;.&amp;€£*%\s\/]+$",
            ErrorMessage = "City contains some invalid characters")]
        public string AddressCity { get; private set; }

        [BindProperty] public double Latitude { get; private set; }

        [BindProperty] public double Longitude { get; private set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your postcode")]
        [StringLength(8, ErrorMessage = "Postcode mustn’t exceed {0} characters")]
        [RegularExpression(
            @"^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$",
            ErrorMessage = "The postcode is not a valid format")]
        public string Postcode { get; private set; }

        public string PostCodeAnywhereApiKey { get; private init; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            PostCodeAnywhereApiKey = _configuration["PostCodeAnywhereApiKey"];
        }

        public void OnGet()
        {
            //PostCodeAnywhereApiKey = _configuration["PostCodeAnywhereApiKey"];
        }

        public IActionResult OnPostSubmitForm()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _logger.LogInformation($"Received employer form");
            _logger.LogInformation($"    - Company name:   {CompanyName}");
            _logger.LogInformation($"    - Address line 1: {AddressLine1}");
            _logger.LogInformation($"    - Address line 2: {AddressLine2}");
            _logger.LogInformation($"    - Address line 3: {AddressLine3}");
            _logger.LogInformation($"    - Address city:   {AddressCity}");
            _logger.LogInformation($"    - Postcode:       {Postcode}");
            _logger.LogInformation($"    - Latitude:       {Latitude}");
            _logger.LogInformation($"    - Longitude:      {Longitude}");

            //ClearForm();
            return Page();
        }

        public IActionResult OnPostClearForm()
        {
            ClearForm();

            ModelState.Clear();

            return Page();
        }

        private void ClearForm()
        {
            CompanyName = null;
            AddressLine1 = null;
            AddressLine2 = null;
            AddressLine3 = null;
            AddressCity = null;
            Postcode = null;
            Latitude = double.NaN;
            Longitude = double.NaN;
        }
    }
}