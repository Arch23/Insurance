<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="RegApolice.aspx.cs" Inherits="Seguradora.Pages.RegApolice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Apolice.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:label runat="server" text="Apólice"></asp:label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="form-style">
        <asp:label runat="server" text="Id de Apólice"></asp:label>
        <asp:textbox runat="server" ID="txtIdApolice" TextMode="Number"></asp:textbox>
        <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
        <div class="dropdownlist">
            <asp:dropdownlist runat="server" ID="cbxCliente"></asp:dropdownlist>
        </div>
        <asp:Label ID="Label2" runat="server" Text="Veículo"></asp:Label>
        <div class="dropdownlist">
            <asp:dropdownlist runat="server" ID="cbxVeiculo"></asp:dropdownlist>
        </div>
        <asp:Label ID="Label3" runat="server" Text="Número da Apólice"></asp:Label>
        <asp:textbox runat="server" ID="txtNumeroApolice"></asp:textbox>
        <asp:Label ID="Label5" runat="server" Text="Data de Início"></asp:Label>
        <asp:textbox runat="server" ID="txtDataInicio" TextMode="Date"></asp:textbox>
        <asp:Label ID="Label6" runat="server" Text="Data de Fim"></asp:Label>
        <asp:textbox runat="server" ID="txtDataFim" TextMode="Date"></asp:textbox>
        <asp:Label ID="label14" runat="server" Text="Valor"></asp:Label>
        <asp:textbox runat="server" ID="txtValor"></asp:textbox>
        <asp:Label ID="Label7" runat="server" Text="Franquia"></asp:Label>
        <asp:textbox runat="server" ID="txtFranquia"></asp:textbox>
        <asp:Label ID="Label8" runat="server" Text="Data de Contrato"></asp:Label>
        <asp:textbox runat="server" ID="txtDataDeContrato" TextMode="Date"></asp:textbox><div class="buttons-form">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primario" OnClick="btnRegistrar_Click"/>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" CssClass="btn btn-primario" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-primario" OnClick="btnExcluir_Click" />
            <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" CssClass="btn btn-primario" OnClick="btnRecuperar_Click" />
        </div>
    </div>
</asp:Content>
