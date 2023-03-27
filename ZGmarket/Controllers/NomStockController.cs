using Microsoft.AspNetCore.Mvc;
using ZGmarket.Models;
using ZGmarket.Models.Repository;
using ZGmarket.Models.ViewModels;

namespace ZGmarket.Controllers
{
    public class NomStockController : Controller
    {
        private readonly NomStockRepository _nomStockRepo;
        private readonly NomRepository _nomRepo;
        private readonly StockRepository _stockRepo;
        public NomStockController(NomStockRepository NomFromStockRepo, StockRepository stockRepo, NomRepository nomRepo)
        {
            _nomStockRepo = NomFromStockRepo;
            _nomRepo = nomRepo;
            _stockRepo = stockRepo;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var nomsFromStock = await _nomStockRepo.GetNomStock();

                var viewModels = new List<NomStockView>();

                foreach (var nomFromStock in nomsFromStock)
                {
                    var nom = await _nomRepo.GetNom(nomFromStock.NomId);
                    var stock = await _stockRepo.GetStock(nomFromStock.StockId);

                    viewModels.Add(new NomStockView { Nom = nom, Stock = stock, Depart = nomFromStock.Depart, Quantity = nomFromStock.Quantity, NomStok = nomFromStock });
                }


                return View(viewModels);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> DepartIndex()
        {
            try
            {
                var nomsFromStock = await _nomStockRepo.GetNomDepart();

                var viewModels = new List<NomStockView>();

                foreach (var nomFromStock in nomsFromStock)
                {
                    var nom = await _nomRepo.GetNom(nomFromStock.NomId);
                    var stock = await _stockRepo.GetStock(nomFromStock.StockId);

                    viewModels.Add(new NomStockView { Nom = nom, Stock = stock, Depart = nomFromStock.Depart, Quantity = nomFromStock.Quantity, NomStok = nomFromStock });
                }


                return View((IEnumerable<NomStockView>)viewModels);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NomStock newNomStok)
        {

            try
            {
                var dbStock = await _nomStockRepo.SendNomToDepart(newNomStok.Id, newNomStok);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                var dbNomStock = await _nomStockRepo.GetNomStock(id);
                return View(dbNomStock);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _nomStockRepo.DeleteNomStock(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
