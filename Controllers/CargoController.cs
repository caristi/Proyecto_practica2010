using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;
using System.ComponentModel.DataAnnotations;

namespace Sigacun.Controllers
{
    public class CargoController : Controller
    {
        CargoModel modeloCar = new CargoModel();

        //Mostrar todas los cargos.
        public ActionResult iniciocargo()
        {

            var car = modeloCar.listarcargo().ToList();

            return View("iniciocargo", car);

        }
        //Mostrar determinado cargo.
        public ActionResult detalle(int id)
        {

            cargos car = modeloCar.detalleCargo(id);
            return View("detalle", car);

        }

        //Registar nuevo cargo.
        public ActionResult nuevocargo()
        {
            cargos car = new cargos();
            try
            {

                UpdateModel(car);
                modeloCar.crearCargo(car);
                modeloCar.guardar();
    
               return RedirectToAction("iniciocargo");

            }

            catch
            {
                return View(car);

            }
         
        }

        public ActionResult editarcar(int id)
        {
            cargos car = modeloCar.detalleCargo(id);
            return View(car);
        }

        //Editar cargo.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarcar(int id, FormCollection formValues)
        {

            cargos car = modeloCar.detalleCargo(id);

            try
            {
                UpdateModel(car);
                modeloCar.guardar();

                return RedirectToAction("iniciocargo");
            }

            catch
            {

                return View(car);
            }


        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            cargos car = modeloCar.detalleCargo(id);

            modeloCar.eliminarCargo(car);
            modeloCar.guardar();

            return View("iniciocargo");
        }
    }
}
