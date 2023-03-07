using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainRepairCRUDWebApplication.Models;
using TrainRepairCRUDWebApplication.Models.InnerModels;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin, engineer")]
    public class RepairController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public RepairController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Repairs = await _dbContext.Repairs.Where(x => x.Id != 0).Include(x => x.IdTypeRepairNavigation).Include(x => x.IdBrigadeNavigation).Select(x => new RepairInner()
            {
                Id = x.Id,
                IdTypeRepair = x.IdTypeRepair,
                TypeRepair = x.IdTypeRepairNavigation!.TypeRepair1,
                Money = x.Money,
                Bonus = x.Bonus,
                BonusPercent = x.BonusPercent,
                DateStart = x.DateStart,
                DateStop = x.DateStop,
                Reason = x.Reason,
                IdBrigade = x.IdBrigade,
                BrigadeName = x.IdBrigadeNavigation!.NameBrigade
            }).ToListAsync();
            return View();
        }
        public async Task<IActionResult> RemoveRepair(int idRepair)
        {
            RailwayCarriage repairableCarriage = new RailwayCarriage();
            try
            {
                repairableCarriage = await _dbContext.RailwayCarriages.FirstAsync(x => x.IdRepair == idRepair);
            }
            catch (InvalidOperationException)
            {
 
            }
            finally
            {
                repairableCarriage.IdRepair = 0;
            }
            
            Repair removableRepair = await _dbContext.Repairs.FirstAsync(x => x.Id == idRepair);
            _dbContext.Repairs.Remove(removableRepair);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
