alert('teste');
(function($) {

    $.fn.validationEngine = function(settings) {

        allRules = $.validationEngineLanguage.allRules;

        settings = jQuery.extend({
            allrules: allRules,
            totals: 0,
            validationEventTriggers: "blur",
            inlineValidation: true,
            returnIsValid: false,
            animateSubmit: true,
            unbindEngine: true,
            ajaxSubmit: false,
            promptPosition: "topRight",
            success: false,
            failure: function() { }
        }, settings);

        $.validationEngine.settings = settings;

        if (settings.inlineValidation == true) {

            if (!settings.returnIsValid) {
                allowReturnIsvalid = false;
                $(this).find("[class*=validate]").not("[type=checkbox]").bind(settings.validationEventTriggers, function(caller) { _inlinEvent(this); })
                $(this).find("[class*=validate][type=checkbox]").bind("click", function(caller) { _inlinEvent(this); })
                firstvalid = false;
            }

            function _inlinEvent(caller) {

                $.validationEngine.settings = settings;

                if ($.validationEngine.intercept == false || !$.validationEngine.intercept) {
                    $.validationEngine.onSubmitValid = false;
                    $.validationEngine.loadValidation(caller);
                } else {
                    $.validationEngine.intercept = false;
                }
            }

        }

        if (settings.returnIsValid) { // Do validation and return true or false, it bypass everything;
            if ($.validationEngine.submitValidation(this, settings)) {
                return false;
            } else {
                return true;
            }
        }

        $(this).bind("submit", function(caller) { // ON FORM SUBMIT, CONTROL AJAX FUNCTION IF SPECIFIED ON DOCUMENT READY

            $.validationEngine.onSubmitValid = true;
            $.validationEngine.settings = settings;

            if ($.validationEngine.submitValidation(this, settings) == false) {
                if ($.validationEngine.submitForm(this, settings) == true) { return false; }
            } else {
                settings.failure && settings.failure();
                //var message = 'Existe(m) ' + $.validationEngine.settings.totals + ' campos obrigatórios inválidos.' // Added by Marco Baccaro.
                //$("div.error span").html(message);
                //$("div.error").show();
                $.validationEngine.settings.totals = 0; // Reset totals.
                return false;
            }

        })

    };

    $.validationEngine = {

        defaultSetting: function(caller) {

            if ($.validationEngineLanguage) {
                allRules = $.validationEngineLanguage.allRules;
            }

            settings = {
                allrules: allRules,
                validationEventTriggers: "blur",
                inlineValidation: true,
                returnIsValid: false,
                animateSubmit: true,
                unbindEngine: true,
                ajaxSubmit: false,
                promptPosition: "topRight", // OPENNING BOX POSITION, IMPLEMENTED: topLeft, topRight, bottomLeft, centerRight, bottomRight
                success: false,
                failure: function() { }
            }
            
            $.validationEngine.settings = settings;
            
        },

        loadValidation: function(caller) {	// GET VALIDATIONS TO BE EXECUTED

            if (!$.validationEngine.settings) {
                $.validationEngine.defaultSetting();
            }

            rulesParsing = $(caller).attr('class');

            // if condition by Marco Baccaro.
            if (rulesParsing.indexOf('validate[required') > -1 || rulesParsing.indexOf('validate[nonrequired') > -1) {

                rulesRegExp = /\[(.*)\]/;
                getRules = rulesRegExp.exec(rulesParsing);
                str = getRules[1];
                pattern = /\W+/;
                result = str.split(pattern);
                
                var validateCalll = $.validationEngine.validateCall(caller, result);

            }

            return validateCalll;

        },

        validateCall: function(caller, rules) {	// EXECUTE VALIDATION REQUIRED BY THE USER FOR THIS FIELD

            var promptText = '';
                        
            if (!$(caller).attr('id')) { $.validationEngine.debug('This field have no ID attribut(name & class displayed): ' + $(caller).attr("name") + " " + $(caller).attr("class")) }

            caller = caller;
            var callerName = $(caller).attr('name');
            $.validationEngine.isError = false;
            $.validationEngine.showTriangle = true;
            callerType = $(caller).attr('type');

            for (i = 0; i < rules.length; i++) {
            
                if($(caller).attr('class').indexOf('[required') == -1 && $(caller).attr('value') == ''){
                     $.validationEngine.isError = false;
                }
                
                else {
                    switch (rules[i]) {
                        case "required":
                            _required(caller, rules);
                            break;
                        case "nonrequired":
                            if ($.validationEngine.onSubmitValid) {
                              $.validationEngine.isError = false;
                              i = rules.length;
                            }
                            break;
                      case "custom": 
	                        _custom(caller,rules,i);
                            break;
                        case "cpf":
                            _cpf(caller, rules, i);
                            break;
                        case "pis":
                            _pis(caller, rules, i);
                            break;
                        case "cnsProvisorio":
                            _pis(caller, rules, i);
                            break;
                        case "cns":
                            _pis(caller, rules, i);
                            break;
                        case "email":
                            _email(caller, rules, i);
                            break;
                        case "date":
                            _date(caller, rules, i);
                            break;
                        case "telephone":
                            _telephone(caller, rules, i);
                            break;
                        case "onlyNumber":
                            _OnlyNumber(caller, rules, i);
                            break;
                        case "noSpecialCaracters":
                            _NoSpecialCaracters(caller, rules, i);
                            break;
                        case "onlyLetter":
                            _OnlyLetter(caller, rules, i);
                            break;
                        case "dayAndMonth":
                            _DayAndMonth(caller, rules, i);
                            break;
                        case "monthAndYear":
                            _MonthAndYear(caller, rules, i);
                            break;
                        case "range":
                            _range(caller, rules, i);
                            break;
                        case "maxCheckbox":
                            _maxCheckbox(caller, rules, i);
                            groupname = $(caller).attr("name");
                            caller = $("input[name='" + groupname + "']");
                            break;
                        case "minCheckbox":
                            _minCheckbox(caller, rules, i);
                            groupname = $(caller).attr("name");
                            caller = $("input[name='" + groupname + "']");
                            break;
                        case "optional":
                            if (!$(caller).val()) {
                                $.validationEngine.closePrompt(caller);
                                return $.validationEngine.isError;
                            }
                            break;
                        case "confirm":
                            _confirm(caller, rules, i);
                            break;
                          case "time":
                            _Hour(caller, rules, i);
                            break;
                        default: ;
                    };
                }
                
            };

            radioHack();

            if ($.validationEngine.isError == true) {
                linkTofield = $.validationEngine.linkTofield(caller);

                ($("div." + linkTofield).size() == 0) ? $.validationEngine.buildPrompt(caller, promptText, "error") : $.validationEngine.updatePromptText(caller, promptText);
            } else { $.validationEngine.closePrompt(caller); }
            /* UNFORTUNATE RADIO AND CHECKBOX GROUP HACKS */
            /* As my validation is looping input with id's we need a hack for my validation to understand to group these inputs */
            function radioHack() {
                if ($("input[name='" + callerName + "']").size() > 1 && (callerType == "radio" || callerType == "checkbox")) {        // Hack for radio/checkbox group button, the validation go the first radio/checkbox of the group
                    caller = $("input[name='" + callerName + "'][type!=hidden]:first");
                    $.validationEngine.showTriangle = false;
                }
            }
            /* VALIDATION FUNCTIONS */
            function _required(caller, rules) {   // VALIDATE BLANK FIELD

                callerType = $(caller).attr("type");

                if (callerType == "text" || callerType == "password") {

                    if (!$(caller).val()) {
                        $.validationEngine.isError = true;
                        $.validationEngine.settings.totals++;
                        promptText += $.validationEngine.settings.allrules[rules[i]].alertText + "<br />";
                    }

                }

                if (callerType == "textarea") {

                    var aux = caller.value.replace(/^\s+|\s+$/g, ""); // added by Marco Baccaro.

                    if (aux == "") {
                        $.validationEngine.isError = true;
                        $.validationEngine.settings.totals++;
                        promptText += $.validationEngine.settings.allrules[rules[i]].alertText + "<br />";
                    }
                }

                if (callerType == "radio" || callerType == "checkbox") {
                    callerName = $(caller).attr("name");

                    if ($("input[name='" + callerName + "']:checked").size() == 0) {
                        $.validationEngine.isError = true;
                        $.validationEngine.settings.totals++;
                        if ($("input[name='" + callerName + "']").size() == 1) {
                            promptText += $.validationEngine.settings.allrules[rules[i]].alertTextCheckboxe + "<br />";
                        } else {
                            promptText += $.validationEngine.settings.allrules[rules[i]].alertTextCheckboxMultiple + "<br />";
                        }
                    }
                }
                
                if (callerType == "select-one") { // added by paul@kinetek.net for select boxes, Thank you		
                    if (!$(caller).val()) {
                        $.validationEngine.isError = true;
                        $.validationEngine.settings.totals++;
                        promptText += $.validationEngine.settings.allrules[rules[i]].alertText + "<br />";
                    }
                }
                
                if (callerType == "select-multiple") { // added by paul@kinetek.net for select boxes, Thank you	
                    
                    if (!$(caller).find("option:selected").val()) {
                        $.validationEngine.isError = true;
                        $.validationEngine.settings.totals++;
                        promptText += $.validationEngine.settings.allrules[rules[i]].alertText + "<br />";
                    }
                    
                }                
                
            }
            
            function _custom(caller,rules,position){   
               
                customCall = rules[position+1];
        
                if($.validationEngine.settings.allrules[customCall] != undefined){
	                fname = $.validationEngine.settings.allrules[customCall].nname;
                }                  
        
                if(eval(fname + "('" + caller.id + "')")){
                   $.validationEngine.isError = true;
                   promptText += $.validationEngine.settings.allrules[customCall].alertText + "<br />";
                }
                 
            }
            
            function _cpf(caller, rules, position) {
			
				var value = $(caller).attr('value');

			    value = value.replace('.', '');
				value = value.replace('.', '');
				var cpf = value.replace('-', '');
				
				while (cpf.length < 11) {
				    cpf = "0" + cpf;
				}
				
				var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
				var a = [];
				var b = new Number;
				var c = 11;

				for (i = 0; i < 11; i++) {				
					a[i] = cpf.charAt(i);
					if (i < 9) b += (a[i] * --c);
				}

				if ((x = b % 11) < 2) {
				 a[9] = 0
				} else {
				 a[9] = 11 - x
				}
				
				b = 0;
				c = 11;

				for (y = 0; y < 10; y++){
					b += (a[y] * c--)
				};
				
				if ((x = b % 11) < 2) { 
				   a[10] = 0;
				} else {
				   a[10] = 11 - x;
				}
				
                if(value != ''){
				    if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) {
					    $.validationEngine.isError = true;
                        promptText += "* Informe um CPF válido.<br />";
                    }
                }
                    
            }
            
            function _pis(caller, rules, position) {
			
				var value = $(caller).attr('value');
			    value = value.replace('.', '');

                var i;			    
                var ftap = '3298765432';
                var total=0;
                var resto = 0;
                var numPIS = value;
                var strResto = '';
           			
	            if (numPIS == '' || numPIS == null) {
		            return false;
	            }
            	
	            for(i=0; i<= 9; i++) {
		            resultado = (numPIS.slice(i,i+1))*(ftap.slice(i,i+1));
		            total=total+resultado;
	            }
            	
	            resto = (total % 11);
            	
	            if (resto != 0) {
		            resto = (11 - resto);
	            }
            	
	            if (resto==10 || resto==11) {
		            strResto = resto + '';
		            resto = strResto.slice(1,2);
	            }
            	
	            if (resto!=(numPIS.slice(10,11))) {
		            $.validationEngine.isError = true;
                    promptText += "* Informe um PIS válido.<br />";
	            }
            	
	            return true;

            }
                        
            function _email(caller, rules, position) {

                pattern = /^[a-zA-Z0-9_\.\-]+\@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,4}$/;

                if (!pattern.test($(caller).attr('value'))) {
                    $.validationEngine.isError = true;
                    promptText += "* E-mail inválido.<br />";
                }

            }

            function _date(caller, rules, position) {

                pattern = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/;

                if($(caller).attr('value') != ''){
                    if (!pattern.test($(caller).attr('value'))) {
                        if (caller.value != '__/__/____') {
                            $.validationEngine.isError = true;
                            promptText += "* Data inválida.<br />";
                        }
                    }
                }

            }

            function _OnlyNumber(caller, rules, position) {
            
                pattern = /^[0-9\]+$]/;

                if($(caller).attr('value') != ''){
                    for(var i = 0; i < $(caller).attr('value').length; i++){
                        if (!pattern.test($(caller).attr('value')[i]) ) {
                            $.validationEngine.isError = true;
                            promptText += "* Informe apenas números.<br />";
                            break;
                        }
                    }
                }

            }
            
            function _NoSpecialCaracters(caller, rules, position) {
            
                pattern = /^[0-9a-zA-Z]+$/;
            
                if (!pattern.test($(caller).attr('value'))) {
                    $.validationEngine.isError = true;
                    promptText += "* Caracteres especiais não permitido.<br />";
                }
                
            }
            
            function _OnlyLetter(caller, rules, position) {
            
                pattern = /^[a-zA-Z\ \']+$/;
            
                if (!pattern.test($(caller).attr('value'))) {
                    $.validationEngine.isError = true;
                    promptText += "* Informe apenas letras.<br />";
                }
                
            }

            function _telephone(caller, rules, position) {

                pattern = /^[0-9\-\(\)\ ]+$/;

                if (!pattern.test($(caller).attr('value'))) {
                    $.validationEngine.isError = true;
                    promptText += "* Telefone inválido.<br />";
                }

            }

            function _DayAndMonth(caller, rules, position) {
                
                var data = new Date();
                var aux = $(caller).attr('value') + '/' +  data.getYear();
                
                pattern = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/;

                if($(caller).attr('value') != ''){
                    if (!pattern.test(aux)) {
                        $.validationEngine.isError = true;
                        promptText += "* Dia e/ou mês inválido.<br />";
                    }
                }
            
            }

            function _MonthAndYear(caller, rules, position) {            
            
                pattern = /^(((0?[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))+$/;

                if (!pattern.test($(caller).attr('value'))) {
                    $.validationEngine.isError = true;
                    promptText += "* Mês e/ou ano inválido.<br />";
                }
                
            }
            
            function _Hour(caller, rules, position) {            

	            pattern = /^([0-1][0-9]|[2][0-3])(:([0-5][0-9])){1,2}$/;

                if($(caller).attr('value') != ''){
	                if (!pattern.test($(caller).attr('value'))) {
		                $.validationEngine.isError = true;
		                promptText += "* Hora inválida.<br />";
	                }
                }
            	
            }
            
            function _confirm(caller, rules, position) { // VALIDATE FIELD MATCH
                confirmField = rules[position + 1];

                if ($(caller).attr('value') != $("#" + confirmField).attr('value')) {
                    $.validationEngine.isError = true;
                    promptText += $.validationEngine.settings.allrules["confirm"].alertText + "<br />";
                }
            }

            function _range(caller, rules, position) { // VALIDATE LENGTH

                startLength = eval(rules[position + 1]);
                endLength = eval(rules[position + 2]);
                feildLength = $(caller).attr('value').length;

                if (feildLength < startLength || feildLength > endLength) {
                    $.validationEngine.isError = true;
                    promptText += $.validationEngine.settings.allrules["range"].alertText + startLength + $.validationEngine.settings.allrules["range"].alertText2 + endLength + $.validationEngine.settings.allrules["range"].alertText3 + "<br />"
                }
            }

            function _maxCheckbox(caller, rules, position) {  // VALIDATE CHECKBOX NUMBER

                nbCheck = eval(rules[position + 1]);
                groupname = $(caller).attr("name");
                groupSize = $("input[name='" + groupname + "']:checked").size();
                if (groupSize > nbCheck) {
                    $.validationEngine.showTriangle = false;
                    $.validationEngine.isError = true;
                    promptText += $.validationEngine.settings.allrules["maxCheckbox"].alertText + "<br />";
                }
            }

            function _minCheckbox(caller, rules, position) { // VALIDATE CHECKBOX NUMBER

                nbCheck = eval(rules[position + 1]);
                groupname = $(caller).attr("name");
                groupSize = $("input[name='" + groupname + "']:checked").size();
                if (groupSize < nbCheck) {

                    $.validationEngine.isError = true;
                    $.validationEngine.showTriangle = false;
                    promptText += $.validationEngine.settings.allrules["minCheckbox"].alertText + " " + nbCheck + " " + $.validationEngine.settings.allrules["minCheckbox"].alertText2 + "<br />";
                }
            }

            return ($.validationEngine.isError) ? $.validationEngine.isError : false;
        },

        submitForm: function(caller) {
            return false;
        },

        buildPrompt: function(caller, promptText, type, ajaxed) { // ERROR PROMPT CREATION AND DISPLAY WHEN AN ERROR OCCUR

            $(caller).addClass("formErrorCaller");
            
          
            if (!$.validationEngine.settings) {
                $.validationEngine.defaultSetting();
            }

            var divFormError = document.createElement('div');
            var formErrorContent = document.createElement('div');

            linkTofield = $.validationEngine.linkTofield(caller)
            $(divFormError).addClass("formError")

            $(divFormError).addClass(linkTofield);
            $(formErrorContent).addClass("formErrorContent");

            $("body").append(divFormError);
            $(divFormError).append(formErrorContent);

            if ($.validationEngine.showTriangle != false) {		// NO TRIANGLE ON MAX CHECKBOX AND RADIO
                var arrow = document.createElement('div');
                $(arrow).addClass("formErrorArrow");
                $(divFormError).append(arrow);
                if ($.validationEngine.settings.promptPosition == "bottomLeft" || $.validationEngine.settings.promptPosition == "bottomRight") {
                    $(arrow).addClass("formErrorArrowBottom")
                    $(arrow).html('<div class="line1"><!-- --></div><div class="line2"><!-- --></div><div class="line3"><!-- --></div><div class="line4"><!-- --></div><div class="line5"><!-- --></div><div class="line6"><!-- --></div><div class="line7"><!-- --></div><div class="line8"><!-- --></div><div class="line9"><!-- --></div><div class="line10"><!-- --></div>');
                }
                if ($.validationEngine.settings.promptPosition == "topLeft" || $.validationEngine.settings.promptPosition == "topRight") {
                    $(divFormError).append(arrow);
                    $(arrow).html('<div class="line10"><!-- --></div><div class="line9"><!-- --></div><div class="line8"><!-- --></div><div class="line7"><!-- --></div><div class="line6"><!-- --></div><div class="line5"><!-- --></div><div class="line4"><!-- --></div><div class="line3"><!-- --></div><div class="line2"><!-- --></div><div class="line1"><!-- --></div>');
                }
            }
            $(formErrorContent).html(promptText)

            callerTopPosition = $(caller).offset().top;
            callerleftPosition = $(caller).offset().left;
            callerWidth = $(caller).width();
            inputHeight = $(divFormError).height();

            /* POSITIONNING */
            if ($.validationEngine.settings.promptPosition == "topRight") { callerleftPosition += callerWidth - 30; callerTopPosition += -inputHeight - 10; }
            if ($.validationEngine.settings.promptPosition == "topLeft") { callerTopPosition += -inputHeight - 10; }

            if ($.validationEngine.settings.promptPosition == "centerRight") { callerleftPosition += callerWidth + 13; }

            if ($.validationEngine.settings.promptPosition == "bottomLeft") {
                callerHeight = $(caller).height();
                callerleftPosition = callerleftPosition;
                callerTopPosition = callerTopPosition + callerHeight + 15;
            }
            if ($.validationEngine.settings.promptPosition == "bottomRight") {
                callerHeight = $(caller).height();
                callerleftPosition += callerWidth - 30;
                callerTopPosition += callerHeight + 15;
            }
            $(divFormError).css({
                top: callerTopPosition,
                left: callerleftPosition,
                opacity: 0
            })
            return $(divFormError).animate({ "opacity": 0.87 }, function() { return true; });
        },
        updatePromptText: function(caller, promptText, type, ajaxed) {	// UPDATE TEXT ERROR IF AN ERROR IS ALREADY DISPLAYED

            linkTofield = $.validationEngine.linkTofield(caller);
            var updateThisPrompt = "." + linkTofield;

            if (type == "pass") { $(updateThisPrompt).addClass("greenPopup") } else { $(updateThisPrompt).removeClass("greenPopup") };
            if (type == "load") { $(updateThisPrompt).addClass("blackPopup") } else { $(updateThisPrompt).removeClass("blackPopup") };
            if (ajaxed) { $(updateThisPrompt).addClass("ajaxed") } else { $(updateThisPrompt).removeClass("ajaxed") };

            $(updateThisPrompt).find(".formErrorContent").html(promptText);
            callerTopPosition = $(caller).offset().top;
            inputHeight = $(updateThisPrompt).height();

            if ($.validationEngine.settings.promptPosition == "bottomLeft" || $.validationEngine.settings.promptPosition == "bottomRight") {
                callerHeight = $(caller).height();
                callerTopPosition = callerTopPosition + callerHeight + 15;
            }
            if ($.validationEngine.settings.promptPosition == "centerRight") { callerleftPosition += callerWidth + 13; }
            if ($.validationEngine.settings.promptPosition == "topLeft" || $.validationEngine.settings.promptPosition == "topRight") {
                callerTopPosition = callerTopPosition - inputHeight - 10;
            }
            
            $(updateThisPrompt).animate({ top: callerTopPosition, "opacity": 0.87 });
            
        },
        linkTofield: function(caller) {
            linkTofield = $(caller).attr("id") + "formError";
            linkTofield = linkTofield.replace(/\[/g, "");
            linkTofield = linkTofield.replace(/\]/g, "");
            return linkTofield;
        },
        closePrompt: function(caller, outside) { // CLOSE PROMPT WHEN ERROR CORRECTED
            if (!$.validationEngine.settings) {
                $.validationEngine.defaultSetting()
            }
            if (outside) {
                $(caller).fadeTo("fast", 0, function() {
                    $(caller).remove();
                });
                return false;
            }
            $(caller).removeClass('formErrorCaller');

            linkTofield = $.validationEngine.linkTofield(caller);
            closingPrompt = "." + linkTofield;
            $(closingPrompt).fadeTo("fast", 0, function() {
                $(closingPrompt).remove();
            });
        },
        debug: function(error) {
            if (!$("#debugMode")[0]) {
                $("body").append("<div id='debugMode'><div class='debugError'><strong>This is a debug mode, you got a problem with your form, it will try to help you, refresh when you think you nailed down the problem</strong></div></div>");
            }
            $(".debugError").append("<div class='debugerror'>" + error + "</div>");
        },
        submitValidation: function(caller) { // FORM SUBMIT VALIDATION LOOPING INLINE VALIDATION

            var stopForm = false;
            $(caller).find(".formError").remove();
            var toValidateSize = $(caller).find("[class*=validate]").size();

            $(caller).find("[class*=validate]").each(function() {
                linkTofield = $.validationEngine.linkTofield(this);

                if (!$("." + linkTofield).hasClass("ajaxed")) {	// DO NOT UPDATE ALREADY AJAXED FIELDS (only happen if no normal errors, don't worry)
                    var validationPass = $.validationEngine.loadValidation(this);
                    return (validationPass) ? stopForm = true : "";
                };
            });

            if (stopForm) {	// GET IF THERE IS AN ERROR OR NOT FROM THIS VALIDATION FUNCTIONS
                if ($.validationEngine.settings.animateSubmit) {
                    destination = $(".formError:not('.greenPopup'):first").offset().top;
                    $(".formError:not('.greenPopup')").each(function() {
                        testDestination = $(this).offset().top;
                        if (destination > testDestination) {
                            destination = $(this).offset().top;
                        }
                    })
                    $("html:not(:animated),body:not(:animated)").animate({ scrollTop: destination }, 1100);
                }
                return true;
            } else {
                return false;
            }
        }
    }
})(jQuery);

