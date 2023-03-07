using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models.InnerModels
{
    public class SalaryInner
    {
        
        public int Id { get; set; }
        [Required]
        public DateOnly? Date { get; set; }
        [Required]
        public int? Allowance { get; set; }
        [Required]
        public string? Worker { get; set; }
        public int IdWorker { get; set; }
    }
}
