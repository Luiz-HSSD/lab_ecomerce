// Globais
var control, controls;
var time;

// Close application.
function CloseApp() {
    document.location.href = 'finally.siss';
    return false;
}

function Get(name) {
    return document.getElementById(name);
}

function ShowPopup(url, width, height) {
    var x = ((screen.width / 2) - (width / 2));
    var y = ((screen.height / 2) - (height / 2));
    return window.open(url, '_blank', 'top=' + y + 'px,left=' + x + 'px,width=' + width + 'px,height=' + height + 'px;menubar=no,location=no,resizable=no,scrollbars=no,status=no');
}

function ShowPopupModal(url, width, height) {
    return window.showModalDialog(url, '', 'dialogWidth:' + width + 'px;dialogHeight:' + height + 'px;status:no;toolbar:no;resizable=no;scroll:no');
}

// Enable controls in the page.
function Enable() {

    controls = Get('container').getElementsByTagName('input');

    for (var i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            // asp.net cria um span para os atributos não identificados no checkboxs, parentNode busca esse span
            if (controls[i].parentNode.attributes["ExceptionEnable"] == undefined || controls[i].parentNode.attributes["ExceptionEnable"].value == 'true') {
                controls[i].disabled = false;
            } else {
                controls[i].disabled = true;
            }
        } else {
            controls[i].disabled = true;
        }

    }

    controls = Get('container').getElementsByTagName('select');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }

    controls = Get('container').getElementsByTagName('a');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }

    controls = Get('container').getElementsByTagName('table');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }

    controls = Get('container').getElementsByTagName('li');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }

    controls = Get('container').getElementsByTagName('ul');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }
    controls = Get('container').getElementsByTagName('textarea');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }

    controls = Get('container').getElementsByTagName('img');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }

    controls = Get('container').getElementsByTagName('div');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == true || controls[i].getAttribute('ExceptionEnable') == 'true' || controls[i].getAttribute('ExceptionEnable') == null) {
            controls[i].disabled = false;
        } else {
            controls[i].disabled = true;
        }
    }


}

// Disable controls in the page.
function Disable() {

    controls = Get('container').getElementsByTagName('input');

    for (var i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            // asp.net cria um span para os atributos não identificados no checkboxs, parentNode busca esse span
            if (controls[i].parentNode.attributes["ExceptionEnable"] == undefined || controls[i].parentNode.attributes["ExceptionEnable"].value == 'false') {
                controls[i].disabled = true;
            } else {
                controls[i].disabled = false;
            }
        }

    }

    controls = Get('container').getElementsByTagName('select');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            controls[i].disabled = true;
        } else {
            controls[i].disabled = false;
        }
    }

    controls = Get('container').getElementsByTagName('a');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            controls[i].disabled = true;
        } else {
            controls[i].disabled = false;
        }
    }

    controls = Get('container').getElementsByTagName('table');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            controls[i].disabled = true;
        } else {
            controls[i].disabled = false;
        }
    }

    controls = Get('container').getElementsByTagName('li');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            controls[i].disabled = true;
        } else {
            controls[i].disabled = false;
        }
    }

    controls = Get('container').getElementsByTagName('ul');

    for (i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            controls[i].disabled = true;
        } else {
            controls[i].disabled = false;
        }
    }

    controls = Get('container').getElementsByTagName('textarea');

    for (var i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            // asp.net cria um span para os atributos não identificados no checkboxs, parentNode busca esse span
            if (controls[i].parentNode.attributes["ExceptionEnable"] == undefined || controls[i].parentNode.attributes["ExceptionEnable"].value == 'false') {
                controls[i].disabled = true;
            } else {
                controls[i].disabled = false;
            }
        }

    }


    controls = Get('container').getElementsByTagName('img');

    for (var i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            // asp.net cria um span para os atributos não identificados no checkboxs, parentNode busca esse span
            if (controls[i].parentNode.attributes["ExceptionEnable"] == undefined || controls[i].parentNode.attributes["ExceptionEnable"].value == 'false') {
                controls[i].disabled = true;
            } else {
                controls[i].disabled = false;
            }
        }

    }
    controls = Get('container').getElementsByTagName('div');

    for (var i = 0; i < controls.length; i++) {
        if (controls[i].getAttribute('ExceptionEnable') == null || controls[i].getAttribute('ExceptionEnable') == false || controls[i].getAttribute('ExceptionEnable') == 'false') {
            // asp.net cria um span para os atributos não identificados no checkboxs, parentNode busca esse span
            if (controls[i].parentNode.attributes["ExceptionEnable"] == undefined || controls[i].parentNode.attributes["ExceptionEnable"].value == 'false') {
                controls[i].disabled = true;
            } else {
                controls[i].disabled = false;
            }
        }

    }
}

