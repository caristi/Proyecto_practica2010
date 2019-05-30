using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class PreguntasController : Controller
    {
        PreguntasModel modeloPre = new PreguntasModel();
        ConeccionMysql modeloMysql = new ConeccionMysql();


        //Mostrar todas las preguntas.
        public ActionResult inicioPreguntas()
        {
            var preguntas = modeloPre.listarpreguntas().ToList();

            return View("inicioPreguntas", preguntas);

        }

        public ActionResult nuevaPre() 
        {

            evapreguntas pre = new evapreguntas();

            try
            {
                UpdateModel(pre);

                modeloPre.crearPregunta(pre);
                modeloPre.guardar();

                return RedirectToAction("inicioPreguntas");

            }
            catch {

                ViewData["epr_id"] = modeloMysql.listarPreguntas();
                return View(pre);
            }
        
        }

        public ActionResult editarPre(int id) {

            evapreguntas pre = modeloPre.detalleCargo(id);

            ViewData["epr_id"] = modeloMysql.listarPreguntasEditar(id);

            return View(pre);
        
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarPre(int id, FormCollection formValues)
        {

            evapreguntas pre = modeloPre.detalleCargo(id);

            try
            {
                UpdateModel(pre);
                modeloPre.guardar();

                return RedirectToAction("inicioPreguntas");

            }
            catch
            {
                ViewData["epr_id"] = modeloMysql.listarPreguntasEditar(id);
                return View(pre);
            }
        }


        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {

            evapreguntas pre = modeloPre.detalleCargo(id);

            modeloPre.eliminarPregunta(pre);
            modeloPre.guardar();

            return View("inicioPreguntas");
        }


    }
}
