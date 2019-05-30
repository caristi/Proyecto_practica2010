using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class RepeticionController : Controller
    {

        RepeticionesModel modeloRep = new RepeticionesModel();

        public ActionResult listarAsignacionRep()
        {
            var asig = modeloRep.listarasignaciones();

            return View("listarAsignacionRep",asig);
        }

    }
}
