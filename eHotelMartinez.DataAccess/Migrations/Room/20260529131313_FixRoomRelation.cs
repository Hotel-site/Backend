using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHotelMartinez.DataAccess.Migrations.Room
{
    /// <inheritdoc />
    public partial class FixRoomRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "RoomImageData",
                newName: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "RoomImageData",
                newName: "URL");
        }
    }
}
