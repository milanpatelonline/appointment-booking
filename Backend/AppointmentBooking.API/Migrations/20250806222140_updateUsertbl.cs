using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class updateUsertbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FirstName",
                table: "AspNetUserRoles",
                type: "NVARCHAR(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastName",
                table: "AspNetUserRoles",
                type: "NVARCHAR(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Email",
                table: "AspNetUserRoles",
                type: "NVARCHAR(150)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AspNetUserRoles");

        }
    }
}
