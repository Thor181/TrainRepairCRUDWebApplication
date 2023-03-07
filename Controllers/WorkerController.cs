using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainRepairCRUDWebApplication.Models;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class WorkerController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public WorkerController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Workers = _dbContext.Workers.ToList();
            return View();
        }

        public async Task<IActionResult> AddWorker([Bind("Surname, Name, Patronymic, BaseWorker, YearWorker, SpecialWorker, BankKart, IsForeman, Salary")] Worker worker)
        {
            _dbContext.Workers.Add(worker);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveWorker(int id)
        {
            Worker? worker = await _dbContext.Workers.FindAsync(id);
            if (worker != null)
            {
                _dbContext.Workers.Remove(worker);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
