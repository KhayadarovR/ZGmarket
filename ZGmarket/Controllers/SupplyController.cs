using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;
using ZGmarket.Models.ViewModels;

//namespace ZGmarket.Controllers
//{
//    [Authorize(Roles = $"{Positions.root},{Positions.Admin},{Positions.Director},{Positions.Salesman}")]
//    public class SupplyController : Controller
//    {
//        private readonly SupplyRepository _SupplyRepo;
//        private readonly NomRepository _NomRepo;
//        public SupplyController(SupplyRepository SupplyRepo, NomRepository nomRepo)
//        {
//            _SupplyRepo = SupplyRepo;
//            _NomRepo = nomRepo;
//        }
//        // GET: Supply/index
//        public async Task<IActionResult> Index()
//        {
//            IEnumerable<Supply> Supplys = await _SupplyRepo.GetSupply();
//            return View(Supplys);
//        }

//        public async Task<IActionResult> Create()
//        {
//            var _types = await _typeRepo.GetTypes();
//            List<string> stringTypes = new();
//            foreach (SupplyType item in _types)
//            {
//                stringTypes.Add(item.Title.ToString().ToLower());
//            }
//            SupplyCreate SupplyCreate = new SupplyCreate() { Types = stringTypes };
//            return View(SupplyCreate);
//        }

//        // POST: Supply/Create
//        [HttpPost]
//        public async Task<IActionResult> Create(SupplyCreate SupplyCreate)
//        {
//            SupplyType curentTypeId = await _typeRepo.GetSupplyType(SupplyCreate.Supply.NType);
//            SupplyCreate.Supply.TypeId = curentTypeId.Id;
//            try
//            {
//                await _SupplyRepo.AddSupply(SupplyCreate.Supply);
//                return RedirectToAction(nameof(Index));
//            }
//            catch(Exception e)
//            {
//                ModelState.AddModelError("", "Некорректные данные " + e);
//                return View(SupplyCreate);
//            }
//        }

//        // GET: Supply/Edit/5
//        public async Task<IActionResult> Edit(int id)
//        {
//            var _types = await _typeRepo.GetTypes();
//            var _Supply = await _SupplyRepo.GetSupply(id);

//            List<string> stringTypes = new();

//            foreach (SupplyType item in _types)
//            {
//                stringTypes.Add(item.Title.ToString().ToLower());
//            }
//            SupplyCreate SupplyCreate = new SupplyCreate() { Types = stringTypes, Supply = _Supply };
//            return View(SupplyCreate);
//        }

//        // POST: Supply/Edit/5
//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, SupplyCreate updateSupply)
//        {

//            SupplyType curentTypeId = await _typeRepo.GetSupplyType(updateSupply.Supply.NType);
//            updateSupply.Supply.TypeId = curentTypeId.Id;
//            try
//            {
//                var dbSupply = await _SupplyRepo.EditSupply(id, updateSupply.Supply);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception e)
//            {
//                ModelState.AddModelError("", "Некорректные данные " + e);
//                return View();
//            }
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                await _SupplyRepo.DeleteSupply(id);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception e)
//            {
//                ModelState.AddModelError("", e.Message);
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        public IActionResult SelectListPartial()
//        {
//            return PartialView();
//        }
//    }

//}
