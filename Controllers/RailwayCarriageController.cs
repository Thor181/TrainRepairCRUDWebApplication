using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using TrainRepairCRUDWebApplication.Models;
using TrainRepairCRUDWebApplication.Models.InnerModels;

namespace TrainRepairCRUDWebApplication.Controllers
{
    [Authorize(Roles = "admin, engineer")]
    public class RailwayCarriageController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        private readonly IWebHostEnvironment _appEnv;
        public RailwayCarriageController(TrainRepairDbContext context, IWebHostEnvironment env)
        {
            _dbContext = context;
            _appEnv = env;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<RailwayCarriageInner> railwayCarriage = await _dbContext.RailwayCarriages.Include(x => x.IdRailwayNavigation)
                                                                              .Include(x => x.IdRepairNavigation).ThenInclude(x => x.IdTypeRepairNavigation)
                                                                              .Include(x => x.IdTypeCarriageNavigation)
                                                                              .Include(x => x.IdDirectorateNavigation)
                                                                              .Select(x => new RailwayCarriageInner()
                                                                              {
                                                                                  Id = x.Id,
                                                                                  Railway = x.IdRailwayNavigation!.RailwayName,
                                                                                  IdRailway = x.IdRailway,
                                                                                  Repair = x.IdRepairNavigation!.IdTypeRepairNavigation!.TypeRepair1,
                                                                                  IdRepair = x.IdRepair,
                                                                                  Directorate = x.IdDirectorateNavigation!.Directorate1,
                                                                                  IdDirectorate = x.IdDirectorate,
                                                                                  TypeCarriage = x.IdTypeCarriageNavigation!.TypeCarriage1,
                                                                                  IdTypeCarriage = x.IdTypeCarriage,
                                                                                  Year = (int)x.Year!,
                                                                                  Picture = x.Picture
                                                                              }).OrderBy(x => x.Id).ToListAsync();
            ViewBag.RailwayCarriage = railwayCarriage;
            ViewBag.Railways = await _dbContext.Railways.ToListAsync();
            ViewBag.TypeCarriage = await _dbContext.TypeCarriages.ToListAsync();
            ViewBag.Directorate = await _dbContext.Directorates.ToListAsync();
            return View("RailwayCarriage");
        }
        [HttpGet]
        public async Task<IActionResult> RemoveRailwayCarriage(int id)
        {
            try
            {
                var railwayCarriage = await _dbContext.RailwayCarriages.FirstAsync(x => x.Id == id);
                _dbContext.RailwayCarriages.Remove(railwayCarriage);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddRailwayCarriage(IFormFile file, int IdRailway, int IdTypeCarriage, int Year, int IdDirectorate)
        {
            string pathImage = "";
            if (file != null)
            {
                pathImage = LoadImageToServer(file).Result;
            }

            _dbContext.Add(new RailwayCarriage()
            {
                IdRailway = IdRailway,
                IdRepair = 0,
                IdTypeCarriage = IdTypeCarriage,
                Year = Year,
                IdDirectorate = IdDirectorate,
                Picture = pathImage
            });
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> LoadImageToServer(IFormFile file)
        {
            string path = "/images/temp/" + file.FileName;
            using (var fs = new FileStream(_appEnv.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return path;
        }
        [HttpGet]
        public async Task<IActionResult> SendToRepair(int id)
        {
            ViewBag.TypeRepair = await _dbContext.TypeRepairs.ToListAsync();
            ViewBag.IdCarriage = id.ToString();
            ViewBag.Brigades = await _dbContext.BrigadeNames.ToListAsync();
            return View();
        }
        public async Task<IActionResult> AddToRepair(int idCarriage, int typeRepair, DateTime dateStart, DateTime dateEnd, decimal price, int bonus, bool isBonus, string reason, int idBrigade)
        {
            int newItemId = await _dbContext.Repairs.OrderBy(x => x.Id).Select(x => x.Id).LastAsync() + 1;
            Repair newRepair = new Repair()
            {
                Id = newItemId,
                IdTypeRepair = typeRepair,
                DateStart = DateOnly.Parse($"{dateStart:d}"),
                DateStop = DateOnly.Parse($"{dateEnd:d}"),
                Money = price,
                Bonus = isBonus,
                BonusPercent = bonus,
                Reason = reason,
                IdBrigade = idBrigade
            };
            _dbContext.Add(newRepair);
            await _dbContext.SaveChangesAsync();
            
            RailwayCarriage railwayCarriageUpdate = await _dbContext.RailwayCarriages.FirstAsync(x => x.Id == idCarriage);
            railwayCarriageUpdate.IdRepair = newItemId;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
