function setDefaultdateofbirth() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0 so need to add 1 to make it 1!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById("DOB").setAttribute("max", today);
    yyyy = yyyy - 60;
    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById("DOB").setAttribute("min", today);
   
    yyyy = yyyy + 5;
    today = yyyy + '-' + mm + '-' + dd;
    $("#DOB").val(today);
   // document.getElementById("DOB").value(today);
}
function GenerateDocumentList(CategoryId, Status) {
    
    $.ajax({
        url: globaUserProperties.domain + '/Home/AddDocumentList?Id=' + CategoryId + '&status=' + Status,
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbodyDocument").html(response);
        }
    });
    return false;
}
function BindSubCategory(CategoryId) {

    var strHTML = "";
    strHTML += "<option value=''>Select</option>"
    var Params = JSON.stringify(
        {
            Id: CategoryId
        });

    $("#SubCategoryId").select2("destroy");
    $("#SubCategoryId").html(strHTML);
    $("#SubCategoryId").select2();

    $.ajax({
        url: globaUserProperties.domain + "/Home/FillSubCategory",
        type: 'POST',
        dataType: "json",
        contentType: "application/json",
        data: Params,
        success: function (response) {

            console.log(response);

            if (response.IsValid == 1) {
                for (var i = 0; i < response.Data.length; i++) {
                    strHTML += "<option data-id='" + response.Data[i].EndMenu + "' value='" + response.Data[i].MstCategoryID + "'>" + response.Data[i].Category + "</option>"
                }
            }

            $("#SubCategoryId").select2("destroy");
            $("#SubCategoryId").html(strHTML);
            $("#SubCategoryId").select2();
            $("#ChildCategoryId").select2("destroy");
            $("#ChildCategoryId").html("<option value=''>Select</option>");
            $("#ChildCategoryId").select2();
        }
    });



}

function BindSubChildCategory(CategoryId, EndPoint) {
    
    if (!EndPoint) {
        $("#divChildCategory").addClass('hidden');
        $("#divServices").addClass('hidden');
        return false;
    }
    $("#divChildCategory").removeClass('hidden');
    var strHTML = "";
    strHTML += "<option value=''>Select</option>"
    var Params = JSON.stringify(
        {
            Id: CategoryId
        });

    $("#ChildCategoryId").select2("destroy");
    $("#ChildCategoryId").html(strHTML);
    $("#ChildCategoryId").select2();
    //$("#divServices").select2("destroy");
    //$("#divServices").html(strHTML);
    //$("#divServices").select2();

    $.ajax({
        url: globaUserProperties.domain + "/Home/FillSubChildCategory",
        type: 'POST',
        dataType: "json",
        contentType: "application/json",
        data: Params,
        success: function (response) {

            console.log(response);

            if (response.IsValid == 1) {
                for (var i = 0; i < response.Data.length; i++) {
                    strHTML += "<option data-id='" + response.Data[i].EndPoint + "' value='" + response.Data[i].Childcate + "'>" + response.Data[i].Name + "</option>"
                }
            }

            $("#ChildCategoryId").select2("destroy");
            $("#ChildCategoryId").html(strHTML);
            $("#ChildCategoryId").select2();
            $("#divServices").select2("destroy");
            $("#divServices").html(strHTML);
            $("#divServices").select2();
        }
    });

    return false;

}
function BindSubService(CategoryId, EndPoint) {
    if (!EndPoint) {
        $("#divServices").addClass('hidden');
        return false;
    }
    $("#divServices").removeClass('hidden');
    var strHTML = "";
    strHTML += "<option value=''>Select</option>"
    var Params = JSON.stringify(
        {
            Id: CategoryId
        });


    $("#ServiceId").select2("destroy");
    $("#ServiceId").html(strHTML);
    $("#ServiceId").select2();
    $.ajax({
        url: globaUserProperties.domain + "/Home/FillSubServiceCategory",
        type: 'POST',
        dataType: "json",
        contentType: "application/json",
        data: Params,
        success: function (response) {

            console.log(response);

            if (response.IsValid == 1) {
                for (var i = 0; i < response.Data.length; i++) {
                    strHTML += "<option  value='" + response.Data[i].SubChildId + "'>" + response.Data[i].Name + "</option>"
                }
            }

            $("#ServiceId").select2("destroy");
            $("#ServiceId").html(strHTML);
            $("#ServiceId").select2();

        }
    });

    return false;

}

function BindRatelist(categoryid, SubCategoryId, ChildCategoryId, services) {
    
    $.ajax({
        url: globaUserProperties.domain + '/Home/AddRateList?categoryid=' + categoryid + '&SubCategoryId=' + SubCategoryId + '&ChildCategoryId=' + ChildCategoryId + '&services=' + services,
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbodyRate").html(response);
        }
    });
    return false;
}

$(".ajaxmodal").unbind("click");
$(".ajaxmodal").each(function () {

    $(this).unbind("click");
    $(this).click(function () {

        var targetmodal = $(this).data("target");
        var ajaxurl = $(this).attr("href");

        $(targetmodal).html($($(this).data("preloaderid")).html());

        $(targetmodal).load(ajaxurl).modal({
            show: true,
            backdrop: 'static',
            keyboard: false,
        });

        return false;
    });

});