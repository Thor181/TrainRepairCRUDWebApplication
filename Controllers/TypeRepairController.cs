using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainRepairCRUDWebApplication.Models;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class TypeRepairController : Controller
    {

        private readonly TrainRepairDbContext _dbContext;
        public TypeRepairController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            ViewBag.TypeRepairs = _dbContext.TypeRepairs.ToList();
            ViewBag.t1 = 1;
            ViewBag.t2 = 2;
            
            return View();
            
        }

        public async Task<IActionResult> AddTypeRepair([Bind("TypeRepair1")] TypeRepair typeRepair)
        {
            int id = _dbContext.TypeRepairs.Select(x => x.Id).OrderBy(x => x).Last() + 1;
            typeRepair.Id = id;
            _dbContext.Add(typeRepair);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveTypeRepair(int id)
        {
            if (_dbContext.TypeRepairs.Find(id) is TypeRepair removableItem)
            {
                _dbContext.Remove(removableItem);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
