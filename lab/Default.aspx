<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="lab._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div>
<section id="ViewLiv" class="ViewLiv" runat="server">

</section>
</div>
<style type="text/css">
    .ViewLiv 
    {
      float:left;  
      width:100%;

    }
    .ViewLiv section
    {
        width: 45%;
        height: 50%;
      float:left;
      text-align:center;
      font: 19px arial, sans-serif;
      color:white;
      background-color:red;
      margin:15px;  
    }
</style>
</asp:Content>
