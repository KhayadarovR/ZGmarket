using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Models.Contracts;

namespace ZGmarket.Controllers
{
    public class NomTypeController : Controller
    {
        private readonly INomTypeRepository _typeRepo;
        public NomTypeController(INomTypeRepository typeRepo)
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

        // GET: NomTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NomTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NomTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NomTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NomTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NomTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NomTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
