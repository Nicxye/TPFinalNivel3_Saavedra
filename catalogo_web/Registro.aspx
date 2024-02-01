<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="catalogo_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Regístrate</h1>
    <div class="row">
        <div class="col-4">
            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
            <div class="mb-3">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="usuario@gmail.com" CssClass="form-control" REQUIRED></asp:TextBox>
                <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server" ErrorMessage="Debe ser formato email."
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" CssClass="error"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Requerido." CssClass="error"></asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="lblPass" runat="server" Text="Contraseña"></asp:Label>
            <div class="mb-3">
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" REQUIRED></asp:TextBox>
                <asp:Label ID="lblExito" runat="server" Text="" style="color: green;"></asp:Label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPass" ErrorMessage="Requerido." CssClass="error"></asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Button ID="btnRegistro" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClientClick="return campoRequerido('txtEmail') && campoRequerido('txtPass')" OnClick="btnRegistro_Click"/>
                <a href="Default.aspx">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
