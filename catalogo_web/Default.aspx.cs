using Accesorio;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace catalogo_web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    CargarLista();
                    txtEntre.Visible = false;
                    AgregarElementosCampo();
                    ddlCampo.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }

            }
        }
        protected void AgregarElementosCampo()
        {
            try
            {
                ddlCampo.Items.Insert(0, "");
                ddlCampo.Items.Add("Artículo");
                ddlCampo.Items.Add("Categoría");
                ddlCampo.Items.Add("Marca");
                ddlCampo.Items.Add("Precio");

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx");
            }

        }

        protected void AgregarElementosTipo()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                ddlTipo.Items.Clear();
                if (ddlCampo.SelectedValue == "Artículo")
                {
                    ddlTipo.Items.Add("Nombre exacto");
                    ddlTipo.Items.Add("Nombre posible");
                }
                else if (ddlCampo.SelectedValue == "Categoría")
                {
                    ddlTipo.DataSource = categoriaNegocio.Listar();
                }
                else if (ddlCampo.SelectedValue == "Marca")
                {
                    ddlTipo.DataSource = marcaNegocio.Listar(); ;
                }
                else if (ddlCampo.SelectedValue == "Precio")
                {
                    ddlTipo.Items.Add("Hasta");
                    ddlTipo.Items.Add("Entre");
                    ddlTipo.Items.Add("Más de");
                    ddlTipo.Items.Add("Igual a");
                }
                ddlTipo.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void CajaEntreOnOff()
        {
            if (ddlTipo.SelectedValue == "Entre" && ddlCampo.SelectedValue == "Precio")
                txtEntre.Visible = true;
            else txtEntre.Visible = false;
        }

        protected void CargarLista()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                Session.Add("listaArticulos", articuloNegocio.Listar());
                repArticulo.DataSource = Session["listaArticulos"];
                repArticulo.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void DesactivarTextoFiltro()
        {
            txtFiltro.Text = string.Empty;

            if (ddlCampo.SelectedValue == "Artículo" || ddlCampo.SelectedValue == "Precio")
            {
                txtFiltro.Enabled = true;
                lblFiltro.Enabled = true;
            }
            else
            {
                txtFiltro.Enabled = false;
                lblFiltro.Enabled = false;
            }
        }

        protected void LimpiarFiltro()
        {
            try
            {
                txtFiltro.Text = string.Empty;
                txtEntre.Text = string.Empty;
                Session.Remove("listaFiltrada");
                CargarLista();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected bool ValidarCampos()
        {
            if (Validacion.TextoVacio(txtFiltro) && (ddlCampo.SelectedValue == "Artículo" || ddlCampo.SelectedValue == "Precio"))
            {
                LimpiarFiltro();
                return true;
            }

            if (ddlCampo.SelectedValue == "Precio" && (Validacion.TextoVacio(txtFiltro) || Validacion.TextoVacio(txtEntre)))
                return true;

            if (Validacion.TextoVacio(ddlCampo))
                return true;

            if (!(Validacion.SoloNumeros(txtFiltro)) && ddlCampo.SelectedValue == "Precio")
                return true;

            return false;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (ValidarCampos())
                    return;

                Session.Add("listaFiltrada", negocio.Filtrar(ddlCampo.SelectedItem.ToString(),
                    ddlTipo.SelectedItem.ToString(), txtFiltro.Text, txtEntre.Text));
                repArticulo.DataSource = Session["listaFiltrada"];
                repArticulo.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
        protected void btnFavorito_Click(object sender, EventArgs e)
        {
            if (Seguridad.SesionActiva(Session["usuario"]))
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                Articulo articulo = new Articulo();
                articulo.Id = int.Parse(((Button)sender).CommandArgument);

                try
                {
                    if (Validacion.ExisteFavorito(usuario, articulo))
                        return;
                    negocio.AgregarFavorito(articulo, usuario);
                    Response.Redirect("Favoritos.aspx", false);
                }
                catch (Exception ex)
                {

                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
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
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltro();
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlCampo.Items[0].Value))
                ddlCampo.Items.RemoveAt(0);

            CajaEntreOnOff();
            DesactivarTextoFiltro();
            AgregarElementosTipo();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlTipo.Items[0].Value))
                ddlTipo.Items.RemoveAt(0);

            CajaEntreOnOff();
        }

    }
}