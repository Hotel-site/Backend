using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHotelMartinez.DataAccess.Migrations.Category
{
    /// <inheritdoc />
    public partial class UpdateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHourData_Attractions_AttractionDataId",
                table: "OpeningHourData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpeningHourData",
                table: "OpeningHourData");

            migrationBuilder.DropColumn(
                name: "AttractionDataId",
                table: "OpeningHourData");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "RequireBooking",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OpeningHourData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpeningHourData",
                table: "OpeningHourData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHourData_AttractionId",
                table: "OpeningHourData",
                column: "AttractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHourData_Attractions_AttractionId",
                table: "OpeningHourData",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHourData_Attractions_AttractionId",
                table: "OpeningHourData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpeningHourData",
                table: "OpeningHourData");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHourData_AttractionId",
                table: "OpeningHourData");

            migrationBuilder.DropColumn(
                name: "RequireBooking",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OpeningHourData");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttractionDataId",
                table: "OpeningHourData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpeningHourData",
                table: "OpeningHourData",
                columns: new[] { "AttractionDataId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHourData_Attractions_AttractionDataId",
                table: "OpeningHourData",
                column: "AttractionDataId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
