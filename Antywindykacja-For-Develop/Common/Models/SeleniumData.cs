using System.Configuration;

namespace Common.Models
{
    public static class SeleniumData
    {
        public static string Phrase = ConfigurationManager.AppSettings["Phrase"];
        public static string Title = ConfigurationManager.AppSettings["Title"];
    }
}
