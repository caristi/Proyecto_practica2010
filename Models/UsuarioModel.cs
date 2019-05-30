using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigacun.Models
{
    public class UsuarioModel
    {


        ConeccionDataContext db = new ConeccionDataContext();


        public IQueryable<usuarios> listarUsuario()
        {

            return db.usuarios;
        }

        public usuarios detalleUsuario(int id)
        {

            return db.usuarios.SingleOrDefault(u => u.usu_id == id);
        }

        public void crearUsuario(usuarios usu) 
        {

            db.usuarios.InsertOnSubmit(usu);

        }

        public void eliminar(usuarios usu) {

            db.usuarios.DeleteOnSubmit(usu);
        
        }

        public usuarios ObtenerUser(string username) {

            return db.usuarios.SingleOrDefault(u=>u.usu_username == username);
        
        }

        public IQueryable<RoleViewModel> GetRolesForUser(string username)
        {

            var result = (
                         from role in db.roles
                         join user in db.usuarios on role.rol_id equals user.rol_id
                         where user.usu_username == username
                         select new RoleViewModel
                         {
                             RoleName = role.rol_descripcion
                         });

            return result;
        }

        public class RoleViewModel
        {
            public string RoleName { get; set; }
        }


        public void guardar()
        {

            db.SubmitChanges();
        }

       

    }
}
