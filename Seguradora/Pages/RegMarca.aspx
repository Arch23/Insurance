<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="RegMarca.aspx.cs" Inherits="Seguradora.Pages.RegMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Marca.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label3" runat="server" Text="Marca"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="form-style">
        <asp:Label ID="Label5" runat="server" Text="Id da Marca"></asp:Label>
        <asp:TextBox ID="txtIdMarca" runat="server" TextMode="Number"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" Text="Descrição"></asp:Label>
        <asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox>
        <div class="buttons-form">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primario" OnClick="btnRegistrar_Click"/>
    
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" CssClass="btn btn-primario" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-primario" OnClick="btnExcluir_Click" />
            <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" CssClass="btn btn-primario" OnClick="btnRecuperar_Click" />
    
        </div>
    </div>
</asp:Content>
