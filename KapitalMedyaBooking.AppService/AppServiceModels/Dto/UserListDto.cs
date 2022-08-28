using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Dto
{
    
    public class UserListDto
    {
        public int? ID { get; set; } //the prop name is ID because in which our db, column name is "ID". I have never prefer this kind of easy prop names. My opinion, It must be UserID
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string JobType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int OnboardingCompletion { get; set; }
    }
}
