<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="RegCliente.aspx.cs" Inherits="Seguradora.Pages.RegCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Cliente.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:label runat="server" text="Cliente"></asp:label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="form-style">

        <asp:label id="Label1" runat="server" text="Id do Cliente"></asp:label>
        <asp:textbox id="txtIdCliente" runat="server" textmode="Number"></asp:textbox>
        <asp:label id="Label2" runat="server" text="Nome"></asp:label>
        <asp:textbox id="txtNome" runat="server"></asp:textbox>
        <asp:label id="Label3" runat="server" text="Telefone"></asp:label>
        <asp:textbox id="txtTelefone" runat="server"></asp:textbox>
        <asp:label id="Label4" runat="server" text="Endereço"></asp:label>
        <asp:textbox id="txtEndereco" runat="server"></asp:textbox>
        <asp:label id="Label5" runat="server" text="Bairro"></asp:label>
        <asp:textbox id="txtBairro" runat="server"></asp:textbox>
        <asp:label id="Label6" runat="server" text="Cep"></asp:label>
        <asp:textbox id="txtCep" runat="server"></asp:textbox>
        <asp:label id="Label7" runat="server" text="Cpf"></asp:label>
        <asp:textbox id="txtCpf" runat="server"></asp:textbox>
        <asp:label id="Label8" runat="server" text="Rg"></asp:label>
        <asp:textbox id="txtRg" runat="server"></asp:textbox>
        <asp:label id="Label9" runat="server" text="Sexo"></asp:label>
        <div class="radio-group">
            <asp:radiobutton id="rbtM" runat="server" groupname="groupSexo" text="M" />
            <asp:radiobutton id="rbtF" runat="server" groupname="groupSexo" text="F" />
        </div>
        <asp:label id="Label10" runat="server" text="Data de Nascimento"></asp:label>
        <asp:textbox id="txtDataNascimento" runat="server" textmode="Date"></asp:textbox>
        
        <div class="buttons-form">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primario" OnClick="btnRegistrar_Click"/>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" CssClass="btn btn-primario" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-primario" OnClick="btnExcluir_Click" />
            <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" CssClass="btn btn-primario" OnClick="btnRecuperar_Click" />
        </div>
    </div>
</asp:Content>
