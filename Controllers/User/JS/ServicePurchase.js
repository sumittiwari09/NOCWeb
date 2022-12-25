/*variable declaration*/

//const { forEach } = require("../../plugins/fullcalendar/packages/core/locales-all");

var serviceList = [];
var hardwareList = [];
/*variable declaration*/
/*Restrict charecters not allows Spaces*/
/*Restrict charecters allows only integer*/

var globaUserProperties = new Object();
var base_url = window.location;
globaUserProperties.domain = base_url.origin;// + "/" + window.location.pathname.split('/')[1];

function ChangeForm(item, step) {

    var yetToMove = true;
        if (serviceList.length <= 0) {
            yetToMove = false;
        var dic = '<div><h4><b>Please select atleast one service</b></h4></div>';
        sweetAlert(dic, 'center', 'error', 0);
    }

    if (yetToMove == true) {
        //console.log('inside change');

        var stepdata = parseInt(item.substr(4, 1));
        $("#main1").css('display', 'none');
        $("#main2").css('display', 'none');
        $("#main3").css('display', 'none');

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
    //console.table(hardwareList);
}
function ValidateServiceCheck() {
    var status = true;

    console.log(serviceList.length);

 
    return status;
}

function GetHardewareList(item) {
    var serviceIds = "";
    var serviceNames = "";

    if (serviceList.length <= 0) {
        serviceList = [];
        $("#" + item + " span ").html('');
        $("#" + item + " .input-text").html('');
        //$("#ServiceOnHardware").html('Please Select Atleast One Service');
        //$("#btnSelectedService").css('display', 'none');
    }
    if (serviceList.length >= 1) {
        //$("#btnSelectedService").css('display', 'inline');
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
            url: globaUserProperties.domain +"/LoginSignup/GetAllHardwares?Type=Hardware",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: "",
            success: function (data) {
                console.log('ajax');
                console.log(data);
                if (data.StatusCode == '1') {
                    var hardwareList = data.Data;

                    if (hardwareList != null) {
                        $("#" + item + " span ").html(serviceNames);
                        var t = "<table class='table table-hover table-responsive' id='hardwareList'><tr><th style='display:none'>HardwareId</th><th>Hardware Name</th><th>Price</th><th>GST Amount</th><th>Select</th></tr>"
                        hardwareList.forEach(function (service) {
                            if (service.GST == '0') {
                                t = t + "<tr><td style='display: none'>" + service.CategoryId + "</td><td>" + service.CategoryName + "</td><td>" + service.TotalAmount + "</td><td>0</td><td><input class='form-check-input' type='checkbox'/></td></tr>";
                            }
                            else {
                                t = t + "<tr><td style='display: none'>" + service.CategoryId + "</td><td>" + service.CategoryName + "</td><td>" + service.TotalAmount + "</td><td>" + service.GstAmount + "</td><td><input class='form-check-input' type='checkbox'/></td></tr>";
                            }
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

function SaveBookingData() {

    if ($("#chkTnc").is(':checked')) {
        var purchaseList = serviceList.concat(hardwareList);
        var TncID = parseInt($('#hdnTncId').val());
        var savePurchaseModel = {
            "PartyId": "",
            "OrderId": "",
            "appTandCId": TncID,
            "PurchaseLists": purchaseList
        }
        var d = JSON.stringify(savePurchaseModel);
        //console.table('Purchase Data');
        //console.table(d);

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: globaUserProperties.domain +"/LoginSignup/SavePurchaseAfterLogin",
            dataType: 'json',
            type: 'Post',
            cache: false,
            data: JSON.stringify(savePurchaseModel),
            success: function (data) {
                console.log('ajax');
                console.log(data);

                if (data.StatusCode == 1) {
                    $('#PartialorderId').val(data.OrderId);
                    $('#registraionNumber').html(data.RegistrationNo);
                    $('#OrderId').html(data.OrderId);
                    $('#sayThanks').click();
                }
                if (data.StatusCode == "-1") {
                    var dic = '<div><h4><b>Data not saved...</b></h4></div>';
                    sweetAlert(dic, 'center', 'error', 0);
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
    $("#step3").removeClass('active');
}

function clearAll() {
    serviceList = [];
    hardwareList = []
}
function gotoWelcome() {
    var url = globaUserProperties.domain +"/Welcome/Index";
    window.location.href = url;
}