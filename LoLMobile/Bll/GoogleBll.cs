using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using LoLMobile.Helper;

namespace LoLMobile.Bll
{
    public class GoogleBll
    {
        private readonly string Sheets = "1v2wfUsuvIXoFdtnT1ZVQDiuBM74moKx58OAyWMCgTiI";
        private readonly int UsersSheetId = 1699828542;
        public ValueRange GetUsers()
        {
            SpreadsheetsResource.GetRequest request =
                    GoogleHelper.SheetsService.Spreadsheets.Get(Sheets);
            Spreadsheet response = request.Execute();
            SheetProperties UsersSheetProperties = response.Sheets.Where(x => x.Properties.SheetId == UsersSheetId).Single().Properties;
            SpreadsheetsResource.ValuesResource.GetRequest sheetRequest =
                GoogleHelper.SheetsService.Spreadsheets.Values.Get(Sheets, UsersSheetProperties.Title + "!B2:E");
            return sheetRequest.Execute();
        }
    }
}
