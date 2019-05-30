using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;
using Sigacun.Helpers;

namespace Sigacun.Controllers
{
    public class ActividadHdController : Controller
    {

        PreguntasModel preguntas = new PreguntasModel();
        ProcedimientoModel modeloPro = new ProcedimientoModel();
        ActividadHdModel modeloAch    = new ActividadHdModel();
        CalcularTiempos tiempocorrido = new CalcularTiempos();
        EvaluacionModel modeloEva = new EvaluacionModel();
        ConeccionMysql modeloMysql = new ConeccionMysql();
        UsuarioModel modeloUsuario = new UsuarioModel();


        //ADMINISTRADOR
        [Authorize(Roles = "Admin")]
        public ActionResult inicioMesaAyuda(int? page) 
        {
            //calcularTodos(); // ESTO ES UNA PRUEBA
            const int pageSize = 10;

            var ach = modeloAch.todosActividad().OrderByDescending(e=>e.est_id).OrderByDescending(f=>f.ahd_fhpeticion);
            

            var paginaAch = new PaginatedList<actividades_hd>(ach,
                                                              page ?? 0,
                                                              pageSize);
            return View(paginaAch);
        
        }
        [Authorize(Roles = "Admin")]
        public ActionResult editaracthd(int id)
        {
            actividades_hd ach = modeloAch.obtenerActividad(id);

            ViewData["id"] = ach.ahd_id;
            ViewData["fecha"] = ach.ahd_fhfin;
            ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
            ViewData["preguntas"] = preguntas.listarpreguntas();

            bool va = modeloEva.evaluacionCali(ach.ahd_id);

            if (va == true)
            {

                var evas = modeloEva.detalle(ach.ahd_id);

                int num = evas.ahd_hd_numsolicitud;

                modeloMysql.registrarEvaluacion(num, ach.ahd_id);

            }   

            return View(ach);
        }

        //Editar actividad.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editaracthd(int id, FormCollection formValues)
        {

            actividades_hd act = modeloAch.obtenerActividad(id);


            try
            {

                if (act.est_id != 3) {

                    DateTime finicio = act.ahd_fhpeticion;
                    DateTime fasig = act.ahd_fhasignacion;
                    DateTime ffinal = (DateTime)act.ahd_fhfin;

                    float duraciontotal = tiempocorrido.calcularTiempo(finicio, ffinal);
                    float duracionmedia = tiempocorrido.calcularTiempo(fasig, ffinal);


                    UpdateModel(act);

                    if (act.ahd_fhcomentario == null && act.ahd_comentario != null)
                    {

                        act.ahd_fhcomentario = DateTime.Now;

                    }

                    if (act.ahd_fhcomsuper == null && act.ahd_comsuper != null)
                    {

                        act.ahd_fhcomsuper = DateTime.Now;
                    }

                    act.ahd_duracion = duracionmedia;
                    act.ahd_duratotal = duraciontotal;
                    modeloAch.guardar();

                    
                }
                else{

                    UpdateModel(act);

                    if (act.ahd_fhcomentario == null && act.ahd_comentario != null)
                    {

                        act.ahd_fhcomentario = DateTime.Now;

                    }

                    if (act.ahd_fhcomsuper == null && act.ahd_comsuper != null)
                    {

                        act.ahd_fhcomsuper = DateTime.Now;
                    }


                    modeloAch.guardar();
                }

                return RedirectToAction("inicioMesaAyuda");
            }

            catch
            {
                ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                return View(act);
            }
         }

