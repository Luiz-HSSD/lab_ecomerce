// MBACCARO
// Verifica ó último texto do campo.
function IsNumeric(campo) {

 var text = document.getElementById(campo).value;
 text = text.substring((text.length - 1));

 if (!/\D/.test(text)) {
  return true;
 } else {
  return false;
 }

}

// Formata data no onkeypress
// Parâmetro. Exemplo: onkeypress="DateFormat(this, event);"
function DateFormat(campo, e) {

 var key = window.event ? e.keyCode : e.which;

 if (key > 31 && (key < 48 || key > 57)) {
  return false;
 } else {
  if (key != 8) {
   if (campo.value.length == 2) {
    campo.value += '/';
   }
   if (campo.value.length == 5) {
    campo.value += '/';
   }
  }

  var keychar = String.fromCharCode(key);
  return keychar;

 }

}

function TimeFormat(campo, e) {

 var key = window.event ? e.keyCode : e.which;

 if (key > 31 && (key < 48 || key > 57)) {
  return false;
 } else {
  if (key != 8) {
   if (campo.value.length == 2) {
    campo.value += ':';
   }
  }
 }
}

function DateValidation(date) {

    var reDate5 = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/;

 if (reDate5.test(date)) {
  return true;
 } 
  return false;
}

function ValidateNavigator(e) {

 if (!e) {
  if (window.event) {
   //DOM
   e = window.event;
  } else {
   //TOTAL FAILURE, WE HAVE NO WAY OF REFERENCING THE EVENT
   return null;
  }
 }
 if (typeof (e.which) == 'number') {
  //NS 4, NS 6+, Mozilla 0.9+, Opera
  e = e.which;
  if (e == '8') return e;
 } else if (typeof (e.keyCode) == 'number') {
  //IE, NS 6+, Mozilla 0.9+
  e = e.keyCode;
  if (e == '8') return e;
 } else if (typeof (e.charCode) == 'number') {
  //also NS 6+, Mozilla 0.9+
  e = e.charCode;
  if (e == '8') return e;
 } else {
  //TOTAL FAILURE, WE HAVE NO WAY OF OBTAINING THE KEY CODE
  return null;
 }
 return e;
}

// Parâmetro e = event
function onKeyPressAllowNumero(e) {

 var key = ValidateNavigator(e);

 if (key == "8" || key == "0") {
  return true;
 }

 var keychar = String.fromCharCode(key);

 if (!/\D/.test(keychar)) {
  return true;
 } else {
  return false;
 }
}

function ValitionCEP(text) {
    var i = text.value.length;
    var retorno = true;

    if (i < 9 && i > 1) {
        jAlert("Formato de CEP inválido!", "Aviso", "");
        setTimeout(function() { text.select(); }, 10);
        retorno = false;
    }

    return retorno;

}

// Formata CEP no onkeypress
// Parâmetro. Exemplo: onkeypress="CepFormat(this, event);"
function CepFormat(campo, e) {
 var key = window.event ? e.keyCode : e.which;

 if (key > 31 && (key < 48 || key > 57))
  return false;
 else {
  if (key != 9) {
   if (campo.value.length == 5) {
    campo.value += "-"
   }
  }
  var keychar = String.fromCharCode(key);
  return keychar;
 }
}


// Usar this e event como parametro
function onkeypressFormataCNPJ(Campo, teclapres) {
 var tecla = window.event ? teclapres.keyCode : teclapres.which; ;

 var keychar = String.fromCharCode(tecla);
 var vr = new String(Campo.value);
 vr = vr.replace(".", "");
 vr = vr.replace(".", "");
 vr = vr.replace("/", "");
 vr = vr.replace("-", "");

 tam = vr.length + 1;


 if (tecla != 9 && tecla != 8 && tecla != 118) {
  if (tam > 2 && tam < 6)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, tam);
  if (tam >= 6 && tam < 9)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, tam - 5);
  if (tam >= 9 && tam < 13)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, 3) + '/' + vr.substr(8, tam - 8);
  if (tam >= 13 && tam < 15)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, 3) + '/' + vr.substr(8, 4) + '-' + vr.substr(12, tam - 12);
 } else {
  return true;
 }

 reg = /[0-9]/;
 return reg.test(keychar);
}

function FormataCNPJ(Campo, teclapres) {
 var tecla = teclapres.keyCode;
 var vr = new String(Campo.value);
 vr = vr.replace(".", "");
 vr = vr.replace(".", "");
 vr = vr.replace("/", "");
 vr = vr.replace("-", "");

 tam = vr.length + 1;

 if (Campo.value != null && Campo.value != "") {
  //if (tecla != 9 && tecla != 8)
  //{			
  if (tam > 1 && tam < 6)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, tam);
  if (tam >= 6 && tam < 9)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, tam - 5);
  if (tam >= 9 && tam < 13)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, 3) + '/' + vr.substr(8, tam - 8);
  if (tam >= 13)
   Campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, 3) + '/' + vr.substr(8, 4) + '-' + vr.substr(12, tam - 12);
  //}
 }
}

