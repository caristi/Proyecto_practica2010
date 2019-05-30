using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class EstadoController : Controller
    {
        EstadoModel modeloEstado = new EstadoModel();
        ConeccionMysql modeloMysql = new ConeccionMysql();

        //Mostrar todos los estado.
        public ActionResult inicioestado()
        {

            var estados = modeloEstado.listarEstados().ToList();

            return View("inicioestado", estados);

        }
        //Mostrar determinada estado.
        public ActionResult detalle(int id)
        {

            estados estado = modeloEstado.detalleEstado(id);

            return View("detalle", estado);

        }

        //Registar nuevo estado.
        public ActionResult nuevoestado()
        {

            estados estado = new estados();

            try
            {

                UpdateModel(estado);

                modeloEstado.crearEstado(estado);
                modeloEstado.guardar();

                return RedirectToAction("inicioestado");
            }
            catch
            {
                ViewData["est_id"] = modeloMysql.listarEstados();
                return View(estado);
            }
        }

        public ActionResult editarEstado(int id)
        {

            estados estado = modeloEstado.detalleEstado(id);
            ViewData["est_id"] = modeloMysql.listarEstadoEditar(id);
            return View(estado);
        }

        //Editar nueva estado.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarEstado(int id, FormCollection formValues)
        {

            estados estado = modeloEstado.detalleEstado(id);

            try
            {
                UpdateModel(estado);
                modeloEstado.guardar();

                return RedirectToAction("inicioestado");
            }

            catch
            {
                ViewData["est_id"] = modeloMysql.listarEstadoEditar(id);
                return View(estado);
            }


        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            estados estado = modeloEstado.detalleEstado(id);

            modeloEstado.eliminarEstado(estado);
            modeloEstado.guardar();

            return View("inicioestado");
        }

    }
}
