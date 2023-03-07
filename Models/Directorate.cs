using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class Directorate
    {
        public Directorate()
        {
            RailwayCarriages = new HashSet<RailwayCarriage>();
        }

        public int Id { get; set; }
        [Required]
        public string Directorate1 { get; set; } = null!;

        public virtual ICollection<RailwayCarriage> RailwayCarriages { get; set; }
    }
}
