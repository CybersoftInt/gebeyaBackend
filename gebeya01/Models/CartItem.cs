namespace gebeya01.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int ShoppingCartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Product Product { get; set; }
    }
}
