using System.Text.Json;

namespace LoLMobile.Extension
{
    public static class StringExtension
    {
        public static bool IsJsonArrayValid(this string txt)
        {
            try
            {
                List<JsonValueKind> JsonValueKinds = new() { JsonValueKind.Array/*, JsonValueKind.Object*/ };
                return JsonValueKinds.Contains(JsonDocument.Parse(txt).RootElement.ValueKind);
            }
            catch { }

            return false;
        }
    }
}
