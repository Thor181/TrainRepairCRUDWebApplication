using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainRepairCRUDWebApplication.Models;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class BrigadeNameController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public BrigadeNameController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }
        
        public IActionResult Index()
        {
            ViewBag.BrigadeNames = _dbContext.BrigadeNames.ToList();
            return View();
        }
        public IActionResult AddBrigadeName([Bind("NameBrigade")] BrigadeName brigade)
        {
            BrigadeName brigadeName = new BrigadeName()
            {
                Id = _dbContext.BrigadeNames.Select(x => x.Id).OrderBy(x => x).Last() + 1,
                NameBrigade = brigade.NameBrigade
            };
            _dbContext.BrigadeNames.Add(brigadeName);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveBrigadeName(int id)
        {
            BrigadeName brigade = _dbContext.BrigadeNames.FirstOrDefault(x => x.Id == id)!;
            _dbContext.BrigadeNames.Remove(brigade);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
