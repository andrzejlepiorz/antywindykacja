using Logic.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Logic.Services
{
    public class SeleniumDriverService : ISeleniumDriverService
    {
        private readonly string _phrase, _title, _address;
        private ChromeDriver _driver;
        private int _ifNotExist = 0, _repeatIfNotExist = 0;
        private int _seconds = 60000;
        private readonly ChromeOptions _options;
        private readonly Proxy _proxy;

        public SeleniumDriverService(string phrase, string title, string address)
        {
            try
            {
                _options = new ChromeOptions();
                _proxy = new Proxy();
                _address = address;
                _proxy.Kind = ProxyKind.Manual;
                _proxy.IsAutoDetect = false;
                _proxy.HttpProxy = _proxy.SslProxy = _address;
                _options.Proxy = _proxy;
                _options.AddArgument("ignore-certificate-errors");

                _phrase = phrase;
                _title = title;

                _driver = new ChromeDriver(@"..\netcoreapp2.2\chromedriver", _options);
                Console.Clear();
                Console.WriteLine($"[{_address}] Odpalam przeglądarkę.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RunChrome()
        {
            while(_ifNotExist<=3 && _repeatIfNotExist<=2)
            {
                try
                {
                    Console.WriteLine($"[{_address}] Przechodzę na google.pl");
                    _driver.Navigate().GoToUrl(@"https://www.google.pl");

                    Console.WriteLine($"[{_address}] Wpisuję frazę.");
                    _driver.FindElementByName("q").SendKeys(_phrase);
                    Console.WriteLine($"[{_address}] Wyszukuję wybraną frazę.");
                    _driver.FindElementByName("q").SendKeys(Keys.Enter);
                    Console.WriteLine($"[{_address}] Szukam tytułu reklamy i klikam w nią.");
                    _driver.FindElementByXPath($"//h3[contains(text(), '{_title}')]").Click();

                    _driver.Close();
                    _ifNotExist = 4;
                    _repeatIfNotExist = 3;
                }
                catch (NoSuchElementException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[{_address}] Error: Nie znaleziono reklamy.");
                    Console.ResetColor();

                    if (_ifNotExist == 3 && _repeatIfNotExist == 2)
                        _driver.Close();

                    if (_ifNotExist == 3 && _repeatIfNotExist <=1)
                    {
                        Console.WriteLine($"Za {_seconds / 1000} sekund spróbuję ponownie.");
                        Thread.Sleep(_seconds);
                        _ifNotExist = 0;
                        _repeatIfNotExist++;
                    }
                    _ifNotExist++;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[{_address}] Error: Nie znaleziono reklamy.");
                    Console.WriteLine($"Ooops wystąpił jakiś błąd: {ex.Message}");
                    Console.ResetColor();

                    if (_ifNotExist == 3 && _repeatIfNotExist == 2)
                        _driver.Close();

                    if (_ifNotExist == 3 && _repeatIfNotExist <= 1)
                    {
                        Console.WriteLine($"Za {_seconds / 1000} sekund spróbuję ponownie.");
                        Thread.Sleep(_seconds);
                        _ifNotExist = 0;
                        _repeatIfNotExist++;
                    }
                    _ifNotExist++;
                }
            }
        }
    }
}