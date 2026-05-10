using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHotelMartinez.DataAccess.Migrations.Category
{
    /// <inheritdoc />
    public partial class UpdateAttractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Attractions");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Attractions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Attractions",
                type: "decimal(10,7)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Attractions",
                type: "decimal(10,7)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Attractions");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Attractions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
