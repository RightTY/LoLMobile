namespace LoLMobile.Helper
{
    public class LineBotHelper
    {
        public static readonly string ChannelAccessToken = "SU0mi6z1Lt02IOa2sxSMYfhO+X/yFMfaTxcMH4QTqjHaXK8KEYPv6WH0ohvFf2pOElwsNyjhg9gLvhstfweDHnguKk4zqOFYC7QMgj8G5ZSc01PZWhKDUMeddu4sLaiSZ5xNSwy9xUqp8YRM0TlsGwdB04t89/1O/w1cDnyilFU=";
        public static readonly string GroupId = "Cd1ca9dc3eefee84eb91a727d3bc2aaac";
        public static readonly string AdminId = "U614a3caf28092783f99e50ccd5372567";

        public static bool CheckMessageLength(string message)
        {
            if (message.Length < 0 || message.Length > 1800)
            {
                return false;
            }
            return true;
        }
    }
}
