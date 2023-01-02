/*variable declaration*/
var serviceList = [];
var hardwareList = [];
var Token = $("#hostID").val();
var MobileOTPAPIKey = $("#mobileOTPAPIKey").val();
var MobileOTPSenderId = $("#mobileOTPSenderID").val();
var MobileOTPChennel = $("#mobileOTPChennel").val();
var panVerified = 0;
var adharVerified = 0;
var mobileverified = 0;
var Emailverified = 0;
var IsGSTApplicable = 0;
var IsTDSApplicable = 0;
var imagebase64 = "";

var sendedOTP = 0;

/*variable declaration*/

/*Restrict charecters not allows Spaces*/


/*Restrict charecters allows only integer*/


var globaUserProperties = new Object();
var base_url = window.location;
globaUserProperties.domain = base_url.origin;// + "/" + window.location.pathname.split('/')[1];


function Login() {
    //   alert('hello');
    //var dat = globaUserProperties.domain + "/LoginSignup/Login";
    //alert(dat);
    //var Params = JSON.stringify({
    //    Email: $("#user_name").val(),
    //    Password: $("#Loginpassword").val()
    //});
    //$.ajax({
    //    url: globaUserProperties.domain + "/LoginSignup/Login", //globaUserProperties.domain +
    //    type: 'POST',
    //    dataType: "json",
    //    data: Params,
    //    contentType: "application/json",
    //    enctype: 'multipart/form-data',
    //    success: function (data) {

    //        alert(data)
    //    },
    //    error: function (d) {
    //        console.log(d);
    //    }
    //});

    //$.ajax({
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json'
    //    },
    //    url: globaUserProperties.domain + "/LoginSignup/Login", //globaUserProperties.domain +
    //    dataType: 'json',
    //    type: 'Post',
    //    cache: false,
    //    data: Params,
    //    success: function (data) {
    //        console.log('ajax');
    //        console.log(data.Data.IsActive);
    //        var status = data.Data.IsActive;
    //        if (data.StatusCode == 1 && status == 1) {
    //            sessionStorage.setItem('UserData', JSON.stringify(data.Data));
    //            sessionStorage.setItem('Token', data.Token);

    //            var sd = JSON.parse(sessionStorage.getItem('UserData'));

    //            var emailId = sd.EmailId;

    //            var url = globaUserProperties.domain +"/Welcome/Setsession?email=" + emailId; //globaUserProperties.domain+

    //            window.location.href = url;
    //        }

    //        else if (data.StatusCode == 1 && status == 0) {
    //            Swal.fire({
    //                position: 'center',
    //                icon: 'error',
    //                title: 'Please Contact to administration to Activate your account...!',
    //                allowOutsideClick: false,
    //                showCloseButton: true,
    //                showConfirmButton: false,
    //            });

    //            $("#user_name").val('');
    //            $("#Loginpassword").val('');
    //        }
    //        else {

    //            Swal.fire({
    //                position: 'center',
    //                icon: 'error',
    //                title: 'Invalid Credentials...!',
    //                allowOutsideClick: false,
    //                showCloseButton: true,
    //                showConfirmButton: false,
    //            });
    //            $("#user_name").val('');
    //            $("#Loginpassword").val('');

    //        }
    //    },
    //    error: function (d) {
    //        console.log(d);
    //    }
    //});
}

function validateUser() {

    console.log('validate');
    var validationRequestModel = {
        "Email": $("#Resetuser_name").val(),
        "Password": $("#txtResetpassword").val(),
        "Type": 'UserCheck'
    }

    var d = JSON.stringify(validationRequestModel);
   
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: globaUserProperties.domain +"/LoginSignup/ValidateUser",
        dataType: 'json',
        type: 'Post',
        cache: false,
        data: JSON.stringify(validationRequestModel),
        success: function (data) {
            console.log('ajax');
            console.log(data);

            if (data.StatusCode == 0 && data.Message == "User Details Not Exists") {
                $("#txtResetpassword").attr('disabled', 'disabled');
                $("#txtResetre-password").attr('disabled', 'disabled');

                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: data.Message + ' Please Try Again...!',
                    allowOutsideClick: false,
                    showCloseButton: true,
                    showConfirmButton: false,
                });
            }
            else {
                $("#txtResetpassword").removeAttr('disabled');
                $("#txtResetre-password").removeAttr('disabled');
            }

        },
        error: function (d) {
            console.log(d);
        }
    });


}

function clearDataFromReset() {
    $("#txtResetpassword").val('');
    $("#txtResetre-password").val('');
    $("#Resetuser_name").val('')
}

function CheckUserMobile() {
    var m = $("#mobile").val();

    if (m.length > 0) {
        var validationRequestModel = {
            "Email": $("#mobile").val(),
            "Password": $("#txtResetpassword").val(),
            "Type": 'UserCheck'
        }

        var d = JSON.stringify(validationRequestModel);
        console.log(d);
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain + "/LoginSignup/ValidateUser",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(validationRequestModel),
            success: function (data) {
                console.log('ajax');
                console.log(data);

                if (data.StatusCode == 1 && data.Message == "User Details Exists") {
                    var d = "Mobile Number Already Registered";
                    sweetAlert(d + ", Please Try Again...", 'center', 'warning', 0);
                    $("#mobile").val('');
                }
            },
            error: function (d) {
                console.log(d);
            }
        });
    }
}

function CheckUserEmailAddress() {
    var m = $("#email").val();

    if (m.length > 0) {
        var validationRequestModel = {
            "Email": $("#email").val(),
            "Password": $("#txtResetpassword").val(),
            "Type": 'UserCheck'
        }

        var d = JSON.stringify(validationRequestModel);
        console.log(d);
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/ValidateUser",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(validationRequestModel),
            success: function (data) {
                console.log('ajax');
                console.log(data);

                if (data.StatusCode == 1 && data.Message == "User Details Exists") {
                    var d = "Email Address Already Registered";
                    sweetAlert(d + ", Please Try Again...", 'center', 'warning', 0);
                    $("#email").val('');
                }
            },
            error: function (d) {
                console.log(d);
            }
        });
    }
}

