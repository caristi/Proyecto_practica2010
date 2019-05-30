using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class PrioridadController : Controller
    {
        PrioridadModel modeloPri = new PrioridadModel();
        ConeccionMysql modeloMysql = new ConeccionMysql();

        //Mostrar todas las prioridades.
        public ActionResult iniciopri()
        {

            var pri = modeloPri.listarprioridad().ToList();

            return View("iniciopri", pri);

        }
        //Mostrar determinada prioridad.
        public ActionResult detalle(int id)
        {

            prioridad pri = modeloPri.detallePrioridad(id);
            return View("detalle", pri);

        }

        //Registar nueva prioridad.
        public ActionResult nuevapri()
        {

            prioridad pri = new prioridad();

            try
            {

                UpdateModel(pri);

                modeloPri.crearPrioridad(pri);
                modeloPri.guardar();

                return RedirectToAction("iniciopri");
            }
            catch
            {
                ViewData["pri_id"] = modeloMysql.listarPrioridad(); 
                return View(pri);
            }
        }

        public ActionResult editarpri(int id)
        {
            prioridad pri = modeloPri.detallePrioridad(id);
            ViewData["pri_id"] = modeloMysql.listarPrioridadEditar(id);

            return View(pri);
        }

        //Editar actividad.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarpri(int id, FormCollection formValues)
        {

            prioridad pri = modeloPri.detallePrioridad(id);

            try
            {
                UpdateModel(pri);
                modeloPri.guardar();

                return RedirectToAction("iniciopri");
            }

            catch
            {
                ViewData["pri_id"] = modeloMysql.listarPrioridadEditar(id);
                return View(pri);
            }


        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            prioridad pri = modeloPri.detallePrioridad(id);

            modeloPri.eliminarPrioridad(pri);
            modeloPri.guardar();

            return View("iniciopri");
        }


    }
}
