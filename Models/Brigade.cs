using System;
using System.Collections.Generic;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class Brigade
    {
        public int Id { get; set; }
        public int? IdWorker { get; set; }
        public int? IdBrigade { get; set; }

        public virtual BrigadeName? IdBrigadeNavigation { get; set; }
        public virtual Worker? IdWorkerNavigation { get; set; }
    }
}
