using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                datos.SetQuery("Select Id, Descripcion from CATEGORIAS");
                datos.ExecuteReader();

                while (datos.Lector.Read())
                {
                    Categoria categ = new Categoria();
                    categ.Id = (int)datos.Lector["Id"];
                    categ.Descripcion = (string)datos.Lector["Descripcion"];
                    
                    listaCategorias.Add(categ);
                }

                return listaCategorias;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }
        }
    }
}
