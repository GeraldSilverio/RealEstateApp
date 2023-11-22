using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsTypesAndRealEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfRealEstate",
                table: "RealEstate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfSale",
                table: "RealEstate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_IdTypeOfRealEstate",
                table: "RealEstate",
                column: "IdTypeOfRealEstate");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_IdTypeOfSale",
                table: "RealEstate",
                column: "IdTypeOfSale");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_TypeOfRealEstate_IdTypeOfRealEstate",
                table: "RealEstate",
                column: "IdTypeOfRealEstate",
                principalTable: "TypeOfRealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_TypeOfSale_IdTypeOfSale",
                table: "RealEstate",
                column: "IdTypeOfSale",
                principalTable: "TypeOfSale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_TypeOfRealEstate_IdTypeOfRealEstate",
                table: "RealEstate");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_TypeOfSale_IdTypeOfSale",
                table: "RealEstate");

            migrationBuilder.DropIndex(
                name: "IX_RealEstate_IdTypeOfRealEstate",
                table: "RealEstate");

            migrationBuilder.DropIndex(
                name: "IX_RealEstate_IdTypeOfSale",
                table: "RealEstate");

            migrationBuilder.DropColumn(
                name: "IdTypeOfRealEstate",
                table: "RealEstate");

            migrationBuilder.DropColumn(
                name: "IdTypeOfSale",
                table: "RealEstate");
        }
    }
}
