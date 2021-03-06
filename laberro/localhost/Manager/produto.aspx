﻿<%@ Page Title="PRODUTOS" Language="C#" MasterPageFile="~/Manager/aspm/principal.Master" AutoEventWireup="true" CodeBehind="produto.aspx.cs" Inherits="lab.Manager.produto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <table border="1">
    <tr><td colspan="5"><h1>cadastro produtos</h1></td></tr>
    <tr><td colspan="2"><b>codigo</b></td> <td colspan="3"><asp:Label  ID="codigo" runat="server"></asp:Label></td></tr>
    <tr><td colspan="2">nome resumido:</td> <td colspan="3"><asp:TextBox MaxLength="50" TextMode="SingleLine" ID="nome" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan="2">descrição:</td> <td colspan="3"><asp:TextBox MaxLength="1000" TextMode="MultiLine" ID="descricao" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan="2">categoria:</td> <td colspan="3">
        <asp:DropDownList ID="DropDownListcat" runat="server" DataTextField="nome_cat" DataValueField="cod">
        </asp:DropDownList>
        </td></tr>
    <tr><td colspan="2"><b>codigo de barras: </b></td> <td colspan="3"><asp:TextBox MaxLength="13" ID="codigo_de_barra" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan="2">fabricante:</td> <td colspan="3"><asp:TextBox MaxLength="60"  TextMode="SingleLine" ID="fabricante" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan="2"><b aria-dropeffect="none">preço: </b></td> <td colspan="3"><asp:TextBox MaxLength="8" ID="preco" runat="server"></asp:TextBox></td></tr>
    <tr><td colspan="2"><b aria-dropeffect="none">peso: </b></td> <td colspan="3"><asp:TextBox MaxLength="8" ID="peso" runat="server"></asp:TextBox></td></tr>
<tr><td colspan="2"><b aria-dropeffect="none">dimensoes: </b></td> <td colspan="3"><asp:TextBox MaxLength="100" ID="dimensoes" runat="server"></asp:TextBox></td></tr>     
    <tr><td colspan="2"><b>imagem </b></td> <td colspan="3"><asp:FileUpload  ID="foto" AllowMultiple="false"  runat="server" Font-Names="pega"></asp:FileUpload></td><td><asp:Button ID="subir" runat="server" Text="Button" OnClick="subir_Click" /></td></tr>
    
    <tr><td colspan="3">    
        <asp:Label ID="lblStatus" style="color: Red;" text="&nbsp;" runat="server" />

    <br /><br />
    <asp:Button ID="btnDelete" Text="Delete" runat="server" onclick="btnDelete_Click" /><br /><br />

        <asp:Repeater ID="rptrUserPhotos" runat="server">
        <ItemTemplate>
            <span class="saucer" style="float: left; padding: 15px; ">

            <a rel="example_group" href="<%# Container.DataItem %>" title="">
            <asp:ImageButton  ImageUrl="<%# Container.DataItem %>" ID="imgUserPhoto" style="width: 100px; height: 100px;" runat="server" /><br />
            </a>
            
            <asp:CheckBox special="<%# Container.DataItem %>" ID="cbDelete" Text="Delete" runat="server" /><br />
            </span>
        </ItemTemplate>
    </asp:Repeater>

        </td></tr>
    </table>

    <table>
    <tr>
    
            <td>
                <asp:Button runat="server" Text="novo" id="novo_pro" OnClick="novo_pro_Click" />
            </td>
            <td>
                <asp:Button runat="server" Text="alterar" id="alterar_pro" OnClick="alterar_pro_Click" />
            </td>
            <td>
               <asp:Button runat="server" Text="cancelar" id="cancelar_pro" OnClick="cancelar_pro_Click" />
            </td>

    </tr>
    <tr>
        <td colspan="3">
        <asp:Label ID="msg" runat="server" />    
        </td>
    </tr>
    </table>
            <br />
          
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
    <div id="divTable" runat="server" style="padding:30px; width: 998px; height: 1px;">
              <asp:GridView runat="server" CssClass="display" ID="GridViewpro" EnableModelValidation="True" Width="204px" >

                    </asp:GridView><br />
        
      </div>
</asp:Content>
