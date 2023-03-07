using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class TypeRepair
    {
        public TypeRepair()
        {
            Repairs = new HashSet<Repair>();
        }

        public int Id { get; set; }
        [Required]
        public string? TypeRepair1 { get; set; }

        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
