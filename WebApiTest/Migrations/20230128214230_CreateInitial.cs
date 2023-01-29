using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAPI.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutorsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenrecsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGenre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenrecsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooksTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfPublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    EditorialOffice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksTable_GenrecsTable_GenreId",
                        column: x => x.GenreId,
                        principalTable: "GenrecsTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IBooksTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfPublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    EditorialOffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IBooksTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IBooksTable_AutorsTable_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AutorsTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IBooksTable_GenrecsTable_GenreId",
                        column: x => x.GenreId,
                        principalTable: "GenrecsTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IAutorsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAutorsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAutorsTable_BooksTable_BookId",
                        column: x => x.BookId,
                        principalTable: "BooksTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksTable_GenreId",
                table: "BooksTable",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_IAutorsTable_BookId",
                table: "IAutorsTable",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_IBooksTable_AutorId",
                table: "IBooksTable",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_IBooksTable_GenreId",
                table: "IBooksTable",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IAutorsTable");

            migrationBuilder.DropTable(
                name: "IBooksTable");

            migrationBuilder.DropTable(
                name: "BooksTable");

            migrationBuilder.DropTable(
                name: "AutorsTable");

            migrationBuilder.DropTable(
                name: "GenrecsTable");
        }
    }
}
