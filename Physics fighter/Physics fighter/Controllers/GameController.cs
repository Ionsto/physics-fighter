using Physics_fighter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Physics_fighter.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/
        public ActionResult Game(string Name)
        {
            return View(new GameModel(Name));
        }
	}
}