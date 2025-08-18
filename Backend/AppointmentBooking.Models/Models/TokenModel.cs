using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBooking.Models.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public SPGetUserProfile UserProfile { get; set; }
    }
    public class AuthenticationResult : TokenModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
