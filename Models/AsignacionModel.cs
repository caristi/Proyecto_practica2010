using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class AsignacionModel
    {


        private ConeccionDataContext db = new ConeccionDataContext();

        //Listar todos los usuarios.
        public IQueryable<usuarios> listarUsuario() {

            return db.usuarios;
            
        }


        // Listar todos las asignaciones de una actividad.
        public IQueryable<asignaciones> listarasignaciones()
        {

            return from asig in db.asignaciones
                   orderby asig.asi_fechaterminar
                   select asig;
                

        }

        public IQueryable<asignaciones> listarTodasAsignaciones() 
        {

            return from asig in db.asignaciones
                   where asig.est_id == 2 || asig.est_id == 3 ||
                         asig.asi_comentario == null || asig.asi_comsuper == null
                   select asig;
        
        }

        // Mostrar determinada asignación.
        public asignaciones detalleasignacion(int id)
        {

            return db.asignaciones.SingleOrDefault(a => a.asi_id == id);

        }

        // Registrar nueva asignación de una actividad.
        public void crearasignacion(asignaciones asig)
        {

            db.asignaciones.InsertOnSubmit(asig);

        }

        // Registrar repeticion de una actividad.
        public void crearRepeticon(repeticiones rep)
        {

            db.repeticiones.InsertOnSubmit(rep);

        }

        // Eliminar determinada asignación.
        public void eliminarasignacion(asignaciones asig)
        {

            db.asignaciones.DeleteOnSubmit(asig);
        }


        // ARCHIVO ASIGNACION

        // Registrar archivo de asigacion.
        public void crearArchivo(archivosasig arc)
        {

            db.archivosasig.InsertOnSubmit(arc);

        }

        public archivosasig ruta(int id)
        {

            return db.archivosasig.SingleOrDefault(a => a.asi_id == id);
        }

        public bool rutaPos(int id)
        {

            var archivo = from arc in db.archivosasig
                          where arc.asi_id == id
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

        public archivosasig detalleArchivo(int id)
        {

            return db.archivosasig.SingleOrDefault(a => a.ars_id == id);
        }


        // Eliminar determinada archivo de actividad.
        public void eliminarArchivo(archivosasig arc)
        {

            db.archivosasig.DeleteOnSubmit(arc);
        }

        public void guardar()
        {

            db.SubmitChanges();

        }
    }
}
