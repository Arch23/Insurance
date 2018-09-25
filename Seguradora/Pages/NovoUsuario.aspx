<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovoUsuario.aspx.cs" Inherits="Seguradora.Pages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Seguradora|Cadastrar Usuário</title>
    <link href="Styles/comum.css" rel="stylesheet" />
    <link href="Styles/NovoUsuario.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="novo-form">
            <asp:Label ID="Label1" runat="server" Text="Login" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtLogin" runat="server" CssClass="form-textfield"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Senha" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" CssClass="form-textfield" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-voltar" OnClick="btnVoltar_Click"/>
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primario btn-registrar" OnClick="btnRegistrar_Click"/>
        </div>
    </form>
</body>
</html>
