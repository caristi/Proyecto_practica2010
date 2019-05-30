using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;
using Sigacun.Helpers;

namespace Sigacun.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        AsignacionModel asigModel = new AsignacionModel();
        ReportesModel repModel = new ReportesModel();
        AreaModel areModel = new AreaModel();
        CalcularTiempos tiempocorrido = new CalcularTiempos();
        PreguntasModel preguntas = new PreguntasModel();
        ProcedimientoModel modeloPro = new ProcedimientoModel();
        ActividadHdModel modeloAch = new ActividadHdModel();
        ActividadModel modeloAct = new ActividadModel();
        UsuarioModel modeloUsuario = new UsuarioModel();


        [Authorize(Roles = "Admin")]
        public ActionResult repostesTodasPersona() {

            return View("repostesTodasPersona");
        
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReporteGraficoTodasPersonas(DateTime desde,DateTime hasta) {


                List<float> chartList = new List<float>();

                List<usuarios> usu = repModel.todosUsuario().ToList();

                List<string> nomUsu = usu.Select(n => n.usu_nombres).ToList();
                List<int> id_Usu = usu.Select(n => n.usu_id).ToList();


                foreach (int id in id_Usu)
                {

                    float nu = repModel.promedioTiempos(id, desde, hasta);
                    chartList.Add(nu);

                }

                ViewData["Chart"] = chartList;
                ViewData["usuario"] = nomUsu;

                return View("ReporteGraficoTodasPersonas");


        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReporteGraficoTodasArea(DateTime desde, DateTime hasta)
        {

            List<float> areaList = new List<float>();

            List<areas> areas = repModel.todasAreas().ToList();

            List<string> nomUsu = areas.Select(a => a.are_nombre).ToList();
            List<int> id_Area = areas.Select(a => a.are_id).ToList();


            foreach (int id in id_Area)
            {

                float nu = repModel.promedioTiemposArea(id, desde, hasta);
                areaList.Add(nu);

            }

            ViewData["Chart"] = areaList;
            ViewData["area"] = nomUsu;

            return View();

        }

        [Authorize(Roles = "Admin")]
        public ActionResult reportesTodasArea()
        {
           return View();
        }


        //ADMINITRADOR
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //LISTAR USUARIOS.
        [Authorize(Roles = "Admin")]
        public ActionResult Usuarios() {

            var usu = asigModel.listarUsuario().Where(u=>u.usu_activo == 1);

            return View("Usuarios", usu);
        
        }

        //LISTAR AREAS
        [Authorize(Roles = "Admin")]
        public ActionResult listaAreas(){

            var area = areModel.listarAreas();
            return View("listaAreas",area);
        
        }

        //LISTAR PROCEDIMIENTOS
        [Authorize(Roles = "Admin")]
        public ActionResult listarProcedimientos(){

            var pro = modeloPro.listarprocedimientos();

            return View("listarProcedimientos", pro);
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult listarActividades() {


            var act = modeloAct.listaractividades().Where(a=>a.act_activo == 1).Where(a=>a.act_operacion == 1);

            return View("listarActividades",act);

        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult mediaOperacion(int id)
        {
            ViewData["id_act"] = id;
            ViewData["nombre_act"] = modeloAct.detalleactividad(id).act_nombre;
            return View();
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult mediadeOperacion(int act_id, DateTime desde, DateTime hasta) {

            ViewData["mediaOperacion"] = repModel.resultadoDeOperacion(act_id, desde, hasta);
            ViewData["actividad"] = modeloAct.detalleactividad(act_id).act_nombre;

            return View();
        
        }

        // REPORTES POR PERSONA

        [Authorize(Roles = "Admin")]
        public ActionResult usuMesaayuda(int id) {

            var usu = repModel.detalleUsuario(id);

            ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
            ViewData["id"] = id;

            return View("usuMesaayuda",usu);
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult InformeMesaAyuda(int id, DateTime desde, DateTime hasta) {


            var usu = repModel.detalleUsuario(id);

            ViewData["finicio"] = desde;
            ViewData["ffin"] = hasta;
            ViewData["id_user"] = usu.usu_id;

            ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
            ViewData["area"] = usu.areas.are_nombre;
            ViewData["promedio"] = repModel.promedioTiempos(id, desde, hasta);
            ViewData["cantidadCasos"] = repModel.cantidadCasos(id, desde, hasta);
            ViewData["calificacion"] = repModel.promedioCalifacion(id,desde,hasta);


            return View("InformeMesaAyuda");
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult casosPersona(int id, DateTime finicio,DateTime ffin)
        {

            var casos = repModel.numero(id,finicio,ffin);

            usuarios usu = repModel.detalleUsuario(id);

            ViewData["persona"] = usu.usu_id;
            ViewData["finicio"] = finicio.Date;
            ViewData["ffin"] = ffin.Date;

            return View(casos);

        }

        //PROCEDIMIENTOS POR PERSONA
        [Authorize(Roles = "Admin")]
        public ActionResult proPersona(int id_user) {

            var act = repModel.procXpersona(id_user);

            var usu = repModel.detalleUsuario(id_user);
            ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
            ViewData["id_user"] = id_user;

            return View("proPersona", act);
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult actividadesProcedimiento(int id_user, int id_pro) {

            var usu = repModel.detalleUsuario(id_user);
            var pro = modeloPro.detalleprocedimientos(id_pro);

            ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
            ViewData["procedimiento"] = pro.pro_codigoun;
            ViewData["id_user"] = id_user;
            ViewData["id_pro"] = id_pro;
            
            
            return View();
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult mostrarProcedmiento(int id_user, int id_pro, DateTime desde, DateTime hasta) {

            var usu = repModel.detalleUsuario(id_user);
            var pro = modeloPro.detalleprocedimientos(id_pro);

            ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
            ViewData["procedimiento"] = pro.pro_codigoun;

            ViewData["cantidadTotal"] = repModel.cantTotalProPer(id_user, id_pro, desde, hasta);
            ViewData["cantidadPositivas"] = repModel.cantPosProPer(id_user, id_pro, desde, hasta);
            ViewData["cantidadNegitivas"] = repModel.cantNegProPer(id_user, id_pro, desde, hasta);

            return View();
        
        }

        // FIN REPORTES POR PERSONA.


        // REPORTES POR AREA.
        [Authorize(Roles = "Admin")]
        public ActionResult areaMesaAyuda(int id) {

            var area = areModel.detalleArea(id);

            ViewData["area"] = area.are_nombre;
            ViewData["id_area"] = area.are_id;

            return View("areaMesaAyuda");
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult areaInfo(int id, DateTime desde, DateTime hasta) {

            var area = areModel.detalleArea(id);

            ViewData["cantidad"] = repModel.cantidadCasosArea(id,desde,hasta);
            ViewData["id_area"] = area.are_id;
            ViewData["Calificacion"] = repModel.promedioCalifacionArea(id,desde,hasta);
            ViewData["area"] = area.are_nombre;
            ViewData["promedioTiempo"] = repModel.promedioTiemposArea(id,desde,hasta);


            return View("areaInfo");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult editaracthd(int id)
        {
            actividades_hd ach = modeloAch.obtenerActividad(id);

            
            ViewData["id"] = ach.ahd_id;
            ViewData["user"] = ach.usu_id;
            ViewData["fecha"] = ach.ahd_fhfin;
            ViewData["procedimientos"] = new SelectList(modeloPro.listarprocedimientos().AsEnumerable(), "pro_id", "pro_codigoun");
            ViewData["preguntas"] = preguntas.listarpreguntas();
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

                if (act.est_id != 3)
                {

                    DateTime finicio = act.ahd_fhpeticion;
                    DateTime fasig = act.ahd_fhasignacion;
                    DateTime ffinal = (DateTime)act.ahd_fhfin;

                    float duraciontotal = tiempocorrido.calcularTiempo(finicio, ffinal);
                    float duracionmedia = tiempocorrido.calcularTiempo(fasig, ffinal);

                    UpdateModel(act);
                    act.ahd_duracion = duracionmedia;
                    act.ahd_duratotal = duraciontotal;
                    modeloAch.guardar();
                }
                else
                {
                    UpdateModel(act);
                    modeloAch.guardar();
                }

                return RedirectToAction("Index");
            }

            catch
            {
                return View(act);
            }
        }

        // REPORTES POR PROCEDIMIENTO

        [Authorize(Roles = "Admin")]
        public ActionResult prodecimientoMesaAyuda(int id) {

            var pro = modeloPro.detalleprocedimientos(id);

            ViewData["codigoPro"] = pro.pro_codigoun;
            ViewData["nombre"] = pro.pro_nombre;

            ViewData["cantidad"] = repModel.cantidadCasosPro(id);

            return View("prodecimientoMesaAyuda");

        }


        //SUPERVISOR
       [Authorize(Roles = "Supervisor")]
       public ActionResult IndexSupervisor()
       {
            return View();
       }

       //LISTAR USUARIOS.
       [Authorize(Roles = "Supervisor")]
       public ActionResult UsuariosSuper()
       {
           // OBTENER USUARIO LOGUEADO
           string ideUser = User.Identity.Name;

           usuarios user = modeloUsuario.ObtenerUser(ideUser);

           int area = user.are_id;

           var usu = asigModel.listarUsuario().Where(u => u.usu_activo == 1).Where(u=>u.are_id == area);

           return View("UsuariosSuper", usu);
       }

       //PROCEDIMIENTOS POR PERSONA
       [Authorize(Roles = "Supervisor")]
       public ActionResult proPersonaSuper(int id_user)
       {

           var act = repModel.procXpersona(id_user);

           var usu = repModel.detalleUsuario(id_user);
           ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
           ViewData["id_user"] = id_user;

           return View("proPersonaSuper", act);
       }

       [Authorize(Roles = "Supervisor")]
       public ActionResult actividadesProcedimientoSuper(int id_user, int id_pro)
       {

           var usu = repModel.detalleUsuario(id_user);
           var pro = modeloPro.detalleprocedimientos(id_pro);

           ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
           ViewData["procedimiento"] = pro.pro_codigoun;
           ViewData["id_user"] = id_user;
           ViewData["id_pro"] = id_pro;


           return View();

       }

       [Authorize(Roles = "Supervisor")]
       public ActionResult mostrarProcedimientoSuper(int id_user, int id_pro, DateTime desde, DateTime hasta)
       {

           var usu = repModel.detalleUsuario(id_user);
           var pro = modeloPro.detalleprocedimientos(id_pro);

           ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
           ViewData["procedimiento"] = pro.pro_codigoun;
           ViewData["id_user"] = id_user;

           ViewData["cantidadTotal"] = repModel.cantTotalProPer(id_user, id_pro, desde, hasta);
           ViewData["cantidadPositivas"] = repModel.cantPosProPer(id_user, id_pro, desde, hasta);
           ViewData["cantidadNegitivas"] = repModel.cantNegProPer(id_user, id_pro, desde, hasta);

           return View();

       }

       [Authorize(Roles = "Supervisor")]
       public ActionResult repostesTodasPersonaSuper()
       {

           return View("repostesTodasPersonaSuper");

       }

       [Authorize(Roles = "Supervisor")]
       [AcceptVerbs(HttpVerbs.Post)]
       public ActionResult ReporteGraficoTodasPersonasSuper(DateTime desde, DateTime hasta)
       {
           // OBTENER USUARIO LOGUEADO
           string ideUser = User.Identity.Name;

           usuarios user = modeloUsuario.ObtenerUser(ideUser);

           int area = user.are_id;

           List<float> chartList = new List<float>();

           List<usuarios> usu = repModel.todosUsuario().Where(u=>u.are_id == area).ToList();

           List<string> nomUsu = usu.Select(n => n.usu_nombres).ToList();
           List<int> id_Usu = usu.Select(n => n.usu_id).ToList();


           foreach (int id in id_Usu)
           {

               float nu = repModel.promedioTiempos(id, desde, hasta);
               chartList.Add(nu);

           }

           ViewData["Chart"] = chartList;
           ViewData["usuario"] = nomUsu;

           return View("ReporteGraficoTodasPersonasSuper");
       }

       [Authorize(Roles = "Supervisor")]
       public ActionResult listarActividadesSuper()
       {

           // OBTENER USUARIO LOGUEADO
           string ideUser = User.Identity.Name;

           usuarios user = modeloUsuario.ObtenerUser(ideUser);

           int area = user.are_id;

           var act = modeloAct.listaractividades().Where(a => a.act_operacion == 1).Where(a=>a.procedimiento.are_id == area);

           return View("listarActividadesSuper", act);

       }

       [Authorize(Roles = "Supervisor")]
       public ActionResult mediaOperacionSuper(int id)
       {
           ViewData["id_act"] = id;
           ViewData["nombre_act"] = modeloAct.detalleactividad(id).act_nombre;
           return View();

       }

       [Authorize(Roles = "Supervisor")]
       public ActionResult mediadeOperacionSuper(int act_id, DateTime desde, DateTime hasta)
       {

           ViewData["mediaOperacion"] = repModel.resultadoDeOperacion(act_id, desde, hasta);
           ViewData["actividad"] = modeloAct.detalleactividad(act_id).act_nombre;

           return View();

       }

       //EMPLEADO

       [Authorize(Roles = "Empleado")]
       public ActionResult IndexEmple()
       {
           return View();
       }

       [Authorize(Roles = "Empleado")]
       public ActionResult usuMesaayudaEmple()
       {
           // OBTENER USUARIO LOGUEADO
           string ideUser = User.Identity.Name;

           int id = modeloUsuario.ObtenerUser(ideUser).usu_id;

           var usu = repModel.detalleUsuario(id);

           ViewData["id"] = id;

           return View("usuMesaayudaEmple", usu);

       }

       [Authorize(Roles = "Empleado")]
       public ActionResult InformeMesaAyudaEmple(int id, DateTime desde, DateTime hasta)
       {

           var usu = repModel.detalleUsuario(id);

           ViewData["finicio"] = desde;
           ViewData["ffin"] = hasta;
           ViewData["id_user"] = usu.usu_id;

           ViewData["area"] = usu.areas.are_nombre;
           ViewData["promedio"] = repModel.promedioTiempos(id, desde, hasta);
           ViewData["cantidadCasos"] = repModel.cantidadCasos(id, desde, hasta);

           return View("InformeMesaAyudaEmple");

       }

       [Authorize(Roles = "Empleado")]
       public ActionResult casosPersonaEmple(int id, DateTime finicio, DateTime ffin)
       {


           var casosemple = repModel.numero(id, finicio, ffin);

           usuarios usu = repModel.detalleUsuario(id);
           

           ViewData["finicio"] = finicio;
           ViewData["ffin"] = ffin;
           ViewData["persona"] = usu.usu_id;

           return View(casosemple);

       }

       //PROCEDIMIENTOS POR PERSONA
       [Authorize(Roles = "Empleado")]
       public ActionResult proPersonaEmple()
       {
           // OBTENER USUARIO LOGUEADO
           string ideUser = User.Identity.Name;

           int id_user = modeloUsuario.ObtenerUser(ideUser).usu_id;

           var act = repModel.procXpersona(id_user);

           var usu = repModel.detalleUsuario(id_user);
           ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
           ViewData["id_user"] = id_user;


           return View("proPersonaEmple", act);


       }

       [Authorize(Roles = "Empleado")]
       public ActionResult actividadesProcedimientoEmple(int id_user, int id_pro)
       {

           var usu = repModel.detalleUsuario(id_user);
           var pro = modeloPro.detalleprocedimientos(id_pro);

           ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
           ViewData["procedimiento"] = pro.pro_codigoun;
           ViewData["id_user"] = id_user;
           ViewData["id_pro"] = id_pro;


           return View();

       }

       [Authorize(Roles = "Empleado")]
       public ActionResult mostrarProcedmientoEmple(int id_user, int id_pro, DateTime desde, DateTime hasta)
       {

           var usu = repModel.detalleUsuario(id_user);
           var pro = modeloPro.detalleprocedimientos(id_pro);

           ViewData["usuario"] = usu.usu_nombres + " " + usu.usu_apellidos;
           ViewData["procedimiento"] = pro.pro_codigoun;

           ViewData["cantidadTotal"] = repModel.cantTotalProPer(id_user, id_pro, desde, hasta);
           ViewData["cantidadPositivas"] = repModel.cantPosProPer(id_user, id_pro, desde, hasta);
           ViewData["cantidadNegitivas"] = repModel.cantNegProPer(id_user, id_pro, desde, hasta);

           return View();

       }

       [Authorize(Roles = "Empleado")]
       public ActionResult listarActividadesEmple()
       {
           // OBTENER USUARIO LOGUEADO
           string ideUser = User.Identity.Name;

           int id_user = modeloUsuario.ObtenerUser(ideUser).usu_id;

           var act = repModel.actividadOperacion(id_user);

           return View("listarActividadesEmple", act);

       }

       [Authorize(Roles = "Empleado")]
       public ActionResult mediaOperacionEmple(int id)
       {
           ViewData["id_act"] = id;
           ViewData["nombre_act"] = modeloAct.detalleactividad(id).act_nombre;
           return View();

       }

       [Authorize(Roles = "Empleado")]
       public ActionResult mediadeOperacionEmple(int act_id, DateTime desde, DateTime hasta)
       {

           ViewData["mediaOperacion"] = repModel.resultadoDeOperacion(act_id, desde, hasta);
           ViewData["actividad"] = modeloAct.detalleactividad(act_id).act_nombre;

           return View();

       }

       
        
    }
}
