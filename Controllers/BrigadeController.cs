using Microsoft.AspNetCore.Mvc;
using TrainRepairCRUDWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using TrainRepairCRUDWebApplication.Models.InnerModels;
using Microsoft.AspNetCore.Authorization;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class BrigadeController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public BrigadeController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            ViewBag.Brigades = _dbContext.Brigades.Include(x => x.IdBrigadeNavigation)
                                                  .Include(x => x.IdWorkerNavigation)
                                                  .Select(x => new BrigadeInner()
                                                  {
                                                      Id = x.Id,
                                                      IdBrigade = x.IdBrigade,
                                                      BrigadeName = x.IdBrigadeNavigation!.NameBrigade,
                                                      IdWorker = x.IdWorker,
                                                      WorkerName = $"{x.IdWorkerNavigation!.Surname} {x.IdWorkerNavigation!.Name} {x.IdWorkerNavigation!.Patronymic}"
                                                  }).OrderBy(x => x.Id).ToList();
            ViewBag.Workers = _dbContext.Workers.ToList();
            ViewBag.BrigadeNames = _dbContext.BrigadeNames.ToList();
            return View();
        }
        public IActionResult AddBrigade([Bind("Id, IdWorker, IdBrigade")] Brigade brigade)
        {
            _dbContext.Add(brigade);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveBrigade(int id)
        {
            Brigade brigade = _dbContext.Brigades.Where(x => x.Id == id).First();
            _dbContext.Brigades.Remove(brigade);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