function Resetpassword() {

    if ($("#txtResetpassword").val().length < 6) {

        var dic = '<div><h4><b>Password Must Have 6 Characters Long</b></h4></div>';

        sweetAlert(dic, 'center', 'warning', 0);
        //$("#re-password").addClass('warning');

    }
    else if ($("#txtResetpassword").val() != $("#txtResetre-password").val()) {

        var dic = '<div><h4><b>Password not matched</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
        //$("#re-password").addClass('warning');
    }
    else {

        var LoginModel = {
            "Email": $("#Resetuser_name").val(),
            "Password": $("#txtResetpassword").val(),
            "Type": 'Reset'
        }

        var d = JSON.stringify(LoginModel);

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/ResetPassword",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(LoginModel),
            success: function (data) {
                console.log('ajax');
                console.log(data);

                if (data.StatusCode == 1 && data.Message == "Password Updated Successfully!") {
                    sweetAlert(data.Message, 'center', 'warning', 0);

                    $("#Resetuser_name").val('');
                    $("#txtResetpassword").val('');
                    $("#txtResetre-password").val('');

                    $("#bcktoLogin").click();
                }
                else {

                }

            },
            error: function (d) {
                console.log(d);
            }
        });

    }
}

function ChangeForm(item, step) {

    var yetToMove = validateform(item);

    if (item.substr(4, 1) > 2) {
        if ($("#password").val().length < 6) {
            yetToMove = false;
            var dic = '<div><h4><b>Password Must Have 6 Characters Long</b></h4></div>';
            sweetAlert(dic, 'center', 'warning', 0);
            $("#re-password").addClass('warning');
        }
    }

    if ($("#password").val() != $("#re-password").val()) {
        yetToMove = false;
        var dic = '<div><h4><b>Password not matched</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
        $("#re-password").addClass('warning');
    }

    if ($('#UserType').val() == 0) {
        yetToMove = false;
        var dic = '<div><h4><b>Please Select User Type</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
        $("#UserType").addClass('warning');
    }

    if ($("#aadhar").val().length < 12 && $("#aadhar").val().length >0) {
            yetToMove = false;
            var dic = '<div><h4><b>Aadhaar Must Have 12 Characters Long</b></h4></div>';
            sweetAlert(dic, 'center', 'warning', 0);
        }

    if (!validatePanFormat($('#pan').val()) && $('#pan').val().length>0) {
        yetToMove = false;
    }
    if (!validateGSTFormat($('#gst').val()) && $('#gst').val().length > 0) {
        yetToMove = false;
    } 

    var selectedUserType = $('#UserType').val();
    if (selectedUserType === "1") {
        $('#gst').attr('require', 'true');
        //console.log('addede');
        //$('.input-div #chkGSTApplicable').removeAttr('disabled');
        $('.input-div #chkGSTApplicable').attr('checked', true);
        //alert('checked added');
        IsGSTApplicable = 1;
        $('#divGST').css('display', 'block');
    }
    else {
        $('#gst').removeAttr('require');
        $('.input-div #chkGSTApplicable').removeAttr('checked')
        //$('.input-div #chkGSTApplicable').attr('disabled', true);

        IsGSTApplicable = 0
        $('#divGST').css('display', 'none');
        //var status = $("#chkGSTApplicable").is(':checked');
        //if (status) {
        //    IsGSTApplicable = 1;
        //    $('#divGST').css('display', 'block');
        //} else {
        //    IsGSTApplicable = 0
        //    $('#divGST').css('display', 'none');
        //}
    }
  
    var NameofUser = $("#name").val();
    var EmailofUser = $("#email").val();
    var MobileofUser = $("#mobile").val();
    const myArray = NameofUser.split(" ");
    const last4Digits = MobileofUser.substr(6, 4);
    var finalUserName = myArray[0] + last4Digits

    if (item == 'main2') {
        $("#Creusername").val(finalUserName);
    }
    $("#verifyMobile").val(parseInt(MobileofUser));
    $("#verifyEmail").val(EmailofUser);

    if (yetToMove == true) {
        //console.log('inside change');

        var stepdata = parseInt(item.substr(4, 1));
        $("#main1").css('display', 'none');
        $("#main2").css('display', 'none');
        $("#main3").css('display', 'none');
        $("#main4").css('display', 'none');
        $("#main5").css('display', 'none');
        $("#main6").css('display', 'none');
        $("#Login").css('display', 'none');
        $("#Resetpassword").css('display', 'none');

        var GetAllToRemoveActiveClass = document.querySelectorAll('[id^=main]');

        GetAllToRemoveActiveClass.forEach(function (input_valid) {
            input_valid.classList.remove('active');
        });


        $("#" + item).removeAttr('style');
        $("#" + item).addClass('active');

        if (step == 'Next') {
            $("#step" + stepdata).addClass('active');
            showProgress(stepdata, step);
        }
        if (step == 'Prev') {
            stepdata = stepdata + 1
            $("#step" + stepdata).removeClass('active');

            showProgress(stepdata, step);
        }
    }
}

function ChangeFormWitoutVelidation(item, step) {

    var yetToMove = true;

    if (yetToMove == true) {
        //console.log('inside change');

        var stepdata = parseInt(item.substr(4, 1));
        $("#main1").css('display', 'none');
        $("#main2").css('display', 'none');
        $("#main3").css('display', 'none');
        $("#main4").css('display', 'none');
        $("#main5").css('display', 'none');
        $("#main6").css('display', 'none');
        $("#Login").css('display', 'none');
        $("#Resetpassword").css('display', 'none');

        var GetAllToRemoveActiveClass = document.querySelectorAll('[id^=main]');

        GetAllToRemoveActiveClass.forEach(function (input_valid) {
            input_valid.classList.remove('active');
        });


        $("#" + item).removeAttr('style');
        $("#" + item).addClass('active');

        if (step == 'Next') {
            $("#step" + stepdata).addClass('active');
            showProgress(stepdata, step);
        }
        if (step == 'Prev') {
            stepdata = stepdata + 1
            $("#step" + stepdata).removeClass('active');

            showProgress(stepdata, step);
        }
    }
}

