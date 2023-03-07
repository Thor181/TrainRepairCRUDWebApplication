using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Salaries = new HashSet<Salary>();
        }

        public int Id { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Patronymic { get; set; }
        [Required]
        public string? BaseWorker { get; set; }
        [Required]
        public int? YearWorker { get; set; }
        [Required]
        public string? SpecialWorker { get; set; }
        [Required]
        public string? BankKart { get; set; }

        public bool? IsForeman { get; set; }
        [Required]
        public decimal? Salary { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
