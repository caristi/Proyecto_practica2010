using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sigacun.Models;
using Sigacun.Membership;

namespace Sigacun.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioModel modelUsuario = new UsuarioModel();
        CargoModel modelCargo = new CargoModel();
        AreaModel modelArea = new AreaModel();
        RolModel modelRol = new RolModel();
        ConeccionMysql modelMysql = new ConeccionMysql();

        SimpleMembershipProvider provider = new SimpleMembershipProvider();
        SimpleRoleProvider providerRoles = new SimpleRoleProvider();


        [Authorize(Roles = "Admin")]
        public ActionResult inicioUsuario()
        {
            var usuarios = modelUsuario.listarUsuario();

            return View("inicioUsuario",usuarios);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RegistrarUsuario() 
        {

            usuarios usu = new usuarios();

            try {

                UpdateModel(usu);
                usu.usu_contrasena = provider.EncryptPassword(usu.usu_contrasena);
                modelUsuario.crearUsuario(usu);
                modelUsuario.guardar();

                
                return RedirectToAction("inicioUsuario");

            
            }
            catch{

                ViewData["cargo"] = new SelectList(modelCargo.listarcargo().AsEnumerable(), "car_id", "car_nombre");
                ViewData["area"] = new SelectList(modelArea.listarAreas().AsEnumerable(), "are_id", "are_nombre");
                ViewData["roles"] = new SelectList(modelRol.listarRoles().AsEnumerable(), "rol_id", "rol_descripcion");
                ViewData["usu_id"] = modelMysql.listarUsuarios();

                return View(usu);
            
            }
          }

        [Authorize(Roles = "Admin")]
        public ActionResult editarUsuario(int id) {


            usuarios usu = modelUsuario.detalleUsuario(id);
                
            ViewData["cargo"] = new SelectList(modelCargo.listarcargo().AsEnumerable(), "car_id", "car_nombre");
            ViewData["area"] = new SelectList(modelArea.listarAreas().AsEnumerable(), "are_id", "are_nombre");
            ViewData["roles"] = new SelectList(modelRol.listarRoles().AsEnumerable(), "rol_id", "rol_descripcion");
            ViewData["usu_id"] = modelMysql.listarUsuariosEditar(id);

            return View("editarUsuario", usu);
        
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarUsuario(int id, FormCollection formValues){
        
            usuarios usu = modelUsuario.detalleUsuario(id);

            try{

                UpdateModel(usu);
                modelUsuario.guardar();

               return RedirectToAction("inicioUsuario");
            
            }
            catch{

                ViewData["cargo"] = new SelectList(modelCargo.listarcargo().AsEnumerable(), "car_id", "car_nombre");
                ViewData["area"] = new SelectList(modelArea.listarAreas().AsEnumerable(), "are_id", "are_nombre");
                ViewData["roles"] = new SelectList(modelRol.listarRoles().AsEnumerable(), "rol_id", "rol_descripcion");

                ViewData["usu_id"] = modelMysql.listarUsuariosEditar(id);
                return View(usu);
            }
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {

            usuarios usu = modelUsuario.detalleUsuario(id);

            modelUsuario.eliminar(usu);
            modelUsuario.guardar();

            return View();

        }

        [Authorize(Roles = "Admin")]
        public ActionResult inicioCambiarContra(int id) {

            usuarios usu = modelUsuario.detalleUsuario(id);

            ViewData["usuario"] = usu.usu_nombres;
            ViewData["id"] = id;

            return View();
        
        }
        

        [Authorize(Roles = "Admin")]
        public ActionResult cambiarContrasena(int id,string nuevaContr) {

            usuarios usu = modelUsuario.detalleUsuario(id);

            string username = usu.usu_username;

            try
            {
                UpdateModel(usu);
                usu.usu_contrasena = provider.EncryptPassword(nuevaContr);
                modelUsuario.guardar();

                return RedirectToAction("inicioUsuario");

            }
            catch {


                return View(usu);
            }
        
        }


        //INICIAR SESION 
        public ActionResult IniciarSesion() {


            return View();
        
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult IniciarSesion(string userName, string password, string returnUrl)
        {
            password = provider.EncryptPassword(password);

            if ( !ValidateLogOn(userName, password))
            {
                return View();
            }
          

            var user = modelUsuario.ObtenerUser(userName);

            FormsAuthentication.SetAuthCookie(user.usu_username, false);
            if (!String.IsNullOrEmpty(returnUrl) && returnUrl != "/")
            {
                return Redirect(returnUrl);
            }
            else
            {
                IQueryable<Sigacun.Models.UsuarioModel.RoleViewModel> rol = modelUsuario.GetRolesForUser(userName);

                string a = rol.First().RoleName;

                if(a.CompareTo("Admin") == 0){
                
                    return RedirectToAction("Index", "Home");
                
                }
                if (a.CompareTo("Supervisor") == 0)
                {


                    return RedirectToAction("IndexSupervisor", "Home");

                }

                if (a.CompareTo("Empleado") == 0) {

                    return RedirectToAction("IndexEmple", "Home");
                
                }


                return View();
            }
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "Debe especificar un nombre de usuario.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Debe especificar una contraseña.");
            }
            if (!provider.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "El nombre de usuario o contraseña son incorrectas.");
            }

            return ModelState.IsValid;
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("IniciarSesion", "Usuario");
        }
        


    }
}
