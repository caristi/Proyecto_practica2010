using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class ActividadHdModel
    {

        private ConeccionDataContext db = new ConeccionDataContext();


        //Listar todos los actividades de mesa de ayuda.
        public IQueryable<actividades_hd> listarActividad() {

            return from ach in db.actividades_hd
                   select ach;

        }

        public actividades_hd obtenerActividad(int id) {

            return db.actividades_hd.SingleOrDefault(a => a.ahd_id == id);

        }

        public actividades_hd detalleActividad(int id) {

            return db.actividades_hd.SingleOrDefault(a=>a.ahd_hd_numsolicitud == id);
        
        }


        public void guardar() {

            db.SubmitChanges();

        }

        // ESTO ES UNA PRUBA
        public IQueryable<actividades_hd> CalcularTodos()
        {

            return from fe in db.actividades_hd
                   select fe;
        }

        public IQueryable<actividades_hd> todosActividad() {


            return from act in db.actividades_hd
                   where act.est_id == 2 || act.est_id == 3 || act.est_id == 4 
                   || act.ahd_fhcomsuper == null || act.ahd_fhcomentario == null
                   select act;
        
        }
    }
}
