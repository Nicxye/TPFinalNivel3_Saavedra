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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Seguridad.SesionActiva(Session["usuario"]))
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;

                    txtEmail.ReadOnly = true;
                    txtEmail.Text = usuario.Email;

                    if (!(string.IsNullOrEmpty(usuario.UrlImagenPerfil)))
                        imgNuevoPerfil.ImageUrl = $"~/Images/Profile/{usuario.UrlImagenPerfil}";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/Profile/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                    usuario.UrlImagenPerfil = "perfil-" + usuario.Id + ".jpg";
                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;

                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = $"~/Images/Profile/{usuario.UrlImagenPerfil}";

                negocio.Modificar(usuario);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}