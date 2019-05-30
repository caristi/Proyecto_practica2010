using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class RolModel
    {
        private ConeccionDataContext db = new ConeccionDataContext();

        // Listar todas los roles.
        public IQueryable<roles> listarRoles()
        {

            return db.roles;

        }

    }
}
