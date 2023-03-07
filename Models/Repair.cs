using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class Repair
    {
        public Repair()
        {
            RailwayCarriages = new HashSet<RailwayCarriage>();
        }

        public int Id { get; set; }
        [Required]
        public int? IdTypeRepair { get; set; }
        [Required]
        public decimal? Money { get; set; }
        [Required]
        public bool? Bonus { get; set; }
        [Required]
        public int? BonusPercent { get; set; }
        [Required]
        public DateOnly? DateStart { get; set; }
        [Required]
        public DateOnly? DateStop { get; set; }
        [Required]
        public string? Reason { get; set; }
        [Required]
        public int? IdBrigade { get; set; }

        public virtual BrigadeName? IdBrigadeNavigation { get; set; }
        public virtual TypeRepair? IdTypeRepairNavigation { get; set; }
        public virtual ICollection<RailwayCarriage> RailwayCarriages { get; set; }
    }
}