function validateMobile() {

    if ($("#mobile").val().length > 0) {
        if ($("#mobile").val().substr(0, 1) == 0 || $("#mobile").val().length < 10) {
            var dic = '<div><h4><b>Mobile No</b> Length Must be <b>10 Digits</b> long and should start with 1 or greater</h4></div>';
            sweetAlert(dic, 'center', 'warning', 0);
        }
    }
}

function isValidEmailAddress(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function sweetAlert(msg, position, icon, timer) {
    Swal.fire({
        position: position,
        icon: icon,
        title: msg,
        showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        },
        timer: timer,
        allowOutsideClick: false,
        showCloseButton: true,
        showConfirmButton: false
        //html: 'I will close in <b></b> milliseconds.',
        //timer: 2000,
        //timerProgressBar: true,
        //background: '#fff url(/Content/LoginSignUp/img/midiumlogo.png)',
        //backdrop: `rgba(0,0,123,0.4)
        //        url("/Content/LoginSignUp/img/image1.gif")
        //        left top
        //        no-repeat`
    })
}

function numberOnly(id) {
    // Get element by id which passed as parameter within HTML element event
    var element = document.getElementById(id);
    // This removes any other character but numbers as entered by user
    element.value = element.value.replace(/[^0-9]/gi, "");
}

function showProgress(item, moving) {
    if (moving == 'Next') {
        if (item == 2) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "60px");
        }
        if (item == 3) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "130px");
        }
        if (item == 4) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "200px");
        }
        if (item == 5) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "270px");
        }
        if (item == 6) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "341px");
        }
    }
    if (moving == 'Prev') {
        if (item == 2) {
            $("#bar").removeAttr('style');
            $("#bar").css("display", "none");
        }
        if (item == 3) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "60px");
        }
        if (item == 4) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "130px");
        }
        if (item == 5) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "200px");
        }
        if (item == 6) {
            $("#bar").removeAttr('style');
            $("#bar").css("height", "270px");
        }
    }
}

function SelectService() {

    var validate_inputs = document.querySelectorAll('[id^=check]:checked');

    serviceList = [];
    validate_inputs.forEach(function (input_valid) {
        var onlyId = input_valid.id.replace('check', '');

        var serviceNameActual = $('#service' + onlyId + ' h3').text();
        var Price = $('#service' + onlyId + ' span.price-one').text();
        var GSTValue = $('#service' + onlyId + ' #GSTVlaue').text();
        //var RegistrationVlaue = $('#service' + onlyId + ' #RegistrationVlaue').text();
        ////var TotalServicePrice = $('#service' + onlyId + ' #TotalServicePrice').text();
        //var priceGST = (parseInt(RegistrationVlaue) * parseInt(GSTValue)) / 100;
        serviceList.push({
            CategoryID: parseInt(onlyId),
            ServiceHardwareName: serviceNameActual,
            TotalPrice: Price.trim(),
            GSTPrice: GSTValue,
            Type: 1
        });
    });
}

function clickDivtocheck(item) {

    var onlyid = item.id.replace('service', '');
    $('#check' + parseInt(onlyid)).click();

    //console.log(item);

}

function GethardwareList() {
    hardwareList = [];
    $("#hardwareList input[type=checkbox]:checked").each(function () {
        var row = $(this).closest("tr")[0];
        var gst = row.cells[3].innerHTML.trim();

        hardwareList.push({
            CategoryID: parseInt(row.cells[0].innerHTML),
            ServiceHardwareName: row.cells[1].innerHTML,
            TotalPrice: row.cells[2].innerHTML,
            GSTPrice: row.cells[3].innerHTML.trim(),
            Type: 2
        });
    });

    ////console.table(hardwareList);
    //var selectedHardware = $("#main5 .input-text").html();

    //console.log('Selected Hardware');
    //console.log(selectedHardware)
    ////localStorage.setItem("HardwareList", selectedHardware)

}

function hidesmenu(status) {

    if (status == 'hide') {

        $("#divmenus").removeClass('show');
        $("#divmenus").addClass(status);

        $("#loginphase").removeClass(status);
        $("#loginphase").addClass('show');
    }
    else {
        $("#divmenus").removeClass('show');
        $("#divmenus").addClass(status);

        $("#loginphase").removeClass(status);
        $("#loginphase").addClass('hide');
    }
}

function changeIcon(sender) {

    var btn = $("#" + sender.id);
    btn.fadeOut(700)
    setTimeout(function () {
        btn.css("background-color", "#28a745").css("font-size", "17px");
        btn.attr("disabled", true);
        btn.html("<i class='fa fa-check-circle'></i>")
    }, 700)
    btn.fadeIn(700)
    btn.html("Verify")
}

function validateform(item) {
    //
    validate = true;
    var validate_inputs = document.querySelectorAll(".main.active .input-text .input-div input");
    validate_inputs.forEach(function (input_valid) {
        //input_valid.classList.remove('warning');
        if (input_valid.hasAttribute('require')) {
            //console.log(item);
            if (item == 'Login' || item == 'Resetpassword' || item == 'main1') {
                validate = true;
            }
            else {
                if (input_valid.value.length == 0) {
                    validate = false;
                    //input_valid.classList.add('warning');
                    //console.log(input_valid.name);
                    var dic = '<div><h4><b>' + input_valid.name + '</b> Is Required, Please Re-try</h4></div>'
                    sweetAlert(dic, 'center', 'warning', 0);
                }

            }
        }
    });

    return validate;
}