// Formata campo hora.
// Parâmetros: campo - this, e - events
function FormatTime(campo, e) {
 var key = window.event ? e.keyCode : e.which;

 if (key > 31 && (key < 48 || key > 57))
  return false;
 else {
  if (key != 8) {
   if (campo.value.length == 2) {
    campo.value += ":"
   }
  }
  var keychar = String.fromCharCode(key);
  return keychar;
 }
}

// Valida hora
// Parâmetros. campo - this
function ValidateTime(campo) {
 if (campo.value != "") {
  if (campo.value.length == 5 || campo.value.length == 4) {
   var hour, minute;
   if (campo.value.length == 5) {
    hour = campo.value.substring(0, 2);
    minute = campo.value.substring(3, 5);
   } else if (campo.value.length == 4) {
    hour = campo.value.substring(0, 1);
    minute = campo.value.substring(2, 4);
   }
   var currentDate = new Date();
   var error = 0;
   try {
    if (hour >= 0 && hour < 24)
     error += 0;
    else
     error += 1;

    if (minute >= 0 && minute < 60)
     error += 0;
    else
     error += 2;

    if (error > 0) {
     alert('Digite um horário válido!');
     setTimeout(function() { campo.value = ''; }, 10);
     return false;
    }
   } catch (err) {
    alert('Digite um horário válido!');
    setTimeout(function() { campo.value = ''; }, 10);
    return false;

   }
  } else {
   alert('Digite um horário válido!');
   setTimeout(function() { campo.value = ''; }, 10);
   return false;
  }
 }

 return true;
}


// ---permite somente a digitação de números e 1 virgula no campo
function FormatDecimal(e, nome, qtdDec, qtdInt) {
 if (document.all) {
  // Internet Explorer
  var tecla = event.keyCode;
 }
 else if (document.layers) {
  // Nestcape
  var tecla = e.which;
 }
 if (nome.value.indexOf(",") != -1 && tecla == 44) {
  event.keyCode = 0;
  return false;
 }
 else if (tecla == 44) {
  return true;
 }

 var strValor = nome.value;
 if (strValor.indexOf(",") != -1) {
  strValor = strValor.substr(0, strValor.indexOf(","));
  if (strValor.length > qtdInt) {
   event.keyCode = 0;
   return false;
  }
 } else if (strValor.length >= qtdInt) {
  if (tecla == 44) {
   return true;
  }
  event.keyCode = 0;
  return false;
 }

 // numeros de 0 a 9, - e ,
 if (tecla > 47 && tecla < 58 || tecla == 44) {
  // numeros de 0 a 9, - e ,
  if (nome.value.indexOf(",") != -1 && tecla == 44) {
   event.keyCode = 0;
   return false;
  }
  else {
   return true;
  }
 }
 else {
  if (tecla != 8) {
   // backspace 
   event.keyCode = 0;
   return false;
  }
  else {
   return true;
  }
 }
}

// Formata on blur
function FormatDecimal_Blur(e, nome, qtdDec, qtdInt) {
 var i, c, blnVirg;
 blnVirg = false;

 for (i = 0; i < nome.value.length; i++) {
  //alert(nome.value.substr(i, 1));
  c = nome.value.substr(i, 1);

  //alert(c > 0);

  if (!(c <= 9 && c >= 0) || c == ' ') {
   if (c == ',') {
    if (blnVirg) {
     nome.value = nome.value.substr(0, i) + nome.value.substr(i + 1) + '';
     i--;
    }
    else {
     if (i == 0 || (i == 1 && nome.value.substr(0, 1) == '-')) {
      nome.value = nome.value.substr(0, i) + nome.value.substr(i + 1) + '';
      i--;
     }
     else if (i < (nome.value.length - qtdDec)) {
      if (i + qtdDec + 1 < nome.value.length) {
       nome.value = nome.value.substr(0, i + qtdDec + 1);
       //i--;
      }
      blnVirg = true;
     }
     else {
      blnVirg = true;
     }
    }
   }
   else {
    nome.value = nome.value.substr(0, i) + nome.value.substr(i + 1) + '';
    i--;
   }
  }
 }

 if (nome.value == '-') {
  alert("Valor digitado inválido!");
  nome.focus();
 }

 if (nome.value.indexOf(",") != -1) {
  if (nome.value.indexOf(",") == (nome.value.length - 1)) {
   nome.value = nome.value.substr(0, nome.value.length - 1);
  }
 }

 var strValor = nome.value.replace('-', '');
 var strZeros;
 strZeros = '';
 for (i = 0; i < eval(qtdInt); i++) {
  strZeros += '0';
 }
 if (strValor.indexOf(",") != -1) {
  strValor = strValor.substr(0, strValor.indexOf(","));
 }

 if (strValor.length > qtdInt) {
  alert("O valor digitado pode conter somente " + qtdInt + " casas inteiras!");
  nome.focus();
 }
}



/*--------------------------- Formata Moeda ----------------------------------*/

