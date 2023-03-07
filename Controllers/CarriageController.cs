using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainRepairCRUDWebApplication.Models;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarriageController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;

        public CarriageController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TypeCarriage> typeCarriages = await _dbContext.TypeCarriages.ToListAsync();
                ViewBag.TypesCarriage = typeCarriages;
            }
            catch (Exception)
            {
                ViewBag.TypesCarriage = new List<string>();
            }
            return View("TypeCarriage");
        }

        public ActionResult AddTypeCarriage()
        {
            return View("TypeCarriage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTypeCarriage([Bind("TypeCarriage1")] TypeCarriage typeCarriage)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    await _dbContext.AddAsync(new TypeCarriage()
                    {
                        TypeCarriage1 = typeCarriage.TypeCarriage1
                    });
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            return RedirectToAction(nameof(Index));
        } 

        public ActionResult RemoveCarriage(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCarriage(int id)
        {
            try
            {
                TypeCarriage? typeCarriage = await _dbContext.TypeCarriages.FindAsync(id);
                if (typeCarriage != null)
                {
                    _dbContext.Remove(typeCarriage);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
