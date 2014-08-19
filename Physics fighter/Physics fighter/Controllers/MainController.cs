using Physics_fighter.Models;
using Physics_fighter.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Physics_fighter.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        public ActionResult Main()
        {
            return View(new MainModel());
        }
	}
}