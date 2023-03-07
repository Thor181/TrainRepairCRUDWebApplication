using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainRepairCRUDWebApplication.Models;
using TrainRepairCRUDWebApplication.Models.InnerModels;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin, accountant")]
    public class SalaryController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public SalaryController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }
        
        public IActionResult Index()
        {
            ViewBag.Salaries = _dbContext.Salaries.Include(x => x.IdWorkerNavigation).Select(x => new SalaryInner()
            {
                Id = x.Id,
                Date = x.Date,
                Allowance = x.Allowance,
                IdWorker = x.IdWorker,
                Worker = $"{x.IdWorkerNavigation.Surname} {x.IdWorkerNavigation.Name} {x.IdWorkerNavigation.Patronymic}"
            }).ToList();
            ViewBag.Workers = _dbContext.Workers.ToList();
            return View();
        }
        public IActionResult AddSalary([Bind("Allowance, IdWorker")] Salary salary, DateTime date)
        {
            salary.Date = DateOnly.Parse($"{date:d}");
            _dbContext.Salaries.Add(salary);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveSalary(int id)
        {
            Salary salary = _dbContext.Salaries.FirstOrDefault(x => x.Id == id)!;
            if (salary != null)
            {
                _dbContext.Salaries.Remove(salary);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
