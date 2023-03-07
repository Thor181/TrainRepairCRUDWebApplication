using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainRepairCRUDWebApplication.Models;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class RailwayController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public RailwayController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<Railway> railways = await _dbContext.Railways.ToListAsync();
                ViewBag.Railways = railways;
            }
            catch (Exception)
            {
                ViewBag.Railways = new List<string>();
            }
            return View("Railway");
        }

        // GET: RailwayController/Create
        public ActionResult AddRailway()
        {
            return View();
        }

        // POST: RailwayController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRailway([Bind("Id,RailwayName,External,BankExternal,InnExternal,AddressExternal")] Railway railway)
        {
            _dbContext.Add(new Railway()
            {
                RailwayName = railway.RailwayName,
                AddressExternal = railway.AddressExternal,
                BankExternal = railway.BankExternal,
                External = railway.External,
                InnExternal = railway.InnExternal
            });
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: RailwayController/Delete/5
        public ActionResult RemoveRailway(int? id)
        {
            return View();
        }

        // POST: RailwayController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRailway(int id)
        {
            try
            {
                Railway? railway = await _dbContext.Railways.FindAsync(id);
                if (railway != null)
                {
                    _dbContext.Railways.Remove(railway);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
