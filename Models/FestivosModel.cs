using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class FestivosModel
    {

        private ConeccionDataContext db = new ConeccionDataContext();

        public IQueryable<fechasfestivo> listarFestivos() {

            return db.fechasfestivo;
        
        }

        public fechasfestivo detalleFestivo(int id)
        {

            return db.fechasfestivo.SingleOrDefault(f => f.fec_id == id);

        }

        // Registrar festivo o dia no laboral.
        public void crearFestivo(fechasfestivo fes)
        {
            db.fechasfestivo.InsertOnSubmit(fes);

        }

        public void eliminar(fechasfestivo fes) 
        {

            db.fechasfestivo.DeleteOnSubmit(fes);
        }


        public void guardar()
        {
            db.SubmitChanges();

        }

        public int listarFestivos(DateTime fecha)
        {
            int cantidad;

            var resul = db.fechasfestivo.Where(t=>t.fec_tipo == 1).Where(f=>f.fec_dia.Month == fecha.Month).Where(d=>d.fec_dia.Day == fecha.Day);

            int cant= resul.Count();

            if (cant == 0)
            {

                cantidad = -1;
            }

            else {

                cantidad = -2;
            
            }

            return cantidad;
        }

        private DateTime convertirFestivosLunes(DateTime fecha)
        {

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("Es-Co");


                if ((ci.DateTimeFormat.GetDayName(fecha.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Tuesday)) == 0)) {

                    return fecha = fecha.AddDays(6);
                }

                if ((ci.DateTimeFormat.GetDayName(fecha.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Wednesday)) == 0))
                {

                    return fecha = fecha.AddDays(5);
                }

                if ((ci.DateTimeFormat.GetDayName(fecha.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Thursday)) == 0))
                {

                    return fecha = fecha.AddDays(4);
                }

                if ((ci.DateTimeFormat.GetDayName(fecha.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Friday)) == 0))
                {

                    return fecha = fecha.AddDays(3);
                }
                if ((ci.DateTimeFormat.GetDayName(fecha.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Saturday)) == 0))
                {

                    return fecha = fecha.AddDays(2);
                }
                if ((ci.DateTimeFormat.GetDayName(fecha.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Sunday)) == 0))
                {

                    return fecha = fecha.AddDays(1);
                }

                else
                {

                    return fecha = fecha.AddDays(0);
                }
        
        }


        public int CalcularFestivosLunes(DateTime fecha) {

            int cantidad = 0;

            IEnumerable<DateTime> resul = from fe in db.fechasfestivo
                                          where fe.fec_tipo == 2
                                          select fe.fec_dia;

            DateTime resulFecha;

            foreach(DateTime fes in resul){

             resulFecha = convertirFestivosLunes(fes);

             if (resulFecha.Month == fecha.Month && resulFecha.Day == fecha.Day)
             {
              cantidad = -4;
             }
            
            }

            return cantidad;
        }

        public int listarFestivosVariados(DateTime fecha)
        {
            int cantidad;

            var resul = db.fechasfestivo.Where(t => t.fec_tipo == 3).Where(f => f.fec_dia == fecha);

            int cant = resul.Count();

            if (cant == 0)
            {

                cantidad = -1;
            }

            else
            {

                cantidad = -2;

            }

            return cantidad;
        }
        
    }
}
