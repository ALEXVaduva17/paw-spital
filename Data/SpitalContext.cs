using System;
using Microsoft.EntityFrameworkCore;
using PawSpital.Models;

namespace PawSpital.Data
{
    public class SpitalContext : DbContext
    {
        public SpitalContext(DbContextOptions<SpitalContext> options)
            : base(options)
        {
        }

        public DbSet<Departament> Departamente { get; set; }
        public DbSet<Doctor> Doctori { get; set; }
        public DbSet<Serviciu> Servicii { get; set; }
        public DbSet<Sala> Salii { get; set; }
        public DbSet<Programare> Programari { get; set; }
        public DbSet<Recenzie> Recenzii { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Seed Departamente ──────────────────────────────────────
            modelBuilder.Entity<Departament>().HasData(
                new Departament { Id = 1, Nume = "Cardiologie", Descriere = "Departamentul de boli cardiovasculare" },
                new Departament { Id = 2, Nume = "Neurologie", Descriere = "Departamentul de afectiuni neurologice" },
                new Departament { Id = 3, Nume = "Ortopedie", Descriere = "Departamentul de chirurgie ortopedica" },
                new Departament { Id = 4, Nume = "Pediatrie", Descriere = "Departamentul pentru ingrijirea copiilor" },
                new Departament { Id = 5, Nume = "Dermatologie", Descriere = "Departamentul de afectiuni ale pielii" },
                new Departament { Id = 6, Nume = "Oftalmologie", Descriere = "Departamentul de boli ale ochilor" }
            );

            // ── Seed Doctori ───────────────────────────────────────────
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Nume = "Dr. Andrei Popescu", Specializare = "Cardiologie", DepartamentId = 1 },
                new Doctor { Id = 2, Nume = "Dr. Maria Ionescu", Specializare = "Neurologie", DepartamentId = 2 },
                new Doctor { Id = 3, Nume = "Dr. Vlad Georgescu", Specializare = "Ortopedie", DepartamentId = 3 },
                new Doctor { Id = 4, Nume = "Dr. Elena Dumitrescu", Specializare = "Pediatrie", DepartamentId = 4 },
                new Doctor { Id = 5, Nume = "Dr. Cristian Radu", Specializare = "Dermatologie", DepartamentId = 5 },
                new Doctor { Id = 6, Nume = "Dr. Ana Vasilescu", Specializare = "Oftalmologie", DepartamentId = 6 },
                new Doctor { Id = 7, Nume = "Dr. Mihai Stanescu", Specializare = "Cardiologie interventionala", DepartamentId = 1 },
                new Doctor { Id = 8, Nume = "Dr. Ioana Popa", Specializare = "Neuropediatrie", DepartamentId = 2 }
            );

            // ── Seed Servicii ──────────────────────────────────────────
            modelBuilder.Entity<Serviciu>().HasData(
                new Serviciu { Id = 1, Nume = "Consultatie generala", Pret = 150.00m, Descriere = "Consultatie medicala de baza" },
                new Serviciu { Id = 2, Nume = "Ecografie cardiaca", Pret = 350.00m, Descriere = "Ecografie Doppler a inimii" },
                new Serviciu { Id = 3, Nume = "EKG", Pret = 120.00m, Descriere = "Electrocardiograma standard" },
                new Serviciu { Id = 4, Nume = "RMN cerebral", Pret = 800.00m, Descriere = "Rezonanta magnetica nucleara craniu" },
                new Serviciu { Id = 5, Nume = "Radiografie", Pret = 100.00m, Descriere = "Radiografie standard" },
                new Serviciu { Id = 6, Nume = "Analiza de sange completa", Pret = 200.00m, Descriere = "Hemoleucograma completa" },
                new Serviciu { Id = 7, Nume = "Consultatie pediatrica", Pret = 180.00m, Descriere = "Consultatie specializata copii" },
                new Serviciu { Id = 8, Nume = "Examen oftalmologic", Pret = 250.00m, Descriere = "Examen complet al vederii" }
            );

            // ── Seed Sali ──────────────────────────────────────────────
            modelBuilder.Entity<Sala>().HasData(
                new Sala { Id = 1, Nume = "Sala A1 - Consultatii", Etaj = 0 },
                new Sala { Id = 2, Nume = "Sala A2 - Consultatii", Etaj = 0 },
                new Sala { Id = 3, Nume = "Sala B1 - Investigatii", Etaj = 1 },
                new Sala { Id = 4, Nume = "Sala B2 - Imagistica", Etaj = 1 },
                new Sala { Id = 5, Nume = "Sala C1 - Operatii", Etaj = 2 },
                new Sala { Id = 6, Nume = "Sala C2 - Recuperare", Etaj = 2 }
            );

            // ── Seed Programari ────────────────────────────────────────
            modelBuilder.Entity<Programare>().HasData(
                new Programare { Id = 1, NumePacient = "Ion Marinescu", Telefon = "0721000001", DoctorId = 1, ServiciuId = 2, SalaId = 1, Data = new DateTime(2026, 5, 5, 9, 0, 0), Status = "Confirmata" },
                new Programare { Id = 2, NumePacient = "Ana Petrescu", Telefon = "0731000002", DoctorId = 2, ServiciuId = 4, SalaId = 3, Data = new DateTime(2026, 5, 6, 10, 30, 0), Status = "In asteptare" },
                new Programare { Id = 3, NumePacient = "George Enescu", Telefon = "0741000003", DoctorId = 4, ServiciuId = 7, SalaId = 2, Data = new DateTime(2026, 5, 7, 14, 0, 0), Status = "Confirmata" },
                new Programare { Id = 4, NumePacient = "Lucia Badea", Telefon = "0751000004", DoctorId = 6, ServiciuId = 8, SalaId = 4, Data = new DateTime(2026, 5, 8, 11, 0, 0), Status = "In asteptare" }
            );

            // ── Seed Recenzii ──────────────────────────────────────────
            modelBuilder.Entity<Recenzie>().HasData(
                new Recenzie { Id = 1, NumePacient = "Ion Marinescu", Rating = 5, Comentariu = "Doctor excelent, foarte atent cu pacientii!", DoctorId = 1 },
                new Recenzie { Id = 2, NumePacient = "Maria Dumitru", Rating = 4, Comentariu = "Profesionalism si rapiditate.", DoctorId = 2 },
                new Recenzie { Id = 3, NumePacient = "George Enescu", Rating = 5, Comentariu = "Recomand cu incredere pentru copii.", DoctorId = 4 },
                new Recenzie { Id = 4, NumePacient = "Elena Stan", Rating = 4, Comentariu = "Foarte multumita de consultatie.", DoctorId = 5 }
            );
        }
    }
}
