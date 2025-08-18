using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class SPGetAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('CREATE OR ALTER PROC SP_GetTodayAppointment
                As
                BEGIN
	                    	select a.*,u.FirstName+ '' ''+ u.LastName as StaffMemberName, c.FirstName+ '' ''+ c.LastName as CustomerName from Appointments a
                            left join AspNetUsers u on a.AssignedToId = u.Id 
                            left join AspNetUsers c on a.CreatedById = c.Id
                            where CAST(a.AppointmentDateTime AS DATE)= CAST(GETDATE() AS DATE)
                End')";

            migrationBuilder.Sql(createProcSql);

            var createProcSql1 = @"EXEC('CREATE OR ALTER PROC SP_GetAppointmentById
                @Id int
                As
                BEGIN
	                    	select a.*,u.FirstName+ '' ''+ u.LastName as StaffMemberName, c.FirstName+ '' ''+ c.LastName as CustomerName from Appointments a
                            left join AspNetUsers u on a.AssignedToId = u.Id 
                            left join AspNetUsers c on a.CreatedById = c.Id
                            where a.id=@Id
                End')";

            migrationBuilder.Sql(createProcSql1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('DROP PROCEDURE SP_GetTodayAppointment')";
            migrationBuilder.Sql(createProcSql);

            var createProcSql1 = @"EXEC('DROP PROCEDURE SP_GetAppointmentById')";
            migrationBuilder.Sql(createProcSql1);

        }
    }
}
