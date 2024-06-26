using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebStore.Models
{
    [Table("Cart")]
    public partial class Cart
    {
        private readonly ConnectDB db = new ConnectDB();
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        [Key]
        public int CartID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public void RemoveCartItem(int productId)
        {
            try
            {
                var cartItemToRemove = db.CartDetails.FirstOrDefault(ci => ci.ProductID == productId && ci.CartID == this.CartID);

                if (cartItemToRemove != null)
                {
                    db.CartDetails.Remove(cartItemToRemove);
                    db.SaveChanges();

                    // Update total after removing item
                    UpdateTotal();
                }
                else
                {
                    throw new InvalidOperationException($"Cart item with ProductID {productId} not found in Cart.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as per your application's error handling strategy
                throw new Exception("Error removing cart item.", ex);
            }
        }






        public void Add(Product product, int quantity = 1)
        {
            CartDetail detail = CartDetails.FirstOrDefault(i => i.ProductID == product.ProductID);
            if (detail == null)
            {
                CartDetails.Add(new CartDetail
                {
                    ProductID = product.ProductID,
                    Product = product,
                    Quantity = quantity,
                    CartID = this.CartID
                });
            }
            else
            {
                detail.Quantity += quantity;
            }
            UpdateTotal();
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            CartDetail detail = CartDetails.FirstOrDefault(i => i.ProductID == productId);
            if (detail != null)
            {
                detail.Quantity = quantity;
                UpdateTotal();
            }
        }

        public decimal TotalMoney()
        {
            return CartDetails.Sum(i => i.Product.Price * i.Quantity);
        }

        private void UpdateTotal()
        {
            Total = TotalMoney();
        }
    }
}
