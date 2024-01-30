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
    }
}