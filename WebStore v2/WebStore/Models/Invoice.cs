namespace WebStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }
        public bool Hide { get; set; } // Added Hide property
        public virtual Customer Customer { get; set; }
        public int InvoiceID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [StringLength(255)]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public decimal? Total { get; set; }

        public DateTime? Date { get; set; }
        // Additional property for CustomerFullName (not mapped to database)

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
    
}
