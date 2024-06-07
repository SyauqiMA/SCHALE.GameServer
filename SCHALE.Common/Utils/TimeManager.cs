namespace SCHALE.Common.Utils
{
    public static class TimeManager
    {
        public static DateTime KoreaNow
        {
            get
            {
                var now = DateTime.Now;
                TimeZoneInfo koreaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                    "Korea Standard Time"
                );
                return TimeZoneInfo.ConvertTime(now, koreaTimeZone);
            }
        }
    }
}
