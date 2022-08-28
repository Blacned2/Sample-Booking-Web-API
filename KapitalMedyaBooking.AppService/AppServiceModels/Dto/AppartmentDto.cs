using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Dto
{
    public class AppartmentDto : BaseDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; } 
        public string ZipCode { get; set; }
        [Required]
        public string Address { get; set; } 
        public string Address2 { get; set; } 
        public double Latitude { get; set; } 
        public double Longitude { get; set; } 
        public string Direction { get; set; } 
    }
}
