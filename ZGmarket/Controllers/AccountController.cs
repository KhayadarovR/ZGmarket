using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZGmarket.Models.Repository;
using ZGmarket.Models;
using System.Security.Claims;
using ZGmarket.Data;
using System.Security.Cryptography;

namespace ZGmarket.Controllers;

public class AccountController : Controller
{
    private readonly EmpRepository _empRepo;
    public AccountController(EmpRepository empRepo)
    {
        _empRepo = empRepo;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        int _id = int.Parse(s: HttpContext.User.FindFirst("id").Value);
        Emp model = await _empRepo.GetEmp(_id);
        return View(model);
    }


    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(Emp model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _empRepo.AddEmp(model.Phone, model.Password);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("Ошибка регистрации ", e.Message);
            return View(model);
        }

        Emp newEmp = await _empRepo.GetEmp(model.Phone);
        var claimPrincipal = GetClaimsPrincipalDefault(newEmp);
        await HttpContext.SignInAsync(claimPrincipal);

        return Redirect("/Account");
    }
    public IActionResult Login(string returnUrl) => View();

    [HttpPost]
    public async Task<IActionResult> Login(Emp model)
    {
        if (!ModelState.IsValid) return View(model);
        Emp empDB;
        try
        {
            empDB = await _empRepo.GetEmp(model.Phone);

            if (model.Password == empDB.Password)
            {

                var principal = GetClaimsPrincipalDefault(empDB);
                await HttpContext.SignInAsync(principal);

                return Redirect("/Account");
            }

            throw new Exception("Неверный пароль");
        }
        catch (Exception e)
        {
            ModelState.AddModelError("Ошибка при входе", e.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private ClaimsPrincipal GetClaimsPrincipalDefault(Emp newEmp)
    {
        var claims = new List<Claim>
            {
                new Claim("id", newEmp.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.MobilePhone, newEmp.Phone),
                new Claim(ClaimTypes.Role, newEmp.Position)
            };

        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimIdentity);

        return claimPrincipal;
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit()
    {
        var id = int.Parse(s: HttpContext.User.FindFirst("id").Value);
        Emp model = await _empRepo.GetEmp(id);
        return View(model);
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(Emp empUpdate)
    {
        var id = int.Parse(s: HttpContext.User.FindFirst("id").Value);

        if (!ModelState.IsValid) return View(empUpdate);
        try
        {
            Emp empUpdateDb = await _empRepo.EditAcc(id, empUpdate.BirthDate, empUpdate.Name, empUpdate.LastName, empUpdate.Phone, empUpdate.Password);
            return Redirect("/Account");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(empUpdate);
        }
    }
}
