# Documentație aplicație - Spital

## 1. Scopul aplicației

Aplicația reprezintă o interfață web simplă pentru un spital. Ea simulează principalele secțiuni pe care un pacient le-ar putea folosi: vizualizare servicii, doctori, programări, departamente, informații de contact și profil.

## 2. Tehnologii utilizate

- HTML5 pentru structura paginilor.
- CSS simplu + Bootstrap 5 (CDN) pentru stilizare minimală.
- JavaScript foarte simplu pentru simularea utilizatorului conectat.

## 3. Pagini existente în aplicație

- **index.html** – Pagina Acasă, prezentare generală.
- **servicii.html** – Listă de servicii medicale.
- **doctori.html** – Tabel cu doctori și specializări.
- **programari.html** – Formular simplu pentru programări.
- **departamente.html** – Prezentare departamente.
- **contact.html** – Date de contact ale spitalului.
- **despre.html** – Descrierea aplicației.
- **profil.html** – Pagina de profil a utilizatorului (simulat).
- **login.html** – Formular de autentificare (nu se contorizează la cele 7 pagini cerute).
- **register.html** – Formular de înregistrare (nu se contorizează la cele 7 pagini cerute).

## 4. Layout comun și navigare

- Toate paginile folosesc același navbar Bootstrap.
- Navbar-ul conține link-uri către: Acasă, Servicii, Doctori, Programări, Departamente, Contact, Despre.
- Există buton spre pagina de **Profil**.
- Există link-uri spre paginile **Login** și **Register**.

## 5. Afișare utilizator conectat

În fișierul `js/script.js` este definit un obiect JavaScript `loggedUser` cu numele și emailul utilizatorului (simulat). La încărcarea paginii, scriptul caută elementele cu id-urile `user-name` și `user-email` și afișează valorile.

## 6. Limitări

- Nu există backend sau bază de date reală.
- Formularele nu trimit date și nu se face validare complexă.
- Scopul este strict educațional / de prezentare, cu cod cât mai simplu.

