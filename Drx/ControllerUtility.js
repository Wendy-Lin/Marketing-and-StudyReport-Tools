
$(function () {
    var docHeight = $(document).height();
    $("body").append("<div id='overlay' style='display: none'></div>");
    $("#overlay")
      .height(docHeight)
      .css({
          'opacity': 0.0,
          'position': 'absolute',
          'top': 0,
          'left': 0,
          'background-color': 'black',
          'width': '100%',
          'z-index': 5000
      });
});

$(document).ajaxStart(function (event, request, settings) {
    //$("*").css("cursor", "wait");
    //$('#loading-indicator').show();
    $('#overlay').show();
});

$(document).ajaxStop(function (event, request, settings) {
    //$("*").css("cursor", "default");
    //$('#loading-indicator').hide();
    $('#overlay').hide();
});

function openPopup() {
    $("#result").dialog({
        autoOpen: true,
        title: 'Information',
        width: 800,
        height: 'auto',
        modal: true
    });
    //$("#result").dialog("open");
}

function exportExcel(form) {
    $("#isExport").val("1");


    if (form == null)
        form = $("form");

    form.submit();

}

function exportPdf(form) {
    $("#isExport").val("2");

    if (form == null)
        form = $("form");

    form.submit();

}

function exportCSV(form) {
    $("#isExport").val("3");


    if (form == null)
        form = $("form");

    form.submit();

}

function submitForm() {
    var form = $("form");
    var action = form.attr("action");

    if ($("#isExport").length)
        $("#isExport").val("0");

    if (form.valid()) {
        $.post(action,
        form.serialize(),
        function (Data) {
            DisplayMessageBox(Data)
        },
        "json"
        );
    }
}

function toCurrency(number) {
    if (isNaN(number) || number === '' || number === null || Number(number) == 0)
        return "";
    else {
        number = Math.round(Number(number) * 100) / 100;
        var number2 = number.toString();
        dollars = number2.split('.')[0];
        cents = (number2.split('.')[1] || '') + '00';
        dollars = dollars.split('').reverse().join('')
        .replace(/(\d{3}(?!$))/g, '$1,')
        .split('').reverse().join('');
        return '$' + dollars + '.' + cents.slice(0, 2);
    }
}

function getcurrencyvalue(currency) {
    var raw = $.trim(currency.text());
    return Number(raw.replace("$", "").replace(",", ""));
}

function FormatNumberWithCommas(x) {
    var parts = x.toString().split(".");
    return parts[0].replace(/\B(?=(\d{3})+(?=$))/g, ",") + (parts[1] ? "." + parts[1] : "");
}

//Add Hint For Input field

function addLoadEvent(func) {

    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            oldonload();
            func();
        }
    }
}

function prepareInputsForHints() {

    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        // test to see if the hint span exists first
        if (inputs[i].parentNode.getElementsByTagName("span")[0]) {
            // the span exists!  on focus, show the hint
            inputs[i].onfocus = function () {
                this.parentNode.getElementsByTagName("span")[0].style.display = "inline";
            }
            // when the cursor moves away from the field, hide the hint
            inputs[i].onblur = function () {
                this.parentNode.getElementsByTagName("span")[0].style.display = "none";
            }
        }
    }
    // repeat the same tests as above for selects
    var selects = document.getElementsByTagName("select");
    for (var k = 0; k < selects.length; k++) {
        if (selects[k].parentNode.getElementsByTagName("span")[0]) {
            selects[k].onfocus = function () {
                this.parentNode.getElementsByTagName("span")[0].style.display = "inline";
            }
            selects[k].onblur = function () {
                this.parentNode.getElementsByTagName("span")[0].style.display = "none";
            }
        }
    }
}

