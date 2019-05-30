using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sigacun
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //Paginación de asignación-Empleado
            routes.MapRoute(
                    "asige",
                    "Asignacion/Page/Emple/{page}",
                    new { controller = "Asignacion", action = "inicioasigEmple" }
            );


            //Paginación de actividad-Empleado
            routes.MapRoute(
                    "acte",
                    "Actividad/Page/Emple/{page}",
                    new { controller = "Actividad", action = "inicioactEmple" }
            );

            //Paginación en actividad_hd-Empleado
            routes.MapRoute(
                    "ache",
                    "ActividadHd/Page/Emple/{page}",
                    new { controller = "ActividadHd", action = "inicioMesaAyudaEmple" }
            );

            //Paginación de asignación-Supervisor
            routes.MapRoute(
                    "asigs",
                    "Asignacion/Page/Super/{page}",
                    new { controller = "Asignacion", action = "inicioasigSuper" }
            );


            //Paginación de actividad-Supervisor
            routes.MapRoute(
                    "acts",
                    "Actividad/Page/Super/{page}",
                    new { controller = "Actividad", action = "inicioactSuper" }
            );

            //Paginación en actividad_hd-Supervisor
            routes.MapRoute(
                    "achs",
                    "ActividadHd/Page/Super/{page}",
                    new { controller = "ActividadHd", action = "inicioMesaAyudaSupervisor" }
            );

            //Paginación en Reportes casos
            routes.MapRoute(
                    "casos",
                    "Home/Page/{page}/{id}",
                    new { controller = "Home", action = "casosPersona" }
            );
            
            
            //Paginación en actividad_hd
            routes.MapRoute(
                    "ach",
                    "ActividadHd/Page/{page}",
                    new { controller = "ActividadHd", action = "inicioMesaAyuda" }
            );
            
            //Paginación en procedimiento
            routes.MapRoute(
                    "pro",
                    "Procedimiento/Page/{page}",
                    new { controller = "Procedimiento", action = "iniciopro" }
            );

            //Paginación de actividad
            routes.MapRoute(
                    "act",
                    "Actividad/Page/{page}",
                    new { controller = "Actividad", action = "inicioact" }
            );

            //Paginación de asignación
            routes.MapRoute(
                    "asig",
                    "Asignacion/Page/{page}",
                    new { controller = "Asignacion", action = "inicioasig" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}