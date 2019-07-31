# Antywindykacja

## Spis treści
* [Technologie](#technologie)
* [Konfiguracja](#konfiguracja)
* [Uruchomienie](#uruchomienie)
* [Działanie programu](#działanie-programu)
* [Licencja](#licencja)

## Technologie
* .NET Core 2.2.150
* Selenium 3.141.0

## Konfiguracja
Przed uruchomieniem programu powinniśmy sprawdzić czy jest on dobrze skonfigurowany. Bardzo ważne jest podanie godzin, w których program powinien działać oraz wpisanie odpowiedniej frazy do wyszukania w google i tytułu reklamy.
Wszystko robimy w pliku o nazwie `SeleniumChrome.dll.config`
* Dodawanie frazy: Wyszukujemy `<add key="Phrase" value="nasza fraza"/>`, gdzie w `value=""` podajemy naszą frazę.
* Dodawanie tytułu: Wyszukujemy `<add key="Title" value="tytuł reklamy"/>`, gdzie w `value=""` podajemy tytuł reklamy.
* Ustawienie godziny od której działa program: Wyszukujemy `<add key="HourFrom" value="8"/>`, gdzie w `value=""` podajemy pełną godzinę. Należy pamiętać o poprawnym formacie godziny z przedziału 0-23.
* Ustawienie godziny do której działa program: Wyszukujemy `<add key="HourTo" value="16"/>`, gdzie w `value=""` podajemy pełną godzinę. Należy pamiętać o poprawnym formacie godziny z przedziału 0-23.
* Dodawanie adresów ip proxy: Aby dodać adres ip to należy wpisać w sekcji <Addresses> `<add key="111.222.111.222:1234" value=""></add>`, gdzie w `key=""` wpisujemy nasz adres ip z proxy.
* Po dokonaniu zmian należy zapisać plik.

## Uruchomienie
* Aby program działał poprawnie musimy się upewnić, że w plikach programu znajduje się `../chromedriver/chromedriver.exe`, w przypadku jego braku plik [ChromeDriver.exe](https://chromedriver.chromium.org/downloads) można pobrać ze strony 
* Otwieramy konsolę
* Przez użycie komendy `cd` przechodzimy do folderu przez podanie ścieżki gdzie znajdują się pliki z programu np. ` cd dokumenty/folder1/folder2 `
* Uruchomić program wpisujemy komendę `dotnet nazwaProgramu.dll` , lecz musimy pamiętać żeby znajdować się w odpowiednim folderze

## Działanie programu
Po uruchomieniu program wybiera adres ip z podanych w pliku konfiguracyjnym (`SeleniumChrome.dll.config`) na podstawie którego łączy się z przeglądarką. Następnie zostaje uruchomiony Google Chrome i jest wczytana strona google.pl, gdzie zostaje wpisana podana przez nas fraza. Po wczytaniu frazy jest szukana reklama z podanym przez nas tytułem. Gdy tytuł zostanie znaleziony to program załaduje nam stronę tej reklamy. Później program zamyka przeglądarkę i losuje kolejny adres ip z podanych po czym zaczyna powtarzać wszystkie procesy co na początku lecz już z innym adresem ip. Program tak będzie działał w kółko aż do godziny, która zakończy jego działanie i została podana w pliku konfiguracyjnym. 
Jeżeli program nie znajdzie wybranego tytułu lub będzie miał problem z połączeniem to następują ponowne próby wyszukania lub połączenia. Jeżeli problem dalej będzie występował to nastąpią dwie kolejne próby w odstępie 60 sekund, w których program trzy razy będzie próbował sie połączyć, a gdy one nie przyniosą rezultatów, to przeglądarka jest zamykana i następuje próba połączenia z nowym losowo wybranym adresem ip.

## Licencja
MIT