using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingErro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstateImprovements_Improvement_ImprovementId",
                table: "RealEstateImprovements");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstateImprovements_RealEstate_RealEstateId",
                table: "RealEstateImprovements");

            migrationBuilder.DropIndex(
                name: "IX_RealEstateImprovements_ImprovementId",
                table: "RealEstateImprovements");

            migrationBuilder.DropIndex(
                name: "IX_RealEstateImprovements_RealEstateId",
                table: "RealEstateImprovements");

            migrationBuilder.DropColumn(
                name: "ImprovementId",
                table: "RealEstateImprovements");

            migrationBuilder.DropColumn(
                name: "RealEstateId",
                table: "RealEstateImprovements");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateImprovements_IdImprovement",
                table: "RealEstateImprovements",
                column: "IdImprovement");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateImprovements_IdRealEstate",
                table: "RealEstateImprovements",
                column: "IdRealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstateImprovements_Improvement_IdImprovement",
                table: "RealEstateImprovements",
                column: "IdImprovement",
                principalTable: "Improvement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstateImprovements_RealEstate_IdRealEstate",
                table: "RealEstateImprovements",
                column: "IdRealEstate",
                principalTable: "RealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstateImprovements_Improvement_IdImprovement",
                table: "RealEstateImprovements");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstateImprovements_RealEstate_IdRealEstate",
                table: "RealEstateImprovements");

            migrationBuilder.DropIndex(
                name: "IX_RealEstateImprovements_IdImprovement",
                table: "RealEstateImprovements");

            migrationBuilder.DropIndex(
                name: "IX_RealEstateImprovements_IdRealEstate",
                table: "RealEstateImprovements");

            migrationBuilder.AddColumn<int>(
                name: "ImprovementId",
                table: "RealEstateImprovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateId",
                table: "RealEstateImprovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateImprovements_ImprovementId",
                table: "RealEstateImprovements",
                column: "ImprovementId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateImprovements_RealEstateId",
                table: "RealEstateImprovements",
                column: "RealEstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstateImprovements_Improvement_ImprovementId",
                table: "RealEstateImprovements",
                column: "ImprovementId",
                principalTable: "Improvement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstateImprovements_RealEstate_RealEstateId",
                table: "RealEstateImprovements",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
