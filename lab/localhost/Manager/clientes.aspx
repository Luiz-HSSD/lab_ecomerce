<%@ Page Title="CLIENTES" Language="C#" MasterPageFile="~/Manager/aspm/principal.Master" AutoEventWireup="true" CodeBehind="clientes.aspx.cs" Inherits="lab.Manager.clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <table>
            <tr>
                <td colspan="4">
                    <h1><b>cadastro de clientes</b></h1>
                </td>
            </tr>
            <tr>
                <td><b>codigo:</b> <asp:Label  ID="codigo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>nome:</b></td>
                <td>
                    <asp:TextBox TextMode="SingleLine" ID="nome" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>sexo:</b></td>
                <td>
                            <asp:DropDownList ID="DropDownListcli" runat="server" DataTextField="sexoid" DataValueField="bd">
                    </asp:DropDownList></td>
                
            </tr>
            <tr>
                <td><b>cpf:</b></td>
                <td>
                    <asp:TextBox TextMode="Number" ID="cpf" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>rg:</b></td>
                <td>
                    <asp:TextBox TextMode="Number" ID="rg" runat="server"></asp:TextBox></td>
                <td><b>data de nascimento:</b></td>
                <td>
                    <asp:TextBox TextMode="Date"  ID="data" runat="server"></asp:TextBox>
                </td> 
                <td><b>email:</b>
                    <asp:TextBox TextMode="Email" ID="email" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>cep:</b></td>
                <td>
                    <asp:TextBox TextMode="SingleLine" ID="cep" MaxLength="9" AutoPostBack="true" OnTextChanged="cep_TextChanged"  runat="server"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td><b>rua:</b></td> <td>
                    <asp:TextBox TextMode="SingleLine" ID="logradouro" runat="server"></asp:TextBox></td>
                <td><b>Numero:</b></td> <td>
                    <asp:TextBox TextMode="Number" ID="numero" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>bairro:</b></td>
                <td>                    
                    <asp:TextBox TextMode="SingleLine" ID="bairro" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>cidade:</b></td>
                <td>
                    <asp:TextBox TextMode="SingleLine" ID="cidade" runat="server"></asp:TextBox></td>
                <td><b>UF:</b></td>
                <td>
                    <asp:DropDownList ID="DropDownListcliuf" runat="server" DataTextField="uf" DataValueField="id">
                    </asp:DropDownList></td>
            </tr>
        </table>

        <table>
            <tr>
            
            <td>
                <asp:Button runat="server" Text="novo" id="novo_cli" OnClick="novo_cli_Click" />
            </td>
            <td>
                <asp:Button runat="server" Text="alterar" id="alterar_cli" OnClick="alterar_cli_Click" />
            </td>
            <td>
               <asp:Button runat="server" Text="cancelar" id="cancelar_cli" OnClick="cancelar_cli_Click" />
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
   
          <asp:GridView runat="server" CssClass="display" ID="GridViewcli" EnableModelValidation="True" Width="204px" >
  <HeaderStyle Font-Bold="true" />
                    </asp:GridView ><br/>
            </div>
  

</asp:Content>
