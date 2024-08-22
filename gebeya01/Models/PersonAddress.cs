namespace gebeya01.Models
{
    public class PersonAddress
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public Person Person { get; set; }
        public Address Address { get; set; }
    }
}
