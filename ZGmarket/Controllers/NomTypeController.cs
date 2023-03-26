using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;

namespace ZGmarket.Controllers
{
    public class NomTypeController : Controller
    {
        private readonly NomTypeRepository _typeRepo;
        public NomTypeController(NomTypeRepository typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public IActionResult Index()
        {
            return RedirectToAction("GetTypes");
        }

        // GET: NomTypeController
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                var types = await _typeRepo.GetTypes();
                return View(types);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("err", ex.Message);
                return View(ModelState);
            }
        }

        // GET: NomTypeController/Create

        [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NomTypeController/Create
        [HttpPost]
        [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
        public async Task<IActionResult> Create(NomType model)
        {
            try
            {
                NomType dbType = await _typeRepo.AddNomType(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }


        // GET: NomTypeController/Delete/5
        [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _typeRepo.DeleteNomType(id);
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