function GetHardewareList(item) {
    var serviceIds = "";
    var serviceNames = "";

    if (serviceList.length <= 0) {
        serviceList = [];
        $("#" + item + " span ").html('');
        $("#" + item + " .input-text").html('');
        $("#ServiceOnHardware").html('Please Select Atleast One Service');
    }
    if (serviceList.length >= 1) {
        var serviceTableOnHardware = "<table class='table table-hover table-responsive' id='tableServicesOnHarwarePage'><tr><th>Service Name</th><th>Price</th><th>GST Amount</th></tr>"
        serviceList.forEach(function (service) {
            console.log(service);
            if (serviceIds == "") {
                serviceIds = service.CategoryID;
                serviceNames = service.ServiceHardwareName;
            }
            else {
                serviceIds += "," + service.CategoryID;
                serviceNames += "," + service.ServiceHardwareName;
            }
            serviceTableOnHardware = serviceTableOnHardware + '<tr><td>' + service.ServiceHardwareName + '</td><td>' + parseInt(service.TotalPrice) + ' /- </td><td>' + parseInt(service.GSTPrice) + '</td></tr>';
        });

        serviceTableOnHardware = serviceTableOnHardware + "</table>";
        $("#ServiceOnHardware").html(serviceTableOnHardware);

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain + "/LoginSignup/GetAllHardwares?Type=Hardware",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: "",
            success: function (data) {
                //console.log('ajax');
                //console.log(data);
                if (data.StatusCode == '1') {
                    var hardwareList1 = data.Data;

                    //console.log('Existing Hardwares');
                    //console.log(hardwareList)

                    //console.log('Got From DB');
                    //console.log(hardwareList1)

                    if (hardwareList1 != null) {
                        $("#" + item + " span ").html(serviceNames);
                        var t = "<table class='table table-hover table-responsive' id='hardwareList'><tr><th style='display:none'>HardwareId</th><th>Hardware Name</th><th>Price</th><th>GST Amount</th><th>Select</th></tr>"
                        hardwareList1.forEach(function (service) {

                            //var isexists =  findDataInArray(hardwareList, service.CategoryId);

                            // if (isexists) {
                            //     if (service.GST == '0') {
                            //         t = t + "<tr><td style='display: none'>" + service.CategoryId + "</td><td>" + service.CategoryName + "</td><td>" + service.TotalAmount + "</td><td>0</td><td><input class='form-check-input' type='checkbox' checked/></td></tr>";
                            //     }
                            //     else {
                            //         t = t + "<tr><td style='display: none'>" + service.CategoryId + "</td><td>" + service.CategoryName + "</td><td>" + service.TotalAmount + "</td><td>" + service.GstAmount + "</td><td><input class='form-check-input' type='checkbox' checked/></td></tr>";
                            //     }
                            // }
                            // else {
                            if (service.GST == '0') {
                                t = t + "<tr><td style='display: none'>" + service.CategoryId + "</td><td>" + service.CategoryName + "</td><td>" + service.TotalAmount + "</td><td>0</td><td><input class='form-check-input' type='checkbox'/></td></tr>";
                            }
                            else {
                                t = t + "<tr><td style='display: none'>" + service.CategoryId + "</td><td>" + service.CategoryName + "</td><td>" + service.TotalAmount + "</td><td>" + service.GstAmount + "</td><td><input class='form-check-input' type='checkbox'/></td></tr>";
                            }
                            //}
                            // console.log(service.Name);
                        });
                        t = t + "</table>";
                        $("#" + item + " .input-text").html(t);
                    }
                }


            },
            error: function (d) {
                console.log(d);
            }
        });



    }
}

function BindCheckoutData() {
    var serviceTable = "";
    var hardwareTable = "";
    var totalServicePrice = 0;
    var totalHardwarePrice = 0;
    var totalGSTPrice = 0;

    $("#ServiceCheckout").html('');
    serviceTable = "<table class='table table-hover table-responsive' id='tableServices'><tr><th style='display:none'>Service Id</th><th>Service Name</th><th>Price</th><th>GST Amount</th><th>Action</th></tr>"

    serviceList.forEach(function (service) {
        serviceTable = serviceTable + "<tr><td style='display:none'>" + service.CategoryID + "</td><td>" + service.ServiceHardwareName + "</td><td>" + parseInt(service.TotalPrice) + "</td><td>" + parseInt(service.GSTPrice) + "</td><td style='text-align:center'><i class='fa fa-1x fa-trash-alt' style='color:red' onclick='DeleteItemFromList(" + service.CategoryID + ")'></i></td></tr>"

        totalServicePrice = parseInt(totalServicePrice) + parseInt(service.TotalPrice);

        totalGSTPrice += parseInt(service.GSTPrice);

    });

    serviceTable = serviceTable + "</table>";
    $("#ServiceCheckout").html(serviceTable);


    $("#hardwareCheckout").html('');

    hardwareTable = "<table class='table table-hover table-responsive' id='tableHardware'><tr><th>Hardware Name</th><th>Price</th><th>GST Amount</th><th>Action</th></tr>"

    hardwareList.forEach(function (hardware) {

        hardwareTable = hardwareTable + "<tr><td>" + hardware.ServiceHardwareName + "</td><td>" + parseInt(hardware.TotalPrice) + "</td><td>" + parseInt(hardware.GSTPrice) + "</td><td style='text-align: center'><i class='fa fa-1x fa-trash-alt' style='color: red' onclick='DeleteItemFromHardwareList(" + hardware.CategoryID + ")'></i></td></tr>"

        totalHardwarePrice = (parseInt(totalHardwarePrice) + parseInt(hardware.TotalPrice));

        totalGSTPrice += parseInt(hardware.GSTPrice);
    });
    hardwareTable = hardwareTable + "</table>";
    $("#hardwareCheckout").html(hardwareTable);

    var finalSum = parseInt(totalServicePrice) + parseInt(totalHardwarePrice)
    var grandTotal = parseInt(finalSum) + parseInt(totalGSTPrice);

    var desing = '<table class="table table-responsive"><tbody>'
        + '<tr><td style="text-align: right;"><b>Total Price</b></td><td><Total_Price></td></tr>'
        + '<tr><td style="text-align: right;"><b>GST Price</b></td><td><GST_Price></td></tr>'
        + '<tr><td style="text-align: right;"><b>Grand Total</b></td><td><Grand_Total></td></tr>'
        + '</tbody></table>'

    var Finaldesing = desing.replace('<Total_Price>', finalSum);
    Finaldesing = Finaldesing.replace('<GST_Price>', totalGSTPrice);
    Finaldesing = Finaldesing.replace('<Grand_Total>', grandTotal);

    $("#TotalCheckout").html(Finaldesing);
}

