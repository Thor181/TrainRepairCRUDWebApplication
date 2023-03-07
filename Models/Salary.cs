using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class Salary
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо выбрать дату")]
        public DateOnly? Date { get; set; }
        [Required(ErrorMessage = "Необходимо ввести сумму")]
        public int? Allowance { get; set; }
        [Required]
        public int IdWorker { get; set; }

        public virtual Worker IdWorkerNavigation { get; set; } = null!;
    }
}
