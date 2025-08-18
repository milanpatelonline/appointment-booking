using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class SPGetStaffUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('CREATE OR ALTER PROC SP_GetStaffUser
                As
                BEGIN
	                    	select U.Id ,U.FirstName,U.LastName ,U.Email from AspNetUsers U
	                        inner join AspNetUserRoles UR on u.Id =UR.UserId
	                        inner join AspNetRoles R on R.Id =UR.RoleId where R.Name =''Staff''
                End')";

            migrationBuilder.Sql(createProcSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('DROP PROCEDURE SP_GetStaffUser')";
            migrationBuilder.Sql(createProcSql);
        }
    }
}
