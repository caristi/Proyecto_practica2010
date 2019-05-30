using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sigacun.Models;

namespace Sigacun.Helpers
{
    public class Repeticion
    {

        AsignacionModel modelAsig = new AsignacionModel();


        public void agregarRepeticio(asignaciones asig, repeticiones rep)
        {
            int incrementar = (int)rep.rep_cantidad;

            DateTime finicio = (DateTime)rep.rep_fechainicio;
            DateTime ffin = (DateTime)rep.rep_fechafin;

            while(finicio.Date != ffin.Date){

                UpdateModel(asig);

                asig.asi_fechaterminar = finicio;

                modelAsig.crearasignacion(asig);
                modelAsig.guardar();
                finicio = finicio.AddDays(incrementar);
            }   
        
        }

        private void UpdateModel(asignaciones asig)
        {
            throw new NotImplementedException();
        }

    }
}
