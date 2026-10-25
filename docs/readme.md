# StayFlow

System do obsługi gości i zarządzania procesami operacyjnymi hotelu. Projekt inżynierski.

Składa się z backendu (ASP.NET Core), bazy PostgreSQL, aplikacji webowej (React) i aplikacji mobilnej dla personelu (React Native). Na razie gotowa jest struktura repozytorium, reszta w trakcie.

## Struktura

- `backend/` – API w .NET
- `database/` – skrypty SQL bazy danych
- `docs/` – dokumentacja

## Uruchomienie bazy

Wymagany Docker.

```
docker compose up -d
```

Przy pierwszym starcie wykonują się skrypty z `database/`. Reset bazy od zera:

```
docker compose down -v
docker compose up -d
```

Połączenie: `localhost:5434`, baza `stayflow`, użytkownik `stayflow`, hasło w pliku `.env` (skopiuj z `.env.example`; plik nie jest w repo).
