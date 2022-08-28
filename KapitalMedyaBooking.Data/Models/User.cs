using System.ComponentModel.DataAnnotations.Schema;

namespace KapitalMedyaBooking.Data.Models
{
    [Table("users",Schema = "public")]
    public class User
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("job_title")]
        public string JobTitle { get; set; }
        [Column("job_type")]
        public string JobType { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("image")]
        public string Image { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("onboarding_completion")]
        public int OnboardingCompletion { get; set; }
    }
}
