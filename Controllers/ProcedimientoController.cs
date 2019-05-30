using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigacun.Models;
using Sigacun.Helpers;
using System.IO;


namespace Sigacun.Controllers
{
    public class ProcedimientoController : Controller
    {
        ProcedimientoModel modeloPro = new ProcedimientoModel();

        //Mostrar todas los procedimiento.
        [Authorize(Roles = "Admin")]
        public ActionResult iniciopro(int? page)
        {
            const int pageSize = 10;

            var pro = modeloPro.listarprocedimientos();
            var paginaPro = new PaginatedList<procedimiento>(pro,
                                                             page ?? 0,
                                                             pageSize);
            return View(paginaPro);

        }

        //Mostrar determinada procedimiento.
        [Authorize(Roles = "Admin")]
        public ActionResult detalle(int id)
        {

            procedimiento pro = modeloPro.detalleprocedimientos(id);

            return View("detalle", pro);

        }

        //Inicio registro procedimiento
        [Authorize(Roles = "Admin")]
        public ActionResult nuevopro()
        {

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["areas"] = new SelectList(modeloPro.listarareas().AsEnumerable(), "are_id", "are_nombre");

            return View();

        }

        //Registar procedimiento.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload()
        {

                procedimiento pro = new procedimiento();
                archivospro arc = new archivospro();

                string upload_dir = Server.MapPath("~/app_data/procedimientos/");

                foreach (string f in Request.Files.Keys)
                {
                    if (Request.Files[f].ContentLength > 0)
                    {

                        UpdateModel(pro);

                        UpdateModel(arc, new[] { "arp_ruta", "arp_fechacrea" });

                        modeloPro.crearprocedimiento(pro);
                        modeloPro.guardar();

                        arc.arp_nombre = "(Num-" + pro.pro_id + ")" + Request.Files[f].FileName;
                        arc.pro_id = pro.pro_id;
                        modeloPro.crearArchivo(arc);

                        modeloPro.guardar();

                        Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + pro.pro_id + ")" + Request.Files[f].FileName));
                    }

                    else
                    {

                        UpdateModel(pro);
                        modeloPro.crearprocedimiento(pro);
                        modeloPro.guardar();

                    }
                }
                return RedirectToAction("iniciopro");
            
        }
        
        //Editar procedimiento.
        [Authorize(Roles = "Admin")]
        public ActionResult editarpro(int id)
        {
            procedimiento pro = modeloPro.detalleprocedimientos(id);
            
            bool cantidad = modeloPro.rutaPos(id);

            if (cantidad == true)
            {

                var archi = modeloPro.ruta(id);

                string nombreArchivo = archi.arp_nombre;

                var files = from f in Directory.GetFiles(
                Server.MapPath("~/app_data/procedimientos/"), nombreArchivo, SearchOption.TopDirectoryOnly)
                            select Path.GetFileName(f);

                ViewData["id_arc"] = archi.arp_id;
                ViewData["files"] = files;

            }
            else {

                ViewData["files"] = "";
            
            }

            ViewData["hora"] = System.DateTime.Now.ToString();
            ViewData["areas"] = new SelectList(modeloPro.listarareas().AsEnumerable(), "are_id", "are_nombre");
            return View(pro);

        }

        //Editar procedimiento.
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult editarpro(int id, FormCollection formValues)
        {
            procedimiento pro = modeloPro.detalleprocedimientos(id);

            archivospro arc = new archivospro();

            try
            { 
            
                string upload_dir = Server.MapPath("~/app_data/procedimientos/");


                    foreach (string f in Request.Files.Keys)
                    {
                        if (Request.Files[f].ContentLength > 0)
                        {

                            UpdateModel(pro);
                            modeloPro.guardar();

                            UpdateModel(arc, new[] { "arp_nombre", "arp_fechacrea" });

                            arc.arp_nombre = "(Num-" + pro.pro_id + ")" + Request.Files[f].FileName;
                            arc.pro_id = id;

                            modeloPro.crearArchivo(arc);
                            modeloPro.guardar();

                            Request.Files[f].SaveAs(upload_dir + System.IO.Path.GetFileName("(Num-" + pro.pro_id + ")" + Request.Files[f].FileName));
                            return RedirectToAction("iniciopro");
                        }

                        else
                        {
                            UpdateModel(pro);
                            modeloPro.guardar();
                            return RedirectToAction("iniciopro");
                        }
                    }
                    UpdateModel(pro);
                    modeloPro.guardar();

                return RedirectToAction("iniciopro");
            }
            catch {

                ViewData["hora"] = System.DateTime.Now.ToString();
                ViewData["areas"] = new SelectList(modeloPro.listarareas().AsEnumerable(), "are_id", "are_nombre");
                return View(pro);
            
            }
            
        }

        //Método para descargar archivo
        [Authorize(Roles = "Admin")]
        public ActionResult Download(string fn)
        {
            string pfn = Server.MapPath("~/App_Data/procedimientos/" + fn);
            if (!System.IO.File.Exists(pfn))
            {
                throw new ArgumentException("El archivo no existe!");
            }

            return new BinaryFileResult.BinaryContentResult()
            {
                FileName = fn,
                ContentType = "application/octet-stream",
                Content = System.IO.File.ReadAllBytes(pfn)
            };
        }

        //Eliminar procedimiento
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminar(int id)
        {
            procedimiento pro = modeloPro.detalleprocedimientos(id);

            modeloPro.eliminarprocedimiento(pro);
            modeloPro.guardar();

            return View("iniciopro");
        }

        //Eliminar archivo de procedimiento
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult eliminarArchivo(int id)
        {
            archivospro arc = modeloPro.detalleArchivo(id);

            modeloPro.eliminarArchivo(arc);
            modeloPro.guardar();

            return View("editarpro");
        }

    }
}
