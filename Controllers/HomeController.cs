using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller

    {
        //public HomeController(Logger  li)
        //{
        //    this.ll = li;

            
        //}
        private readonly AppDBContext std;
        private readonly ILogger<HomeController> _li;

        public HomeController(AppDBContext std , ILogger<HomeController> li)
        {
            this.std = std;
            _li = li;
        }
        public IActionResult Index()
        {
            var stddata = std.Teams.ToList();
            return View(stddata);
        }
        public async Task<IActionResult> Create(Team dt)
        {
            if (ModelState.IsValid)
            {
                await std.Teams.AddAsync(dt);
                await std.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

                
            }

            return View(dt);
        }
        public async Task<IActionResult> Details(int id)
        {
            var stddata = await std.Teams.FirstOrDefaultAsync(x => x.Id == id);
            return View(stddata);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stddata = await std.Teams.FindAsync(id);
            return View(stddata);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team ss)
        {
            if (id != ss.Id)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                std.Teams.Update(ss);
                await std.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(ss);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null || std.Teams == null)
            {
                return NotFound(); 
            }
            var stdd = await std.Teams.FindAsync(id);
            if (stdd == null)
            {
                return NotFound();
                
            }
            return View(stdd);
        }
        [HttpPost ,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var stdd = await std.Teams.FindAsync(id);
            if (stdd != null)
            {
                std.Teams.Remove(stdd);
               
                
            }
            await std.SaveChangesAsync();
            return RedirectToAction("Index","Home");


        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                ViewBag.mysession = HttpContext.Session.GetString("email").ToString();

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Log lg)
        {
            var user = std.Logs.Where(x => x.email == lg.email && x.password ==lg.password).FirstOrDefault();
            //var ss = HttpContext.Session.GetString("email");
            if (user !=null)
            {
                HttpContext.Session.SetString("email" , lg.email);
                return RedirectToAction("Dashboard", "Home");

            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password";
            }
            return View();

        }

    }
}
