using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;
using ZGmarket.Models.ViewModels;

namespace ZGmarket.Controllers
{
    [Authorize(Roles = $"{Positions.root},{Positions.Admin},{Positions.Director},{Positions.Salesman}")]
    public class NomController : Controller
    {
        private readonly NomRepository _nomRepo;
        private readonly NomTypeRepository _typeRepo;
        public NomController(NomRepository nomRepo, NomTypeRepository typeRepo)
        {
            _nomRepo = nomRepo;
            _typeRepo = typeRepo;
        }
        // GET: Nom/index
        public async Task<IActionResult> Index()
        {
            IEnumerable<Nom> Noms = await _nomRepo.GetNom();
            return View(Noms);
        }

        /*        public async Task<IActionResult> Index(IndexViewModel filtr)
                {
                    IEnumerable<Nom> Noms = await _nomRepo.GetNom();
                    return View(Noms);
                }*/

        // GET: Nom/Create
        public async Task<IActionResult> Create()
        {
            var _types = await _typeRepo.GetTypes();
            List<string> stringTypes = new();
            foreach (NomType item in _types)
            {
                stringTypes.Add(item.Title.ToString().ToLower());
            }
            NomCreate nomCreate = new NomCreate() { Types = stringTypes };
            return View(nomCreate);
        }

        // POST: Nom/Create
        [HttpPost]
        public async Task<IActionResult> Create(NomCreate nomCreate)
        {
            NomType curentTypeId = await _typeRepo.GetNomType(nomCreate.Nom.NType);
            nomCreate.Nom.TypeId = curentTypeId.Id;
            try
            {
                await _nomRepo.AddNom(nomCreate.Nom);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View(nomCreate);
            }
        }

        // GET: Nom/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var _types = await _typeRepo.GetTypes();
            var _nom = await _nomRepo.GetNom(id);

            List<string> stringTypes = new();

            foreach (NomType item in _types)
            {
                stringTypes.Add(item.Title.ToString().ToLower());
            }
            NomCreate nomCreate = new NomCreate() { Types = stringTypes, Nom = _nom };
            return View(nomCreate);
        }

        // POST: Nom/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NomCreate updateNom)
        {

            NomType curentTypeId = await _typeRepo.GetNomType(updateNom.Nom.NType);
            updateNom.Nom.TypeId = curentTypeId.Id;
            try
            {
                var dbNom = await _nomRepo.EditNom(id, updateNom.Nom);
                return RedirectToAction(nameof(Index));
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
                await _nomRepo.DeleteNom(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult SelectListPartial()
        {
            return PartialView();
        }
    }

}