// Clear values of the controls in the page.
function Clear(container) {

    if (container == null) {
        control = document.getElementById('container');
    } else {
        control = container;
    }

    controls = control.getElementsByTagName('input');

    for (var i = 0; i < controls.length; i++) {

        if (controls[i].type == 'text') {
            controls[i].value = '';
        } else if (controls[i].type == 'checkbox') {

            if (controls[i].getAttribute('ExceptionClear') == null || controls[i].getAttribute('ExceptionClear') == false || controls[i].getAttribute('ExceptionClear') == "false") {

                // asp.net cria um span para os atributos não identificados no checkboxs, parentNode busca esse span
                if (controls[i].parentNode.attributes["ExceptionClear"] == null || controls[i].parentNode.attributes["ExceptionClear"].value == 'false') {
                    controls[i].checked = false;
                }
                else {
                    controls[i].checked = true;
                }
            }
        }
    }

    controls = control.getElementsByTagName('textarea');

    for (var i = 0; i < controls.length; i++) {
        controls[i].value = '';
    }

    controls = control.getElementsByTagName('select');

    for (i = 0; i < controls.length; i++) {

        if (controls[i].getAttribute('ExceptionClear') == null || controls[i].getAttribute('ExceptionClear') == false) {
            controls[i].value = "";
        }

    }

}

// Clear Dropdown.
function ClearDropDown(id) {
    var selectObj = document.getElementById(id);
    var selectParentNode = selectObj.parentNode;
    var newSelectObj = selectObj.cloneNode(false);
    selectParentNode.replaceChild(newSelectObj, selectObj);
    return newSelectObj;
}

function ShowReportViewer(isPopup) {

    if (isPopup) {
        ShowPopup('../../reportviewer', 800, 600);
    } else {
        window.open('../../reportviewer');
    }

}

///	<summary>
/// Método que aciona o postback com update panel.
///	</summary>
///<param>
///  eventArgument:Parâmetro livre opcional.
///  controlId:Id do controle existente dentro do update panel para funcionar o post assíncrono.
///  requiredValidate:True/False se requer acionar a validação dos controles obrigatórios da página.
///	</param>
function CallPostBack(eventArgument, controlId, requiredValidate) {

    if (requiredValidate != null && requiredValidate) {

        var formid = '#' + document.forms[0].id;
        var validateResult = $(formid).validationEngine({ returnIsValid: true });

        if (validateResult) {
            // Submit assíncrono do update panel. 
            __doPostBack(controlId, eventArgument);
        }

        return validateResult;
    } else {
        __doPostBack(controlId, eventArgument);
    }

}

///	<summary>
/// Método que aciona o postback com update panel.
///	</summary>
function CallValidateForm(allRequiredFields) {

    if (allRequiredFields) {
        EnableRequiredFields();
    }

    var formid = '#' + document.forms[0].id;
    return $(formid).validationEngine({ returnIsValid: true });

}

// Functions for Wizard.
function ShowDescription(description) {
    if (Get('ctl00_operationsMenu_txtWizardDescription') != null) {
        Get('ctl00_operationsMenu_txtWizardDescription').value = description;
    }
}

function ClearDescription() {
    if (Get('ctl00_operationsMenu_txtWizardDescription') != null) {
        Get('ctl00_operationsMenu_txtWizardDescription').value = '';
    }
}

// Registry BaseValidation class.
Function.prototype.inherits = function (DefaultValidation) {
    this.prototype = new DefaultValidation;
    this.prototype.Base = DefaultValidation.prototype;
    this.prototype.constructor = this.constructor;
};

// Class with validations default.
function DefaultValidation() { }

// Method with validations default.
DefaultValidation.prototype.Validation = function (requiredElements) {
    return RequiredFields();
}

