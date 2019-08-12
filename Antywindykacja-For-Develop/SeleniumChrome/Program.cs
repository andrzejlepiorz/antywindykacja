using Common.Helpers;
using Common.Models;
using Logic.Services;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace SeleniumChrome
{
    class Program
    {
        static void Main(string[] args)
        {
            var addresses = ConfigurationManager.GetSection("Addresses") as NameValueCollection;
            int index = 0;
            var random = new Random();
            SeleniumDriverService seleniumDriverService;
            DateTime timeCurrent = DateTime.Now;

            try
            {
                if (HourFormat.CorrectHourFormat(SeleniumTimeData.hourFrom, SeleniumTimeData.hourTo))
                {
                    do
                    {
                        while (timeCurrent.Hour >= SeleniumTimeData.hourFrom && timeCurrent.Hour < SeleniumTimeData.hourTo)
                        {
                            index = random.Next(addresses.AllKeys.Count());
                            var address = addresses.AllKeys[index];
                            
                            seleniumDriverService = new SeleniumDriverService(SeleniumData.Phrase, SeleniumData.Title, address);
                            seleniumDriverService.RunChrome();
                            
                            timeCurrent = DateTime.Now;
                        }

                        if (timeCurrent.Hour <= SeleniumTimeData.hourFrom)
                        {
                            Console.Clear();
                            Console.WriteLine($"Program uruchomi się o godzinie {SeleniumTimeData.hourFrom}.");
                            Thread.Sleep(1000);
                        }
                        timeCurrent = DateTime.Now;
                    } while (timeCurrent.Hour <= SeleniumTimeData.hourFrom);
                    Console.Clear();
                    Console.WriteLine("Czas się skończył.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Format godziny jest niepoprawny, podaj godzinę z przedziału od 0 do 23");
                    Console.ResetColor();
                }
            }
            catch(TypeInitializationException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Format godziny jest niepoprawny, podaj godzinę z przedziału od 0 do 23");
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.ReadKey();
        }
    }
}
