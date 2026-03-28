## Ghid de prezentare – PawSpital

Acest ghid sumarizează pe scurt ce este implementat, unde se află în proiect și cum funcționează, în raport cu cerințele:

### 1) Design-ul bazei de date (5–6 tabele, fără tabele de user)
- **Tabele**: `Departament`, `Doctor`, `Serviciu`, `Programare`, `Recenzie` (5 tabele).
- **Diagrama relațională**:
  - Imagine: `docs/diagrama-model-relational.png`
  - Document cu ER (Mermaid): `docs/Raport_BazaDeDate.md` (secțiunea „Diagrama modelului relațional”).

### 2) Implementarea bazei de date – Code First
- **Modele (entități)**: folderul `Models/`
  - `Models/Departament.cs`
  - `Models/Doctor.cs`
  - `Models/Serviciu.cs`
  - `Models/Programare.cs`
  - `Models/Recenzie.cs`
- **Context EF Core**: `Data/SpitalContext.cs` – mapează entitățile la tabele prin `DbSet<>`.
- **Migrații**: folderul `Migrations/` – conține istoricul pentru generarea bazei în PostgreSQL.
- Cum funcționează: în abordarea Code First, structura DB este derivată din clasele C# și relațiile dintre ele; migrațiile generează schema în baza de date.

### 3) Conexiunea aplicației cu baza de date (Entity Framework Core + PostgreSQL/Npgsql)
- **String de conexiune**: `appsettings.json` la cheia `ConnectionStrings:DefaultConnection`.
- **Înregistrarea DbContext**: `Program.cs` – serviciul EF Core este adăugat cu providerul Npgsql (PostgreSQL).
- Pipeline-ul MVC este configurat în `Program.cs` (routing, assets, etc.).

### 4) Testarea conexiunii (CRUD pe o entitate)
- **Controller CRUD**: `Controllers/DepartamenteController.cs` – operații Create/Read/Update/Delete pentru `Departament`.
- **View-uri generate pentru testare**: `Views/Departamente/` (`Index`, `Details`, `Create`, `Edit`, `Delete`).
- Cum se verifică:
  - Accesezi pagina `Acasă -> Departamente` sau ruta `/Departamente`.
  - Creezi, editezi, ștergi și vizualizezi departamentele din DB prin UI, confirmând funcționarea EF + conexiune.

### Cum rulezi local pentru demo
1) Asigură-te că PostgreSQL rulează local și datele de conectare din `appsettings.json` sunt corecte.
2) Aplică migrațiile (prima rulare sau după schimbări de model):
   - PowerShell, din folderul proiectului:
     - Instalează uneltele (o singură dată): `dotnet tool install --global dotnet-ef`
     - Actualizează baza: `dotnet ef database update`
3) Pornește aplicația: `dotnet run`
4) Deschide în browser ruta `/Departamente` și execută operațiile CRUD.

### Unde găsește profesoara rapid elementele-cheie
- Design DB: `docs/diagrama-model-relational.png` și `docs/Raport_BazaDeDate.md`
- Modele: `Models/*`
- Context EF: `Data/SpitalContext.cs`
- Conexiune + provider: `appsettings.json` și `Program.cs`
- Test CRUD: `Controllers/DepartamenteController.cs` + `Views/Departamente/*`

### Observații
- Providerul folosit este **PostgreSQL (Npgsql)**.
- Soluția respectă separarea pe straturi: modele, context, controllers, views.

