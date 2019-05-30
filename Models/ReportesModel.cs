using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class ReportesModel
    {
        ConeccionDataContext db = new ConeccionDataContext();

        public List<usuarios> todosUsuario() {

            return db.usuarios.Where(a=>a.usu_activo == 1).ToList();
        
        }

        public List<areas> todasAreas() {

            return db.areas.Where(a=>a.are_id != 0).ToList();
        }
        
        
        // Mostrar determinado usuario.
        public usuarios detalleUsuario(int id){

            return db.usuarios.SingleOrDefault(u => u.usu_id == id);

        }

        public actividades_hd obtenerActividad(int id)
        {

            return db.actividades_hd.SingleOrDefault(a => a.ahd_id == id);

        }

        //PROMEDIO DE TIEMPOS POR PERSONA
        public IQueryable<actividades_hd> numero(int id, DateTime finicio, DateTime ffin)
        {

            return from act in db.actividades_hd
                    where act.usu_id == id
                    where act.est_id != 3
                    where act.ahd_fhpeticion.Date >= finicio
                    where act.ahd_fhpeticion.Date <= ffin
                    select act;  
        }

        public int cantidadCasos(int id, DateTime finicio, DateTime ffin)
        {

            int cantidad = numero(id, finicio, ffin).Count();

            return cantidad;

        }

        public float promedioTiempos(int id, DateTime finicio,DateTime ffin) {

            float suma = 0;

            int cantidad = cantidadCasos(id, finicio, ffin);

            if (cantidad == 0) {

                cantidad = 1;
            }            

            var tim = numero(id,finicio,ffin);

            if (tim.Sum(d=>d.ahd_duracion) == null)
            {

                suma = -1;

            }
            else
            {

                suma = (float)tim.Sum(d => d.ahd_duracion);
            
            }

            float promedio = suma / cantidad;

            return promedio;
        }

        // PROMEDIO DE CALIFICACIONES DE EVALUACION
        public IQueryable<evaluacion> evaluacionN(int id, DateTime finicio, DateTime ffinal)
        {
            var casos = from act in db.actividades_hd
                        where act.usu_id == id
                        where act.ahd_fhpeticion.Date >= finicio
                        where act.ahd_fhpeticion.Date <= ffinal
                        select act.ahd_id;


            return from eva in db.evaluacion
                   where casos.Contains(eva.ahd_id)
                   where eva.eva_calificacion != null
                   select eva;
        }

        public int cantidadCasosEvaluados(int id, DateTime finicio, DateTime ffinal)
        {

            int cantidad = evaluacionN(id,finicio,ffinal).Count();

            return cantidad;
        
        }

        public float promedioCalifacion(int id,DateTime finicio,DateTime ffinal) {

            int cantidad = cantidadCasosEvaluados(id,finicio,ffinal);

            if (cantidad == 0) {

                cantidad =  1;
            
            }

            var eva = evaluacionN(id,finicio,ffinal);

            float califi = 0;

            if (eva.Sum(c => c.eva_calificacion) == null)
            {

                califi = -1;

            }

            else {

                califi = (float) eva.Sum(c => c.eva_calificacion);
            }

            float promedio = califi / cantidad;

            

            return promedio;
        
        }


        //TIEMPOS PROMEDIO POR AREA

        private IQueryable<actividades_hd> actividad_area(int id, DateTime finicio, DateTime ffinal)
        {

            var usus = from usu in db.usuarios
                       where usu.are_id == id
                       where usu.usu_activo == 1
                       select usu.usu_id;

            return from act in db.actividades_hd
                   where usus.Contains(act.usu_id)
                   where act.est_id != 3
                   where act.ahd_duracion != null
                   where act.ahd_fhpeticion.Date >= finicio
                   where act.ahd_fhpeticion.Date <= ffinal
                   select act;

        }

        public int cantidadCasosArea(int id,DateTime finicio, DateTime ffinal){

            int cantidad = actividad_area(id,finicio,ffinal).Count();
            return cantidad;

        }

        public float promedioTiemposArea(int id,DateTime finicio, DateTime ffinal)
        {
            float suma = 0;

            int cantidad = cantidadCasosArea(id,finicio,ffinal);

            if (cantidad == 0) {

                cantidad = -1;

            
            }

            var act = actividad_area(id,finicio, ffinal);

            if (act.Sum(a => a.ahd_duracion) == null) 
            {

                suma = 1;
            }

            else{

                suma = (float)act.Sum(a=>a.ahd_duracion);
            
            }

            float promedio = suma / cantidad;

            return promedio;
        }


        // PROMEDIO DE CALIFICACIONES DE EVALUACION
        public IQueryable<evaluacion> evaluacionA(int id, DateTime finicio, DateTime ffinal)
        {
            var casos = from act in db.actividades_hd
                        where act.usuarios.are_id == id
                        where act.ahd_fhpeticion.Date >= finicio
                        where act.ahd_fhpeticion.Date <= ffinal
                        select act.ahd_id;


            return from eva in db.evaluacion
                   where casos.Contains(eva.ahd_id)
                   where eva.eva_calificacion != null
                   select eva;
        }

        public int cantidadCasosEvaluadosArea(int id, DateTime finicio, DateTime ffinal)
        {

            int cantidad = evaluacionA(id,finicio,ffinal).Count();

            return cantidad;

        }

        public float promedioCalifacionArea(int id, DateTime finicio, DateTime ffinal)
        {

            int cantidad = cantidadCasosEvaluadosArea(id,finicio,ffinal);

            if (cantidad == 0)
            {

                cantidad = 1;

            }

            var eva = evaluacionA(id,finicio,ffinal);

            float califi = 0;

            if (eva.Sum(c => c.eva_calificacion) == null)
            {

                califi = -1;

            }

            else
            {

                califi = (float)eva.Sum(c => c.eva_calificacion);
            }

            float promedio = califi / cantidad;



            return promedio;

        }


        //PROCEDIMIENTOS.

        public IQueryable<actividades_hd> casosProcedimientos(int id)
        {

            return from act in db.actividades_hd
                   where act.pro_id == id
                   where act.est_id != 3
                   select act;

        }

        public int cantidadCasosPro(int id)
        {

            int cantidad = casosProcedimientos(id).Count();
            return cantidad;

        }


        //PROCEDIMIENTOS POR PERSONA
        public IQueryable<actividades> procXpersona(int id) {

            var asignaciones = from asig in db.asignaciones
                               where asig.use_id == id
                               select asig.act_id;

                       return from act in db.actividades
                              where asignaciones.Contains(act.act_id)
                              select act;

        }

        private IQueryable<asignaciones> canXprocedimiento(int id_user,int id_pro,DateTime finicio,DateTime ffinal) {

            var activi = from act in db.actividades
                         where act.pro_id == id_pro
                         select act.act_id;


                  return from asig in db.asignaciones
                         where asig.use_id == id_user
                         where asig.asi_fechaterminar >= finicio
                         where asig.asi_fechaterminar <= ffinal
                         where activi.Contains(asig.act_id)
                         select asig;

        
        }

        public int cantTotalProPer(int id_user, int id_pro, DateTime finicio, DateTime ffinal)
        {

            int total = canXprocedimiento(id_user,id_pro,finicio,ffinal).Count();

            return total;
        
        }

        public int cantPosProPer(int id_user, int id_pro, DateTime finicio, DateTime ffinal)
        {

            int positivos = canXprocedimiento(id_user, id_pro, finicio, ffinal).Where(a => a.asi_fechaterminar >= (DateTime)a.asi_fhfin.Value.Date).Count();
            

            return positivos;

        }

        public int cantNegProPer(int id_user, int id_pro, DateTime finicio, DateTime ffinal)
        {

            int negativos = canXprocedimiento(id_user, id_pro, finicio, ffinal).Where(a => a.asi_fechaterminar < (DateTime)a.asi_fhfin.Value.Date).Count();
            int negativos2 = canXprocedimiento(id_user, id_pro, finicio, ffinal).Where(b => b.asi_fhfin == null).Where(f => f.asi_fechaterminar < DateTime.Now).Count();

            return negativos + negativos2;

        } 
    
        // REPORTE POR EL CAMPO OPERADOR

        public float resultadoDeOperacion(int id_act, DateTime finicio, DateTime ffinal)
        {
            var asigOpe = asignacionesOperacion(id_act, finicio, ffinal);

            
            int cant = asigOpe.Count();

            if (cant != 0)
            {
                float suma = (float)asigOpe.Select(o => o.asi_operador).Sum();

                return suma / cant;
            }
            else {

                return -1;
            }
            
        
        }

        private IQueryable<asignaciones> asignacionesOperacion(int id_act, DateTime finicio, DateTime ffinal)
        {
        
            return  from act in db.asignaciones
                            where act.act_id == id_act
                            where act.asi_operador != null
                            select act;
        }


        public IQueryable<actividades> actividadOperacion(int id_user) 
        {
            
             var asign = from asig in db.asignaciones
                        where asig.use_id == id_user
                        select asig.act_id;

             return from act in db.actividades
                         where asign.Contains(act.act_id)
                         select act;
        
        }
        

    }
}
