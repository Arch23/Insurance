<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="RegVeiculo.aspx.cs" Inherits="Seguradora.Pages.RegVeiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Veiculo.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Veículo"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="form-style">

        <asp:Label ID="Label2" runat="server" Text="Id do Veículo"></asp:Label>
        <asp:TextBox ID="txtIdVeiculo" runat="server" TextMode="Number"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="Modelo"></asp:Label>
        <div class="dropdownlist">
            <asp:DropDownList ID="cbxModelo" runat="server">
            </asp:DropDownList>
        </div>
        <asp:Label ID="Label4" runat="server" Text="Placa"></asp:Label>
        <asp:TextBox ID="txtPlaca" runat="server"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="Ano"></asp:Label>
        <asp:TextBox ID="txtAno" runat="server" TextMode="Number"></asp:TextBox>
        <div class="buttons-form">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primario" OnClick="btnRegistrar_Click"/>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" CssClass="btn btn-primario" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-primario" OnClick="btnExcluir_Click" />
            <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" CssClass="btn btn-primario" OnClick="btnRecuperar_Click" />
        </div>
    </div>
</asp:Content>
