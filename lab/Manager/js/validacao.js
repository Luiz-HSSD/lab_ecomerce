$(document).ready(function () {

    //      $("#login").mask("*****************");
    //     $("#senha").mask("**********");
   // CreateTableExort("#GridViewven");
    //CreateTableExort("#GridViewcli");
    //CreateTableExort("#GridViewpro");
    $('#GridViewcat').DataTable({
        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "sSwfPath": "/Manager/swf/copy_csv_xls_pdf.swf"
        }
    });
    $("#cpf").mask("999.999.999-99");
    $("#rg").mask("99.999.999-A");
    $("#codigo").mask("9999999999999999");

});
