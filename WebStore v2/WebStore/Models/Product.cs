namespace WebStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            CartDetails = new HashSet<CartDetail>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        [Key]
        public int ProductID { get; set; }
        public bool Hide { get; set; } // Added Hide property
        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [StringLength(255)]
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }

        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        [Display(Name = "Sale Off")]
        [Column(TypeName = "decimal")]
        public decimal? SaleOff { get; set; }

        public int? Quantity { get; set; }

        [Display(Name = "Is Best Seller")]
        public bool IsBestSeller { get; set; }

        [Display(Name = "Is Hot")]
        public bool IsHot { get; set; } = false; // Giá trị mặc định là false

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }

    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public int CategoryID { get; set; }
        public decimal SaleOff { get; set; }
        public int Quantity { get; set; }
        public bool IsHot { get; set; } = false; // Giá trị mặc định là false
        public bool Hide { get; set; } // Added Hide property
        [Display(Name = "Is Best Seller")]
        public bool IsBestSeller { get; set; }
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }



    }
}