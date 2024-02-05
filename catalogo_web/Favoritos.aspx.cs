using Accesorio;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace catalogo_web
{
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Seguridad.SesionActiva(Session["usuario"]))
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Usuario usuario = (Usuario)Session["usuario"];
                    try
                    {
                        Session.Add("listaFavoritos", negocio.ListarFavoritos(usuario));
                        CargarFavoritos();

                    }
                    catch (Exception ex)
                    {

                        Session.Add("error", ex.ToString());
                        Response.Redirect("Error.aspx", false);
                    }
                }
            }
        }

        protected void CargarFavoritos()
        {
            try
            {
                repArticulo.DataSource = Session["listaFavoritos"];
                repArticulo.DataBind();

                if (repArticulo.Items.Count == 0)
                    lblFavoritos.Text = "No tienes favoritos.";
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnQuitarFavorito_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = new Articulo();
            Usuario usuario = (Usuario)Session["usuario"];
            articulo.Id = int.Parse(((Button)sender).CommandArgument);

            try
            {
                negocio.EliminarFavorito(articulo, usuario);
                Session["listaFavoritos"] = negocio.ListarFavoritos(usuario);
                CargarFavoritos();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            try
            {
                Response.Redirect($"ArticuloForms.aspx?id={id}", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}