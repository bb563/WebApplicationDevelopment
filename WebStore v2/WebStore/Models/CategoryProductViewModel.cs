using System.Collections.Generic;

namespace WebStore.Models
{
    public class CategoryProductViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