function DisplayYesNoConfirm(msg, title, callbackYes, callbackNo, callbackArgument) {
    $("#confirmlDialogText").text(msg);
    $("#confirmDialog").dialog({
        resizable: false,
        width: 450,
        height: 'auto',
        modal: true,
        autoOpen: true,
        title: title,
        buttons: {
            "Yes": function () {

                if (callbackYes && typeof (callbackYes) === "function") {
                    if (callbackArgument == null) {
                        callbackYes();
                    } else {
                        callbackYes(callbackArgument);
                    }
                }
                $(this).dialog("close");
            },
            "No": function () {
                if (callbackNo && typeof (callbackNo) === "function") {
                    if (callbackArgument == null) {
                        callbackNo();
                    } else {
                        callbackNo(callbackArgument);
                    }
                }
                $(this).dialog("close");
                return false;
            }
        }
    });

}
function DisplayYesNoConfirmWithDefaultNo(msg, title, callbackYes, callbackNo, callbackArgument) {
    $("#confirmlDialogText").text(msg);

    $("#confirmDialog").dialog({
        resizable: false,
        width: 450,
        height: 'auto',
        modal: true,
        autoOpen: true,
        title: title,
        open: function () {
            $(this).siblings('.ui-dialog-buttonpane').find('button:eq(1)').focus();
        },
        buttons: {
            "Yes": function () {

                if (callbackYes && typeof (callbackYes) === "function") {
                    if (callbackArgument == null) {
                        callbackYes();
                    } else {
                        callbackYes(callbackArgument);
                    }
                }
                $(this).dialog("close");
            },
            "No": function () {
                if (callbackNo && typeof (callbackNo) === "function") {
                    if (callbackArgument == null) {
                        callbackNo();
                    } else {
                        callbackNo(callbackArgument);
                    }
                }
                $(this).dialog("close");
                return false;
            }
        }
    });

}

function DisplayConfirm(msg, title, callbackYes, callbackNo, callbackArgument) {
    $("#confirmlDialogText").html(msg);
    $("#confirmDialog").dialog({
        resizable: false,
        width: 450,
        height: 'auto',
        modal: true,
        autoOpen: true,
        title: title,
        buttons: {
            "Continue": function () {

                if (callbackYes && typeof (callbackYes) === "function") {
                    if (callbackArgument == null) {
                        callbackYes();
                    } else {
                        callbackYes(callbackArgument);
                    }
                }
                $(this).dialog("close");
            },
            Cancel: function () {
                if (callbackNo && typeof (callbackNo) === "function") {
                    if (callbackArgument == null) {
                        callbackNo();
                    } else {
                        callbackNo(callbackArgument);
                    }
                }
                $(this).dialog("close");
                return false;
            }
        }
    });

}


function DisplayMessage(success, title, message, redirectTo) {
    var Data = {
        Success: success,
        Title: title,
        Message: message,
        RedirectTo: redirectTo
    };
    DisplayMessageBox(Data);
}

function DisplayMessageBox(Data, obj) {

    if (Data.Success == 0) {

        $("#failDialogText").html(Data.Message);
        $("#failDialog").dialog({
            autoOpen: true,
            title: Data.Title,
            width: 500,
            height: 'auto',
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                    if (Data.RedirectTo.length > 0) {
                        $("body, html").animate({
                            scrollTop: $(Data.RedirectTo).offset().top
                        }, 600);

                    }
                }
            }
        });
    }
    else if (Data.Success == 1 || Data.Success == 3) {

        $("#successDialogText").html(Data.Message);
        $("#successDialog").dialog({
            autoOpen: true,
            title: Data.Title,
            width: 500,
            height: 'auto',
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            },
            close: function (event, ui) {

                if (Data.RedirectTo != "") {
                    if (Data.Success == 3) {

                        var url = Data.RedirectTo;
                        $.ajax({
                            url: url,
                            type: "GET",
                            //Do not cache the page
                            cache: false,
                            success: function (data) {
                                if (obj)
                                    obj.html(data);
                            }
                        });

                    }
                    else {

                        if (Data.RedirectTo == "reload")
                            location.reload();
                        else if (Data.RedirectTo == "back")
                            history.back(-1);
                        else
                            location.href = Data.RedirectTo;

                    }
                }

            }
        });
    }
    else if (Data.Success == 2) {
        if (Data.RedirectTo != "") {
            if (Data.RedirectTo == "reload")
                location.reload();
            else if (Data.RedirectTo == "back")
                history.back(-1);
            else
                location.href = Data.RedirectTo;
        }
    }
    else if (Data.Success == 4) {

        var url = Data.RedirectTo;
        $.ajax({
            url: url,
            type: "GET",
            //Do not cache the page
            cache: false,
            success: function (data) {
                obj.html(data);
            }
        });
    }
}

