<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="catalogo_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <label style="font-weight: bold">FILTRAR</label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <label>Campo</label>
                        <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <label>Criterio</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label ID="lblFiltro" runat="server">Filtro</asp:Label>
                        <asp:TextBox CssClass="form-control" ID="txtFiltro" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <asp:TextBox ID="txtEntre" runat="server" CssClass="form-control " CssStyle="mb-3" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-3" style="display: flex; flex-direction: column; justify-content: flex-end;">
                        <div class="mb-3">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Borrar" CssClass="btn btn-outline-primary" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row row-cols-1 row-cols-md-3">
                <asp:Repeater ID="repArticulo" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card mb-4" style="width: 18rem;">
                                <img src="<%# Eval("ImagenUrl")%>" class="card-img-top" alt="<%#"Imagen de" + Eval("Nombre") %>"
                                    onerror="this.onerror= null;this.src='https://www.puntodventamx.com/wp-content/uploads/2016/11/product-placeholder.jpg';" />
                                <div class="card-body">
                                    <asp:Label runat="server" ID="lblCodigo" CssClass="form-label"><%#Eval("Codigo") %></asp:Label>
                                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                    <h3 class="card-title"><%#"$" + Eval("Precio") %></h3>
                                    <p class="card-text"><%# Eval("Descripcion") %></p>
                                    <asp:Button ID="btnVer" CssClass="btn btn-primary" runat="server" Text="Detalles" CommandArgument='<%#Eval("Id")%>' OnClick="btnVer_Click" />
                                    <asp:Button ID="btnFavorito" CssClass="btn btn-danger" runat="server" Text="❤" CommandArgument='<%#Eval("Id") %>' OnClick="btnFavorito_Click"/>
                                </div>
                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item"><%#Eval ("Categoria") %></li>
                                    <li class="list-group-item"><%#Eval("Marca") %></li>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