// By - Marco Baccaro
// Method to disable validate fields in others container, except the container parameter.
function DisableRequiredFields(exceptionContainer) {

    $.validationEngine.closePrompt('.formError', true);

    var i = 0;
    var allField = document.forms[0].elements;

    for (i = 0; i < allField.length; i++) {
        if (allField[i].className.substring(0, 17) == 'validate[required') {
            allField[i].className = allField[i].className.replace('validate[required', 'validate[disabled');
        }
    }

    if (Get(exceptionContainer) != null) {
    
        var inputFields = Get(exceptionContainer).getElementsByTagName('input');
        var selectFields = Get(exceptionContainer).getElementsByTagName('select');

        for (i = 0; i < inputFields.length; i++) {
            if (inputFields[i].className.substring(0, 17) == 'validate[disabled') {
                inputFields[i].className = inputFields[i].className.replace('validate[disabled', 'validate[required');
            }
            
        }
        
         for (i = 0; i < selectFields.length; i++) {
            if (selectFields[i].className.substring(0, 17) == 'validate[disabled') {
                selectFields[i].className = selectFields[i].className.replace('validate[disabled', 'validate[required');
            }
            
        }
        
    }

}

function EnableRequiredFields() {

    var allField = document.forms[0].elements;

    for (var i = 0; i < allField.length; i++) {
        if (allField[i].className.substring(0, 17) == 'validate[disabled') {
            allField[i].className = allField[i].className.replace('validate[disabled', 'validate[required');
        }
    }
    
}

