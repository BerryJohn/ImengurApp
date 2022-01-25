# Imengur - Image upload app
#### Projekt zaliczeniowy z przedmiotu "Programowanie w środowisku ASP.NET"

`Stworzone przez: Bąk Jan`  
`Numer indeku: 12158`

---
## Konfiguracja:
### Połączenie z bazą danych
Konfiguracje rozpoczynamy od zmiany `ConnectionString` w pliku `appsetings.json`.  
Odpowiednie formuły zmieniamy na potrzeby naszego systemu

`DATA SOURCE={Nazwa maszyny};Integrated Security=true;DATABASE={Nazwa bazy};Trusted_Connection=True;MultipleActiveResultSets=True`  
Na przykład:  
`DATA SOURCE=DESKTOP-SHG0JP3;Integrated Security=true;DATABASE=Imengur_DB;Trusted_Connection=True;MultipleActiveResultSets=True`  

### Stworzenie bazy danych
W konsoli menedżera pakietów wpisać odpowiednie komendy:  
`add-migration {nazwa migracji}`  
następnie  
`update-database -verbose`  
Po tych komendach powinnam nam się utworzyć baza danych, która powinna być widziana na naszym lokalnym serwerze MSSQL (Na przykład w programie `Microsoft SQL Server Managment Studio`).
### Utworzenie katalogu na zdjęcia
Przed uruchomieniem aplikacji, należy utworzyć odpowiedni katalog na trzymanie zdjęć.  
W katalogu `wwwroot` tworzymy folder o nazwie `uploads`.

### !Przed uruchomieniem należy sprawdzić, czy w pakietach NuGet znajdują się odpowiednie biblioteki!

 1. Microsoft.AspNetCore.Identity.EntityFrameworkCore w wersji 5.0.10
 2. Microsoft.EntityFrameworkCore w wersji 5.0.10
 3. Microsoft.EntityFrameworkCore.SqlServer w wersji 5.0.10
 4. Microsoft.EntityFrameworkCore.Tools w wersji 5.0.10
 5. Microsoft.VisualStudio.Web.BrowserLink w wersji 2.2.0
 6. PagedList.Mvc w wersji 4.5.0

## Działanie aplikacji
### Do dyspozycji domyślnie mamy 2 utworzone konta

 
|  | Administrator |Moderator |
|--|--|--|
|**Login:**  |adminZbigniew  | moderatorPieter|
|**Hasło:**|Qwe12#|Qwe12#|
---
#### Administrator może:
 - Usuwać zdjęcia
 - Usuwać komentarze
 - Usuwać użytkowników
 - Edytować komentarze
 #### Moderator może:
 - Usuwać komentarze
 ---
Poza domyślnymi kontami mamy możliwość utworzenia własnego konta.  
Po zalogowaniu się, mamy opcję dodawania własnych zdjęć oraz komentowania ich.  

---
Projekt polegał na stworzeniu aplikacji do uploadowania własnych zdjęć/gifów, na wzór strony `www.imgur.com`. Dostęp do tych funkcji mają wyłączenie zalogowani użytkownicy. Użytkownik niezalogowany ma dostęp do przeglądania zdjęć, bez możliwości komentowania. 
---
*Jan Bąk 25.01.2022, Kraków*