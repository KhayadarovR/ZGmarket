using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;
using ZGmarket.Models.ViewModels;

namespace ZGmarket.Controllers
{
    [Authorize(Roles = $"{Positions.root},{Positions.Admin},{Positions.Director},{Positions.Salesman}")]
    public class SupplyController : Controller
    {
        private readonly SupplyRepository _supplyRepo;
        private readonly NomRepository _nomRepo;
        private readonly EmpRepository _empRepo;
        private readonly StockRepository _stockRepo;

        public SupplyController(SupplyRepository sp, NomRepository nr, EmpRepository er, StockRepository str)
        {
            _supplyRepo = sp;
            _nomRepo = nr;
            _empRepo = er;
            _stockRepo = str;

        }
        // GET: Supply/index
        public async Task<IActionResult> Index()
        {
            var viewModels = new List<SupplyView>();
            try
            {

                var supplies = await _supplyRepo.GetSupply();

                foreach (var supply in supplies)
                {
                    var emp = await _empRepo.GetEmp(supply.EmpId);
                    var nom = await _nomRepo.GetNom(supply.NomId);
                    var stock = await _stockRepo.GetStock(supply.StockId);

                    viewModels.Add(new SupplyView { Nom = nom, Emp = emp, Stock = stock, Supply = supply });
                }


                return View(viewModels);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModels);
            }
        }

        public async Task<IActionResult> Create()
        {
            var _noms = await _nomRepo.GetNom();
            var _emps = await _empRepo.GetEmp();
            var _stocks = await _stockRepo.GetStock();


            SupplyCreateView supplyCreate = new()
            {
                Noms = _noms,
                Emps = _emps,
                Stocks = _stocks,
                NewSupply = new Supply()
            };

            return View(supplyCreate);
        }

        // POST: Supply/Create
        [HttpPost]
        public async Task<IActionResult> Create(SupplyCreateView newSupplyView)
        {
            try
            {
                await _supplyRepo.AddSupply(newSupplyView.NewSupply);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View(newSupplyView);
            }
        }

        // GET: Supply/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var _types = await _NomRepo.GetTypes();
        //    var _Supply = await _supplyRepo.GetSupply(id);

        //    List<string> stringTypes = new();

        //    foreach (SupplyType item in _types)
        //    {
        //        stringTypes.Add(item.Title.ToString().ToLower());
        //    }
        //    SupplyCreate SupplyCreate = new SupplyCreate() { Types = stringTypes, Supply = _Supply };
        //    return View(SupplyCreate);
        //}

        // POST: Supply/Edit/5
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, SupplyCreate updateSupply)
        //{

        //    SupplyType curentTypeId = await _NomRepo.GetSupplyType(updateSupply.Supply.NType);
        //    updateSupply.Supply.TypeId = curentTypeId.Id;
        //    try
        //    {
        //        var dbSupply = await _supplyRepo.EditSupply(id, updateSupply.Supply);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("", "Некорректные данные " + e);
        //        return View();
        //    }
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _supplyRepo.(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("", e.Message);
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        public IActionResult SelectListPartial()
        {
            return PartialView();
        }
    }

}
