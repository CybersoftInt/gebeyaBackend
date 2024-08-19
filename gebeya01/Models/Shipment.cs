namespace gebeya01.Models
{
    public class Shipment
    {
        public int ShipmentID { get; set; }
        public int OrderID { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

        public virtual Order Order { get; set; }
    }
}
