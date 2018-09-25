<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Seguradora.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Seguradora|Login</title>
    <link href="Styles/comum.css" rel="stylesheet" />
    <link href="Styles/Login.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-form">
            <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label>
            <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Senha"></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            <div class="div-lembrar">
                <asp:CheckBox ID="ckbLembrar" runat="server" Text="Lembrar usuário?" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primario btn-login" OnClick="btnLogin_Click"/>
            <asp:Button ID="btnNovoUsuario" runat="server" Text="Novo Usuário" CssClass="btn btn-login" OnClick="btnNovoUsuario_Click"/>
        </div>
    </form>
</body>
</html>
