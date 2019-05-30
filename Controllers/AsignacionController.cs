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
    public class AsignacionController : Controller
    {
        
        AsignacionModel modeloAsig = new AsignacionModel();
        ActividadModel modeloActi = new ActividadModel();
        PrioridadModel modeloPri = new PrioridadModel();
        ActividadModel modeloAct = new ActividadModel();
        ProcedimientoModel modeloPro = new ProcedimientoModel();
        EstadoModel modeloEst = new EstadoModel();
        Repeticion repeticion = new Repeticion();
        UsuarioModel modeloUsuario = new UsuarioModel();


        ////// FUNCIONES DE ADMINISTRADOR

        //Mostrar todas las asignaciones de las actividades.
        [Authorize(Roles = "Admin")]
        public ActionResult inicioasig(int? page)
        {
            const int pageSize = 10;

            var asig = modeloAsig.listarTodasAsignaciones().OrderBy(f=>f.asi_fechaterminar);
            var paginaasig = new PaginatedList<asignaciones>(asig,
                                                             page ?? 0,
                                                             pageSize);
            return View(paginaasig);

        }
        //Mostrar determinada asignación.
        [Authorize(Roles = "Admin")]
        public ActionResult detalle(int id)
        {
            asignaciones asig = modeloAsig.detalleasignacion(id);
            return View("detalle", asig);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult nuevaasig()
        {
            ViewData["usuario"] = User.Identity.Name;
            ViewData["hora"] = DateTime.Now;
            ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(e=>e.usu_activo == 1).AsEnumerable(),"usu_id","usu_nombres");
            ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
            ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).AsEnumerable(),"act_id","act_nombre");

            return View();

        }

        //Registar nueva asignación.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult registrarasig()
        {

            repeticiones rep = new repeticiones();
            asignaciones asig = new asignaciones();
            archivosasig arc = new archivosasig();

            string upload_dir = Server.MapPath("~/app_data/asignaciones/");

                UpdateModel(rep);

                if (rep.rep_fechainicio != null && rep.rep_cantidad != null)
                {
                    int incremento = (int)rep.rep_cantidad;

                    UpdateModel(asig);

                    DateTime finicio = (DateTime)rep.rep_fechainicio;
                    DateTime ffin = asig.asi_fechaterminar;

                    while (finicio.Date <= ffin.Date)
                    {

                        asig = new asignaciones();
                        arc = new archivosasig();

                        if (finicio.DayOfWeek == DayOfWeek.Saturday || finicio.DayOfWeek == DayOfWeek.Sunday)
                        {
                            finicio = finicio.AddDays(incremento);
                        }
                        else
                        {
                            foreach (string f in Request.Files.Keys)
                            {
                                if (Request.Files[f].ContentLength > 0)
                                {

                                    UpdateModel(asig);

                                    asig.asi_fechaterminar = finicio;

                                    UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                                    modeloAsig.crearasignacion(asig);
                                    modeloAsig.guardar();
                                    finicio = finicio.AddDays(incremento);


                                    arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                                    arc.asi_id = asig.asi_id;
                                    modeloAsig.crearArchivo(arc);

                                    modeloAsig.guardar();

                                    Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                                }

                                else
                                {

                                    UpdateModel(asig);

                                    asig.asi_fechaterminar = finicio;

                                    modeloAsig.crearasignacion(asig);
                                    modeloAsig.guardar();
                                    finicio = finicio.AddDays(incremento);

                                }

                            }
                        }
                    }
                    return RedirectToAction("inicioasig");
                }
                else
                {
                    foreach (string f in Request.Files.Keys)
                    {
                        if (Request.Files[f].ContentLength > 0)
                        {

                            UpdateModel(asig);

                            UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                            modeloAsig.crearasignacion(asig);
                            modeloAsig.guardar();

                            arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                            arc.asi_id = asig.asi_id;
                            modeloAsig.crearArchivo(arc);

                            modeloAsig.guardar();

                            Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                        }

                        else
                        {

                            asig = new asignaciones();
                            UpdateModel(asig);
                            modeloAsig.crearasignacion(asig);
                            modeloAsig.guardar();

                        }

                    }

                    return RedirectToAction("inicioasig");
                }
            
        }


        [Authorize(Roles = "Admin")]
        public ActionResult editarasig(int id)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);

            actividades act = modeloAct.detalleactividad(asig.act_id);

            bool cantidadPro = modeloPro.rutaPos(act.pro_id);
            bool cantidadAct = modeloAct.rutaPos(asig.act_id);
            bool cantidadAsi = modeloAsig.rutaPos(id);

            //CARGAR ARCHIVOS ASIGNACIONES
            if (cantidadAsi == true)
            {

                var archi = modeloAsig.ruta(asig.asi_id);

                string nombreArchivo = archi.ars_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/asignaciones/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_ars"] = archi.ars_id;
                ViewData["filesAsig"] = files;

            }
            else
            {

                ViewData["filesAsig"] = "";

            }


            //CARGAR ARCHIVOS ACTIVIDAD
            if (cantidadAct == true)
            {

                var archi = modeloAct.ruta(asig.act_id);

                string nombreArchivo = archi.ara_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["filesAct"] = files;

            }
            else
            {

                ViewData["filesAct"] = "";

            }

            // CARGAR ARCHIVOS PROCEDMIENTO.
            if (cantidadPro == true)
            {

                var archi = modeloPro.ruta(act.pro_id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["filesPro"] = files;

            }
            else
            {

                ViewData["filesPro"] = "";

            }


            ViewData["hora"] = DateTime.Now;
            ViewData["operacion"] = asig.actividades.act_operacion;
            ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a=>a.est_hd_estado == 1).AsEnumerable(),"est_id","est_descripcion");
            ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(u=>u.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
            ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
            ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a => a.act_activo == 1).AsEnumerable(), "act_id", "act_nombre");

            return View(asig);
        }

        //Editar asignacion.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarasig(int id, FormCollection formValues)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);

            archivosasig arc = new archivosasig();

            try
            {

                string upload_dir = Server.MapPath("~/app_data/asignaciones/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(asig);

                        if (asig.est_id == 3)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 4)
                        {

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 7)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;

                        }

                        if (asig.est_id == 8)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                        }

                        if (asig.asi_comentario != null)
                        {

                            if (asig.asi_hfcomentario == null)

                                asig.asi_hfcomentario = DateTime.Now;
                        }

                        if (asig.asi_comsuper != null)
                        {

                            if (asig.asi_hfcomesuper == null)

                                asig.asi_hfcomesuper = DateTime.Now;
                        }

                        modeloAsig.guardar();

                        UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                        arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                        arc.asi_id = id;

                        modeloAsig.crearArchivo(arc);
                        modeloAsig.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                        return RedirectToAction("inicioasig");
                    }

                    else
                    {
                        UpdateModel(asig);

                        if (asig.est_id == 3)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 4)
                        {

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 7)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;

                        }

                        if (asig.est_id == 8)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                        }

                        if (asig.asi_comentario != null)
                        {

                            if (asig.asi_hfcomentario == null)

                                asig.asi_hfcomentario = DateTime.Now;
                        }

                        if (asig.asi_comsuper != null)
                        {

                            if (asig.asi_hfcomesuper == null)

                                asig.asi_hfcomesuper = DateTime.Now;
                        }



                        modeloAsig.guardar();
                        return RedirectToAction("inicioasig");
                    }
                }

                UpdateModel(asig);

                if (asig.est_id == 3)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;
                }

                if (asig.est_id == 4)
                {

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;
                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;
                }

                if (asig.est_id == 7)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;

                }

                if (asig.est_id == 8)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;
                }

                if (asig.asi_comentario != null)
                {

                    if (asig.asi_hfcomentario == null)

                        asig.asi_hfcomentario = DateTime.Now;
                }

                if (asig.asi_comsuper != null)
                {

                    if (asig.asi_hfcomesuper == null)

                        asig.asi_hfcomesuper = DateTime.Now;
                }



                modeloAsig.guardar();

                return RedirectToAction("inicioasig");
            }
            catch
            {
                ViewData["hora"] = DateTime.Now;
                ViewData["operacion"] = asig.actividades.act_operacion;
                ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a=>a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
                ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(u=>u.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
                ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
                ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a => a.act_activo == 1).AsEnumerable(), "act_id", "act_nombre");

                return View(asig);
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult obtenerActividad(int id)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);


            if (asig == null)
            {

                return RedirectToAction("noExite");

            }
            else
            {

                actividades act = modeloAct.detalleactividad(asig.act_id);

                bool cantidadPro = modeloPro.rutaPos(act.pro_id);
                bool cantidadAct = modeloAct.rutaPos(asig.act_id);
                bool cantidadAsi = modeloAsig.rutaPos(id);

                //CARGAR ARCHIVOS ASIGNACIONES
                if (cantidadAsi == true)
                {

                    var archi = modeloAsig.ruta(asig.asi_id);

                    string nombreArchivo = archi.ars_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/asignaciones/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["id_ars"] = archi.ars_id;
                    ViewData["filesAsig"] = files;

                }
                else
                {

                    ViewData["filesAsig"] = "";

                }


                //CARGAR ARCHIVOS ACTIVIDAD
                if (cantidadAct == true)
                {

                    var archi = modeloAct.ruta(asig.act_id);

                    string nombreArchivo = archi.ara_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["filesAct"] = files;

                }
                else
                {

                    ViewData["filesAct"] = "";

                }

                // CARGAR ARCHIVOS PROCEDMIENTO.
                if (cantidadPro == true)
                {

                    var archi = modeloPro.ruta(act.pro_id);

                    string nombreArchivo = archi.arp_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["filesPro"] = files;

                }
                else
                {

                    ViewData["filesPro"] = "";

                }

                ViewData["id"] = id;
                ViewData["hora"] = DateTime.Now;
                ViewData["operacion"] = asig.actividades.act_operacion;
                ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
                ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(u=>u.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
                ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
                ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a => a.act_activo == 1).AsEnumerable(), "act_id", "act_nombre");

                return View(asig);

            }


        }

        [Authorize(Roles = "Admin")]
        public ActionResult noExite()
        {

            return View();

        }

        [Authorize(Roles = "Admin")]
        public ActionResult buscarActividad()
        {

            return View();

        }


        //// FUNCIONES DE SUPERVISOR

        //Mostrar todas las asignaciones de las actividades.
        [Authorize(Roles = "Supervisor")]
        public ActionResult inicioasigSuper(int? page)
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id; 

            const int pageSize = 10;

            var asigs = modeloAsig.listarTodasAsignaciones().Where(u=>u.usuarios.are_id == area);
                
            var paginaasig = new PaginatedList<asignaciones>(asigs,
                                                             page ?? 0,
                                                             pageSize);
            return View(paginaasig);

        }

        //Mostrar determinada asignación.
        [Authorize(Roles = "Supervisor")]
        public ActionResult detalleSuper(int id)
        {
            asignaciones asig = modeloAsig.detalleasignacion(id);
            return View("detalleSuper", asig);

        }

        [Authorize(Roles = "Supervisor")]
        public ActionResult nuevaasigSuper()
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["usuario"] = ideUser;
            ViewData["hora"] = DateTime.Now;
            ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(u=>u.usu_activo == 1).Where(a=>a.are_id == area).AsEnumerable(), "usu_id", "usu_nombres");
            ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
            ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a => a.act_activo == 1).Where(a => a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

            return View();

        }

        //Registar nueva asignación.
        [Authorize(Roles = "Supervisor")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult registrarasigSuper()
        {

            repeticiones rep = new repeticiones();
            asignaciones asig = new asignaciones();
            archivosasig arc = new archivosasig();

            string upload_dir = Server.MapPath("~/app_data/asignaciones/");

            UpdateModel(rep);

            if (rep.rep_fechainicio != null && rep.rep_cantidad != null)
            {
                int incremento = (int)rep.rep_cantidad;

                UpdateModel(asig);

                DateTime finicio = (DateTime)rep.rep_fechainicio;
                DateTime ffin = asig.asi_fechaterminar;

                while (finicio.Date <= ffin.Date)
                {

                    asig = new asignaciones();
                    arc = new archivosasig();

                    if (finicio.DayOfWeek == DayOfWeek.Saturday || finicio.DayOfWeek == DayOfWeek.Sunday)
                    {
                        finicio = finicio.AddDays(incremento);
                    }
                    else
                    {
                        foreach (string f in Request.Files.Keys)
                        {
                            if (Request.Files[f].ContentLength > 0)
                            {

                                UpdateModel(asig);

                                asig.asi_fechaterminar = finicio;

                                UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                                modeloAsig.crearasignacion(asig);
                                modeloAsig.guardar();
                                finicio = finicio.AddDays(incremento);


                                arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                                arc.asi_id = asig.asi_id;
                                modeloAsig.crearArchivo(arc);

                                modeloAsig.guardar();

                                Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                            }

                            else
                            {

                                UpdateModel(asig);

                                asig.asi_fechaterminar = finicio;

                                modeloAsig.crearasignacion(asig);
                                modeloAsig.guardar();
                                finicio = finicio.AddDays(incremento);

                            }

                        }
                    }
                }
                return RedirectToAction("inicioasigSuper");
            }
            else
            {
                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(asig);

                        UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                        modeloAsig.crearasignacion(asig);
                        modeloAsig.guardar();

                        arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                        arc.asi_id = asig.asi_id;
                        modeloAsig.crearArchivo(arc);

                        modeloAsig.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                    }

                    else
                    {

                        asig = new asignaciones();
                        UpdateModel(asig);
                        modeloAsig.crearasignacion(asig);
                        modeloAsig.guardar();

                    }

                }

                return RedirectToAction("inicioasigSuper");
            }

        }



        [Authorize(Roles = "Supervisor")]
        public ActionResult editarasigSuper(int id)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);

            actividades act = modeloAct.detalleactividad(asig.act_id);

            bool cantidadPro = modeloPro.rutaPos(act.pro_id);
            bool cantidadAct = modeloAct.rutaPos(asig.act_id);
            bool cantidadAsi = modeloAsig.rutaPos(id);

            //CARGAR ARCHIVOS ASIGNACIONES
            if (cantidadAsi == true)
            {

                var archi = modeloAsig.ruta(asig.asi_id);

                string nombreArchivo = archi.ars_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/asignaciones/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_ars"] = archi.ars_id;
                ViewData["filesAsig"] = files;

            }
            else
            {

                ViewData["filesAsig"] = "";

            }


            //CARGAR ARCHIVOS ACTIVIDAD
            if (cantidadAct == true)
            {

                var archi = modeloAct.ruta(asig.act_id);

                string nombreArchivo = archi.ara_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["filesAct"] = files;

            }
            else
            {

                ViewData["filesAct"] = "";

            }

            // CARGAR ARCHIVOS PROCEDMIENTO.
            if (cantidadPro == true)
            {

                var archi = modeloPro.ruta(act.pro_id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["filesPro"] = files;

            }
            else
            {

                ViewData["filesPro"] = "";

            }

            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["hora"] = DateTime.Now;
            ViewData["operacion"] = asig.actividades.act_operacion;
            ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
            ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(a=>a.usu_activo == 1).Where(a=>a.are_id == area).AsEnumerable(), "usu_id", "usu_nombres");
            ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
            ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).Where(a=>a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

            return View(asig);
        }

        //Editar asignacion.
        [Authorize(Roles = "Supervisor")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarasigSuper(int id, FormCollection formValues)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);

            archivosasig arc = new archivosasig();

            try
            {

                string upload_dir = Server.MapPath("~/app_data/asignaciones/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(asig);

                        if (asig.est_id == 3)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 4)
                        {

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 7)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;

                        }

                        if (asig.est_id == 8)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                        }

                        if (asig.asi_comentario != null)
                        {

                            if (asig.asi_hfcomentario == null)

                                asig.asi_hfcomentario = DateTime.Now;
                        }

                        if (asig.asi_comsuper != null)
                        {

                            if (asig.asi_hfcomesuper == null)

                                asig.asi_hfcomesuper = DateTime.Now;
                        }

                        modeloAsig.guardar();

                        UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                        arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                        arc.asi_id = id;

                        modeloAsig.crearArchivo(arc);
                        modeloAsig.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                        return RedirectToAction("inicioasigSuper");
                    }

                    else
                    {
                        UpdateModel(asig);

                        if (asig.est_id == 3)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 4)
                        {

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 7)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;

                        }

                        if (asig.est_id == 8)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                        }

                        if (asig.asi_comentario != null)
                        {

                            if (asig.asi_hfcomentario == null)

                                asig.asi_hfcomentario = DateTime.Now;
                        }

                        if (asig.asi_comsuper != null)
                        {

                            if (asig.asi_hfcomesuper == null)

                                asig.asi_hfcomesuper = DateTime.Now;
                        }



                        modeloAsig.guardar();
                        return RedirectToAction("inicioasigSuper");
                    }
                }

                UpdateModel(asig);

                if (asig.est_id == 3)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;
                }

                if (asig.est_id == 4)
                {

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;
                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;
                }

                if (asig.est_id == 7)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;

                }

                if (asig.est_id == 8)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;
                }

                if (asig.asi_comentario != null)
                {

                    if (asig.asi_hfcomentario == null)

                        asig.asi_hfcomentario = DateTime.Now;
                }

                if (asig.asi_comsuper != null)
                {

                    if (asig.asi_hfcomesuper == null)

                        asig.asi_hfcomesuper = DateTime.Now;
                }



                modeloAsig.guardar();

                return RedirectToAction("inicioasigSuper");
            }
            catch
            {
                string ideUser = User.Identity.Name;

                usuarios user = modeloUsuario.ObtenerUser(ideUser);

                int area = user.are_id;

                ViewData["hora"] = DateTime.Now;
                ViewData["operacion"] = asig.actividades.act_operacion;
                ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
                ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(a=>a.usu_activo == 1).Where(a=>a.are_id == area).AsEnumerable(), "usu_id", "usu_nombres");
                ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
                ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).Where(a=>a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

                return View(asig);
            }
        }



        [Authorize(Roles = "Supervisor")]
        public ActionResult obtenerActividadSuper(int id)
        {

            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            asignaciones asig = modeloAsig.detalleasignacion(id);

            if (asig == null)
            {

                return RedirectToAction("noExiteSuper");

            }

            int act_area = asig.usuarios.are_id;

            if(act_area == area)
            {
                actividades act = modeloAct.detalleactividad(asig.act_id);

                bool cantidadPro = modeloPro.rutaPos(act.pro_id);
                bool cantidadAct = modeloAct.rutaPos(asig.act_id);
                bool cantidadAsi = modeloAsig.rutaPos(id);

                //CARGAR ARCHIVOS ASIGNACIONES
                if (cantidadAsi == true)
                {

                    var archi = modeloAsig.ruta(asig.asi_id);

                    string nombreArchivo = archi.ars_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/asignaciones/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["id_ars"] = archi.ars_id;
                    ViewData["filesAsig"] = files;

                }
                else
                {

                    ViewData["filesAsig"] = "";

                }


                //CARGAR ARCHIVOS ACTIVIDAD
                if (cantidadAct == true)
                {

                    var archi = modeloAct.ruta(asig.act_id);

                    string nombreArchivo = archi.ara_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["filesAct"] = files;

                }
                else
                {

                    ViewData["filesAct"] = "";

                }

                // CARGAR ARCHIVOS PROCEDMIENTO.
                if (cantidadPro == true)
                {

                    var archi = modeloPro.ruta(act.pro_id);

                    string nombreArchivo = archi.arp_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["filesPro"] = files;

                }
                else
                {

                    ViewData["filesPro"] = "";

                }

                ViewData["id"] = id;
                ViewData["hora"] = DateTime.Now;
                ViewData["operacion"] = asig.actividades.act_operacion;
                ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
                ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(a=>a.usu_activo == 1).Where(a => a.are_id == area).AsEnumerable(), "usu_id", "usu_nombres");
                ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
                ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).Where(a => a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

                return View(asig);
            
            }
            else
            {

                return RedirectToAction("noExiteSuper");

            }


        }

        [Authorize(Roles = "Supervisor")]
        public ActionResult noExiteSuper()
        {

            return View();

        }

        [Authorize(Roles = "Supervisor")]
        public ActionResult buscarActividadSuper()
        {

            return View();

        }

        //FUNCIONES DE EMPLEADO

        //Mostrar todas las asignaciones de las actividades.
        [Authorize(Roles = "Empleado")]
        public ActionResult inicioasigEmple(int? page)
        {
            string ideUser = User.Identity.Name;

            const int pageSize = 10;

            var asige = modeloAsig.listarTodasAsignaciones().Where(u=>u.usuarios.usu_username == ideUser);
            var paginaasig = new PaginatedList<asignaciones>(asige,
                                                             page ?? 0,
                                                             pageSize);
            return View(paginaasig);

        }


        //Mostrar determinada asignación.
        [Authorize(Roles = "Empleado")]
        public ActionResult detalleEmple(int id)
        {
            asignaciones asig = modeloAsig.detalleasignacion(id);
            return View("detalleEmple", asig);

        }

        [Authorize(Roles = "Empleado")]
        public ActionResult nuevaasigEmple()
        {
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["usuario"] = ideUser;
            ViewData["hora"] = DateTime.Now;
            ViewData["usuarios"] = user.usu_id;
            ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
            ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).Where(a => a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

            return View();

        }

        //Registar nueva asignación.
        [Authorize(Roles = "Empleado")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult registrarasigEmple()
        {

            repeticiones rep = new repeticiones();
            asignaciones asig = new asignaciones();
            archivosasig arc = new archivosasig();

            string upload_dir = Server.MapPath("~/app_data/asignaciones/");

            UpdateModel(rep);

            if (rep.rep_fechainicio != null && rep.rep_cantidad != null)
            {
                int incremento = (int)rep.rep_cantidad;

                UpdateModel(asig);

                DateTime finicio = (DateTime)rep.rep_fechainicio;
                DateTime ffin = asig.asi_fechaterminar;

                while (finicio.Date <= ffin.Date)
                {

                    asig = new asignaciones();
                    arc = new archivosasig();

                    if (finicio.DayOfWeek == DayOfWeek.Saturday || finicio.DayOfWeek == DayOfWeek.Sunday)
                    {
                        finicio = finicio.AddDays(incremento);
                    }
                    else
                    {
                        foreach (string f in Request.Files.Keys)
                        {
                            if (Request.Files[f].ContentLength > 0)
                            {

                                UpdateModel(asig);

                                asig.asi_fechaterminar = finicio;

                                UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                                modeloAsig.crearasignacion(asig);
                                modeloAsig.guardar();
                                finicio = finicio.AddDays(incremento);


                                arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                                arc.asi_id = asig.asi_id;
                                modeloAsig.crearArchivo(arc);

                                modeloAsig.guardar();

                                Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                            }

                            else
                            {

                                UpdateModel(asig);

                                asig.asi_fechaterminar = finicio;

                                modeloAsig.crearasignacion(asig);
                                modeloAsig.guardar();
                                finicio = finicio.AddDays(incremento);

                            }

                        }
                    }
                }
                return RedirectToAction("inicioasigEmple");
            }
            else
            {
                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(asig);

                        UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                        modeloAsig.crearasignacion(asig);
                        modeloAsig.guardar();

                        arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                        arc.asi_id = asig.asi_id;
                        modeloAsig.crearArchivo(arc);

                        modeloAsig.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                    }

                    else
                    {

                        asig = new asignaciones();
                        UpdateModel(asig);
                        modeloAsig.crearasignacion(asig);
                        modeloAsig.guardar();

                    }

                }

                return RedirectToAction("inicioasigEmple");
            }

        }


        [Authorize(Roles = "Empleado")]
        public ActionResult editarasigEmple(int id)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);

            actividades act = modeloAct.detalleactividad(asig.act_id);

            bool cantidadPro = modeloPro.rutaPos(act.pro_id);
            bool cantidadAct = modeloAct.rutaPos(asig.act_id);
            bool cantidadAsi = modeloAsig.rutaPos(id);

            //CARGAR ARCHIVOS ASIGNACIONES
            if (cantidadAsi == true)
            {

                var archi = modeloAsig.ruta(asig.asi_id);

                string nombreArchivo = archi.ars_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/asignaciones/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_ars"] = archi.ars_id;
                ViewData["filesAsig"] = files;

            }
            else
            {

                ViewData["filesAsig"] = "";

            }


            //CARGAR ARCHIVOS ACTIVIDAD
            if (cantidadAct == true)
            {

                var archi = modeloAct.ruta(asig.act_id);

                string nombreArchivo = archi.ara_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["filesAct"] = files;

            }
            else
            {

                ViewData["filesAct"] = "";

            }

            // CARGAR ARCHIVOS PROCEDMIENTO.
            if (cantidadPro == true)
            {

                var archi = modeloPro.ruta(act.pro_id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["filesPro"] = files;

            }
            else
            {

                ViewData["filesPro"] = "";

            }

            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            ViewData["hora"] = DateTime.Now;
            ViewData["operacion"] = asig.actividades.act_operacion;
            ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
            ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(a=>a.usu_activo == 1).Where(a => a.are_id == area).AsEnumerable(), "usu_id", "usu_nombres");
            ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
            ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).Where(a => a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

            return View(asig);
        }

        //Editar asignacion.
        [Authorize(Roles = "Empleado")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarasigEmple(int id, FormCollection formValues)
        {

            asignaciones asig = modeloAsig.detalleasignacion(id);

            archivosasig arc = new archivosasig();

            try
            {

                string upload_dir = Server.MapPath("~/app_data/asignaciones/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(asig);

                        if (asig.est_id == 3)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 4)
                        {

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 7)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;

                        }

                        if (asig.est_id == 8)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                        }

                        if (asig.asi_comentario != null)
                        {

                            if (asig.asi_hfcomentario == null)

                                asig.asi_hfcomentario = DateTime.Now;
                        }

                        if (asig.asi_comsuper != null)
                        {

                            if (asig.asi_hfcomesuper == null)

                                asig.asi_hfcomesuper = DateTime.Now;
                        }

                        modeloAsig.guardar();

                        UpdateModel(arc, new[] { "ars_nombre", "ars_fecha" });

                        arc.ars_nombre = "(Num-" + asig.asi_id + ")" + Request.Files[f].FileName;
                        arc.asi_id = id;

                        modeloAsig.crearArchivo(arc);
                        modeloAsig.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + asig.asi_id + ")" + Request.Files[f].FileName));
                        return RedirectToAction("inicioasigEmple");
                    }

                    else
                    {
                        UpdateModel(asig);

                        if (asig.est_id == 3)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 4)
                        {

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;
                        }

                        if (asig.est_id == 7)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;

                        }

                        if (asig.est_id == 8)
                        {

                            if (asig.asi_fhinicio == null)
                                asig.asi_fhinicio = DateTime.Now;

                            if (asig.asi_fhfin == null)
                                asig.asi_fhfin = DateTime.Now;
                        }

                        if (asig.asi_comentario != null)
                        {

                            if (asig.asi_hfcomentario == null)

                                asig.asi_hfcomentario = DateTime.Now;
                        }

                        if (asig.asi_comsuper != null)
                        {

                            if (asig.asi_hfcomesuper == null)

                                asig.asi_hfcomesuper = DateTime.Now;
                        }



                        modeloAsig.guardar();
                        return RedirectToAction("inicioasigEmple");
                    }
                }

                UpdateModel(asig);

                if (asig.est_id == 3)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;
                }

                if (asig.est_id == 4)
                {

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;
                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;
                }

                if (asig.est_id == 7)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;

                }

                if (asig.est_id == 8)
                {

                    if (asig.asi_fhinicio == null)
                        asig.asi_fhinicio = DateTime.Now;

                    if (asig.asi_fhfin == null)
                        asig.asi_fhfin = DateTime.Now;
                }

                if (asig.asi_comentario != null)
                {

                    if (asig.asi_hfcomentario == null)

                        asig.asi_hfcomentario = DateTime.Now;
                }

                if (asig.asi_comsuper != null)
                {

                    if (asig.asi_hfcomesuper == null)

                        asig.asi_hfcomesuper = DateTime.Now;
                }



                modeloAsig.guardar();

                return RedirectToAction("inicioasigEmple");
            }
            catch
            {
                string ideUser = User.Identity.Name;

                usuarios user = modeloUsuario.ObtenerUser(ideUser);

                int area = user.are_id;

                ViewData["hora"] = DateTime.Now;
                ViewData["operacion"] = asig.actividades.act_operacion;
                ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
                ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(a=>a.usu_activo == 1).Where(a => a.are_id == area).AsEnumerable(), "usu_id", "usu_nombres");
                ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
                ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).Where(a => a.procedimiento.are_id == area).AsEnumerable(), "act_id", "act_nombre");

                return View(asig);
            }
        }


        //Método para descargar archivo
        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        public ActionResult DownloadAsignacion(string fn)
        {
            string pfn = Server.MapPath("~/App_Data/asignaciones/" + fn);
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



        [Authorize(Roles = "Empleado")]
        public ActionResult obtenerActividadEmple(int id)
        {
            string ideUser = User.Identity.Name;

            asignaciones asig = modeloAsig.detalleasignacion(id);


            if (asig == null)
            {

                return RedirectToAction("noExiteEmple");

            }

            string numAct = asig.usuarios.usu_username;
                

            if (numAct == ideUser)
            {

                actividades act = modeloAct.detalleactividad(asig.act_id);

                bool cantidadPro = modeloPro.rutaPos(act.pro_id);
                bool cantidadAct = modeloAct.rutaPos(asig.act_id);
                bool cantidadAsi = modeloAsig.rutaPos(id);

                //CARGAR ARCHIVOS ASIGNACIONES
                if (cantidadAsi == true)
                {

                    var archi = modeloAsig.ruta(asig.asi_id);

                    string nombreArchivo = archi.ars_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/asignaciones/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["id_ars"] = archi.ars_id;
                    ViewData["filesAsig"] = files;

                }
                else
                {

                    ViewData["filesAsig"] = "";

                }


                //CARGAR ARCHIVOS ACTIVIDAD
                if (cantidadAct == true)
                {

                    var archi = modeloAct.ruta(asig.act_id);

                    string nombreArchivo = archi.ara_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/actividades/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["filesAct"] = files;

                }
                else
                {

                    ViewData["filesAct"] = "";

                }

                // CARGAR ARCHIVOS PROCEDMIENTO.
                if (cantidadPro == true)
                {

                    var archi = modeloPro.ruta(act.pro_id);

                    string nombreArchivo = archi.arp_nombre;

                    var files = from f in Directory.GetFiles(
                    Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                                select Path.GetFileName(f);

                    ViewData["filesPro"] = files;

                }
                else
                {

                    ViewData["filesPro"] = "";

                }

                ViewData["id"] = id;
                ViewData["hora"] = DateTime.Now;
                ViewData["operacion"] = asig.actividades.act_operacion;
                ViewData["estado"] = new SelectList(modeloEst.listarEstados().Where(a => a.est_hd_estado == 1).AsEnumerable(), "est_id", "est_descripcion");
                ViewData["usuarios"] = new SelectList(modeloAsig.listarUsuario().Where(a=>a.usu_activo == 1).AsEnumerable(), "usu_id", "usu_nombres");
                ViewData["prioridades"] = new SelectList(modeloPri.listarprioridad().AsEnumerable(), "pri_id", "pri_nombre");
                ViewData["actividades"] = new SelectList(modeloActi.listaractividades().Where(a=>a.act_activo == 1).AsEnumerable(), "act_id", "act_nombre");

                return View(asig);


            }
            else
            {

                return RedirectToAction("noExiteEmple");


            }


        }

        [Authorize(Roles = "Empleado")]
        public ActionResult noExiteEmple()
        {

            return View();

        }

        [Authorize(Roles = "Empleado")]
        public ActionResult buscarActividadEmple()
        {

            return View();

        }


        //FUNCIONES PARA TODOS.
        
        //Método para descargar archivo
        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        public ActionResult DownloadProcedimiento(string fn)
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
            archivosasig arc = modeloAsig.detalleArchivo(id);

            modeloAsig.eliminarArchivo(arc);
            modeloAsig.guardar();

            return View("editarasig");
        }

        [Authorize(Roles = "Admin,Supervisor,Empleado")]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            asignaciones asig = modeloAsig.detalleasignacion(id);

            modeloAsig.eliminarasignacion(asig);
            modeloAsig.guardar();

            return View("inicioasig");
        }
    }
}