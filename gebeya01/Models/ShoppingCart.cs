namespace gebeya01.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }
        public int UserID { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
