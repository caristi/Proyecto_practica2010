using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class PreguntasModel
    {

        ConeccionDataContext db = new ConeccionDataContext();

        // Listar todas las preguntas.
        public IQueryable<evapreguntas> listarpreguntas()
        {

            return db.evapreguntas;

        }

        public void crearPregunta(evapreguntas pre)
        {

            db.evapreguntas.InsertOnSubmit(pre);
        
        }

        public void eliminarPregunta(evapreguntas pre) {

            db.evapreguntas.DeleteOnSubmit(pre);
        
        }

        public void guardar() {

            db.SubmitChanges();
        
        }

        // Mostrar determinado pregunta.
        public evapreguntas detalleCargo(int id)
        {

            return db.evapreguntas.SingleOrDefault(e => e.epr_id == id);

        }


    }
}
