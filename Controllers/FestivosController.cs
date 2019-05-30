using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class FestivosController : Controller
    {


        FestivosModel modelFestivos = new FestivosModel();


        // GET: /Festivos/

        public ActionResult InicioFestivos()
        {

            var fest = modelFestivos.listarFestivos();

            return View(fest);
        }

        public ActionResult registrarFestivo(){

            return View();
        }


        //Registar nuevo festivo o dia no laboral.
        public ActionResult crearFestivo()
        {
            fechasfestivo fest = new fechasfestivo();

            try
            {
                UpdateModel(fest);

                fest.fec_tipo = 3;
                modelFestivos.crearFestivo(fest);
                modelFestivos.guardar();

                return RedirectToAction("InicioFestivos");
            }
            catch {

                return View();
            }

        }

        public ActionResult editarFes(int id)
        {
            fechasfestivo fes = modelFestivos.detalleFestivo(id);
            return View(fes);
        }

        //Editar actividad.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarFes(int id, FormCollection formValues)
        {

            fechasfestivo fes = modelFestivos.detalleFestivo(id);

            try
            {
                UpdateModel(fes);
                modelFestivos.guardar();

                return RedirectToAction("InicioFestivos");
            }

            catch
            {

                return View(fes);
            }

        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            fechasfestivo fes = modelFestivos.detalleFestivo(id);

            modelFestivos.eliminar(fes);
            modelFestivos.guardar();

            return View("InicioFestivos");
        }

    }
}
