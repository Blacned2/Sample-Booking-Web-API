using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Dto
{
    public class BookingDto : BaseDto
    {
        public int UserID { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime BookedAt { get; set; }
        public int BookedFor { get; set; }
        public int AppartmentID { get; set; } //todo: fk to appartment
        public int Confirmed { get; set; } = 1; //confirmed => true
    }
}
