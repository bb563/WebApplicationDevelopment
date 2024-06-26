// Category.cs
namespace WebStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int CategoryID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        public bool Hide { get; set; } // Added Hide property
        public virtual ICollection<Product> Products { get; set; }
    }

    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }

}