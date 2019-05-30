using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class PrioridadModel
    {
        private ConeccionDataContext db = new ConeccionDataContext();


        // Listar todas las prioridad.
        public IQueryable<prioridad> listarprioridad()
        {

            return db.prioridad;

        }

        // Mostrar determinada prioridad.
        public prioridad detallePrioridad(int id)
        {

            return db.prioridad.SingleOrDefault(p => p.pri_id == id);

        }

        // Registrar nueva prioridad.
        public void crearPrioridad(prioridad pri)
        {

            db.prioridad.InsertOnSubmit(pri);

        }

        // Eliminar determinada prioridad.
        public void eliminarPrioridad(prioridad pri)
        {

            db.prioridad.DeleteOnSubmit(pri);
        }


        public void guardar()
        {

            db.SubmitChanges();

        }


    }
}
