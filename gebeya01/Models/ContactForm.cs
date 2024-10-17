using System.ComponentModel.DataAnnotations;

namespace gebeya01.Models
{
    // Models/ContactForm.cs
    public class ContactForm
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }

}
