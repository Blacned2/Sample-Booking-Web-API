using System;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Dto
{
    public class BookingListDto
    {
        public int ID { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime BookedAt { get; set; }
        public int BookedFor { get; set; }
        public DateTime EndDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ApartmentName { get; set; }
        public string AppartmentAddress { get; set; }
        public string AppartmentZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Confirmed { get; set; }
    }
}
