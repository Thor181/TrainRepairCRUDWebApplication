using System;
using System.Collections.Generic;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class BrigadeName
    {
        public BrigadeName()
        {
            Repairs = new HashSet<Repair>();
        }

        public int Id { get; set; }
        public string? NameBrigade { get; set; }

        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
