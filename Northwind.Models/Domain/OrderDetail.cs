using Northwind.Models.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Domain {
    [Table("Order Details")]
    public partial class OrderDetail : EntityBase {
        public static OrderDetail CreateInstance(State state = State.Added) {
            return CreateInstance<OrderDetail>(state);
        }

        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; }
    }
}
