﻿<%@ Page Title="CATEGORIA" Language="C#" MasterPageFile="~/Manager/aspm/principal.Master" AutoEventWireup="true" CodeBehind="categoria.aspx.cs" Inherits="lab.Manager.Categoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function bora() {
        $('#GridViewcat').DataTable();
    };
        function storeinput(value) {
            document.getElementById("<%=hidValue.ClientID%>").value = value;
        }
</script>
    <style type="text/css">
        .auto-style1 {
            width: 297px;
        }
        .auto-style2 {
            width: 40px;
        }
        .auto-style3 {
            width: 54px;
        }
        .auto-style4 {
            width: 71px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">     
    <table border="1" class="auto-style1">
        <tr>
            <td colspan="5">
                <h1>cadastro generos</h1>
            </td>
        </tr>
        <tr>
            <td colspan="2"><b>codigo</b><input type="hidden" id="hidValue" runat="server" /></td>
            <td colspan="3">
                       <asp:Label  ID="txtcod" runat="server"></asp:Label>
                </td>
        </tr>
        <tr>
            <td colspan="2">nome resumido:</td>
            <td colspan="3">
                <asp:TextBox TextMode="SingleLine" ID="txtnome" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">descrição</td>
            <td colspan="3">
                        <asp:TextBox TextMode="MultiLine" ID="txtdescricao" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Button runat="server" Text="novo" id="novo_cat" OnClick="novo_cat_Click" />
            </td>
            <td class="auto-style3">
                <asp:Button runat="server" Text="alterar" id="alterar_cat" OnClick="alterar_cat_Click" />
            </td>
            <td class="auto-style4">
               <asp:Button runat="server" Text="cancelar" id="cancelar_cat" OnClick="cancelar_cat_Click" />
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
      <asp:GridView runat="server" CssClass="display" ID="GridViewcat" EnableModelValidation="True" Width="204px" >
  <HeaderStyle Font-Bold="true" />
                    </asp:GridView >
</asp:Content>
