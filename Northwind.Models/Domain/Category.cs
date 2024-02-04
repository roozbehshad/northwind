using Northwind.Models.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Domain {
    public partial class Category : EntityBase {
        public Category() {
            Products = new HashSet<Product>();
        }

        public static Category CreateInstance(State state = State.Added) {
            return CreateInstance<Category>(state);
        }

        [Required]
        [StringLength(15)]
        [Column("CategoryName")]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
