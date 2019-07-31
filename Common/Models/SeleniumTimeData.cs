using System.Configuration;

namespace Common.Models
{
    public static class SeleniumTimeData
    {
        public static int hourFrom = int.Parse(ConfigurationManager.AppSettings["HourFrom"]);
        public static int hourTo = int.Parse(ConfigurationManager.AppSettings["HourTo"]);
    }
}
