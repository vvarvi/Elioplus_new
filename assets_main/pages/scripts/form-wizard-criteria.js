var FormWizard = function () {


    return {
        //main function to initiate the module
        init: function () {
            if (!jQuery().bootstrapWizard) {
                return;
            }

            function format(state) {
                if (!state.id) return state.text; // optgroup
                return "<img class='flag' src='/assets/global/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
            }

            $("#CbxSuppDedicated").click(function (event) {
                var x1 = document.getElementById("CbxSuppIndifferent");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxSuppPhone").click(function (event) {
                var x1 = document.getElementById("CbxSuppIndifferent");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxSuppMail").click(function (event) {
                var x1 = document.getElementById("CbxSuppIndifferent");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxSuppIndifferent").click(function (event) {
                var x1 = document.getElementById("CbxSuppDedicated");
                var x2 = document.getElementById("CbxSuppPhone");
                var x3 = document.getElementById("CbxSuppMail");

                var y1 = x1.parentNode;
                var y2 = x2.parentNode;
                var y3 = x3.parentNode;

                if (this.checked) {
                    x1.checked = x2.checked = x3.checked = false;
                    y1.className = y2.className = y3.className = "";
                    $("input.CbxSuppIndifferent").attr("disabled", true);
                } else {
                    $("input.CbxSuppIndifferent").removeAttr("disabled");
                }
            });

            $("#CbxCountriesSelAll").click(function (event) {
                var x1 = document.getElementById("CbxCountriesAsiaPacif");
                var x2 = document.getElementById("CbxCountriesAfrica");
                var x3 = document.getElementById("CbxCountriesEurope");
                var x4 = document.getElementById("CbxCountriesMidEast");
                var x5 = document.getElementById("CbxCountriesNortAmer");
                var x6 = document.getElementById("CbxCountriesSoutAmer");
                var x7 = document.getElementById("CbxCountriesArgen");
                var x8 = document.getElementById("CbxCountriesAustr");
                var x9 = document.getElementById("CbxCountriesBraz");
                var x10 = document.getElementById("CbxCountriesCanada");
                var x11 = document.getElementById("CbxCountriesFrance");
                var x12 = document.getElementById("CbxCountriesGermany");
                var x13 = document.getElementById("CbxCountriesIndia");
                var x14 = document.getElementById("CbxCountriesMexico");
                var x15 = document.getElementById("CbxCountriesPakistan");
                var x16 = document.getElementById("CbxCountriesSpain");
                var x17 = document.getElementById("CbxCountriesUnKing");
                var x18 = document.getElementById("CbxCountriesUnStat");

                var y1 = x1.parentNode;
                var y2 = x2.parentNode;
                var y3 = x3.parentNode;
                var y4 = x4.parentNode;
                var y5 = x5.parentNode;
                var y6 = x6.parentNode;
                var y7 = x7.parentNode;
                var y8 = x8.parentNode;
                var y9 = x9.parentNode;
                var y10 = x10.parentNode;
                var y11 = x11.parentNode;
                var y12 = x12.parentNode;
                var y13 = x13.parentNode;
                var y14 = x14.parentNode;
                var y15 = x15.parentNode;
                var y16 = x16.parentNode;
                var y17 = x17.parentNode;
                var y18 = x18.parentNode;

                if (this.checked) {
                    y1.className = y2.className = y3.className = y4.className = y5.className = y6.className = y7.className = y8.className = y9.className = y10.className = y11.className = y12.className = y13.className = y14.className = y15.className = y16.className = y17.className = y18.className = "checked";
                    x1.checked = x2.checked = x3.checked = x4.checked = x5.checked = x6.checked = x7.checked = x8.checked = x9.checked = x10.checked = x11.checked = x12.checked = x13.checked = x14.checked = x15.checked = x16.checked = x17.checked = x18.checked = false;
                    $("input.CbxCountriesSelAll").attr("disabled", true);
                }
                else {
                    $("input.CbxCountriesSelAll").removeAttr("disabled");
                    y1.className = y2.className = y3.className = y4.className = y5.className = y6.className = y7.className = y8.className = y9.className = y10.className = y11.className = y12.className = y13.className = y14.className = y15.className = y16.className = y17.className = y18.className = "";
                }
            });

            $("#CbxCountriesAsiaPacif").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesAfrica").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesEurope").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesMidEast").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesNortAmer").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesSoutAmer").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesArgen").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesAustr").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesBraz").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesCanada").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesFrance").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesGermany").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesIndia").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesMexico").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesPakistan").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesSpain").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesUnKing").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            $("#CbxCountriesUnStat").click(function (event) {
                var x1 = document.getElementById("CbxCountriesSelAll");
                var y1 = x1.parentNode;

                if (this.checked) {
                    x1.checked = false;
                    y1.className = "";
                }
            });

            var form = $('#submit_form');
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    //account
                    username: {
                        minlength: 5,
                        required: true
                    },
                    password: {
                        minlength: 5,
                        required: true
                    },
                    rpassword: {
                        minlength: 5,
                        required: true,
                        equalTo: "#submit_form_password"
                    },
                    //profile
                    fullname: {
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    phone: {
                        required: true
                    },
                    gender: {
                        required: true
                    },
                    address: {
                        required: true
                    },
                    city: {
                        required: true
                    },
                    country: {
                        required: true
                    },
                    setUpFee: {
                        required: true
                    },
                    revenue: {
                        required: true
                    },
                    //payment
                    card_name: {
                        required: true
                    },
                    card_number: {
                        minlength: 16,
                        maxlength: 16,
                        required: true
                    },
                    card_cvc: {
                        digits: true,
                        required: true,
                        minlength: 3,
                        maxlength: 4
                    },
                    card_expiry_date: {
                        required: true
                    },
                    'payment[]': {
                        required: true,
                        minlength: 1
                    },
                    'support[]': {
                        required: true,
                        minlength: 1
                    }
                },

                messages: { // custom messages for radio buttons and checkboxes
                    'payment[]': {
                        required: "Please select at least one option",
                        minlength: jQuery.validator.format("Please select at least one option")
                    },
                    'support[]': {
                        required: "Please select at least one option",
                        minlength: jQuery.validator.format("Please select at least one option")
                    }
                },

                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.attr("name") == "gender") { // for uniform radio buttons, insert the after the given container
                        error.insertAfter("#form_gender_error");
                    } else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
                        error.insertAfter("#form_payment_error");
                    } else if (element.attr("name") == "support[]") { // for uniform checkboxes, insert the after the given container
                        error.insertAfter("#form_payment_error1");
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavior
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    success.hide();
                    error.show();
                    App.scrollTo(error, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    if (label.attr("for") == "gender" || label.attr("for") == "payment[]" || label.attr("for") == "support[]") { // for checkboxes and radio buttons, no need to show OK icon
                        label
                            .closest('.form-group').removeClass('has-error').addClass('has-success');
                        label.remove(); // remove error label here
                    } else { // display success icon for other inputs
                        label
                            .addClass('valid') // mark the current input as valid and display OK icon
                        .closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                    }
                },

                submitHandler: function (form) {
                    success.show();
                    error.hide();
                    //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
                }

            });

            var displayConfirm = function () {
                $('#tab4 .form-control-static', form).each(function () {
                    var input = $('[name="' + $(this).attr("data-display") + '"]', form);
                    if (input.is(":radio")) {
                        input = $('[name="' + $(this).attr("data-display") + '"]:checked', form);
                    }
                    if (input.is(":text") || input.is("textarea")) {
                        $(this).html(input.val());
                    } else if (input.is("select")) {
                        $(this).html(input.find('option:selected').text());

                        //                        if ($(this).html(input.find('option:selected').text() == 'Select one')) {
                        //                            $(this).html('-');
                        //                        }
                        //                        else {
                        //                            $(this).html(input.find('option:selected').text());
                        //                        }

                    } else if (input.is(":radio") && input.is(":checked")) {
                        $(this).html(input.attr("data-title"));
                    } else if ($(this).attr("data-display") == 'payment[]') {
                        var payment = [];
                        $('[value="payment[]"]:checked', form).each(function () {
                            payment.push($(this).attr('data-title'));
                        });
                        $(this).html(payment.join("<br>"));
                    } else if ($(this).attr("data-display") == 'support[]') {
                        var support = [];
                        $('[value="support[]"]:checked', form).each(function () {
                            support.push($(this).attr('data-title'));
                        });
                        $(this).html(support.join("<br>"));
                    } else if ($(this).attr("data-display") == 'countries[]') {
                        var countries = [];
                        $('[value="countries[]"]:checked', form).each(function () {
                            countries.push($(this).attr('data-title'));
                        });

                        if (countries.length == 0)
                            countries[0] = 'Select All';

                        $(this).html(countries.join("<br>"));
                    } else if (input.is(":file") && input.attr("id") == 'PdfUploadFile') {
                        var pdfName = document.getElementById('PdfUploadFile').value;
                        $(this).html(pdfName.split(/(\\|\/)/g).pop());
                    } else if (input.is(":file") && input.attr("id") == 'csvFile') {
                        var csvName = document.getElementById('csvFile').value;
                        $(this).html(csvName.split(/(\\|\/)/g).pop());
                    }
                });
            }

            var handleTitle = function (tab, navigation, index) {
                var total = navigation.find('li').length;
                var current = index + 1;
                // set wizard title
                $('.step-title', $('#form_wizard_1')).text('Step ' + (index + 1) + ' of ' + total);
                // set done steps
                jQuery('li', $('#form_wizard_1')).removeClass("done");
                var li_list = navigation.find('li');
                for (var i = 0; i < index; i++) {
                    jQuery(li_list[i]).addClass("done");
                }

                if (current == 1) {
                    $('#form_wizard_1').find('.button-previous').hide();
                } else {
                    $('#form_wizard_1').find('.button-previous').show();
                }

                if (current >= total) {
                    $('#form_wizard_1').find('.button-next').hide();
                    $('#form_wizard_1').find('.button-submit').show();
                    displayConfirm();
                } else {
                    $('#form_wizard_1').find('.button-next').show();
                    $('#form_wizard_1').find('.button-submit').hide();
                }
                //                App.scrollTo($('.page-title'));
            }

            // default form wizard
            $('#form_wizard_1').bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                onTabClick: function (tab, navigation, index, clickedIndex) {
                    return false;

                    success.hide();
                    error.hide();
                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, clickedIndex);
                },
                onNext: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, index);
                },
                onPrevious: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    handleTitle(tab, navigation, index);
                },
                onTabShow: function (tab, navigation, index) {
                    var total = navigation.find('li').length;
                    var current = index + 1;
                    var $percent = (current / total) * 100;
                    $('#form_wizard_1').find('.progress-bar').css({
                        width: $percent + '%'
                    });
                }
            });

            $('#form_wizard_1').find('.button-previous').hide();
            $('#form_wizard_1 .button-submit').click(function () {
                SubmitFunction();
            }).hide();

            //apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            $('#country_list', form).change(function () {
                form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            });
        }

    };

} ();

jQuery(document).ready(function () {
    FormWizard.init();
});