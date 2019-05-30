using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;

namespace Sigacun.Controllers
{
    public class AreaController : Controller
    {

        AreaModel modeloArea = new AreaModel();
        UsuarioModel modelUsuario = new UsuarioModel();
        ConeccionMysql modelMysql = new ConeccionMysql();


        //Mostrar todas las areas.
        public ActionResult inicioArea() {


            var areas = modeloArea.listarAreas().ToList();

            return View("inicioArea",areas);

        }
        //Mostrar determinada area.
        public ActionResult detalle(int id) {

            areas area = modeloArea.detalleArea(id);

            int super = (int) area.are_supervisor;

            usuarios usu = modelUsuario.detalleUsuario(super);

            ViewData["Supervisor"] = usu.usu_nombres;

            return View("detalle", area);
        
        }

        //Registar nueva area.
        public ActionResult nuevaArea() {

            areas area = new areas();

            try {
            
                UpdateModel(area);

                modeloArea.crearArea(area);
                modeloArea.guardar();

                return RedirectToAction("inicioArea");
            }
            catch{

                ViewData["are_id"] = modelMysql.listarAres();
                ViewData["supervisor"] = new SelectList(modelUsuario.listarUsuario().Where(s => s.rol_id != 3).Where(e=>e.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
                return View(area);
            }
        }

        public ActionResult editarArea(int id)
        {
            ViewData["are_id"] = modelMysql.listarAresEditar(id);
            ViewData["supervisor"] = new SelectList(modelUsuario.listarUsuario().Where(s => s.roles.rol_descripcion.CompareTo("Empleado") != 0).Where(e => e.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
            areas area = modeloArea.detalleArea(id);
            return View(area);
        }

        //Editar nueva area.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarArea(int id, FormCollection formValues) {

            areas area = modeloArea.detalleArea(id);

            try {
                UpdateModel(area);
                modeloArea.guardar();

                return RedirectToAction("inicioArea");
            }

            catch {

                ViewData["are_id"] = modelMysql.listarAresEditar(id);
                ViewData["supervisor"] = new SelectList(modelUsuario.listarUsuario().Where(s => s.roles.rol_descripcion.CompareTo("Empleado") != 0).Where(e => e.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
                return View(area);
            }
            
        
        }

        [AcceptVerbs(HttpVerbs.Delete)] 
        public ActionResult eliminar(int id)
        {
            areas area = modeloArea.detalleArea(id);

            modeloArea.eliminarArea(area);
            modeloArea.guardar();

            return View("inicioArea");
        }

    }
}
