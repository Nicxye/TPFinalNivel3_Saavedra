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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ArticuloNegocio articuloNegocio = new ArticuloNegocio();

                    Session.Add("listaArticulos", articuloNegocio.Listar());
                    repArticulo.DataSource = Session["listaArticulos"];
                    repArticulo.DataBind();

                    AgregarElementosCampo();
                    ddlCampo.DataBind();

                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.Message);
                    Response.Redirect("Error.aspx");
                }

            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Response.Redirect($"ArticuloForms.aspx?id={id}");
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
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlCampo.Items[0].Value))
                ddlCampo.Items.RemoveAt(0);
            AgregarElementosTipo();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlTipo.Items[0].Value))
                ddlTipo.Items.RemoveAt(0);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                Session.Add("listaFiltrada", negocio.Filtrar(ddlCampo.SelectedItem.ToString(),
                    ddlTipo.SelectedItem.ToString(), txtFiltro.Text));
                //repArticulo.DataSource = Session["listaFiltrada"];
                repArticulo.DataSource = negocio.Filtrar(ddlCampo.SelectedValue,
                    ddlTipo.SelectedValue, txtFiltro.Text);
                repArticulo.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }
    }
}