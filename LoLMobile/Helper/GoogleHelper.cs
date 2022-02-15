using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace LoLMobile.Helper
{
    public class GoogleHelper
    {
        private static readonly string GoogleKey = Path.Combine("Helper/GoogleKey.json");
        private static readonly string ApplicationName = "Google Sheets API .NET Quickstart";
        public static readonly SheetsService SheetsService;

        static GoogleHelper()
        {
            List<string> Scoped = new() { SheetsService.Scope.Spreadsheets };
            GoogleCredential credential = GoogleCredential
            .FromFile(GoogleKey)
            .CreateScoped(Scoped);

            SheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
    }
}
