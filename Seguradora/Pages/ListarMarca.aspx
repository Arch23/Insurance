<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="ListarMarca.aspx.cs" Inherits="Seguradora.Pages.ListarMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Marca.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:label runat="server" text="Listar Marcas"></asp:label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    
    
    <asp:Table ID="Table" runat="server" CssClass="table-form">
        <asp:TableHeaderRow ID="TableHeaderRow" CssClass="table-header">
            <asp:TableHeaderCell ID="TableHeaderCell1" runat="server" CssClass="table-header-1">Id</asp:TableHeaderCell>
            <asp:TableHeaderCell ID="TableHeaderCell2" runat="server" CssClass="table-header-2">Descrição</asp:TableHeaderCell>
            <asp:TableHeaderCell ID="TableHeaderCell3" runat="server" CssClass="table-header-3">Editar</asp:TableHeaderCell>
            <asp:TableHeaderCell ID="TableHeaderCell4" runat="server" CssClass="table-header-4">Excluir</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    
    
    
</asp:Content>
