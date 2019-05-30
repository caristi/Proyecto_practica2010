using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sigacun.Models;

namespace Sigacun.Helpers
{
    public class CalcularTiempos 
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("Es-Co");

        FestivosModel festivosModel = new FestivosModel();

        TimeSpan hiniciomana = new TimeSpan(08, 00, 00); //Hora 08:00 am
        TimeSpan hfinmanana = new TimeSpan(12, 00, 00); //Hora 12:00 pm
        TimeSpan hiniciotarde = new TimeSpan(14, 00, 00); //Hora 02:00 pm
        TimeSpan hfintarde = new TimeSpan(18, 00, 00); //Hora 06:00 pm


        private DateTime colocarinicio(DateTime finicio)
        {
            finicio = festivoInicio(finicio);
            finicio = festivosInicioLunes(finicio);
            finicio = festivosInicioFijos(finicio);

            // SI EL DÍA INICIO ES SADABO COLOCAR EL FECHA Y HORA LUNES A LAS 8:00 AM
            if ((ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Saturday)) == 0))
            {
                finicio = finicio.AddDays(2);
                finicio = new DateTime(finicio.Year,finicio.Month,finicio.Day,7, 59, 58);
            }

            // SI EL DÍA INICIO ES DOMINGO COLOCAR EL FECHA Y HORA LUNES A LAS 8:00 AM
            if ((ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Sunday)) == 0))
            {
                finicio = finicio.AddDays(1);
                finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, 7, 59, 58);
            }

            finicio = festivoInicio(finicio);
            finicio = festivosInicioLunes(finicio);
            finicio = festivosInicioFijos(finicio);

            return finicio;
        }

        private DateTime colocarfin(DateTime ffinal)
        {

            ffinal = festivoFin(ffinal);
            ffinal = festivosFinLunes(ffinal);
            ffinal = festivosFinFijos(ffinal);
            
            // SI EL DÍA FINAL ES SADABO COLOCAR EL FECHA Y HORA LUNES A LAS 8:00 AM
            if ((ci.DateTimeFormat.GetDayName(ffinal.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Saturday)) == 0))
            {
                ffinal = ffinal.AddDays(2);
                ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, 7, 59, 59);
            }

            // SI EL DÍA FINAL ES DOMINGO COLOCAR EL FECHA Y HORA LUNES A LAS 8:00 AM
            if ((ci.DateTimeFormat.GetDayName(ffinal.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Sunday)) == 0))
            {
                ffinal = ffinal.AddDays(1);
                ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, 7, 59, 59);
            }

            ffinal = festivoFin(ffinal);
            ffinal = festivosFinLunes(ffinal);
            ffinal = festivosFinFijos(ffinal);

            return ffinal;
        }


        private int calculardiasfines(DateTime finicio, DateTime ffinal)
        {

            int incremento = 1;
            int totalnohabiles = 0;

            while (finicio.Date != ffinal.Date)
            {
                finicio = finicio.AddDays(incremento);

                if ((ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Sunday)) == 0)
                    || (ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Saturday)) == 0))
                {
                    totalnohabiles += incremento;
                }
            }

            return totalnohabiles;

        }

        private DateTime festivoInicio(DateTime finicio) {


            int fini = festivosModel.listarFestivos(finicio);

            while (fini == -2) {

                finicio = finicio.AddDays(1);
                finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, 7, 59, 58);
                fini = festivosModel.listarFestivos(finicio);
            }

            return finicio;
        
        }

        private DateTime festivoFin(DateTime ffinal) {

           int ffin = festivosModel.listarFestivos(ffinal);
           
           while(ffin == -2 ){

             ffinal = ffinal.AddDays(1);
             ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, 7, 59, 59);
             ffin = festivosModel.listarFestivos(ffinal);
           
           }

            return ffinal;
        
        }

        private DateTime festivosInicioLunes(DateTime finicio)
        {

            int festivosLunes = festivosModel.CalcularFestivosLunes(finicio);

            if (festivosLunes == -4)
            {

                finicio = finicio.AddDays(1);
                finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, 7, 59, 58);

            }

            return finicio;

        }

        private DateTime festivosFinLunes(DateTime ffinal) {

            int festivosLunes = festivosModel.CalcularFestivosLunes(ffinal);

            if (festivosLunes == -4) {

                ffinal = ffinal.AddDays(1);
                ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, 7, 59, 59);
            
            }

            return ffinal;

        }

        private DateTime festivosInicioFijos(DateTime finicio)
        {

            int festivos = festivosModel.listarFestivosVariados(finicio);

            if (festivos == -2)
            {

                finicio = finicio.AddDays(1);
                finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, 7, 59, 58);

            }

            return finicio;

        }

        private DateTime festivosFinFijos(DateTime ffinal)
        {

            int festivos = festivosModel.listarFestivosVariados(ffinal);

            if (festivos == -2)
            {

                ffinal = ffinal.AddDays(1);
                ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, 7, 59, 58);

            }

            return ffinal;

        }

        private int calcularFestivos(DateTime finicio, DateTime ffinal)
        {

            int cantidad = 0;
            int incremento = 1;

            while (finicio.Date != ffinal.Date)
            {
                finicio = finicio.AddDays(incremento);

                int festivos = festivosModel.listarFestivos(finicio);
                int festivosLunes = festivosModel.CalcularFestivosLunes(finicio);
                int festivosVaridados = festivosModel.listarFestivosVariados(finicio);

                if (festivos == -2 &&
                    (ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Saturday))!= 0) &&
                     (ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Sunday)) != 0))
                {

                    cantidad++;
                }

               else 
                    if (festivosLunes == -4) {

                    cantidad++;
                
                    }

               else
                    if (festivosVaridados == -2 &&
                    (ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Saturday)) != 0) &&
                     (ci.DateTimeFormat.GetDayName(finicio.DayOfWeek).CompareTo(ci.DateTimeFormat.GetDayName(DayOfWeek.Sunday)) != 0))
               {

                   cantidad++;
               }

                   
            }

            return cantidad;
        }

        
        private int calcularasHoras(DateTime finicio, DateTime ffinal)
        {


            int incrementoh = 1;
            int totalhoras = 0;         

            
           if (finicio.Hour == ffinal.Hour && finicio.Minute > ffinal.Minute)
           {

               TimeSpan ts = ffinal.Subtract(finicio);
                    
               int dia = ts.Days;


                  if (finicio.Hour >= hiniciomana.Hours && finicio.Hour < hfinmanana.Hours)
                  {
                      finicio = finicio.AddHours(incrementoh);
                  }


                  if (finicio.Hour >= hiniciotarde.Hours && finicio.Hour < hfintarde.Hours)
                  {
                      finicio = finicio.AddHours(incrementoh);
                  }
            }
            
            while (finicio.Hour != ffinal.Hour)
            {
                finicio = finicio.AddHours(incrementoh);

                // Si piden un caso despues de las 6:00 pm y antes de las 8:00 am
                if (finicio.Hour < hiniciomana.Hours || finicio.Hour >= hfintarde.Hours)
                {
                    totalhoras += incrementoh;
                }

                // Si piden un caso al medio dia de 12:00 pm a 1:59 pm 
                if (finicio.Hour >= hfinmanana.Hours && finicio.Hour < hiniciotarde.Hours)
                {
                    totalhoras += incrementoh;
                }
            }

            return totalhoras;
        }

        public float calcularTiempo(DateTime finicio,DateTime ffinal)
        {

                finicio = colocarinicio(finicio);
                ffinal = colocarfin(ffinal);

                int totalnohabiles = calculardiasfines(finicio,ffinal);
    
                int totalhoras = calcularasHoras(finicio,ffinal);
            
                int diasFestivos = calcularFestivos(finicio, ffinal);


                // SI LA HORA INICIO ES MENOR DE LAS 8:00 AM. SE CONVIERTE LOS MINUTOS A 00
                // MAÑANA
                if (finicio.Hour < hiniciomana.Hours)
                {
                    finicio = finicio.AddHours(1);
                    finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, finicio.Hour, 00, 00);
                }

                // SI LA HORA INICIO ES MAYOR DE LAS 12:00 PM SE CONVIERTE LOS MINUTOS EN 00
                // MEDIO DIA
                if (finicio.Hour >= hfinmanana.Hours && finicio.Hour < hiniciotarde.Hours)
                {
                    finicio = finicio.AddHours(1);
                    finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, finicio.Hour, 00, 00);
                }

                // SI LA HORA INICIO ES MAYOR DE LAS 6:00 PM SE CONVIERTE LOS MINITUOS EN 00
                // NOCHE
                if (finicio.Hour >= hfintarde.Hours)
                {
                    finicio = finicio.AddHours(1);
                    finicio = new DateTime(finicio.Year, finicio.Month, finicio.Day, finicio.Hour, 00, 00);
                }

                // SI LA HORA FINAL ES MENOR A LAS 8:00 AM SE CONVIERTE A LAS 8:00 AM
                // MAÑANA
                if (ffinal.Hour < hiniciomana.Hours)
                {
                    ffinal = ffinal.AddHours(1);
                    ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, ffinal.Hour, 00, 00);
                }

                // SI LA HORA FINAL ES MAYOR DE LAS 12:00 SE CONVIERTE LOS MINUTOS EN 00
                // MEDIO DIA
                if (ffinal.Hour >= hfinmanana.Hours && ffinal.Hour < hiniciotarde.Hours)
                {
                    ffinal = ffinal.AddHours(1);
                    ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, ffinal.Hour, 00, 00);
                }

                // SI LA HORA FINAL ES MAYOR DE LAS 6:OO PM SE CONVIERTE LOS MINUTOS EN 00
                // NOCHE
                if (ffinal.Hour >= hfintarde.Hours)
                {
                    ffinal = ffinal.AddHours(1);
                    ffinal = new DateTime(ffinal.Year, ffinal.Month, ffinal.Day, ffinal.Hour, 00, 00);
                }

                TimeSpan ts = ffinal.Subtract(finicio);

                int dias = ts.Days;
                int horas = ts.Hours;
                float minuto = ts.Minutes;

                int tdias = (dias - totalnohabiles - diasFestivos) * 8;

                if (tdias < 0) {

                    tdias = 0;

                }
                int thoras = horas - totalhoras;
                float tminutos = minuto / 60;

                float totalh = tdias + thoras + tminutos;

                return totalh;
        }



    }
}