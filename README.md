# Forum RP

Forum RP to aplikacja webowa stworzona w ASP.NET Core, umożliwiająca użytkownikom tworzenie, przeglądanie i komentowanie postów na forum internetowym.

## Wymagania

- .NET 8.0+
- SQL Server (np. LocalDB)
- Node.js (jeśli używane są zasoby frontendowe)

## Instalacja i uruchomienie

1. **Sklonuj repozytorium:**
   ```sh
   git clone https://github.com/twoje-repozytorium/forum-rp.git
   cd forum-rp
   ```

2. **Przygotuj bazę danych:**
   - W pliku `appsettings.json` skonfiguruj połączenie do SQL Server.
   - Uruchom migracje bazy danych:
     ```sh
     dotnet ef database update
     ```

3. **Uruchom aplikację:**
   ```sh
   dotnet run
   ```
   Aplikacja powinna być dostępna pod `http://localhost:5000` (lub innym portem ustawionym w konfiguracji).

## Struktura projektu

- `Forum_RP/Program.cs` - Główny punkt wejścia aplikacji.
- `Forum_RP/appsettings.json` - Konfiguracja aplikacji, w tym połączenie do bazy danych.
- `Forum_RP/Models/` - Modele bazy danych.
- `Forum_RP/Controllers/` - Kontrolery obsługujące logikę biznesową.
- `Forum_RP/Views/` - Widoki (MVC).

## Technologie

- **ASP.NET Core** - Backend aplikacji.
- **Entity Framework Core** - ORM do obsługi bazy danych.
- **SQL Server** - Przechowywanie danych.
- **Bootstrap/Tailwind (jeśli używane)** - Stylowanie UI.
