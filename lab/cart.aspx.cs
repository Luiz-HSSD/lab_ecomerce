using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab
{
    public partial class cart : viewgenerico
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void calc_frete_Click(object sender, EventArgs e)
        {

            /*
            var oXmlHttp = Server.CreateObject("Microsoft.XMLHTTP");
            string sSoapServer = @"http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx";
            string peso, comprimento, altura, largura, diametro, valordeclarado;
            if (request.form("peso") == "") peso = "0";
            else peso = request.form("peso");
            if( request.form("comprimento") == "")
comprimento = "0"
else
comprimento = request.form("comprimento")
end if
if request.form("altura") = "" then
altura = "0"
else
altura = request.form("altura")
end if
if request.form("largura") = "" then
largura = "0"
else
largura = request.form("largura")
end if
if request.form("diametro") = "" then
diametro = "0"
else
diametro = request.form("diametro")
end if
if request.form("valordeclarado") = "" then
valordeclarado = "0"
else
valordeclarado = request.form("valordeclarado")
end if
*/
        }
    }
}