using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainRepairCRUDWebApplication.Models;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class DirectorateController : Controller
    {
        private TrainRepairDbContext _dbContext;

        public DirectorateController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Directorates = await _dbContext.Directorates.ToListAsync();
            }
            catch (Exception)
            {
                return View();
            }
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDirectorate([Bind("Directorate1")] Directorate directorate)
        {
            if (ModelState.IsValid == true)
            {
                _dbContext.Add(new Directorate()
                {
                    Directorate1 = directorate.Directorate1
                });
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult RemoveDirectorate()
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveDirectorate(int id)
        {
            Directorate? directorateEntity = await _dbContext.Directorates.FindAsync(id);
            if (directorateEntity != null)
            {
                _dbContext.Remove(directorateEntity);
                await _dbContext.SaveChangesAsync();               
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
