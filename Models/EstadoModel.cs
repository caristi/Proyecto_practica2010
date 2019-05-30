using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class EstadoModel
    {

        private ConeccionDataContext db = new ConeccionDataContext();


        // Listar todos los estados.
        public IQueryable<estados> listarEstados()
        {

            return db.estados;

        }

        // Mostrar determinado estado.
        public estados detalleEstado(int id)
        {

            return db.estados.SingleOrDefault(e => e.est_id == id);

        }

        // Registrar nuevo estado.
        public void crearEstado(estados estado)
        {

            db.estados.InsertOnSubmit(estado);

        }

        // Eliminar determinado Estado.
        public void eliminarEstado(estados estado)
        {

            db.estados.DeleteOnSubmit(estado);
        }

        public void guardar()
        {

            db.SubmitChanges();

        }


    }
}
