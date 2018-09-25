<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="Seguradora.Pages.PaginaErro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Seguradora Erro</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet"/>
    <link href="Styles/comum.css" rel="stylesheet" />
    <link href="Styles/Erro.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="erro-form">
            <div class="erro-form-header">
                <asp:Label ID="lblHeader" runat="server" Text="header-label"></asp:Label>
            </div>
            <div class="erro-form-body">
                <asp:Label ID="lblBody" runat="server" Text="body-label"></asp:Label>
                <br />
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" CssClass="btn btn-primario btn-erro"/>
            </div>
        </div>
    </form>
</body>
</html>
