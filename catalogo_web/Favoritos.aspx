<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="catalogo_web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-md-3">
                <asp:Label runat="server" ID="lblFavoritos" CssClass="form-label"></asp:Label>
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
                                    <asp:Button ID="btnQuitarFavorito" CssClass="btn btn-danger" runat="server" Text="💔" CommandArgument='<%#Eval("Id")%>' OnClick="btnQuitarFavorito_Click" />
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
