<%@ Page Title="CLIENTES" Language="C#" MasterPageFile="~/Manager/aspm/principal.Master" AutoEventWireup="true" CodeBehind="clientes.aspx.cs" Inherits="lab.Manager.clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 4px;
        }
    </style>
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
                <td><b>senha:</b></td>
                <td>
                    <asp:TextBox TextMode="Password" ID="senha" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Button ID="mudar" Text="mudar_senha" runat="server" OnClick="mudar_Click"></asp:Button></td>
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
                <td><b>complemento:</b></td>
                <td>                    
                    <asp:TextBox TextMode="SingleLine" ID="complemento" runat="server"></asp:TextBox></td>
                <td><b>Tipo de endereço:</b></td>
                <td>
                    <asp:DropDownList ID="DropDownList_tipo_end" runat="server" DataTextField="tipo" DataValueField="id">
                    </asp:DropDownList>

                </td>
                <tr>
                <td><b>cidade:</b></td>
                <td>
                    <asp:TextBox TextMode="SingleLine" ID="cidade" runat="server"></asp:TextBox></td>
                <td><b>UF:</b></td>
                <td>
                    <asp:DropDownList ID="DropDownListcliuf" runat="server" DataTextField="uf" DataValueField="id">
                    </asp:DropDownList></td>
                
                
            </tr>
            <tr>
                <td>
                <asp:Button runat="server" Text="novo endereco" id="add_endereco" OnClick="add_endereco_Click" />
                </td>
            </tr>
            </table>
                <div id="enderecos" runat="server" style="padding:30px; width: auto; height: auto;">
                <table class="auto-style19">
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                            </tr>
                </table>
                </div>
       <table> 
            <tr>
                <td><b>nome do titular</b></td>
                <td>
                    <asp:TextBox TextMode="SingleLine" ID="nome_titular" runat="server"></asp:TextBox></td>
                <td><b>numero cartão</b></td>
                <td>
                <asp:TextBox TextMode="SingleLine" ID="num_car" MaxLength="16" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td><b>validade</b></td>
                <td>
                    <asp:TextBox TextMode="SingleLine" ID="validade" runat="server"></asp:TextBox></td>
                <td><b>ccv</b></td>
                <td>
                <asp:TextBox TextMode="SingleLine" ID="ccv" runat="server"></asp:TextBox>

                    <asp:CheckBox ID="CHK_preferencial" runat="server" />

                </td>
            </tr>
              <tr>
                <td>
                <asp:Button runat="server" Text="novo cartão" id="novo_cartao" OnClick="novo_cartao_Click"  />
                </td>
                <td>
                <asp:Label runat="server"  id="erro_cartao"   />
                </td>
            </tr>
        </table>
                <div id="Cartoes" runat="server" style="padding:30px; width: 998px; height: 1px;">
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
                    <br />
                    <br />
                </div>
    <br />
        <div>
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
            </div>
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
   

            </div>
           <asp:GridView runat="server" CssClass="display" ID="GridViewcli" EnableModelValidation="True" >
  <HeaderStyle Font-Bold="true" />
                    </asp:GridView ><br/>

</asp:Content>
