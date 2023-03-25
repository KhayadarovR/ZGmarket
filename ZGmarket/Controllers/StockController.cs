using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;

namespace ZGmarket.Controllers
{
    public class StockController : Controller
    {
        private readonly StockRepository _stockRepo;
        public StockController(StockRepository sr)
        {
            _stockRepo = sr;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var types = await _stockRepo.GetStock();
                return View(types);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("err", ex.Message);
                return View(ModelState);
            }
        }

        // GET: StockController/Create

        [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockController/Create
        [HttpPost]
        [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
        public async Task<IActionResult> Create(Stock model)
        {
            try
            {
                Stock dbType = await _stockRepo.AddStock(model);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Stock dbStock;
            try
            {
                dbStock = await _stockRepo.GetStock(id);
                return View(dbStock);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Stock updateStock)
        {
            if (!ModelState.IsValid) return View(updateStock);
            try
            {
                var dbEmp = await _stockRepo.EditStock(id, updateStock);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View();
            }
        }

        // GET: StockController/Delete/5
        [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stockRepo.DeleteStock(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
