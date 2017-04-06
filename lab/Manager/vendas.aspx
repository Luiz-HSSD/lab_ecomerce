<%@ Page Title="VENDAS" Language="C#" MasterPageFile="~/Manager/aspm/principal.Master" AutoEventWireup="true" CodeBehind="vendas.aspx.cs" Inherits="lab.Manager.vendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <table border="1">
    <tr><td colspan="5"><h1>cadastro vendas </h1></td></tr>
    <tr><td colspan="2"><b>codigo</b></td> <td colspan="3"><asp:Label ID="codigo" runat="server"></asp:Label></td></tr>
    <tr><td colspan="2">cliente:</td> <td colspan="3"><asp:DropDownList ID="DropDownListcli" runat="server" DataTextField="nome_cli" DataValueField="cod"></asp:DropDownList></td></tr>
    <tr><td colspan="2">produto:</td> <td colspan="3"><asp:DropDownList ID="DropDownListpro" runat="server" DataTextField="nome_pro" DataValueField="cod"></asp:DropDownList></td></tr>
    <tr><td colspan="2"><b>quantidade:</b></td> <td colspan="3"><asp:TextBox TextMode="Number" ID="qtd" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan="2"><b>preço: </b></td><td> <asp:TextBox TextMode="Number" ID="preco" runat="server"></asp:TextBox></td></tr>
    </table>


    <table>
    <tr>
            <td>
                <asp:Button runat="server" Text="novo" id="novo_ven" OnClick="novo_ven_Click" />
            </td>
            <td>
                <asp:Button runat="server" Text="alterar" id="alterar_ven" OnClick="alterar_ven_Click" />
            </td>
            <td>
               <asp:Button runat="server" Text="cancelar" id="cancelar_ven" OnClick="cancelar_ven_Click" />
            </td>

    </tr>
    </table>
            <br />
          <div id="divTable" runat="server" style="padding:30px; width: 998px; height: 1px;">
      <table class="auto-style19">
          <tr>
              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
              <td>&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
          </tr>
      </table>
      </div>
                  <asp:GridView runat="server" CssClass="display" ID="GridViewven" EnableModelValidation="True" Width="204px" >
  <HeaderStyle Font-Bold="true" />
                    </asp:GridView >
</asp:Content>
