using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class MedicalRecord
    {
        public int MRNO { get; set; }
        public string Drugs { get; set; }
        public string  Diagnois { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