function calcBusinessDays(dDate1, dDate2) {         // input given as Date objects

    var iWeeks, iDateDiff, iAdjust = 0;

    if (dDate2 < dDate1) return -1;                 // error code if dates transposed

    var iWeekday1 = dDate1.getDay();                // day of week
    var iWeekday2 = dDate2.getDay();

    iWeekday1 = (iWeekday1 == 0) ? 7 : iWeekday1;   // change Sunday from 0 to 7
    iWeekday2 = (iWeekday2 == 0) ? 7 : iWeekday2;

    if ((iWeekday1 > 5) && (iWeekday2 > 5)) iAdjust = 1;  // adjustment if both days on weekend

    iWeekday1 = (iWeekday1 > 5) ? 5 : iWeekday1;    // only count weekdays
    iWeekday2 = (iWeekday2 > 5) ? 5 : iWeekday2;

    // calculate differnece in weeks (1000mS * 60sec * 60min * 24hrs * 7 days = 604800000)
    iWeeks = Math.floor((dDate2.getTime() - dDate1.getTime()) / 604800000)

    if (iWeekday1 <= iWeekday2) {
        iDateDiff = (iWeeks * 5) + (iWeekday2 - iWeekday1)
    } else {
        iDateDiff = ((iWeeks + 1) * 5) - (iWeekday1 - iWeekday2)
    }

    iDateDiff -= iAdjust                            // take into account both days on weekend

    return (iDateDiff + 1);                         // add 1 because dates are inclusive

}

function checkFileSize(obj) {

}

$('input[type=file]').each(function () {

    //upload file max size:
    var max_size = 3000000;
    $(this).change(function (evt) {
        var finput = $(this);
        var files = evt.target.files;
        var f = files[0];
        if (f.size > max_size) {
            DisplayMessage(0, 'File Size Exceed Maximum Allowed', 'The file size must not exceed 3MB.', '')
            $(this).replaceWith($(this).clone());

        }
    });
});



function isInt(n) {
    return ($.isNumeric(n) && Math.floor(n) == n && Math.floor(n) > 0)
    //return +n === n && !(n % 1);
}

function isIntOrZero(n) {
    return ($.isNumeric(n) && Math.floor(n) == n && Math.floor(n) >= 0)
    //return +n === n && !(n % 1);
}

function isValidDate(dateVal, format) {
    var isValid = true;

    try {
        $.datepicker.parseDate(format, dateVal, null);
    }
    catch (error) {
        isValid = false;
    }

    return isValid;
}

function saveConfirm() {
    return confirm('Please save your changes before you leave this page. Do you want to continue?');
}

function PreviousButtonClick(RediredtTo) {
    DisplayConfirm("Please save your changes before you leave this page. Do you want to continue?", "Comfirmation", Redirect, null, RediredtTo)
}

function Redirect(RediredtTo) {
    location.href = RediredtTo;
}

function clearForm(oForm) {
    var frm_elements = oForm.elements;

    for (i = 0; i < frm_elements.length; i++) {
        if (frm_elements[i].type) {
            field_type = frm_elements[i].type.toLowerCase();
            switch (field_type) {
                case "text":
                case "password":
                case "textarea":
                case "hidden":
                    frm_elements[i].value = "";
                    break;
                case "radio":
                case "checkbox":
                    if (frm_elements[i].checked) {
                        frm_elements[i].checked = false;
                    }
                    break;
                case "select-one":
                case "select-multi":
                    frm_elements[i].selectedIndex = -1;
                    break;
                default:
                    break;
            }
        }
    }
}

function EnableButton(obj, enable) {

    if (enable) {
        obj.attr("disabled", false);
        obj.addClass("bluebutton");
        obj.removeClass("bluebuttonDisable");
    }
    else {
        obj.attr("disabled", true);
        obj.addClass("bluebuttonDisable");
        obj.removeClass("bluebutton");
    }
}

function BlockUIAjaxPost() {
    $.blockUI(
                {
                    message: "Please wait......",
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#111',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    },
                    overlayCSS: { backgroundColor: '#ccc' }
                }
            );
}