        [Authorize(Roles = "Admin")]
        public ActionResult obtenerActividad(int numero)
        {

            actividades_hd ach = modeloAch.detalleActividad(numero);

            if (ach == null)
            {

                return RedirectToAction("noExite");

            }
            else {

                ViewData["id"] = ach.ahd_id;
                ViewData["fecha"] = ach.ahd_fhfin;
                ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                ViewData["preguntas"] = preguntas.listarpreguntas();

                bool va = modeloEva.evaluacionCali(ach.ahd_id);

                if (va == true)
                {

                    var evas = modeloEva.detalle(ach.ahd_id);

                    int num = evas.ahd_hd_numsolicitud;
                    modeloMysql.registrarEvaluacion(num, ach.ahd_id);

                }

                return View(ach);

            
            }

            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult noExite() {

            return View();
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult buscarActividad()
        {

            return View();

        }

        // SUPERVISOR


        [Authorize(Roles = "Supervisor")]
        public ActionResult inicioMesaAyudaSupervisor(int? page)
        {
            // OBTENER USUARIO LOGUEADO
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;

            const int pageSize = 10;
                    
            var achs = modeloAch.todosActividad().Where(u=>u.usuarios.are_id == area).OrderByDescending(f=>f.ahd_fhpeticion).OrderBy(a=>a.est_id);
                
            var paginaAch = new PaginatedList<actividades_hd>(achs,
                                                              page ?? 0,
                                                              pageSize);

            return View(paginaAch);

        }

        [Authorize(Roles = "Supervisor")]
        public ActionResult editaracthdsuper(int id)
        {
            actividades_hd ach = modeloAch.obtenerActividad(id);
            ViewData["id"] = ach.ahd_id;
            ViewData["fecha"] = ach.ahd_fhfin;
            ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
            ViewData["preguntas"] = preguntas.listarpreguntas();

            bool va = modeloEva.evaluacionCali(ach.ahd_id);

            if (va == true)
            {

                var evas = modeloEva.detalle(ach.ahd_id);

                int num = evas.ahd_hd_numsolicitud;
                modeloMysql.registrarEvaluacion(num, ach.ahd_id);

            }

            return View(ach);
        }

        //Editar actividad.
        [Authorize(Roles = "Supervisor")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editaracthdsuper(int id, FormCollection formValues)
        {

            actividades_hd act = modeloAch.obtenerActividad(id);


            try
            {

                if (act.est_id != 3)
                {

                    DateTime finicio = act.ahd_fhpeticion;
                    DateTime fasig = act.ahd_fhasignacion;
                    DateTime ffinal = (DateTime)act.ahd_fhfin;

                    float duraciontotal = tiempocorrido.calcularTiempo(finicio, ffinal);
                    float duracionmedia = tiempocorrido.calcularTiempo(fasig, ffinal);


                    UpdateModel(act);

                    if (act.ahd_fhcomentario == null && act.ahd_comentario != null)
                    {

                        act.ahd_fhcomentario = DateTime.Now;

                    }

                    if (act.ahd_fhcomsuper == null && act.ahd_comsuper != null)
                    {

                        act.ahd_fhcomsuper = DateTime.Now;
                    }

                    act.ahd_duracion = duracionmedia;
                    act.ahd_duratotal = duraciontotal;
                    modeloAch.guardar();


                }
                else
                {

                    UpdateModel(act);

                    if (act.ahd_fhcomentario == null && act.ahd_comentario != null)
                    {

                        act.ahd_fhcomentario = DateTime.Now;

                    }

                    if (act.ahd_fhcomsuper == null && act.ahd_comsuper != null)
                    {

                        act.ahd_fhcomsuper = DateTime.Now;
                    }

                    modeloAch.guardar();
                }

                return RedirectToAction("inicioMesaAyudaSupervisor");
            }

            catch
            {
                ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                return View(act);
            }
        }

        [Authorize(Roles = "Supervisor")]
        public ActionResult obtenerActividadSuper(int numero)
        {

            // OBTENER USUARIO LOGUEADO
            string ideUser = User.Identity.Name;

            usuarios user = modeloUsuario.ObtenerUser(ideUser);

            int area = user.are_id;


            actividades_hd ach = modeloAch.detalleActividad(numero);



            if (ach == null)
            {

                return RedirectToAction("noExiteSuper");

            }

            int numAct_ar = ach.usuarios.are_id;

            if(numAct_ar == area)
            {
                ViewData["id"] = ach.ahd_id;
                ViewData["fecha"] = ach.ahd_fhfin;
                ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                ViewData["preguntas"] = preguntas.listarpreguntas();

                bool va = modeloEva.evaluacionCali(ach.ahd_id);

                if (va == true)
                {

                    var evas = modeloEva.detalle(ach.ahd_id);

                    int num = evas.ahd_hd_numsolicitud;
                    modeloMysql.registrarEvaluacion(num, ach.ahd_id);

                }

                return View(ach);
            
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


       

        // FUNCIONES DE EMPLEADO

        [Authorize(Roles = "Empleado")]
        public ActionResult inicioMesaAyudaEmple(int? page)
        {
            // OBTENER USUARIO LOGUEADO
            string ideUser = User.Identity.Name;

            const int pageSize = 10;

            var ache = modeloAch.todosActividad().Where(u => u.usuarios.usu_username == ideUser).OrderByDescending(f => f.ahd_fhpeticion).OrderBy(a => a.est_id);


            var paginaAch = new PaginatedList<actividades_hd>(ache,
                                                              page ?? 0,
                                                              pageSize);

            return View(paginaAch);

        }


        [Authorize(Roles = "Empleado")]
        public ActionResult editaracthdEmple(int id)
        {
            actividades_hd ach = modeloAch.obtenerActividad(id);
            ViewData["id"] = ach.ahd_id;
            ViewData["fecha"] = ach.ahd_fhfin;
            ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
            ViewData["preguntas"] = preguntas.listarpreguntas();

            bool va = modeloEva.evaluacionCali(ach.ahd_id);

            if (va == true)
            {

                var evas = modeloEva.detalle(ach.ahd_id);

                int num = evas.ahd_hd_numsolicitud;
                modeloMysql.registrarEvaluacion(num, ach.ahd_id);

            }

            return View(ach);
        }

        //Editar actividad.
        [Authorize(Roles = "Empleado")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editaracthdEmple(int id, FormCollection formValues)
        {

            actividades_hd act = modeloAch.obtenerActividad(id);


            try
            {

                if (act.est_id != 3)
                {

                    DateTime finicio = act.ahd_fhpeticion;
                    DateTime fasig = act.ahd_fhasignacion;
                    DateTime ffinal = (DateTime)act.ahd_fhfin;

                    float duraciontotal = tiempocorrido.calcularTiempo(finicio, ffinal);
                    float duracionmedia = tiempocorrido.calcularTiempo(fasig, ffinal);


                    UpdateModel(act);

                    if (act.ahd_comentario != null)
                    {
                        if (act.ahd_fhcomentario == null)
                            act.ahd_fhcomentario = DateTime.Now;

                    }

                    act.ahd_duracion = duracionmedia;
                    act.ahd_duratotal = duraciontotal;
                    modeloAch.guardar();


                }
                else
                {

                    UpdateModel(act);

                    if (act.ahd_comentario != null)
                    {
                        if (act.ahd_fhcomentario == null)
                            act.ahd_fhcomentario = DateTime.Now;

                    }

                    modeloAch.guardar();
                }

                return RedirectToAction("inicioMesaAyudaEmple");
            }

            catch
            {
                ViewData["id"] = act.ahd_id;
                ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                return View(act);
            }
        }

        [Authorize(Roles = "Empleado")]
        public ActionResult obtenerActividadEmple(int numero)
        {
            string ideUser = User.Identity.Name;

            actividades_hd ach = modeloAch.detalleActividad(numero);


            if (ach == null)
            {

                return RedirectToAction("noExiteEmple");

            }

            string numAct = ach.usuarios.usu_username;

            if (numAct == ideUser) {

                ViewData["id"] = ach.ahd_id;
                ViewData["fecha"] = ach.ahd_fhfin;
                ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().Where(a=>a.pro_activo == 1).AsEnumerable(), "pro_id", "pro_codigoun");
                ViewData["preguntas"] = preguntas.listarpreguntas();

                bool va = modeloEva.evaluacionCali(ach.ahd_id);

                if (va == true)
                {

                    var evas = modeloEva.detalle(ach.ahd_id);

                    int num = evas.ahd_hd_numsolicitud;
                    modeloMysql.registrarEvaluacion(num, ach.ahd_id);

                }

                return View(ach);
            
            
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

        
       //ESTO ES UNA PRUEBA
        /*public void calcularTodos()
        {

            ActividadHdModel modeloAch = new ActividadHdModel();

            var resul = modeloAch.CalcularTodos();

            foreach (var fes in resul)
            {

                int id = fes.ahd_id;

                actividades_hd act = modeloAch.obtenerActividad(id);


                DateTime fin = (DateTime)act.ahd_fhfin;
                DateTime inicio = (DateTime)act.ahd_fhpeticion;
                DateTime asignacion = (DateTime)act.ahd_fhasignacion;

                float total = tiempocorrido.calcularTiempo(inicio, fin);
                float media = tiempocorrido.calcularTiempo(asignacion, fin);

                UpdateModel(act);
                act.ahd_duracion = media;
                act.ahd_duratotal = total;
                modeloAch.guardar();

            }
        }*/
         
    }
}
