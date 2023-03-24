using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using ZGmarket.Data;
using ZGmarket.Models;
using ZGmarket.Models.Repository;

namespace ZGmarket.Controllers
{
    [Authorize(Roles = $"{Positions.root},{Positions.Admin}")]
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

        // GET: Emp/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Emp dbEmp;
            try
            {
                dbEmp = await _empRepo.GetEmp(id);
                return View(dbEmp);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View();
            }
        }

        // POST: Emp/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Emp updateEmp)
        {
            if (!ModelState.IsValid) return View(updateEmp);
            try
            {
                var dbEmp = await _empRepo.EditEmp(id, updateEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Некорректные данные " + e);
                return View();
            }
        }
    }
}
