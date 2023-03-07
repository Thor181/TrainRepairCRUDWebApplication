using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class RailwayCarriage
    {
        public int Id { get; set; }
        [Required]
        public int? IdRailway { get; set; }
        [Required]
        public int? IdRepair { get; set; }
        [Required]
        public int? IdTypeCarriage { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public int? IdDirectorate { get; set; }
        [Required]
        public string? Picture { get; set; }

        public virtual Directorate? IdDirectorateNavigation { get; set; }
        public virtual Railway? IdRailwayNavigation { get; set; }
        public virtual Repair? IdRepairNavigation { get; set; }
        public virtual TypeCarriage? IdTypeCarriageNavigation { get; set; }
    }
}
