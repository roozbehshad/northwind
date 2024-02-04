using Northwind.Models.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Domain {
    public partial class Shipper : EntityBase {
        public Shipper() {
            Orders = new HashSet<Order>();
        }

        public static Shipper CreateInstance(State state = State.Added) {
            return CreateInstance<Shipper>(state);
        }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [InverseProperty(nameof(Order.ShipVia))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