// Create Validation - Marco Baccaro
(function($) {
    $.fn.validationEngineLanguage = function() { };

    $.validationEngineLanguage = {

        newLang: function() {

            $.validationEngineLanguage.allRules = { "required": {
                "regex": "none",
                "alertText": "* Campo obrigatório.",
                "alertTextCheckboxMultiple": "* Selecione um opção.",
                "alertTextCheckboxe": "* Opção obrigatória."
                },
                
                "range": {
                    "regex": "none",
                    "alertText": "* Informe de ",
                    "alertText2": " a ",
                    "alertText3": " caracteres."
                }, 
                
                "ValidarIdade": {
 		            "nname":"ValidarIdade",
 		            "alertText": "* Não é permitido cadastrar pessoa com mais de 150 anos!"
 	            },	
 	           
 	            "ValidarNome": {  
					"nname": "ValidarNome",
					"alertText": "* É necessário informar nome e sobrenome."		
		 	    },
		 	    
		 	   "ValidarCnsProvisorio": {  
					"nname": "ValidarCnsProvisorio",
					"alertText": "* Número de CNS provisório inválido."		
		 	    },
		 	    
		 	   "ValidarCns": {  
					"nname": "ValidarCns",
					"alertText": "* Número de CNS inválido."		
		 	    },
		 	    
		 	    "DataMaiorDiaAtual": {  
					"nname": "DataMaiorIgualDiaAtual",
					"alertText": "* A data precisa ser maior ou igual que a data atual."		
		 	    }
               
               
            }
            
        }
        
    }
    
})(jQuery);

