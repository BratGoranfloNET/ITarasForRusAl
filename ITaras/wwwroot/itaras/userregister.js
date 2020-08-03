$(document).ready(function () {
    var inputBirthDate = $('#BirthDate')[0];

    $("#BirthDate").keypress(function (event) {
        event = event || window.event;
        if (event.charCode && event.charCode != 0 && event.charCode != 46 && (event.charCode < 48 || event.charCode > 57))
            return false;
    });


    $("#Phone").keypress(function (event) {
        event = event || window.event;
        if (event.charCode && event.charCode != 0 && event.charCode != 43 && (event.charCode < 48 || event.charCode > 57))
            return false;
    });

    // Маска для ввода даты
    var dateInputMask = function dateInputMask(elm) {

        elm.addEventListener('keyup', function (e) {

            if (event.keyCode < 48 || event.keyCode > 57)
                event.returnValue = false;

            if (e.keyCode < 47 || e.keyCode > 57) {
                e.preventDefault();
            }

            var len = elm.value.length;

            if (len !== 1 || len !== 3) {
                if (e.keyCode == 47) {
                    e.preventDefault();
                }
            }
            if (len === 2) {
                if (e.keyCode !== 8 && e.keyCode !== 46) {
                    elm.value = elm.value + '.';
                }
            }

            if (len === 5) {
                if (e.keyCode !== 8 && e.keyCode !== 46) {
                    elm.value = elm.value + '.';
                }
            }
        });
    };

    dateInputMask(inputBirthDate);
});


function RegisterUser() {

    var FavoriteColors = ($("#checkbox_blue").prop('checked') ? "1" : "0") +
        ($("#checkbox_yellow").prop('checked') ? "1" : "0") +
        ($("#checkbox_red").prop('checked') ? "1" : "0");

    var FavoriteDrinks = ($("#checkbox_tea").prop('checked') ? "1" : "0") +
        ($("#checkbox_coffe").prop('checked') ? "1" : "0") +
        ($("#checkbox_juice").prop('checked') ? "1" : "0") +
        ($("#checkbox_water").prop('checked') ? "1" : "0");

    if (!ValidateInputs())
        return;
    var User = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        BirthDate: $('#BirthDate').val(),
        Phone: $('#Phone').val(),
        FavoriteColors: FavoriteColors,
        FavoriteDrinks: FavoriteDrinks
    };
    
    $.ajax({
        url: '/Users/Register',
        type: 'POST',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(User),
        success: function (response) {
            if (response.isExit) {
                var name = $('#Name');
                name.closest('.form-group').addClass('has-error');
                name.closest('.form-group').addClass('has-error');
                return;
            }
            Clean();
            $('#btnCloseModal').click();            
        },
        error: function (err) { alert("Error: " + err.responseText); }
    });
};

function Clean() {
    $('#modalRegister').find('textarea,input').val('');
};

function ValidateInputs() {
    var flag = true;
    var firstname = $('#FirstName');
    if ($.trim(firstname.val()) != '') {       
        flag = true;
    }
    if ($.trim(firstname.val()) === '') {       
        flag = false;
    }

    var lastname = $('#LastName');
    if ($.trim(lastname.val()) != '') {       
        flag = true;
    }
    if ($.trim(lastname.val()) === '') {       
        flag = false;
    }

    var phone = $('#Phone');
    if ($.trim(phone.val()) != '') {        
        flag = true;
    }
    if ($.trim(phone.val()) === '') {       
        flag = false;
    }

    if (flag) {
        $('#showError').hide();
    }
    else {
        $('#showError').show();
    }
    return flag;
};
