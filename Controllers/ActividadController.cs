using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;
using Sigacun.Helpers;
using System.IO;

namespace Sigacun.Controllers
{
    public class ActividadController : Controller
    {
         ActividadModel modeloAct = new ActividadModel();
         ProcedimientoModel modeloPro = new ProcedimientoModel();
         UsuarioModel modeloUsuario = new UsuarioModel();


        //FUNCIONES DE AMDMINISTRADOR
        
         //Mostrar todas las actividades.
        [Authorize(Roles = "Admin")]
        public ActionResult inicioact(int? page)
        {
            const int pageSize = 10;
            var act = modeloAct.listaractividades();
            var paginaact = new PaginatedList<actividades>(act,page ?? 0,pageSize);

            return View(paginaact);
        }

        //Mostrar determinada actividad.
        [Authorize(Roles = "Admin")]
        public ActionResult detalle(int id)
        {

            actividades act = modeloAct.detalleactividad(id);
            return View("detalle", act);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult nuevaact() {

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
            return View();
        
        }
        //Registar nuevo actividad.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult registraract()
        {
            actividades act = new actividades();
            archivosact arc = new archivosact();

            string upload_dir = Server.MapPath("~/app_data/actividades/");

            foreach (string f in Request.Files.Keys)
            {
                if (Request.Files[f].ContentLength > 0)
                {

                    UpdateModel(act);

                    UpdateModel(arc, new[] { "ara_nombre", "ara_fecha" });

                    modeloAct.crearactividad(act);
                    modeloAct.guardar();

                    arc.ara_nombre = "(Num-" + act.act_id + ")" + Request.Files[f].FileName;
                    arc.act_id = act.act_id;
                    modeloAct.crearArchivo(arc);

                    modeloAct.guardar();

                    Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + act.act_id + ")" + Request.Files[f].FileName));
                }

                else
                {

                    UpdateModel(act);
                    modeloAct.crearactividad(act);
                    modeloAct.guardar();

                }
            }
            return RedirectToAction("inicioact");
        }


