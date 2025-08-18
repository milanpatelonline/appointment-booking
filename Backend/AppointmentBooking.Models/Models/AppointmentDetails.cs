
namespace AppointmentBooking.Models.Models
{
    public class AppointmentDetails
    {
        public int Id { get; set; }
        public string CreatedById { get; set; }
        public string AssignedToId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Details { get; set; }
        public string CustomerUserId { get; set; }
        public string StaffMemberName { get; set; }
        public string CustomerName { get; set; }
    }
}
