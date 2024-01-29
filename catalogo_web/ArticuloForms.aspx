<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticuloForms.aspx.cs" Inherits="catalogo_web.ArticuloForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="lblId" class="form-label">ID</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="lblCodigo" class="form-label">Código</label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="lblNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" REQUIRED></asp:TextBox>
            </div>
            <div class="mb-3">
                <label cssclass="form-label">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label cssclass="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3">
                <label for="lblDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="lblPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" REQUIRED></asp:TextBox>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="lblImagen" class="form-label">Url Imagen</label>
                        <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged"></asp:TextBox>
                    </div>
                    <img id="imgPlace" runat="server" src="https://www.puntodventamx.com/wp-content/uploads/2016/11/product-placeholder.jpg" onerror="this.onerror = null; this.src='https://www.puntodventamx.com/wp-content/uploads/2016/11/product-placeholder.jpg'" alt="Imagen de producto"/>
                    <%--<asp:Image ID="imgProducto" runat="server" CssClass="img-fluid mb-3" ImageUrl="https://www.puntodventamx.com/wp-content/uploads/2016/11/product-placeholder.jpg"
                        alt="imagen del producto" NullImageUrl="https://www.puntodventamx.com/wp-content/uploads/2016/11/product-placeholder.jpg" On/>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                <a href="Default.aspx">Cancelar</a>
            </div>
        </div>
    </div>
    <%if (Request.QueryString["id"] != null)
        {%>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Button ID="btnEliminar" CssClass="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                <% if (ConfirmarEliminacion)
                    {%>
                <div class="mb-3">
                    <asp:CheckBox ID="chkConfirmarEliminacion" runat="server" Text="¿Eliminar <b>PERMANENTEMENTE</b>? (¡Eso es mucho tiempo!)" />
                    <asp:Button ID="btnConfirmarEliminacion" CssClass="btn btn-outline-danger" runat="server" Text="Eliminar" OnClick="btnConfirmarEliminacion_Click" />
                </div>
                <%}%>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
