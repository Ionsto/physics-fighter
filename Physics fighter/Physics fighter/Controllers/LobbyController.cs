using Physics_fighter.Models;
using Physics_fighter.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Physics_fighter.Controllers
{
    public class LobbyController : Controller
    {
        //
        // GET: /Lobby/
        public ActionResult Lobby()
        {
            if(Game.InGame)
            {
                return RedirectToAction("Game", "Game", new { name = "Spec"});
            }
            return View(new LobbyModel());
        }
	}
}