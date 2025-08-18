using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class SPGetUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('CREATE OR ALTER PROC SP_GetUserProfile
                @UserId varchar(100)
                As
                BEGIN
	                    select U.Id as UserId,U.FirstName,U.LastName ,U.Email,UR.RoleId,R.Name UserRole from AspNetUsers U
	                    inner join AspNetUserRoles UR on u.Id =UR.UserId
	                    inner join AspNetRoles R on R.Id =UR.RoleId
	                    where U.Id =@UserId
                End')";

            migrationBuilder.Sql(createProcSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('DROP PROCEDURE SP_GetUserProfile')";
            migrationBuilder.Sql(createProcSql);
        }
    }
}
