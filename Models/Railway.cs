using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class Railway
    {
        public Railway()
        {
            RailwayCarriages = new HashSet<RailwayCarriage>();
        }

        public int Id { get; set; }
        [Required]
        public string? RailwayName { get; set; }
        [Required]
        public string? External { get; set; }
        [Required]
        public string? BankExternal { get; set; }
        [Required]
        public string? InnExternal { get; set; }
        [Required]
        public string? AddressExternal { get; set; }

        public virtual ICollection<RailwayCarriage> RailwayCarriages { get; set; }

    }
    
}