// dropdownlist: Nome do dropdownlist
// obj:Dictionary de <string, string>
function CarregarDropDownList(dropdownlist, result) {

    var control = Get(dropdownlist);

    if (control != null) {
        control.options.length = 0;

        var item = document.createElement("option");
        item.text = '';
        item.value = '';
        control.options.add(item);

        lista = result.value != null ? result.value : result;

        if (lista != null && lista.values != null) {

            for (var i = 0; i < lista.values.length; i++) {
                var opt = document.createElement('option');
                opt.value = lista.keys[i];
                opt.text = lista.values[i];
                control.options.add(opt);
            }
        }
    }
}

function ClearDropDownList(id) {
    var selectObj = document.getElementById(id);
    var selectParentNode = selectObj.parentNode;
    var newSelectObj = selectObj.cloneNode(false);
    selectParentNode.replaceChild(newSelectObj, selectObj);
    return newSelectObj;
}



function ImplementsSession(min) {

    try {
        if (window.parent.length == 0) {
            ImplementsSessionTimout(min);
        } else {
            window.parent.ImplementsSessionTimout(min);
        }
    } catch (ex) { }

    window.clearTimeout(time);
    time = window.setTimeout(function () { window.location.href = '../../finally.siss' }, (min * 60 * 1000));
}


// PROTOTYPE
function LoadPrototypeArray() {

    Array.prototype.indexOf = function (obj) {

        for (var i = 0; i < this.length; i++) {

            if (this[i] == obj) {
                return i;
            }

        }

        return -1;
    }

}

// Esse método não está no prototype porque o IE implementava,
// porém não funcionava.
function Splice(array, start, size) {

    if (array.length > 0) {

        var result = new Array();
        var aux = 0;

        for (var i = 0; i < array.length; i++) {
            if (i != start) {
                result[aux] = array[i];
                aux++;
            } else {
                start++;
                if (start >= size) {
                    start = -1;
                }
            }
        }

        return result;

    } else {
        return null;
    }

}

function toggleDisabled(el) {

    if (el.nodeName != '#text') {

        el.disabled = true;

        if (el.childNodes && el.childNodes.length > 0) {
            for (var x = 0; x < el.childNodes.length; x++) {
                toggleDisabled(el.childNodes[x]);
            }
        }
    }
}

function toggleEnabled(el) {

    if (el.nodeName != '#text') {

        el.disabled = false;

        if (el.childNodes && el.childNodes.length > 0) {
            for (var x = 0; x < el.childNodes.length; x++) {
                toggleEnabled(el.childNodes[x]);
            }
        }
    }
}

function DisableContainer(id) {
    control = document.getElementById(id);
    toggleDisabled(control);
    control.style.opacity = 0.7;
}

function EnabledContainer(id) {
    control = document.getElementById(id);
    toggleEnabled(control);
    control.style.opacity = 1.0;

}

// Centralização de elemento no browser.
window.size = function () {

    var w = 0;
    var h = 0;

    if (!window.innerWidth) {

        if (!(document.documentElement.clientWidth == 0)) {
            w = document.documentElement.clientWidth;
            h = document.documentElement.clientHeight;
        } else {
            w = document.body.clientWidth;
            h = document.body.clientHeight;
        }

    } else {
        w = window.innerWidth;
        h = window.innerHeight;
    }

    return { width: w, height: h };

}

window.center = function () {

    var hWnd = (arguments[0] != null) ? arguments[0] : { width: 0, height: 0 };
    var _x = 0;
    var _y = 0;
    var offsetX = 0;
    var offsetY = 0;

    if (!window.pageYOffset) {

        if (!(document.documentElement.scrollTop == 0)) {
            offsetY = document.documentElement.scrollTop;
            offsetX = document.documentElement.scrollLeft;
        } else {
            offsetY = document.body.scrollTop;
            offsetX = document.body.scrollLeft;
        }

    } else {
        offsetX = window.pageXOffset;
        offsetY = window.pageYOffset;
    }

    _x = ((this.size().width - hWnd.width) / 2) + offsetX;
    _y = ((this.size().height - hWnd.height) / 2) + offsetY;
    return { x: _x, y: _y };

}

