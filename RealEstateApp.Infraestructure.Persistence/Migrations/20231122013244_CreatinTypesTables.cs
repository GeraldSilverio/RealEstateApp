using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatinTypesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RealEstates",
                table: "RealEstates");

            migrationBuilder.RenameTable(
                name: "RealEstates",
                newName: "RealEstate");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "RealEstate",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealEstate",
                table: "RealEstate",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TypeOfRealEstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfRealEstate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfSale", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_Code",
                table: "RealEstate",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeOfRealEstate");

            migrationBuilder.DropTable(
                name: "TypeOfSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RealEstate",
                table: "RealEstate");

            migrationBuilder.DropIndex(
                name: "IX_RealEstate_Code",
                table: "RealEstate");

            migrationBuilder.RenameTable(
                name: "RealEstate",
                newName: "RealEstates");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealEstates",
                table: "RealEstates",
                column: "Id");
        }
    }
}