$(document).ready(function() {
    $.validationEngineLanguage.newLang();
    SetMask();
});

// Marco Baccaro - Implements.
function CreateCustomValidate(functionName, erroText) {
    $.validationEngineLanguage.allRules[functionName] = { "nname":functionName, "alertText": erroText };
}

// Set mask by Marco Baccaro.
function SetMask() {
    $("input.phone").mask("(99) 9999-9999");
    $("input.zipcode").mask("99999-999");
    $("input.cpf").mask("999.999.999-99");
    $("input.cnpj").mask("99.999.999/9999-99");
    $("input.date").mask("99/99/9999");
    $("input.dateDayMonth").mask("99/99");
    $("input.dateMonthYear").mask("99/9999");
    $("input.datetime").mask("99/99/9999 99:99:99");
    $("input.time").mask("99:99");
    $("input.number4").mask("9999");
    $("input.number2").mask("99");
    $("input.number7").mask("9999999");
    $("input.number15").mask("999999999999999");
    $("input.number11").mask("99999999999");
    $("input.money").priceFormat({
        prefix: '',
        centsSeparator: ',',
        thousandsSeparator: '.'
    });
    $("input.km").priceFormat({
        centsSeparator: ',',
        thousandsSeparator: '.',
        centsLimit: 3
    });
}

