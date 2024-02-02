<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="catalogo_web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-3">
            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
            <div class="mb-3">
                <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="usuario@gmail.com" REQUIRED></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexValidator" ControlToValidate="txtEmail" runat="server" ErrorMessage="Debe ser formato email."
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" CssClass="error"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ErrorMessage="Requerido." CssClass="error"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-3">
            <asp:Label ID="lblPass" runat="server" Text="Contraseña"></asp:Label>
            <div class="mb-3">
                <asp:TextBox ID="txtPass" runat="server" ClientIDMode="Static" CssClass="form-control" TextMode="Password" REQUIRED></asp:TextBox>
                <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
                <asp:RequiredFieldValidator ControlToValidate="txtPass" runat="server" ErrorMessage="Requerido." CssClass="error"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Ingresar" OnClientClick="return campoRequerido('txtEmail') && campoRequerido('txtPass')" OnClick="btnLogin_Click" />
            </div>
        </div>
    </div>
</asp:Content>
