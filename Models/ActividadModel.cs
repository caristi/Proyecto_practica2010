using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class ActividadModel
    {
        private ConeccionDataContext db = new ConeccionDataContext();


        //Listar todos los procediemientos.
        public IQueryable<procedimiento> listarprocedmientos() {

            return from pro in db.procedimiento
                   select pro;
        
        }


        // Listar todos las actividades.
        public IQueryable<actividades> listaractividades()
        {
            
            return db.actividades;

        }

        // Mostrar determinado actividad.
        public actividades detalleactividad(int id)
        {

            return db.actividades.SingleOrDefault(a => a.act_id == id);

        }

        // Registrar nueva actividad.
        public void crearactividad(actividades act)
        {

            db.actividades.InsertOnSubmit(act);

        }

        // Registrar archivo de actividad.
        public void crearArchivo(archivosact arc)
        {

            db.archivosact.InsertOnSubmit(arc);

        }

        // Eliminar determinada actividad.
        public void eliminaractividad(actividades act)
        {

            db.actividades.DeleteOnSubmit(act);
        }

        public void guardar()
        {

            db.SubmitChanges();

        }

        //ARCHIVO ACTIVIDAD

        public archivosact ruta(int id)
        {

            return db.archivosact.SingleOrDefault(a => a.act_id == id);
        }

        public bool rutaPos(int id)
        {

            var archivo = from arc in db.archivosact
                          where arc.act_id == id
                          select arc;


            int cantidad = archivo.Count();

            if (cantidad > 0)
            {

                return true;

            }
            else
            {

                return false;
            }

        }

        public archivosact detalleArchivo(int id)
        {

            return db.archivosact.SingleOrDefault(a => a.ara_id == id);
        }


        // Eliminar determinada archivo de actividad.
        public void eliminarArchivo(archivosact arc)
        {

            db.archivosact.DeleteOnSubmit(arc);
        }

    }
}
