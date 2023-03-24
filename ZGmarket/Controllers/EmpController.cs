using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;

namespace ZGmarket.Controllers
{
    [Authorize(Roles = Positions.root)]
    public class EmpController : Controller
    {
        private readonly EmpRepository _empRepo;
        public EmpController(EmpRepository empRepo)
        {
            _empRepo = empRepo;
        }
        // GET: Emp/index
        public async Task<IActionResult> Index()
        {
            IEnumerable<Emp> emps = await _empRepo.GetEmp();
            return View(emps);
        }

        // GET: Emp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emp/Create
        [HttpPost]
        public async Task<IActionResult> Create(Emp newEmp)
        {
            try
            {
                await _empRepo.AddEmp(newEmp);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View(newEmp);
            }
        }

        // GET: EmpController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmpController/Edit/5
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

        // GET: EmpController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmpController/Delete/5
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
