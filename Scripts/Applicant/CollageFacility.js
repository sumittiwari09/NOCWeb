
$(function () {

    $('#chkAll').click(function () {
        $('.chk').prop('checked', this.checked)
    });

    $('#btnupload').click(function () {
        var row_data = $('#tblFacility tbody tr').length;
        $("#tblFacility tbody TR").each(function () {
            var row_data = $(this);
            var Idget = row_data.find("td").eq(0).html().trim();
            if ($('#up_' + Idget)[0].files.length > 0) {
                var Idupload = $('#up_' + Idget)[0].files;
                var reader = new FileReader();
                reader.onload = function (e) {
                    binaryString = this.result;
                    $('#Upbase64_' + Idget).val(binaryString);
                }
                reader.readAsDataURL(Idupload[0]);
            }
        });
        var Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 4000,
            timerProgressBar: true,
            onOpen: function (toast) {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        })
        Toast.fire({
            icon: 'success',
            title: 'Uploded Successfully!!'
        })
        $('#btnupload').attr("style", "display:none");
        $('#btnSave').attr("style", "display:block");
        $('#btnSave').attr("style", "float:right");
    });
    $("#btnSave").click(function () {
        var tableData = new Array();
        debugger;
        //var trustId = $('#TrustId').val();
        //var CollageId = $('#CollageId').val();
        //var Guid = $('#Guid').val();
        $("#tblFacility tbody TR").each(function () {
            var row_data = $(this);
            var Idget = row_data.find("td").eq(0).html().trim();
            var select = false;
            if ($('#select_' + Idget).is(":checked")) {
                select = true;
            }
            debugger;
            var getdata = { FacilityId: Idget, IsSelect: select, uploadFile: $('#Upbase64_' + Idget).val() }
            tableData.push(getdata);
        });

        var modal = {
            //Guid: Guid,
            list: tableData
        }

        $.ajax({
            type: "POST",
            url: GetglobalDomain() + "/Trustee/CollageFacilitysAdd",
            data: modal,
            success: function (data) {
                if (data.failure) {

                    $('#btnSave').attr("style", "display:none");
                    $('#btnupload').attr("style", "display:block");
                    //Swal.fire(
                    //    'Success',
                    //    'Success',
                    //    'Data Save Successfully!!'
                    //)
                    var url = GetglobalDomain() + "/Trustee/CollageFacilitys";
                    window.location.href = url;
                }
                else {
                    Swal.fire(
                        'error',
                        'Failed',
                        'Data not Saved'
                    )
                    $('#btnSave').attr("style", "display:none");
                    $('#btnupload').attr("style", "display:block");
                }
            }
        });


    });

    $(document).ready(function () {



    });
});
    