function SaveDetails(isSaved) {

    // Type : 1 for Company, 2 for distributer, 3 for Retailer
    // 
    var SelectedServices = "";
    //alert(serviceList.length);
    serviceList.forEach(function (service) {
        if (SelectedServices == "") {
            SelectedServices = service.CategoryID;
        }
        else {
            SelectedServices += "," + service.CategoryID;
        }
    });

    var username = $("#Creusername").val();
    //alert(username);
    //console.log(username);

    var panImage = $('#UploadedPanImage').val().split(",");
    var aadharImage = $('#UploadedAadharImage').val().split(",");

    //console.log(panImage[1]);
    //console.log(aadharImage[1]);

    //var purchaseList = serviceList.concat(hardwareList);

    //console.table(purchaseList);

    var statesModel = {
        "FirstName": $("#name").val(),
        "MobileNumber": parseInt($("#mobile").val()),
        "Type": $("#UserType").val(),
        "EmailId": $("#email").val(),
        "Name": username,
        "Password": $("#password").val(),
        "ServicesCollection": SelectedServices,
        "PanCard": $("#pan").val(),
        "AdhaarCard": $("#aadhar").val(),
        "Gst": $("#gst").val(),
        "PanVerified": panVerified,
        "AdhaarVerified": adharVerified,
        "MobileVerified": mobileverified,
        "EmailVerified": Emailverified,
        "PanCardImg": panImage[1],
        "AdhaarCardImg": aadharImage[1],
        "IsGST_Applicable": parseInt(IsGSTApplicable),
        "IsTDS_Applicable": parseInt(IsTDSApplicable)
    }
    //,"PurchaseLists": purchaseList     ----- This is not needed here it is sended when user clicks on pay now.

    var d = JSON.stringify(statesModel);
    console.table(d);
    if (isSaved === 'Yes') {

        $('#loadingDivwhileRecharge').css('display', 'flex');

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/SaveDetails",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(statesModel),
            success: function (data) {
                console.log('ajax');
                console.log(data);
                if (data.StatusCode == 1) {
                    var dic = '<div><h4><b>' + data.Message + '</b></h4></div>';

                    sweetAlert(dic, 'center', 'success', 0);
                    $('#UserID').val(data.Data.UserID);
                    $('#RegNo').val(data.Data.RegistrationId);
                    $('#PartialorderId').val(data.Data.ID);

                    console.log('UserId', $('#UserID').val());
                    console.log('Reg', $('#RegNo').val());
                    console.log('OrderId', $('#PartialorderId').val());

                    ChangeForm('main6', 'Next');
                       $('#loadingDivwhileRecharge').css('display', 'none');
                }
                //if (data.statusCode == 1)
                //    ChangeForm('Login', 'Prev'); hidesmenu('hide');
            },
            error: function (d) {
                console.log(d);
                //alert("404. Please wait until the File is Loaded.");
            }
        });
    }
    /*  alert($('#UserInsertedID').val());*/
}

function verifyDocs(doctype, sender) {
    var VerificationURL = "";
    var id = "";


    if (doctype === "PAN") {
        VerificationURL = 'https://kyc-api.aadhaarkyc.io/api/v1/pan/pan';
        id = $("#pan").val();

        var h = '<span class="spinner1" style="font-size: 20px;padding: 0px 5px;"><i class="fa fa-spinner fa-pulse"></i></span>'

        $('#panvarify').addClass('has-spinner1').addClass('activeSpin');

        $('#panvarify').html('');
        $('#panvarify').html(h);
    }

    if (doctype === "AADHAROTP") {
        VerificationURL = 'https://kyc-api.aadhaarkyc.io/api/v1/aadhaar-v2/generate-otp';
        id = $("#aadhar").val();

        var h = '<span class="spinner1" style="font-size: 20px;padding: 0px 5px;"><i class="fa fa-spinner fa-pulse"></i></span>'

        $('#aadharvarify').addClass('has-spinner1').addClass('activeSpin');

        $('#aadharvarify').html('');
        $('#aadharvarify').html(h);

    }

    if (id.length <= 0) {
        if (doctype.includes("OTP")) {
            doctype = doctype.replace("OTP", "");
        }
        var dic = '<div><h4><b>' + doctype + ' Number is not valid</b></h4>please try again...</div>';
        sweetAlert(dic, 'center', 'warning', 0);

        $('#panvarify').removeClass('has-spinner1').removeClass('activeSpin');
        $('#panvarify').html('Verify');

        $('#aadharvarify').removeClass('has-spinner1').removeClass('activeSpin');
        $('#aadharvarify').html('OTP');
    }
    else {


        var Model = {
            "id_number": id,
            "url": VerificationURL,
            "type": doctype
        }

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/VerifyAdharPAN",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(Model),
            success: function (data) {

                if (data.OperationType == "PAN") {
                    if (data.StatusCode == 200 && data.Message == "success") {

                        var btn = $("#panvarify");
                        btn.removeClass('has-spinner1').removeClass('activeSpin');
                        btn.fadeOut(700)
                        setTimeout(function () {
                            btn.css("background-color", "#28a745").css("font-size", "17px").css("padding", "0px 37px");
                            btn.attr("disabled", true);
                            btn.html("<i class='fa fa-check-circle'></i>")
                        }, 700)
                        btn.fadeIn(700)
                        btn.html("Verify")

                        panVerified = 1;
                    }
                    else {
                        var btn = $("#panvarify");
                        btn.removeClass('has-spinner1').removeClass('activeSpin');
                        btn.fadeOut(700)
                        setTimeout(function () {
                            btn.css("background-color", "Red").css("font-size", "17px").css("padding", "0px 37px");
                            //btn.attr("disabled", true);
                            btn.html("<i class='fa fa-times-circle'></i>")
                        }, 700)
                        btn.fadeIn(700)
                        btn.html("Verify")
                        panVerified = 0;
                    }
                }

                if (data.OperationType == "AADHAROTP") {

                    if (data.StatusCode == 200 && data.Message == "success") {

                        $("#adharClientID").val(data.Data.client_id);
                        $("#verifyAdhar").css('display', 'block');
                        $("#verifyMobileOTP").css('display', 'none');
                        $("#aadharvarify").removeClass('has-spinner1').removeClass('activeSpin');
                        $("#aadharvarify").html('OTP');

                        var Toast = Swal.mixin({
                            toast: true,
                            position: 'top-end',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                            onOpen: function (toast) {
                                toast.addEventListener('mouseenter', Swal.stopTimer)
                                toast.addEventListener('mouseleave', Swal.resumeTimer)
                            }
                        })
                        Toast.fire({
                            icon: 'success',
                            title: 'OTP Sent To Aadhaar Linked Mobile Number '
                        })

                        $("#buttonotp").click();

                    }

                    if (data.StatusCode == 422 && data.Message == "verification_failed") {
                        $("#aadharvarify").removeClass('has-spinner1').removeClass('activeSpin');
                        $("#aadharvarify").html('OTP');
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: data.Message + ' Please Check Your Aadhaar Number : ' + id,
                            allowOutsideClick: false,
                            showCloseButton: true,
                            showConfirmButton: false
                        });

                        var Toast = Swal.mixin({
                            toast: true,
                            position: 'top-end',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                            onOpen: function (toast) {
                                toast.addEventListener('mouseenter', Swal.stopTimer)
                                toast.addEventListener('mouseleave', Swal.resumeTimer)
                            }
                        })
                        Toast.fire({
                            icon: 'error',
                            title: 'OTP Not Sent'
                        })
                    }

                    console.log(data);
                }

                //console.log(data.Data);
            },
            error: function (d) {
                console.log(d.status);

                if (d.status == 500) {
                    var dic = '<div><h4><b>Server Is Down Kindly Do Manual Verification Process Or Try After 10 Minutes</b></h4></div>';
                    sweetAlert(dic, 'center', 'error', 0);

                    //var btn = $("#panvarify");
                    //btn.removeClass('has-spinner1').removeClass('activeSpin');
                    //btn.html('Verify');

                    $("#aadharvarify").removeClass('has-spinner1').removeClass('activeSpin');
                    $("#aadharvarify").html('OTP');
                }

            }
        });
    }
}

