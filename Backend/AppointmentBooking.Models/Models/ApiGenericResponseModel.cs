using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBooking.Models.Models
{
    public class ApiGenericResponseModel<T>
    {
        public ApiGenericResponseModel()
        {
            ErrorMessage = new List<string>();
        }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
