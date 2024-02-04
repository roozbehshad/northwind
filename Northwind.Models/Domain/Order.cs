using Northwind.Models.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Domain {
    public partial class Order : EntityBase {
        public Order() {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public static Order CreateInstance(State state = State.Added) {
            return CreateInstance<Order>(state);
        }

        [Column("CustomerID")]
        [StringLength(5)]
        public string CustomerId { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }

        [Column("ShipVia")]
        public int? ShipperId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        [StringLength(40)]
        public string ShipName { get; set; }

        [StringLength(60)]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string ShipRegion { get; set; }

        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [StringLength(15)]
        public string ShipCountry { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Orders")]
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(ShipperId))]
        [InverseProperty(nameof(Shipper.Orders))]
        public virtual Shipper ShipVia { get; set; }

        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