function EmailVerification() {

    var email = $('#verifyEmail').val();
    if (email.length <= 0) {

        var dic = '<div><h4><b>' + doctype + ' is not valid</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
        Emailverified = 0;
    }

    else {
        var btn = $("#Emailvarify");
        btn.removeClass('has-spinner1').removeClass('activeSpin');
        btn.fadeOut(700)
        setTimeout(function () {
            btn.css("background-color", "#28a745").css("font-size", "17px").css("padding", "0px 37px");
            btn.attr("disabled", true);
            btn.html("<i class='fa fa-check-circle'></i>")
        }, 700)
        btn.fadeIn(700)
        btn.html("Verify")

        Emailverified = 1;
    }
}

function mobileVerify() {

    var mob = $("#verifyMobile").val();
    var maskedMobile = mob.replace(/\d(?=\d{4})/g, "*");
    if (mob.length <= 0) {
        var dic = '<div><h4>Mobile Number Can`t be blank</h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
    }
    else {
        //var mobileVerification = {
        //    "ApiKey": MobileOTPAPIKey,
        //    "senderID": MobileOTPSenderId,
        //    "Chennel": MobileOTPChennel,
        //    "mobile": mob,
        //    "messageContent": 'Welcome to www.zapurse.com.Client Verification Use this OTP ##var## to register your Mobile Number.'
        //}

        var mobileVerification = {
            "mobile": mob,
            "messageContent": 'Welcome to www.zapurse.com.Client Verification Use this OTP ##var## to register your Mobile Number.'
        }

        var h = '<span class="spinner1" style="font-size: 20px;padding: 0px 5px;"><i class="fa fa-spinner fa-pulse"></i></span>'

        $('#mobilevarify').addClass('has-spinner1').addClass('activeSpin');

        $('#mobilevarify').html('');
        $('#mobilevarify').html(h);

        var d = JSON.stringify(mobileVerification);
        console.table(d);
        $("#verifyMobileOTP").css('display', 'block');
        $("#verifyAdhar").css('display', 'none');
        // $("#buttonotpMobile").click();
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/MobileVerify",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(mobileVerification),
            success: function (data) {
                console.log('ajax');
                console.log(data);
                if (data.Message == "Done") {
                    $('#mobilevarify').removeClass('has-spinner1').removeClass('activeSpin');

                    $('#mobilevarify').html('')
                    $('#mobilevarify').html('Verify')

                    $('#sendedOTP').val(data.Data.OTP);
                    var Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 3000,
                        timerProgressBar: true,
                        onOpen: function (toast) {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })
                    Toast.fire({
                        icon: 'success',
                        title: 'OTP Sent to ' + maskedMobile
                    })
                    $("#buttonotpMobile").click();
                }
                else {

                    var dic = '<div><h4>' + data.Message + '</h4></div>';
                    sweetAlert(dic, 'center', 'error', 0);
                    $('#mobilevarify').removeClass('has-spinner1').removeClass('active');

                    $('#mobilevarify').html('Verify')
                }

            },
            error: function (d) {
                console.log(d);
                //alert("404. Please wait until the File is Loaded.");

            }
        });
    }
}

