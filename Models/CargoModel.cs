using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sigacun.Models
{

    public class CargoModel
    {


        private ConeccionDataContext db = new ConeccionDataContext();


        // Listar todas los cargos.
        public IQueryable<cargos> listarcargo()
        {

            return db.cargos;

        }

        // Mostrar determinada cargo.
        public cargos detalleCargo(int id)
        {

            return db.cargos.SingleOrDefault(c => c.car_id == id);

        }

        // Registrar nuevo cargo.
        public void crearCargo(cargos car)
        {

            db.cargos.InsertOnSubmit(car);

        }

        // Eliminar determinado cargo.
        public void eliminarCargo(cargos car)
        {

            db.cargos.DeleteOnSubmit(car);
        }


        public void guardar()
        {

            db.SubmitChanges();

        }

    }
}
