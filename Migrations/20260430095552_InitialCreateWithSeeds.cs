using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PawSpital.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descriere = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Etaj = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Pret = table.Column<decimal>(type: "TEXT", nullable: false),
                    Descriere = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Specializare = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DepartamentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctori", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctori_Departamente_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departamente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumePacient = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiciuId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programari_Doctori_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programari_Salii_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salii",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Programari_Servicii_ServiciuId",
                        column: x => x.ServiciuId,
                        principalTable: "Servicii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumePacient = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comentariu = table.Column<string>(type: "TEXT", nullable: true),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzii_Doctori_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departamente",
                columns: new[] { "Id", "Descriere", "Nume" },
                values: new object[,]
                {
                    { 1, "Departamentul de boli cardiovasculare", "Cardiologie" },
                    { 2, "Departamentul de afectiuni neurologice", "Neurologie" },
                    { 3, "Departamentul de chirurgie ortopedica", "Ortopedie" },
                    { 4, "Departamentul pentru ingrijirea copiilor", "Pediatrie" },
                    { 5, "Departamentul de afectiuni ale pielii", "Dermatologie" },
                    { 6, "Departamentul de boli ale ochilor", "Oftalmologie" }
                });

            migrationBuilder.InsertData(
                table: "Salii",
                columns: new[] { "Id", "Etaj", "Nume" },
                values: new object[,]
                {
                    { 1, 0, "Sala A1 - Consultatii" },
                    { 2, 0, "Sala A2 - Consultatii" },
                    { 3, 1, "Sala B1 - Investigatii" },
                    { 4, 1, "Sala B2 - Imagistica" },
                    { 5, 2, "Sala C1 - Operatii" },
                    { 6, 2, "Sala C2 - Recuperare" }
                });

            migrationBuilder.InsertData(
                table: "Servicii",
                columns: new[] { "Id", "Descriere", "Nume", "Pret" },
                values: new object[,]
                {
                    { 1, "Consultatie medicala de baza", "Consultatie generala", 150.00m },
                    { 2, "Ecografie Doppler a inimii", "Ecografie cardiaca", 350.00m },
                    { 3, "Electrocardiograma standard", "EKG", 120.00m },
                    { 4, "Rezonanta magnetica nucleara craniu", "RMN cerebral", 800.00m },
                    { 5, "Radiografie standard", "Radiografie", 100.00m },
                    { 6, "Hemoleucograma completa", "Analiza de sange completa", 200.00m },
                    { 7, "Consultatie specializata copii", "Consultatie pediatrica", 180.00m },
                    { 8, "Examen complet al vederii", "Examen oftalmologic", 250.00m }
                });

            migrationBuilder.InsertData(
                table: "Doctori",
                columns: new[] { "Id", "DepartamentId", "Nume", "Specializare" },
                values: new object[,]
                {
                    { 1, 1, "Dr. Andrei Popescu", "Cardiologie" },
                    { 2, 2, "Dr. Maria Ionescu", "Neurologie" },
                    { 3, 3, "Dr. Vlad Georgescu", "Ortopedie" },
                    { 4, 4, "Dr. Elena Dumitrescu", "Pediatrie" },
                    { 5, 5, "Dr. Cristian Radu", "Dermatologie" },
                    { 6, 6, "Dr. Ana Vasilescu", "Oftalmologie" },
                    { 7, 1, "Dr. Mihai Stanescu", "Cardiologie interventionala" },
                    { 8, 2, "Dr. Ioana Popa", "Neuropediatrie" }
                });

            migrationBuilder.InsertData(
                table: "Programari",
                columns: new[] { "Id", "Data", "DoctorId", "NumePacient", "SalaId", "ServiciuId", "Status", "Telefon" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ion Marinescu", 1, 2, "Confirmata", "0721000001" },
                    { 2, new DateTime(2026, 5, 6, 10, 30, 0, 0, DateTimeKind.Unspecified), 2, "Ana Petrescu", 3, 4, "In asteptare", "0731000002" },
                    { 3, new DateTime(2026, 5, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), 4, "George Enescu", 2, 7, "Confirmata", "0741000003" },
                    { 4, new DateTime(2026, 5, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), 6, "Lucia Badea", 4, 8, "In asteptare", "0751000004" }
                });

            migrationBuilder.InsertData(
                table: "Recenzii",
                columns: new[] { "Id", "Comentariu", "DoctorId", "NumePacient", "Rating" },
                values: new object[,]
                {
                    { 1, "Doctor excelent, foarte atent cu pacientii!", 1, "Ion Marinescu", 5 },
                    { 2, "Profesionalism si rapiditate.", 2, "Maria Dumitru", 4 },
                    { 3, "Recomand cu incredere pentru copii.", 4, "George Enescu", 5 },
                    { 4, "Foarte multumita de consultatie.", 5, "Elena Stan", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctori_DepartamentId",
                table: "Doctori",
                column: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Programari_DoctorId",
                table: "Programari",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Programari_SalaId",
                table: "Programari",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Programari_ServiciuId",
                table: "Programari",
                column: "ServiciuId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_DoctorId",
                table: "Recenzii",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programari");

            migrationBuilder.DropTable(
                name: "Recenzii");

            migrationBuilder.DropTable(
                name: "Salii");

            migrationBuilder.DropTable(
                name: "Servicii");

            migrationBuilder.DropTable(
                name: "Doctori");

            migrationBuilder.DropTable(
                name: "Departamente");
        }
    }
}
