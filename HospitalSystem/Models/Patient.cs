using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        [Required]
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Address { get; set; }
        public int RoomNo { get; set; }
    }
}
