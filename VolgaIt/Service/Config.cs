namespace VolgaIt.Service
{
    public class Config
    {
        public static string ConnectionString { get; set; }
        public static int AccesTokenExpiredTimePerMinute { get; set; }
        public static int RefreshTokenExpiredTimePerDays { get; set; }
    }
}