// Correção set mask Ivan Battistin / Leandro Theobaldo
(function($) {

	var pasteEventName = ($.browser.msie ? 'paste' : 'input') + ".mask";
	var iPhone = (window.orientation != undefined);

	$.mask = {
		//Predefined character definitions
		definitions: {
			'9': "[0-9]",
			'a': "[A-Za-z]",
			'*': "[A-Za-z0-9]"
		}
	};

	$.fn.extend({
		//Helper Function for Caret positioning
		caret: function(begin, end) {
			if (this.length == 0) return;
			if (typeof begin == 'number') {
				end = (typeof end == 'number') ? end : begin;
				return this.each(function() {
					if (this.setSelectionRange) {
						this.focus();
						this.setSelectionRange(begin, end);
					} else if (this.createTextRange) {
						var range = this.createTextRange();
						range.collapse(true);
						range.moveEnd('character', end);
						range.moveStart('character', begin);
						range.select();
					}
				});
			} else {
				if (this[0].setSelectionRange) {
					begin = this[0].selectionStart;
					end = this[0].selectionEnd;
				} else if (document.selection && document.selection.createRange) {
					var range = document.selection.createRange();
					begin = 0 - range.duplicate().moveStart('character', -100000);
					end = begin + range.text.length;
				}
				return { begin: begin, end: end };
			}
		},
		unmask: function() { return this.trigger("unmask"); },
		mask: function(mask, settings) {
			if (!mask && this.length > 0) {
				var input = $(this[0]);
				var tests = input.data("tests");
				return $.map(input.data("buffer"), function(c, i) {
					return tests[i] ? c : null;
				}).join('');
			}
			settings = $.extend({
				placeholder: "_",
				completed: null
			}, settings);

			var defs = $.mask.definitions;
			var tests = [];
			var partialPosition = mask.length;
			var firstNonMaskPos = null;
			var len = mask.length;

			$.each(mask.split(""), function(i, c) {
				if (c == '?') {
					len--;
					partialPosition = i;
				} else if (defs[c]) {
					tests.push(new RegExp(defs[c]));
					if(firstNonMaskPos==null)
						firstNonMaskPos =  tests.length - 1;
				} else {
					tests.push(null);
				}
			});

			return this.each(function() {
                $(this).unmask();
				var input = $(this);
				var buffer = $.map(mask.split(""), function(c, i) { if (c != '?') return defs[c] ? settings.placeholder : c });
				var ignore = false;  			//Variable for ignoring control keys
				var focusText = input.val();

				input.data("buffer", buffer).data("tests", tests);

				function seekNext(pos) {
					while (++pos <= len && !tests[pos]);
					return pos;
				};

				function shiftL(pos) {
					while (!tests[pos] && --pos >= 0);
					for (var i = pos; i < len; i++) {
						if (tests[i]) {
							buffer[i] = settings.placeholder;
							var j = seekNext(i);
							if (j < len && tests[i].test(buffer[j])) {
								buffer[i] = buffer[j];
							} else
								break;
						}
					}
					writeBuffer();
					input.caret(Math.max(firstNonMaskPos, pos));
				};

				function shiftR(pos) {
					for (var i = pos, c = settings.placeholder; i < len; i++) {
						if (tests[i]) {
							var j = seekNext(i);
							var t = buffer[i];
							buffer[i] = c;
							if (j < len && tests[j].test(t))
								c = t;
							else
								break;
						}
					}
				};

				function keydownEvent(e) {
					var pos = $(this).caret();
					var k = e.keyCode;
					ignore = (k < 16 || (k > 16 && k < 32) || (k > 32 && k < 41));

					//delete selection before proceeding
					if ((pos.begin - pos.end) != 0 && (!ignore || k == 8 || k == 46))
						clearBuffer(pos.begin, pos.end);

					//backspace, delete, and escape get special treatment
					if (k == 8 || k == 46 || (iPhone && k == 127)) {//backspace/delete
						shiftL(pos.begin + (k == 46 ? 0 : -1));
						return false;
					} else if (k == 27) {//escape
						input.val(focusText);
						input.caret(0, checkVal());
						return false;
					}
				};

				function keypressEvent(e) {
					if (ignore) {
						ignore = false;
						//Fixes Mac FF bug on backspace
						return (e.keyCode == 8) ? false : null;
					}
					e = e || window.event;
					var k = e.charCode || e.keyCode || e.which;
					var pos = $(this).caret();

					if (e.ctrlKey || e.altKey || e.metaKey) {//Ignore
						return true;
					} else if ((k >= 32 && k <= 125) || k > 186) {//typeable characters
						var p = seekNext(pos.begin - 1);
						if (p < len) {
							var c = String.fromCharCode(k);
							if (tests[p].test(c)) {
								shiftR(p);
								buffer[p] = c;
								writeBuffer();
								var next = seekNext(p);
								$(this).caret(next);
								if (settings.completed && next == len)
									settings.completed.call(input);
							}
						}
					}
					return false;
				};

				function clearBuffer(start, end) {
					for (var i = start; i < end && i < len; i++) {
						if (tests[i])
							buffer[i] = settings.placeholder;
					}
				};

				function writeBuffer() { return input.val(buffer.join('')).val(); };

				function checkVal(allow) {
					//try to place characters where they belong
					var test = input.val();
					var lastMatch = -1;
					for (var i = 0, pos = 0; i < len; i++) {
						if (tests[i]) {
							buffer[i] = settings.placeholder;
							while (pos++ < test.length) {
								var c = test.charAt(pos - 1);
								if (tests[i].test(c)) {
									buffer[i] = c;
									lastMatch = i;
									break;
								}
							}
							if (pos > test.length)
								break;
						} else if (buffer[i] == test[pos] && i!=partialPosition) {
							pos++;
							lastMatch = i;
						} 
					}
					if (!allow && lastMatch + 1 < partialPosition) {
						input.val("");
						clearBuffer(0, len);
					} else if (allow || lastMatch + 1 >= partialPosition) {
						writeBuffer();
						if (!allow) input.val(input.val().substring(0, lastMatch + 1));
					}
					return (partialPosition ? i : firstNonMaskPos);
				};

				if (!input.attr("readonly"))
					input
					.one("unmask", function() {
						input
							.unbind(".mask")
							.removeData("buffer")
							.removeData("tests");
					})
					.bind("focus.mask", function() {
						focusText = input.val();
						var pos = checkVal();
						writeBuffer();
						setTimeout(function() {
							if (pos == mask.length)
								input.caret(0, pos);
							else
								input.caret(pos);
						}, 0);
					})
					.bind("blur.mask", function() {
						checkVal();
						if (input.val() != focusText)
							input.change();
					})
					.bind("keydown.mask", keydownEvent)
					.bind("keypress.mask", keypressEvent)
					.bind(pasteEventName, function() {
						setTimeout(function() { input.caret(checkVal(true)); }, 0);
					});

				checkVal(); //Perform initial check for existing values
			});
		}
	});
})(jQuery);

