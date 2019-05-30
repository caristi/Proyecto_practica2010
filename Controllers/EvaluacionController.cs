using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class EvaluacionController : Controller
    {

        EvaluacionModel modeloEva = new EvaluacionModel();
        ConeccionMysql modeloMysql = new ConeccionMysql();
        
        public ActionResult detalle(int id)
        {
         /*   bool va = modeloEva.evaluacionCali(id);

            if (va == true) {

                var evas = modeloEva.detalle(id);

                int num = evas.ahd_hd_numsolicitud;
                modeloMysql.registrarEvaluacion(num, id);
            
            }
            */
            
            List<evaluacion> eva = modeloEva.detalleEvaluacion(id);

                return View("detalle", eva);

        }

    }
}