        // EDITAR ACTIVIDAD
        [Authorize(Roles = "Admin")]
        public ActionResult editaract(int id)
        {
            actividades act = modeloAct.detalleactividad(id);

            bool cantidad = modeloPro.rutaPos(act.pro_id);
            bool cantidadAct = modeloAct.rutaPos(id);

            //CARGAR ARCHIVOS ACTIVIDAD
            if (cantidadAct == true)
            {

                var archi = modeloAct.ruta(id);

                string nombreArchivo = archi.ara_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_arc"] = archi.ara_id;
                ViewData["filesAct"] = files;

            }
            else
            {

                ViewData["filesAct"] = "";

            }

            // CARGAR ARCHIVOS PROCEDMIENTO.
            if (cantidad == true)
            {

                var archi = modeloPro.ruta(act.pro_id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["files"] = files;

            }
            else
            {

                ViewData["files"] = "";

            }


            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a => a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
            return View(act);
        }

        //Editar actividad.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editaract(int id, FormCollection formValues)
        {

            actividades act = modeloAct.detalleactividad(id);

            archivosact arc = new archivosact();

            try
            {

                string upload_dir = Server.MapPath("~/app_data/actividades/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(act);
                        modeloAct.guardar();

                        UpdateModel(arc, new[] { "ara_nombre", "ara_fecha" });

                        arc.ara_nombre = "(Num-" + act.act_id + ")" + Request.Files[f].FileName;
                        arc.act_id = id;

                        modeloAct.crearArchivo(arc);
                        modeloAct.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + act.act_id + ")" + Request.Files[f].FileName));
                        return RedirectToAction("inicioact");
                    }

                    else
                    {
                        UpdateModel(act);
                        modeloAct.guardar();
                        return RedirectToAction("inicioact");
                    }
                }
                UpdateModel(act);
                modeloAct.guardar();
                return RedirectToAction("inicioact");
            }
            catch
            {

                ViewData["hora"] = System.DateTime.Now.ToString();
                 ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                return View(act);

            }
        }



        ///FUNCIONES DE SUPERVISOR

        //Mostrar todas las actividades.
        [Authorize(Roles = "Supervisor")]
        public ActionResult inicioactSuper(int? page)
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id; 
            
            const int pageSize = 10;
            var acts = modeloAct.listaractividades().Where(p=>p.procedimiento.are_id == area).OrderBy(a=>a.act_id);
            var paginaact = new PaginatedList<actividades>(acts, page ?? 0, pageSize);

            return View(paginaact);
        }

        //Mostrar determinada actividad.
        [Authorize(Roles = "Supervisor")]
        public ActionResult detalleSuper(int id)
        {

            actividades act = modeloAct.detalleactividad(id);
            return View("detalleSuper", act);

        }


        [Authorize(Roles = "Supervisor")]
        public ActionResult nuevaactSuper()
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id; 

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a => a.pro_activo == 1).Where(p => p.are_id == area).AsEnumerable(), "pro_id", "pro_codigoun");
            return View();

        }
        //Registar nuevo actividad.
        [Authorize(Roles = "Supervisor")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult registraractSuper()
        {
            actividades act = new actividades();
            archivosact arc = new archivosact();

            string upload_dir = Server.MapPath("~/app_data/actividades/");

            foreach (string f in Request.Files.Keys)
            {
                if (Request.Files[f].ContentLength > 0)
                {

                    UpdateModel(act);

                    UpdateModel(arc, new[] { "ara_nombre", "ara_fecha" });

                    modeloAct.crearactividad(act);
                    modeloAct.guardar();

                    arc.ara_nombre = "(Num-" + act.act_id + ")" + Request.Files[f].FileName;
                    arc.act_id = act.act_id;
                    modeloAct.crearArchivo(arc);

                    modeloAct.guardar();

                    Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + act.act_id + ")" + Request.Files[f].FileName));
                }

                else
                {

                    UpdateModel(act);
                    modeloAct.crearactividad(act);
                    modeloAct.guardar();

                }
            }
            return RedirectToAction("inicioactSuper");
        }



        // EDITAR ACTIVIDAD
        [Authorize(Roles = "Supervisor")]
        public ActionResult editaractSuper(int id)
        {
            actividades act = modeloAct.detalleactividad(id);

            bool cantidad = modeloPro.rutaPos(act.pro_id);
            bool cantidadAct = modeloAct.rutaPos(id);

            //CARGAR ARCHIVOS ACTIVIDAD
            if (cantidadAct == true)
            {

                var archi = modeloAct.ruta(id);

                string nombreArchivo = archi.ara_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_arc"] = archi.ara_id;
                ViewData["filesAct"] = files;

            }
            else
            {

                ViewData["filesAct"] = "";

            }

            // CARGAR ARCHIVOS PROCEDMIENTO.
            if (cantidad == true)
            {

                var archi = modeloPro.ruta(act.pro_id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["files"] = files;

            }
            else
            {

                ViewData["files"] = "";

            }
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a => a.pro_activo == 1).Where(p => p.are_id == area).AsEnumerable(), "pro_id", "pro_codigoun", id);
            return View(act);
        }

        //Editar actividad.
        [Authorize(Roles = "Supervisor")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editaractSuper(int id, FormCollection formValues)
        {

            actividades act = modeloAct.detalleactividad(id);

            archivosact arc = new archivosact();

            try
            {

                string upload_dir = Server.MapPath("~/app_data/actividades/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(act);
                        modeloAct.guardar();

                        UpdateModel(arc, new[] { "ara_nombre", "ara_fecha" });

                        arc.ara_nombre = "(Num-" + act.act_id + ")" + Request.Files[f].FileName;
                        arc.act_id = id;

                        modeloAct.crearArchivo(arc);
                        modeloAct.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + act.act_id + ")" + Request.Files[f].FileName));
                        return RedirectToAction("inicioactSuper");
                    }

                    else
                    {
                        UpdateModel(act);
                        modeloAct.guardar();
                        return RedirectToAction("inicioactSuper");
                    }
                }
                UpdateModel(act);
                modeloAct.guardar();
                return RedirectToAction("inicioactSuper");
            }
            catch
            {
                string ideUser = User.Identity.Name;

                usuarios user = modeloUsuario.ObtenerUser(ideUser);

                int area = user.are_id;

                ViewData["hora"] = System.DateTime.Now.ToString();
                ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a => a.pro_activo == 1).Where(p => p.are_id == area).AsEnumerable(), "pro_id", "pro_codigoun", id);
                return View(act);

            }
        }


        //FUNCIONES DE EMPLEADO

        //Mostrar todas las actividades.
        [Authorize(Roles = "Empleado")]
        public ActionResult inicioactEmple(int? page)
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            const int pageSize = 10;
            var acte = modeloAct.listaractividades().Where(p => p.procedimiento.are_id == area).OrderBy(a => a.act_id);
            var paginaact = new PaginatedList<actividades>(acte, page ?? 0, pageSize);

            return View(paginaact);
        }

        //Mostrar determinada actividad.
        [Authorize(Roles = "Empleado")]
        public ActionResult detalleEmple(int id)
        {

            actividades act = modeloAct.detalleactividad(id);
            return View("detalleEmple", act);

        }

        [Authorize(Roles = "Empleado")]
        public ActionResult nuevaactEmple()
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a => a.pro_activo == 1).Where(p => p.are_id == area).AsEnumerable(), "pro_id", "pro_codigoun");
            return View();

        }
        //Registar nuevo actividad.
        [Authorize(Roles = "Empleado")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult registraractEmple()
        {
            actividades act = new actividades();
            archivosact arc = new archivosact();

            string upload_dir = Server.MapPath("~/app_data/actividades/");

            foreach (string f in Request.Files.Keys)
            {
                if (Request.Files[f].ContentLength > 0)
                {

                    UpdateModel(act);

                    UpdateModel(arc, new[] { "ara_nombre", "ara_fecha" });

                    modeloAct.crearactividad(act);
                    modeloAct.guardar();

                    arc.ara_nombre = "(Num-" + act.act_id + ")" + Request.Files[f].FileName;
                    arc.act_id = act.act_id;
                    modeloAct.crearArchivo(arc);

                    modeloAct.guardar();

                    Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + act.act_id + ")" + Request.Files[f].FileName));
                }

                else
                {

                    UpdateModel(act);
                    modeloAct.crearactividad(act);
                    modeloAct.guardar();

                }
            }
            return RedirectToAction("inicioactEmple");
        }


        // EDITAR ACTIVIDAD
        [Authorize(Roles = "Empleado")]
        public ActionResult editaractEmple(int id)
        {
            actividades act = modeloAct.detalleactividad(id);

            bool cantidad = modeloPro.rutaPos(act.pro_id);
            bool cantidadAct = modeloAct.rutaPos(id);

            //CARGAR ARCHIVOS ACTIVIDAD
            if (cantidadAct == true)
            {

                var archi = modeloAct.ruta(id);

                string nombreArchivo = archi.ara_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_arc"] = archi.ara_id;
                ViewData["filesAct"] = files;

            }
            else
            {

                ViewData["filesAct"] = "";

            }

            // CARGAR ARCHIVOS PROCEDMIENTO.
            if (cantidad == true)
            {

                var archi = modeloPro.ruta(act.pro_id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["files"] = files;

            }
            else
            {

                ViewData["files"] = "";

            }
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a => a.pro_activo == 1).Where(p => p.are_id == area).AsEnumerable(), "pro_id", "pro_codigoun", id);
            return View(act);
        }

        //Editar actividad.
        [Authorize(Roles = "Empleado")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editaractEmple(int id, FormCollection formValues)
        {

            actividades act = modeloAct.detalleactividad(id);

            archivosact arc = new archivosact();

            try
            {

                string upload_dir = Server.MapPath("~/app_data/actividades/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(act);
                        modeloAct.guardar();

                        UpdateModel(arc, new[] { "ara_nombre", "ara_fecha" });

                        arc.ara_nombre = "(Num-" + act.act_id + ")" + Request.Files[f].FileName;
                        arc.act_id = id;

                        modeloAct.crearArchivo(arc);
                        modeloAct.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + act.act_id + ")" + Request.Files[f].FileName));
                        return RedirectToAction("inicioactEmple");
                    }

                    else
                    {
                        UpdateModel(act);
                        modeloAct.guardar();
                        return RedirectToAction("inicioactEmple");
                    }
                }
                UpdateModel(act);
                modeloAct.guardar();
                return RedirectToAction("inicioactEmple");
            }
            catch
            {
                string ideUser = User.Identity.Name;

                usuarios user = modeloUsuario.ObtenerUser(ideUser);

                int area = user.are_id;

                ViewData["hora"] = System.DateTime.Now.ToString();
                ViewData["procedimientos"] = new SelectList(modeloAct.listarprocedmientos().Where(a=>a.pro_activo == 1).Where(p => p.are_id == area).AsEnumerable(), "pro_id", "pro_codigoun", id);
                return View(act);

            }
        }






        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            actividades act = modeloAct.detalleactividad(id);

            modeloAct.eliminaractividad(act);
            modeloAct.guardar();

            return View("inicioact");
        }

        //Método para descargar archivo
        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        public ActionResult Download(string fn)
        {
            string pfn = Server.MapPath("~/App_Data/procedimientos/" + fn);
            if (!System.IO.File.Exists(pfn))
            {
                throw new ArgumentException("El archivo no existe!");
            }

            return new BinaryFileResult.BinaryContentResult()
            {
                FileName = fn,
                ContentType = "application/octet-stream",
                Content = System.IO.File.ReadAllBytes(pfn)
            };
        }
       
        //Método para descargar archivo
        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        public ActionResult DownloadActividad(string fn)
        {
            string pfn = Server.MapPath("~/App_Data/actividades/" + fn);
            if (!System.IO.File.Exists(pfn))
            {
                throw new ArgumentException("El archivo no existe!");
            }

            return new BinaryFileResult.BinaryContentResult()
            {
                FileName = fn,
                ContentType = "application/octet-stream",
                Content = System.IO.File.ReadAllBytes(pfn)
            };
        }

        //Eliminar archivo de actividad
        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminarArchivo(int id)
        {
            archivosact arc = modeloAct.detalleArchivo(id);

            modeloAct.eliminarArchivo(arc);
            modeloAct.guardar();

            return View("editaract");
        }



    }
}
