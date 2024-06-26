namespace WebStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Cart")]
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        private List<CartItem> items = new List<CartItem>();

        [NotMapped]
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(Product product, int quantity = 1)
        {
            CartItem item = items.FirstOrDefault(i => i.shopping_Product.ProductID == product.ProductID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    shopping_Product = product,
                    shopping_Quantity = quantity
                });
            }
            else
            {
                item.shopping_Quantity += quantity;
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            CartItem item = items.FirstOrDefault(i => i.shopping_Product.ProductID == productId);
            if (item != null)
            {
                item.shopping_Quantity = quantity;
            }
        }

        public void Remove(int productId)
        {
            CartItem item = items.FirstOrDefault(i => i.shopping_Product.ProductID == productId);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        public decimal Total_Money()
        {
            decimal total = items.Sum(i => i.shopping_Product.Price * i.shopping_Quantity);
            return total;
        }

        public int CartID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public decimal? Total { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }

    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public bool Hide { get; set; } // Added Hide property
        public int shopping_ProductID { get; set; }

        public virtual Product shopping_Product { get; set; }

        public int shopping_Quantity { get; set; }
    }
}