function VerifyAdharWithOTP() {

    var OTP = $("#verifyAdharOTP").val()

    if (OTP.length <= 0) {
        var dic = '<div><h4><b>please enter valid OTP</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
    }
    else {
        const ipAPI = 'https://kyc-api.aadhaarkyc.io/api/v1/aadhaar-v2/submit-otp'

        var h = '<span class="spinner1"><i class="fa fa-spinner fa-pulse" style="font-size: 20px;"></i></span>'

        $('#verifyAdhar').addClass('has-spinner1').addClass('activeSpin');

        $('#verifyAdhar').html('');
        $('#verifyAdhar').html(h);

        var Model = {
            "client_id": $("#adharClientID").val(),
            "otp": OTP,
            "url": ipAPI
        }
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/VerifyAdharWithOTP",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(Model),
            success: function (data) {

                console.log('aadhar Data');
                var aadharData = JSON.stringify(data);
                console.log(aadharData);
                if (data.Message == "success" && data.StatusCode == 200) {

                    var btn = $("#aadharvarify");
                    btn.fadeOut(700)
                    setTimeout(function () {
                        btn.css("background-color", "#28a745").css("font-size", "17px").css("padding", "0px 37px");
                        btn.attr("disabled", true);
                        btn.html("<i class='fa fa-check-circle'></i>")
                    }, 700)
                    btn.fadeIn(700)
                    btn.html("Verify")

                    localStorage.setItem("AadharData", aadharData)

                    adharVerified = 1;
                    $('#verifyAdharOTP').val('');
                    $("#modalClose").click();

                    $('#verifyAdhar').removeClass('has-spinner1').removeClass('activeSpin');
                    $('#verifyAdhar').html('Verify Aadhar');

                }

                else if (data.Message == "InternalServerError" && data.StatusCode == 500) {
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Server Is Down Kindly Do Manual Verification Process Or Try After 10 Minutes',
                        allowOutsideClick: false,
                        showCloseButton: true,
                        showConfirmButton: false,
                    });
                    adharVerified = 0;
                    $('#verifyAdharOTP').val('');
                    $('#modalClose').click();

                    $('#verifyAdhar').removeClass('has-spinner1').removeClass('activeSpin');
                    $('#verifyAdhar').html('Verify Aadhar');

                }


                else {

                    var btn = $("#aadharvarify");
                    btn.fadeOut(700)
                    setTimeout(function () {
                        btn.css("background-color", "Red").css("font-size", "17px").css("padding", "0px 37px");
                        //btn.attr("disabled", true);
                        btn.html("<i class='fa fa-times-circle'></i>")
                    }, 700)
                    btn.fadeIn(700)
                    btn.html("Verify")
                    adharVerified = 0;
                    $('#verifyAdharOTP').val('');

                    $('#verifyAdhar').removeClass('has-spinner1').removeClass('activeSpin');
                    $('#verifyAdhar').html('Verify Aadhar');

                }
                //console.log('localstorage Set');
                //console.log(data);
                //console.log('data from localstorage');
                //console.log(localStorage.setItem("AadharData"))
            },
            error: function (d) {
                var dic = '<div><h4><b>Service Not Available Please Try after some time</b></h4></div>';
                sweetAlert(dic, 'center', 'error', 0);
            }
        });
    }
}

function VerifyMobileWithOTP() {

    var retriveOTP = $('#sendedOTP').val();
    var enteredOTP = $('#verifyAdharOTP').val();
    console.log('otp');
    console.log('retrive OTP ', retriveOTP);
    if (enteredOTP.length <= 0) {
        var dic = '<div><h4><b>please enter valid OTP</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
    }
    else {
        if (enteredOTP === retriveOTP) {

            $("#modalClose").click();

            mobileverified = 1;
            var btn = $("#mobilevarify");
            btn.fadeOut(700)
            setTimeout(function () {
                btn.css("background-color", "#28a745").css("font-size", "17px").css("padding", "0px 37px");
                btn.attr("disabled", true);
                btn.html("<i class='fa fa-check-circle'></i>")
            }, 700)
            btn.fadeIn(700)
            btn.html("Verify")
            $('#verifyAdharOTP').val('');
        }
        else {
            mobileverified = 0;
            var btn = $("#mobilevarify");
            btn.fadeOut(700)
            setTimeout(function () {
                btn.css("background-color", "Red").css("font-size", "17px").css("padding", "0px 37px");
                //btn.attr("disabled", true);
                btn.html("<i class='fa fa-times-circle'></i>")
            }, 700)
            btn.fadeIn(700)
            btn.html("Verify")
            $('#verifyAdharOTP').val('');
        }
    }
}

function revealPassword(sender, itemId) {

    var formId = sender.id.replace('eye', '');
    var type = $('#' + itemId).attr('type');
    if (type == 'password') {


        console.log('inside password type')
        $('#' + itemId).attr('type', 'text');

        $('#withslash-' + formId).css('display', 'none');
        $('#withoutslash-' + formId).css('display', 'block');

    }
    if (type == 'text') {
        console.log('inside text type')
        $('#' + itemId).attr('type', 'password');
        $('#withslash-' + formId).css('display', 'block');
        $('#withoutslash-' + formId).css('display', 'none');
    }

    //<i class="fa fa-eye-slash EyeIcon" id="toggelLoginPass" onclick="revealPassword(this,'Loginpassword')"></i>
    //                                        <i class="fa fa-eye EyeIcon" style="display:none" id="toggelLoginPassReveal" onclick="revealPassword(this,'Loginpassword')"></i>



}

function encodeImageFileAsURL(element) {
    var elementID = element.id;
    var file = element.files[0];
    console.log(file);
    console.log(elementID);
    var reader = new FileReader();
    reader.onloadend = function () {
        imagebase64 = reader.result;
        if (elementID == 'PAN') {
            /*console.log(imagebase64);*/
            $('#UploadedPanImage').val(imagebase64);
            //var c = $('#UploadedImage').val();
            // console.log("Converted "+ c)
        }
        if (elementID == 'Aadhar') {
            /*console.log(imagebase64);*/
            $('#UploadedAadharImage').val(imagebase64);

        }
    }
    reader.readAsDataURL(file);
    return imagebase64;
}

function Thankyou() {

    //var Reg = $('#RegNo').val();
    //var PartialorderId = $('#PartialorderId').val();

    //var reg = Reg;//'Reg' + userID + year;
    //var inv = PartialorderId;

    //$('#registraionNumber').html(reg);
    //$('#OrderId').html(inv);
}

