<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="moredetails.aspx.cs" Inherits="lab.moredetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
      <tr>
          <td rowspan="2">  
            <asp:Image ID="Image_liv" runat="server" Height="200px" Width="248px" />
          </td>
    </tr>  
          <tr style="vertical-align:central">  
    <td>
    <asp:TextBox ID="frete" runat="server"></asp:TextBox>
    <asp:Button ID="calcular" runat="server" OnClick="calcular_Click" Text="calcular frete" /><br/>
    <label id="frete_res" runat="server">
    </label>
    </td>
    </tr>

    <tr>
        <td>
    <asp:Label ID="preço" runat="server" />
            </td>
        <td>
    <asp:Button ID="comprar" runat="server" OnClick="comprar_Click" Text="comprar" />
    </td><td>
            <div ID="detalhes" runat="server"  onclick="">
            </div>
        </td>
        </tr>
    </table>
</asp:Content>