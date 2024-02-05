using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace catalogo_web
{
    public static class Validacion
    {
        public static bool TextoVacio(object control)
        {
            bool resultado = false;

            if (control is TextBox texto)
            {
                resultado = string.IsNullOrWhiteSpace(texto.Text);
            }
            else if (control is DropDownList ddl)
            {
                resultado = string.IsNullOrWhiteSpace(ddl.SelectedValue);

            }
            return resultado;
        }
        public static bool SoloNumeros(object control)
        {
            bool resultado = false;

            if (control is TextBox texto)
            {
                resultado = double.TryParse(texto.Text, out _);
            }

            return resultado;
        }

        public static bool ExisteFavorito(Usuario usuario, Articulo articulo)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();
            bool resultado = false;

            try
            {
                datos.SetQuery("Select F.Id, F.IdArticulo, F.IdUser, A.Id, A.Nombre, A.Descripcion from FAVORITOS F INNER JOIN ARTICULOS A ON F.IdArticulo = A.Id where F.IdUser = @IdUser and F.IdArticulo = @IdArticulo");
                datos.SetParameters("@IdUser", usuario.Id);
                datos.SetParameters("@IdArticulo", articulo.Id);

                datos.ExecuteReader();

                if (datos.Lector.HasRows)
                    resultado = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }

            return resultado;
        }
    }
}