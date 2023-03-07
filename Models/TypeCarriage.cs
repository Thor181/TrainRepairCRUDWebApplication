using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class TypeCarriage
    {
        public TypeCarriage()
        {
            RailwayCarriages = new HashSet<RailwayCarriage>();
        }

        public int Id { get; set; }
        [Required]
        public string? TypeCarriage1 { get; set; }

        public virtual ICollection<RailwayCarriage> RailwayCarriages { get; set; }
    }
}
