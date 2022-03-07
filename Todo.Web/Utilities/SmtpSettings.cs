namespace Todo.Web.Utilities
{
    public static class SmtpSettings
    {
        public static string Host = DataUtility.GetSmtpHost();
        public static int Port = DataUtility.GetSmtpPort();
        public static string User = DataUtility.GetSmtpUser();
        public static string Password = DataUtility.GetSmtpPassword();
        public static string Gmail_Host = DataUtility.GetGmailHost();
        public static string Gmail_Password = DataUtility.GetGmailPassword();

    }
}
