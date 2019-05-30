using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class RepeticionesModel
    {

        private ConeccionDataContext db = new ConeccionDataContext();


        // Listar todos las asignaciones de una actividad.
        public IQueryable<asignaciones> listarasignaciones()
        {
            var repe = from rep in db.repeticiones
                      select rep.asi_id;


            return from asig in db.asignaciones
                   where repe.Contains(asig.asi_id)
                   orderby asig.asi_fechaterminar
                   select asig;

        }
    }
}
