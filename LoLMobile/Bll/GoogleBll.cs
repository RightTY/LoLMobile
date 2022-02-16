using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using LoLMobile.Helper;

namespace LoLMobile.Bll
{
    public class GoogleBll
    {
        private readonly string Sheets = "1v2wfUsuvIXoFdtnT1ZVQDiuBM74moKx58OAyWMCgTiI";
        private readonly int UsersSheetId = 1699828542;
        private readonly int ProgramSettingtSheetId = 1692751547;
        public ValueRange GetUsers()
        {
            SpreadsheetsResource.GetRequest request =
                GoogleHelper.SheetsService.Spreadsheets.Get(Sheets);
            Spreadsheet response = request.Execute();
            SheetProperties usersSheetProperties = response.Sheets.Where(x => x.Properties.SheetId == UsersSheetId).Single().Properties;
            SpreadsheetsResource.ValuesResource.GetRequest sheetRequest =
                GoogleHelper.SheetsService.Spreadsheets.Values.Get(Sheets, usersSheetProperties.Title + "!B2:E");
            return sheetRequest.Execute();
        }

        public string GetWelcomeMsg()
        {
            SpreadsheetsResource.GetRequest request =
                GoogleHelper.SheetsService.Spreadsheets.Get(Sheets);
            Spreadsheet response = request.Execute();
            SheetProperties ProgramSettingtSheetProperties = response.Sheets.Where(x => x.Properties.SheetId == ProgramSettingtSheetId).Single().Properties;
            SpreadsheetsResource.ValuesResource.GetRequest sheetRequest =
                GoogleHelper.SheetsService.Spreadsheets.Values.Get(Sheets, ProgramSettingtSheetProperties.Title + "!B1");
            ValueRange valueRange = sheetRequest.Execute();
#nullable disable
            return valueRange.Values[0][0].ToString();
        }

        public string GetAnnouncement()
        {
            SpreadsheetsResource.GetRequest request =
                GoogleHelper.SheetsService.Spreadsheets.Get(Sheets);
            Spreadsheet response = request.Execute();
            SheetProperties ProgramSettingtSheetProperties = response.Sheets.Where(x => x.Properties.SheetId == ProgramSettingtSheetId).Single().Properties;
            SpreadsheetsResource.ValuesResource.GetRequest sheetRequest =
                GoogleHelper.SheetsService.Spreadsheets.Values.Get(Sheets, ProgramSettingtSheetProperties.Title + "!B2");
            ValueRange valueRange = sheetRequest.Execute();
#nullable disable
            return valueRange.Values[0][0].ToString();
        }
    }
}
