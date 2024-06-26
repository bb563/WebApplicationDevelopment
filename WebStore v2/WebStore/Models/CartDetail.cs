using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Models
{
    [Table("CartDetail")]
    public partial class CartDetail
    {
        [Key]
        public int CartDetailID { get; set; }

        public int CartID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("CartID")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