function CompareDate(prm1, prm2) {

    var dtaIni = new Date(prm1.substring(6, 10), prm1.substring(3, 5) - 1, prm1.substring(0, 2));
    var dtaFim = new Date(prm2.substring(6, 10), prm2.substring(3, 5) - 1, prm2.substring(0, 2));
    var total = (dtaFim.getTime() - dtaIni.getTime());

    if (total <= 0) {
        return true;
    } else {
        return false;
    }

}


// Adiciona método para acrescentar valor na hora em objetos Date.
Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + h);
    return this;
}

// Algoritmo customizado, que verifica se o dia atual está dentro do período de horário de verão.
// Caso o micro esteja com o horário ajustado retorna true, senão false.
function VerifyDaylight() {

    var year = new Date().getYear();
    if (year < 1000)
        year += 1900;

    var firstSwitch = 0;
    var lastOffset = 99;

    for (i = 0; i < 12; i++) {
        var newDate = new Date(Date.UTC(year, i, 0, 0, 0, 0, 0));
        var tz = -1 * newDate.getTimezoneOffset() / 60;
        if (tz > lastOffset)
            firstSwitch = i - 1;

        lastOffset = tz;
    }

    var firstDstDate = FindDstSwitchDate(year, firstSwitch);

    if (firstDstDate == null) {
        // Daylight Savings is not observed in your timezone.
        return false;
    } else {
        return true;
    }

}

function FindDstSwitchDate(year, month) {

    var baseDate = new Date(Date.UTC(year, month, 0, 0, 0, 0, 0));
    var changeDay = 0;
    var changeMinute = -1;
    var baseOffset = -1 * baseDate.getTimezoneOffset() / 60;
    var dstDate;

    for (day = 0; day < 50; day++) {
        var tmpDate = new Date(Date.UTC(year, month, day, 0, 0, 0, 0));
        var tmpOffset = -1 * tmpDate.getTimezoneOffset() / 60;

        if (tmpOffset != baseOffset) {
            var minutes = 0;
            changeDay = day;

            tmpDate = new Date(Date.UTC(year, month, day - 1, 0, 0, 0, 0));
            tmpOffset = -1 * tmpDate.getTimezoneOffset() / 60;

            while (changeMinute == -1) {
                tmpDate = new Date(Date.UTC(year, month, day - 1, 0, minutes, 0, 0));
                tmpOffset = -1 * tmpDate.getTimezoneOffset() / 60;

                if (tmpOffset != baseOffset) {
                    tmpOffset = new Date(Date.UTC(year, month, day - 1, 0, minutes - 1, 0, 0));
                    changeMinute = minutes;
                    break;
                }
                else
                    minutes++;
            }

            dstDate = tmpOffset.getMonth() + 1;
            if (dstDate < 10) dstDate = "0" + dstDate;

            dstDate += '/' + tmpOffset.getDate() + '/' + year + ' ';

            tmpDate = new Date(Date.UTC(year, month, day - 1, 0, minutes - 1, 0, 0));
            dstDate += tmpDate.toTimeString().split(' ')[0];
            return dstDate;

        }

    }

}

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
		    val = String(val);
		    len = len || 2;
		    while (val.length < len) val = "0" + val;
		    return val;
		};

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
			    d: d,
			    dd: pad(d),
			    ddd: dF.i18n.dayNames[D],
			    dddd: dF.i18n.dayNames[D + 7],
			    m: m + 1,
			    mm: pad(m + 1),
			    mmm: dF.i18n.monthNames[m],
			    mmmm: dF.i18n.monthNames[m + 12],
			    yy: String(y).slice(2),
			    yyyy: y,
			    h: H % 12 || 12,
			    hh: pad(H % 12 || 12),
			    H: H,
			    HH: pad(H),
			    M: M,
			    MM: pad(M),
			    s: s,
			    ss: pad(s),
			    l: pad(L, 3),
			    L: pad(L > 99 ? Math.round(L / 10) : L),
			    t: H < 12 ? "a" : "p",
			    tt: H < 12 ? "am" : "pm",
			    T: H < 12 ? "A" : "P",
			    TT: H < 12 ? "AM" : "PM",
			    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
			    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
			    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
} ();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
    monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};

function SetFrame() {
    Get('teste2_frmFormulario').src = "../upload/UploadAjuste.aspx";
}
