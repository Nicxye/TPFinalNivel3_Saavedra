using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class AccesoBaseDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public AccesoBaseDatos()
        {
            conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
            //conexion = new SqlConnection("server=.\\SQLEXPRESS; database= CATALOGO_WEB_DB; integrated security= true");
            comando = new SqlCommand();
        }

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public void SetQuery(string consulta)
        {
            comando.CommandType= System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetParameters(string nombre, object value)
        {
            comando.Parameters.AddWithValue(nombre, value);
        }

        public void ExecuteReader()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CloseConnection()
        {
            if (lector != null)
                lector.Close();

            conexion.Close();
        }

        public void ExecuteAction()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
