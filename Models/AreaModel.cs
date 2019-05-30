using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class AreaModel
    {
        private ConeccionDataContext db = new ConeccionDataContext();


        // Listar todas las areas.
        public IQueryable<areas> listarAreas() {

            return db.areas;

        }

        // Mostrar determinada area.
        public areas detalleArea(int id) {

            return db.areas.SingleOrDefault(a => a.are_id == id);

        }

        // Registrar nueva area.
        public void crearArea(areas area) {

            db.areas.InsertOnSubmit(area);
        
        }

        // Eliminar determinada area.
        public void eliminarArea(areas area) {

            db.areas.DeleteOnSubmit(area);
        }


        public void guardar()
        {

            db.SubmitChanges();

        }

    }
}