function SaveBookingData() {

    if ($("#chkTnc").is(':checked')) {
        //alert($('#UserID').val());
        var uID = $('#UserID').val();
        var PartialorderId = $('#PartialorderId').val();
        var TncID = parseInt($('#hdnTncId').val());
        var purchaseList = serviceList.concat(hardwareList);

        var savePurchaseModel = {
            "PartyId": uID,
            "OrderId": PartialorderId,
            "appTandCId": TncID,
            "PurchaseLists": purchaseList
        }
        var d = JSON.stringify(savePurchaseModel);
        console.table('Purchase Data');
        console.table(d);

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/SavePurchase",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(savePurchaseModel),
            success: function (data) {
                console.log('ajax');
                console.log(data);

                if (data.StatusCode == 1) {

                    $('#PartialorderId').val(data.OrderId);

                    $('#registraionNumber').html($('#RegNo').val());
                    $('#OrderId').html(data.OrderId);

                    saveAadhaarData(uID);

                    $('#sayThanks').click();
                }

            },
            error: function (d) {
                console.log(d);
                //alert("404. Please wait until the File is Loaded.");
            }
        });

    }
    else {

        var dic = '<div><h4><b>Please Check the Terms and Condition First</b></h4></div>';
        sweetAlert(dic, 'center', 'error', 0);
    }
}

function ddMMyyyy(dateIn) {
    var yyyy = dateIn.getFullYear();
    var mm = dateIn.getMonth() + 1; // getMonth() is zero-based
    var dd = dateIn.getDate();
    if (mm < 10) {
        mm = '0' + mm;
    }
    if (dd < 10) {
        dd = '0' + dd;
    }
    return String(dd) + String(mm) + String(yyyy);

}

function ExtendServiceDiv() {
    //var validate_inputs = document.querySelectorAll('[id^=service]:not(.active1)');
    //validate_inputs.forEach(function (input_valid) {

    //    $("#" + input_valid.id).on('mouseover', () => {
    //        $("#" + input_valid.id).addClass('expendDiv');

    //        $("#" + input_valid.id + " #price").addClass('price show')
    //        $("#" + input_valid.id + " #price1").addClass('price-text show')
    //    });
    //    $("#" + input_valid.id).on('mouseout', () => {
    //        $("#" + input_valid.id).removeClass('expendDiv');
    //        $("#" + input_valid.id + " #price").removeClass('price show')
    //        $("#" + input_valid.id + " #price1").removeClass('price-text show')
    //    });

    //});
}

function CheckTermsAndCondition() {
    var status = $("#chkTnc").is(':checked');
    if (!status) {
        $('#chkTnc').click();
    }

}

function DeleteItemFromList(id) {
    serviceList = serviceList.filter(function (e) {
        return e.CategoryID !== id
    })
    var checkboxID = '#check' + id;
    $(checkboxID).click();
    BindCheckoutData();
}

function DeleteItemFromHardwareList(id) {
    hardwareList = hardwareList.filter(function (e) {
        return e.CategoryID !== id
    })

    BindCheckoutData();
}

function Movebackword() {
    $("#step6").removeClass('active');
}

function saveAadhaarData(partyId) {


    var aadharData = JSON.parse(localStorage.getItem("AadharData"));


    var addAadharModal = {
        PartyId: partyId,
        Aadhaar: aadharData
    }

    //var d = JSON.stringify(addAadharModal)

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: globaUserProperties.domain +"/LoginSignup/SaveAadhaarData",
        dataType: 'json',
        type: 'Post',
        cache: false,
        data: JSON.stringify(addAadharModal),
        success: function (data) {
            console.log('ajax from aadhaar');
            //console.log(data);

            localStorage.removeItem("AadharData");
        },
        error: function (d) {
            console.log(d);
        }
    });

}

function verifyEmailAtNext() {

    var valu = $('#email').val();

    var returnFromfun = isValidEmailAddress(valu);

    if (returnFromfun == false) {
        var dic = '<div><h4><b>Email Address Not Valid</b></h4></div>';
        sweetAlert(dic, 'center', 'warning', 0);
        return false;
    }
    else {
        return true;
    }

}

function clearAll() {
    localStorage.removeItem("AadharData");
    $('#user_name').val('');
    $('#Loginpassword').val('');
    $('#Resetuser_name').val('');
    $('#txtResetpassword').val('');
    $('#txtResetre-password').val('');
    $('#name').val('');
    $('#mobile').val('');
    $('#email').val('');
    $('#Creusername').val('');
    $('#password').val('');
    $('#re-password').val('');
    $('#pan').val('');
    $('#PAN').val('');
    $('#aadhar').val('');
    $('#Aadhar').val('');
    $('#verifyMobile').val('');
    $('#Emailvarify').val('');
    $('#gst').val('');
    $('#verifyAdharOTP').val('');
    $('#sendedOTP').val('');
    $('#adharClientID').val('');
    $('#UserID').val('');
    $('#RegNo').val('');
    $('#PartialorderId').val('');
    serviceList = [];
    hardwareList = []
}

function gotoLogin() {
    var url = globaUserProperties.domain +"/authentication/login-alt";
    window.location.href = url;
}

function findDataInArray(dataList, itemTobeFound) {
    var status = false;
    if (dataList != null) {
        console.log(dataList);
        dataList.find(element => {
            //console.log(element)
            //console.log(typeof (element.CategoryID))
            //console.log(typeof (parseInt(itemTobeFound)))
            if (element.CategoryID == parseInt(itemTobeFound)) {
                status = true;
            }
            else {
                status = false;
            }
        });
        console.log(status);
        return status;
    }
}

function validatePanFormat(panNumber) {
    panNumber = panNumber.toUpperCase();
    if (panNumber.length > 0) {
        var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
        if (!regex.test(panNumber)) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Invalid Pan Number, Please Try Again',
                allowOutsideClick: false,
                showCloseButton: true,
                showConfirmButton: false,
            });
            return regex.test(panNumber);
        }
        else {
            return regex.test(panNumber);
        }
        //console.log('status', regex.test(panNumber));
    }
}

function validateGSTFormat(GSTNumber) {
    GSTNumber = GSTNumber.toUpperCase();
    if (GSTNumber.length > 0) {
        var regex = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
        if (!regex.test(GSTNumber)) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Invalid GST Number, Please Try Again',
                allowOutsideClick: false,
                showCloseButton: true,
                showConfirmButton: false,
            });
            return regex.test(GSTNumber);
        }
        else {
            return regex.test(GSTNumber);
        }
    }
}
function ClearOTP() {
    //alert('clicked')
    $('#verifyAdharOTP').val('');
}