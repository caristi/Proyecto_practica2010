using System;
using System.ComponentModel.DataAnnotations;

namespace Sigacun.Models
{
    public class Cargos
    {
        private ConeccionDataContext db = new ConeccionDataContext();

       

        [Required(ErrorMessage = "Nombre es requerido.")]
        public string  car_nombre { get; set; }

    }
}
