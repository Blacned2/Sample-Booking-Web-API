using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.Data.Models
{
    [Table("appartments", Schema = "public")]
    public class Appartment
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("image")]
        public string Image { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("zip_code")]
        public string ZipCode { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("address2")]
        public string Address2 { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
        [Column("direction")]
        public string Direction { get; set; }
        [Column("booked")]
        public int Booked { get; set; }
    }
}
