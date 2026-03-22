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
        public DbSet<Programare> Programari { get; set; }
        public DbSet<Recenzie> Recenzii { get; set; }
    }
}
