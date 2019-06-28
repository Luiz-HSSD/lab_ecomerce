<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="moredetails.aspx.cs" Inherits="lab.moredetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Image ID="Image1" runat="server" Height="200px" Width="248px" />
    <asp:TextBox ID="cep" runat="server"></asp:TextBox>
    <asp:Button ID="calcular" runat="server" OnClick="calcular_Click" Text="calcular cep" /><br />
    <asp:Label ID="preço" runat="server" />
    <asp:Button ID="comprar" runat="server" OnClick="comprar_Click" Text="comprar" />
    <asp:Literal ID="detalhes" runat="server" />
</asp:Content>
