using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sigacun.Models
{
    public class ProcedimientoModel
    {
                     
        
        private ConeccionDataContext db = new ConeccionDataContext();

        public archivospro ruta(int id) {

            return db.archivospro.SingleOrDefault(a => a.pro_id == id);
        }

        public bool rutaPos(int id) {

            var archivo = from arc in db.archivospro
                          where arc.pro_id == id
                          select arc;


            int cantidad = archivo.Count();

            if (cantidad > 0)
            {

                return true;

            }
            else {

                return false;
            }

        
        }

     
        //Este método ayuda a recoger las areas que se relaciona con los procedimientos.
        public IQueryable<areas> listarareas()
        {
            return from area in db.areas
                   select area;
        }


        // Listar todos los procedimientos.
        public IQueryable<procedimiento> listarprocedimientos()
        {

            return db.procedimiento;

        }

        // Mostrar determinado procedimiento.
        public procedimiento detalleprocedimientos(int id)
        {

            return db.procedimiento.SingleOrDefault(a => a.pro_id == id);

        }

        public void crearprocedimiento(procedimiento pro)
        {
            db.procedimiento.InsertOnSubmit(pro);

        }

        public void crearArchivo(archivospro arc) {

            db.archivospro.InsertOnSubmit(arc);
        
        }

        // Eliminar determinada procedimiento.
        public void eliminarprocedimiento(procedimiento pro)
        {

            db.procedimiento.DeleteOnSubmit(pro);
        }

        public archivospro detalleArchivo(int id){

            return db.archivospro.SingleOrDefault(a => a.arp_id == id);
        }
 

        // Eliminar determinada archivo de procedimiento.
        public void eliminarArchivo(archivospro arc)
        {

            db.archivospro.DeleteOnSubmit(arc);
        }

        public void guardar()
        {

            db.SubmitChanges();

        }
    }
}
