using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBooking.Models.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

       // [Required]
        public string CreatedById { get; set; }

        //[Required]
        public string AssignedToId { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public string CustomerUserId { get; set; }
        
        public string Details { get; set; }

        //[ForeignKey("CreatedById")]
        //public ApplicationUser CreatedBy { get; set; }

        //[ForeignKey("AssignedToId")]
        //public ApplicationUser AssignedTo { get; set; }
    }
}
