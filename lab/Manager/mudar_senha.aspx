<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/aspm/principal.Master" AutoEventWireup="true" CodeBehind="mudar_senha.aspx.cs" Inherits="lab.Manager.mudar_senha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="senha" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="confirmar" runat="server"></asp:TextBox>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />
    <asp:Button ID="salvar" runat="server" Text="Alterar" OnClick="salvar_Click" />
</asp:Content>
