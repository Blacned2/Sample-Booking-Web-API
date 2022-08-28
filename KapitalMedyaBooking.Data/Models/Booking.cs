using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KapitalMedyaBooking.Data.Models
{
    [Table("bookings", Schema = "public")]
    public class Booking
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("user_id")]
        public int UserID { get; set; }
        [Column("starts_at",TypeName = "text")]
        public string StartsAt { get; set; }
        [Column("booked_at", TypeName = "text")]
        public string BookedAt { get; set; }
        [Column("booked_for")]
        public int BookedFor { get; set; }
        [Column("apartment_id")]
        public int ApartmentID { get; set; }
        [Column("confirmed")]
        public int Confirmed { get; set; }
    }
}
