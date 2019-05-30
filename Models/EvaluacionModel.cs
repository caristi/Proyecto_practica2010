using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class EvaluacionModel
    {

        ConeccionDataContext db = new ConeccionDataContext();

        // Mostrar determinada evaluacion.
        public List<evaluacion> detalleEvaluacion(int id)
        {
            return db.evaluacion.Where(e=> e.ahd_id == id).ToList();
        }

        public actividades_hd detalle(int id)
        {
          return db.actividades_hd.SingleOrDefault(a => a.ahd_id == id);
        }

        public bool evaluacionCali(int id)
        {
            var evalu = from eva in db.evaluacion
                        where eva.ahd_id == id
                        select eva;

            int cant = evalu.Count();

            if (cant > 0){
                return false;
            }

            else {

                return true;
            }
        }
    }
}
