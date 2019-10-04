using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using BOS.StarterCode.Helpers;
using BOS.StarterCode.Models.BOSModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOS.StarterCode.Controllers
{
    [Authorize(Policy = "IsAuthenticated")]
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View(GetPageData());
        }

        private dynamic GetPageData()
        {
            var modules = HttpContext.Session.GetObject<List<Module>>("Modules");
            dynamic model = new ExpandoObject();
            model.Modules = modules;
            if (User.FindFirst(c => c.Type == "Username") != null || User.FindFirst(c => c.Type == "Role") != null)
            {
                model.Username = User.FindFirst(c => c.Type == "Username").Value.ToString();
                model.Roles = User.FindFirst(c => c.Type == "Role").Value.ToString();
            }
            return model;
        }

        public IActionResult NavigateToModule(Guid id, string code, bool isDefault)
        {
            if (isDefault)
            {
                switch (code)
                {
                    case "MYPFL":
                        return RedirectToAction("Index", "Profile");
                    case "USERS":
                        return RedirectToAction("Index", "Users");
                    case "ROLES":
                        return RedirectToAction("Index", "Roles");
                    case "PRMNS":
                        return RedirectToAction("Index", "Permissions");
                    default:

                        return View("Index", GetPageData());
                }
            }
            return null;
        }
    }
}