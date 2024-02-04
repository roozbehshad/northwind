using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Domain.Base {
    public abstract class EntityBase {
        protected EntityBase() {
            State = State.Unchanged;
        }

        public static T CreateInstance<T>(State state) where T : EntityBase, new() {
            T instance = new T {
                State = state
            };

            return instance;
        }

        public int Id { get; set; }

        [NotMapped]
        public State State { get; set; }

        public IEnumerable<ValidationResult> Validate() {
            throw new NotImplementedException();
        }

        //public int CreatedById { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime DateCreated { get; set; }

        //public int ModifiedById { get; set; }

        //public string ModifiedBy { get; set; }

        //public DateTime DateModified { get; set; }

        //[Timestamp]
        //public byte[] TimeStamp { get; set; }
    }
}
