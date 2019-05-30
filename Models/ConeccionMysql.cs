using System;
using System.Data;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;
using Sigacun.Models;


namespace Sigacun.Models
{
    public class ConeccionMysql
    {
        ConeccionDataContext db = new ConeccionDataContext();

        private string ConexionBaseDatos = "DataBase=mesaayuda;DataSource=localhost;;User Id=root;Password=root;Port=3306";

        public void registrarEvaluacion(int num_sol,int id) {

            ActividadHdModel modeloAch = new ActividadHdModel();

            string consulta = consultarEvaluacion(num_sol);

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataSet ds = new DataSet();

            mda.Fill(ds, "tabla");

            int tam = ds.Tables["tabla"].Rows.Count;

            if (tam > 0) {

                for (int i = 0; i < tam; i++)
                {
                    
                    string obs = ds.Tables["tabla"].Rows[i][0].ToString();
                    int cal = (int) ds.Tables["tabla"].Rows[i][1];
                    int num = id;
                    int pre = (int) ds.Tables["tabla"].Rows[i][3];
                    
                    db.InsertarEvalucion(num,pre,cal,obs);

                }

                
                

                actividades_hd ach = modeloAch.obtenerActividad(id);


                    ach.est_id = 5;
                    modeloAch.guardar();



            }

        }

        private void UpdateModel(actividades_hd act)
        {
            throw new NotImplementedException();
        }

        private void UpdateModel()
        {
            throw new NotImplementedException();
        }

        private string consultarEvaluacion(int id) {

            string consulta = "SELECT eva_observacion, eva_calificacion, eva_pnsolid, eva_pre_id " +
                              "FROM  intra_unhd_ceno2_tbevaluacion " +
                              "WHERE eva_pnsolid = " + id;

            return consulta;
        
        }

        //USUARIOS

        public IEnumerable<SelectListItem>  listarUsuarios()
        {

            string consulta = "SELECT *" +
                               "FROM intra_unhd_ceno2_tbrecursoh";

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataTable dt = new DataTable();
            
            mda.Fill(dt);

            List<SelectListItem> lista = new List<SelectListItem>();
            
            int i = 0;
             
            foreach(var data in dt.Rows)
            {

                lista.Add(new SelectListItem
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString(),

                });
                i++;
            }
            
            

            return lista;
 

        }

        public IEnumerable<SelectListItem> listarUsuariosEditar(int id)
        {

            string consulta = "SELECT *" +
                               "FROM intra_unhd_ceno2_tbrecursoh";

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataTable dt = new DataTable();

            mda.Fill(dt);

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                if ((int)dt.Rows[i][0] == id)
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),
                        Selected = true
                    });


                }
                else {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),

                    });
                }

                i++;
            }

            return lista;

        }

        // AREAS

        private DataTable obternerAreas()
        {
            string consulta = "SELECT *" +
                   "FROM intra_unhd_ceno2_tbarea";

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataTable dt = new DataTable();

            mda.Fill(dt);

            return dt;
        
        }

        public IEnumerable<SelectListItem> listarAres()
        {

            DataTable dt = obternerAreas();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                lista.Add(new SelectListItem
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString(),

                });
                i++;
            }

            return lista;
        }

        public IEnumerable<SelectListItem> listarAresEditar(int id)
        {

            DataTable dt = obternerAreas();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                if ((int)dt.Rows[i][0] == id)
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),
                        Selected = true
                    });


                }
                else
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),

                    });
                }

                i++;
            }

            return lista;
        }

        //ESTADOS

        private DataTable obternerEstados()
        {
            string consulta = "SELECT *" +
                   "FROM intra_unhd_ceno2_tbestsolicitud";

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataTable dt = new DataTable();

            mda.Fill(dt);

            return dt;

        }

        public IEnumerable<SelectListItem> listarEstados()
        {

            DataTable dt = obternerEstados();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                lista.Add(new SelectListItem
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString(),

                });
                i++;
            }

            return lista;
        }

        public IEnumerable<SelectListItem> listarEstadoEditar(int id)
        {

            DataTable dt = obternerEstados();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                if ((int)dt.Rows[i][0] == id)
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),
                        Selected = true
                    });


                }
                else
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),

                    });
                }

                i++;
            }

            return lista;
        }


        // PRIORIDADES

        private DataTable obternerPrioridad()
        {
            string consulta = "SELECT *" +
                   "FROM intra_unhd_ceno2_tbprioridad";

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataTable dt = new DataTable();

            mda.Fill(dt);

            return dt;

        }

        public IEnumerable<SelectListItem> listarPrioridad()
        {

            DataTable dt = obternerPrioridad();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                lista.Add(new SelectListItem
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString(),

                });
                i++;
            }

            return lista;
        }

        public IEnumerable<SelectListItem> listarPrioridadEditar(int id)
        {

            DataTable dt = obternerPrioridad();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                if ((int)dt.Rows[i][0] == id)
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),
                        Selected = true
                    });


                }
                else
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),

                    });
                }

                i++;
            }

            return lista;
        }

        //PREGUNTAS

        private DataTable obternerPreguntas()
        {
            string consulta = "SELECT *" +
                   "FROM intra_unhd_ceno2_tbevapreguntas";

            MySqlConnection cnn = new MySqlConnection(this.ConexionBaseDatos);
            MySqlDataAdapter mda = new MySqlDataAdapter(consulta, cnn);

            DataTable dt = new DataTable();

            mda.Fill(dt);

            return dt;

        }

        public IEnumerable<SelectListItem> listarPreguntas()
        {

            DataTable dt = obternerPreguntas();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                lista.Add(new SelectListItem
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString(),

                });
                i++;
            }

            return lista;
        }

        public IEnumerable<SelectListItem> listarPreguntasEditar(int id)
        {

            DataTable dt = obternerPreguntas();

            List<SelectListItem> lista = new List<SelectListItem>();

            int i = 0;

            foreach (var data in dt.Rows)
            {

                if ((int)dt.Rows[i][0] == id)
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),
                        Selected = true
                    });


                }
                else
                {

                    lista.Add(new SelectListItem
                    {
                        Text = dt.Rows[i][1].ToString(),
                        Value = dt.Rows[i][0].ToString(),

                    });
                }

                i++;
            }

            return lista;
        }


    }
}