function FormataValor(fld, milSep, decSep, e) {
 var sep = 0;
 var key = '';
 var i = j = 0;
 var len = len2 = 0;
 var strCheck = '0123456789';
 var aux = aux2 = '';
 var whichCode = ValidateNavigator(e); //(window.Event.value) ? e.which : e.keyCode;

 if (whichCode == 13) return true;
 key = String.fromCharCode(whichCode);  // Valor para o código da Chave
 if (strCheck.indexOf(key) == -1) return false;  // Chave inválida
 len = fld.value.length;
 for (i = 0; i < len; i++)
  if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
 aux = '';
 for (; i < len; i++)
  if (strCheck.indexOf(fld.value.charAt(i)) != -1) aux += fld.value.charAt(i);
 aux += key;
 len = aux.length;
 if (len == 0) fld.value = '';
 if (len == 1) fld.value = '0' + decSep + '0' + aux;
 if (len == 2) fld.value = '0' + decSep + aux;
 if (len > 2) {
  aux2 = '';
  for (j = 0, i = len - 3; i >= 0; i--) {
   if (j == 3) {
    aux2 += milSep;
    j = 0;
   }
   aux2 += aux.charAt(i);
   j++;
  }
  fld.value = '';
  len2 = aux2.length;
  for (i = len2 - 1; i >= 0; i--)
   fld.value += aux2.charAt(i);
  fld.value += decSep + aux.substr(len - 2, len);
 }
 return false;
}

function LimitaNumero2_Blur(e, nome, qtdDec, qtdInt) {
 var i, c, blnVirg;
 blnVirg = false;

 for (i = 0; i < nome.value.length; i++) {
  c = nome.value.substr(i, 1);

  //alert(c > 0);

  if (!(c <= 9 && c >= 0) || c == ' ') {
   if (c == ',') {
    if (blnVirg) {
     nome.value = nome.value.substr(0, i) + nome.value.substr(i + 1) + '';
     i--;
    }
    else {
     if (i == 0 || (i == 1 && nome.value.substr(0, 1) == '-')) {
      nome.value = nome.value.substr(0, i) + nome.value.substr(i + 1) + '';
      i--;
     }
     else if (i < (nome.value.length - qtdDec)) {
      if (i + qtdDec + 1 < nome.value.length) {
       nome.value = nome.value.substr(0, i + qtdDec + 1);
       //i--;
      }
      blnVirg = true;
     }
     else {
      blnVirg = true;
     }
    }
   }
   else {
    nome.value = nome.value.substr(0, i) + nome.value.substr(i + 1) + '';
    i--;
   }
  }
 }

 if (nome.value == '-') {
  alert("Valor digitado inválido!");
  nome.focus();
 }

 if (nome.value.indexOf(",") != -1) {
  if (nome.value.indexOf(",") == (nome.value.length - 1)) {
   nome.value = nome.value.substr(0, nome.value.length - 1);
  }
 }

 var strValor = nome.value.replace('-', '');
 var strZeros;
 strZeros = '';
 for (i = 0; i < eval(qtdInt); i++) {
  strZeros += '0';
 }
 if (strValor.indexOf(",") != -1) {
  strValor = strValor.substr(0, strValor.indexOf(","));
 }
 var qtd;
 qtd = qtdInt - qtdDec;
 if (strValor.length > qtd) {
  alert("O valor digitado pode conter somente " + qtd + " casas inteiras!");
  nome.focus();
 }
}

function formataValorMonetario(campooriginal, decimais) {
 var posicaoPontoDecimal;
 var campo = '';
 var resultado = '';
 var pos, sep, dec;

 //Retira possiveis separadores de milhar
 for (pos = 0; pos < campooriginal.value.length; pos++) {
  if (campooriginal.value.charAt(pos) != '.')
   campo = campo + campooriginal.value.charAt(pos);
 }

 //Formata valor monetário com decimais
 posicaoPontoDecimal = campo.indexOf(',');
 if (posicaoPontoDecimal != -1) {
  sep = 0;
  for (pos = posicaoPontoDecimal - 1; pos >= 0; pos--) {
   sep++;
   if (sep > 3) {
    resultado = '.' + resultado;
    sep = 1;
   }

   resultado = campo.charAt(pos) + resultado;
  }

  // Trata parte decimal
  if (parseInt(decimais) > 0) {
   resultado = resultado + ',';

   pos = posicaoPontoDecimal + 1;
   for (dec = 1; dec <= parseInt(decimais); dec++) {
    if (pos < campo.length) {
     resultado = resultado + campo.charAt(pos);
     pos++;
    }
    else
     resultado = resultado + '0';
   }

  } // trata decimais
 }
 // Trata valor monetário sem decimais
 else {
  if (campooriginal.value != "") {
   sep = 0;
   for (pos = campo.length - 1; pos >= 0; pos--) {
    sep++;
    if (sep > 3) {
     resultado = '.' + resultado;
     sep = 1;
    }
    resultado = campo.charAt(pos) + resultado;
   }
   // Trata parte decimal
   if (parseInt(decimais) > 0) {
    resultado = resultado + ',';
    for (dec = 1; dec <= parseInt(decimais); dec++) {
     resultado = resultado + '0';
    }
   } // trata decimais
  }
 }
 campooriginal.value = resultado;
}



/*****************************************************************************/
