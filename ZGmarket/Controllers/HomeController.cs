using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZGmarket.Models;
using ZGmarket.Models.Repository;
using ZGmarket.Models.ViewModels;

namespace ZGmarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly NomStockRepository _nomStockRepo;
        private readonly NomRepository _nomRepo;
        private readonly StockRepository _stockRepo;
        public HomeController(NomStockRepository NomFromStockRepo, StockRepository stockRepo, NomRepository nomRepo)
        {
            _nomStockRepo = NomFromStockRepo;
            _nomRepo = nomRepo;
            _stockRepo = stockRepo;
        }

        public async Task<IActionResult> Index()
        {
            var ItemViewList = new List<HomeView>();

            var nomstoks = await _nomStockRepo.GetNomDepart();

            foreach (var nomdepart in nomstoks)
            {
                var tmp = new HomeView()
                {
                    Nom = await _nomRepo.GetNom(nomdepart.NomId),
                    NomStock = await _nomStockRepo.GetNomStock(nomdepart.Id)
                };

                ItemViewList.Add(tmp);
            }


            return View(ItemViewList);
        }

        public async Task<IActionResult> Depart(string depart)
        {
            var ItemViewList = new List<HomeView>();

            var nomstoks = await _nomStockRepo.GetNomDepart(depart);

            foreach (var nomdepart in nomstoks)
            {
                var tmp = new HomeView()
                {
                    Nom = await _nomRepo.GetNom(nomdepart.NomId),
                    NomStock = await _nomStockRepo.GetNomStock(nomdepart.Id)
                };

                ItemViewList.Add(tmp);
            }


            return View("Index", ItemViewList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}