/* DECIMAL */

(function($) {

    $.fn.priceFormat = function(options) {

        var defaults = {
            prefix: '',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: false,
            centsLimit: 2
        };

        var options = $.extend(defaults, options);

        return this.each(function() {

            // pre defined options
            var obj = $(this);
            var is_number = /[0-9]/;

            // load the pluggings settings
            var prefix = options.prefix;
            var centsSeparator = options.centsSeparator;
            var thousandsSeparator = options.thousandsSeparator;
            var limit = options.limit;
            var centsLimit = options.centsLimit;

            // skip everything that isn't a number
            // and also skip the left zeroes
            function to_numbers(str) {
                var formatted = '';
                for (var i = 0; i < (str.length); i++) {
                    char = str.charAt(i);
                    if (formatted.length == 0 && char == 0) char = false;
                    if (char && char.match(is_number)) {
                        if (limit) {
                            if (formatted.length < limit) formatted = formatted + char;
                        } else {
                            formatted = formatted + char;
                        }
                    }
                }
                return formatted;
            }

            // format to fill with zeros to complete cents chars
            function fill_with_zeroes(str) {
                while (str.length < (centsLimit + 1)) str = '0' + str;
                return str;
            }

            // format as price
            function price_format(str) {

                // formatting settings
                var formatted = fill_with_zeroes(to_numbers(str));
                var thousandsFormatted = '';
                var thousandsCount = 0;

                // split integer from cents
                var centsVal = formatted.substr(formatted.length - centsLimit, centsLimit);
                var integerVal = formatted.substr(0, formatted.length - centsLimit);

                // apply cents pontuation
                formatted = integerVal + centsSeparator + centsVal;

                // apply thousands pontuation
                if (thousandsSeparator) {
                    for (var j = integerVal.length; j > 0; j--) {
                        char = integerVal.substr(j - 1, 1);
                        thousandsCount++;
                        if (thousandsCount % 3 == 0) char = thousandsSeparator + char;
                        thousandsFormatted = char + thousandsFormatted;
                    }
                    if (thousandsFormatted.substr(0, 1) == thousandsSeparator) thousandsFormatted = thousandsFormatted.substring(1, thousandsFormatted.length);
                    formatted = thousandsFormatted + centsSeparator + centsVal;
                }

                // apply the prefix
                if (prefix) formatted = prefix + formatted;

                return formatted;

            }

            // filter what user type (only numbers and functional keys)
            function key_check(e) {

                var code = (e.keyCode ? e.keyCode : e.which);
                var typed = String.fromCharCode(code);
                var functional = false;
                var str = obj.val();
                var newValue = price_format(str + typed);

                // allow keypad numbers, 0 to 9
                if (code >= 96 && code <= 105) functional = true;

                // check Backspace, Tab, Enter, and left/right arrows
                if (code == 8) functional = true;
                if (code == 9) functional = true;
                if (code == 13) functional = true;
                if (code == 37) functional = true;
                if (code == 39) functional = true;

                if (!functional) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (str != newValue) obj.val(newValue);
                }

            }

            // inster formatted price as a value of an input field
            function price_it() {
                var str = obj.val();
                var price = price_format(str);
                if (str != price) obj.val(price);
            }

            // bind the actions
            $(this).bind('keydown', key_check);
            $(this).bind('keyup', price_it);
            if ($(this).val().length > 0) price_it();

        });

    };

})(jQuery);

// Register in load page for work.
$(document).ready(function() {
    if (document.forms[0].id != '' && document.forms[0].id == 'aspnetForm') {
        $('#aspnetForm').validationEngine();
    } else {
        $('#' + document.forms[0].id).validationEngine();
    }
});

function RegisterJQueryValidate() {
    $(document).ready(function() {
        if (document.forms[0].id != '' && document.forms[0].id == 'aspnetForm') {
            $('#aspnetForm').validationEngine();
        } else {
            $('#' + document.forms[0].id).validationEngine();
        }
    });
}

/* Validação de nome, se contém nome e sobrenome. */
function ValidarNome() {

    var nome = Get("txtNome").value.replace(/^\s+|\s+$/g, '').replace(/\s+/g, ' ');

    if (nome.split(' ').length < 2) {
        return true;
    } else {
        return false;
    }

}

/* Validação, se a data informada é maior ou igual do que a data atual. */
function DataMaiorIgualDiaAtual(id) {

    var atual = new Date();
    var date = document.getElementById(id).value;
    atual = atual.format("dd/MM/yyyy");

    if (atual == date.value) {
        return false;
    }

    if (CompareDate(date, atual)) {    
        return false;
    } else {
        return true;
    }
    
}