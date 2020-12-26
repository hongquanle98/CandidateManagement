$(document).ready(function () {
    toastr.options = {
        toastClass: 'toastr', //fix conflict css with bootstrap 4.2
        progressBar: true,
        timeOut: 2000
    };    

    if (!$.fn.DataTable.isDataTable('.table')) {
        //datatable with button, column visible
        $('.table:not(.table-in-table)').DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'copy',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'csv',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                'colvis'
            ]
        });
    }


    //Handler
    //show/hide ability point input on checkbox
    $('[id^="ability_"]').on("change", function () {
        var abilityId = $(this).attr('id').substr($(this).attr('id').indexOf('_') + 1);
        var isCheckedBox = $(this).is(':checked');
        if (isCheckedBox) {
            $('#abilityPoint_' + abilityId).prop('disabled', false);
            $('#abilityPoint_' + abilityId).prop('required', true);
        } else {
            $('#abilityPoint_' + abilityId).prop('disabled', true);
            $('#abilityPoint_' + abilityId).prop('required', false);
        }
    });
    //set label for upload file input
    $('.custom-file-input').on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).next('.custom-file-label').html(fileName);
    });
    //set Email time span
    $('input#interviewDate, input#interviewTime').on("change", function () {
        setInterviewEmailBodyInfo();
    });
    //set Email template
    $('select#interviewEmailTemplate').change(function () {
        setInterviewEmailBody();
    });

    //get ability on change apply position
    $('select#applyPosition').change(function () {
        var selectedOption = $('select#applyPosition option:selected');
        var applyPositionId = selectedOption.attr('data-id');
        $.ajax({
            url: '/Admin/GetAbilityByApplyPosition?applyPositionId=' + applyPositionId,
            success: function (data) {
                $('#apply-position-ability').html(data);
            }
        });
    });

    //remove modal on close prevent 2 modal at same time
    $('#addInterviewModal').on('hidden.bs.modal', function () {
        $('#addInterviewModal').remove();
    });
    $('#editInterviewModal').on('hidden.bs.modal', function () {
        $('#editInterviewModal').remove();
    });
});

//Method
function random(start, end) {
    return Math.floor(Math.random() * (end - start + 1) + start);
}
function clearAllInput(element) {
    element
        .find("input,textarea,select")
        .val('')
        .end()
        .find("input[type=checkbox], input[type=radio]")
        .prop("checked", "")
        .end();
}

//Function
function saveRequirement(el, candidateId, requirementId, isSaving, isRequirementDetail) {
    //candidateId = 0;
    var button = $(el);
    var buttonText = $(el).text();
    var icon = button.children();
    var iconClass = icon.attr('class');
    var loadingClass = 'fas fa-spinner fa-spin';
    var loadingIcon = '<i class="' + loadingClass + '"></i>';

    $.ajax({
        method: "POST",
        url: '/Home/SaveRequirementAsync?candidateId=' + candidateId + '&requirementId=' + requirementId + '&saving=' + isSaving + '&requirementDetail=' + isRequirementDetail,
        beforeSend: function () {
            icon.removeClass(iconClass).addClass(loadingClass);
            if (buttonText.trim() == 'Delete' || buttonText.trim() == 'Xóa')
                button.html(loadingIcon);
        },
        success: function (response) {
            if (response.success) {
                button.parent().html(response.dataSaveRequirementButtonPartial);
                $('#saved').html(response.dataSavedRequirementPartial);
                toastr.success(response.message, response.title);
            } else {
                toastr.error(response.message, response.title);
            }
        }, complete: function () {
            icon.removeClass(loadingClass).addClass(iconClass);
        }
    });
}

success = fail = function (response) {
    if (response.success) {
        toastr.success(response.message, response.title);
    } else {
        toastr.error(response.message, response.title);
    }
};

function showBanner(bannerWannaShow) {
    var showedBanner = new Array();
    show();
    setInterval(show, 5000);

    function show() {
        showedBanner.splice(0, showedBanner.length - 1);
        $('.group-banner').children().hide();
        for (var i = 0; i < bannerWannaShow; i++) {
            var random = getRandomNumber();
            $('.group-banner').children().eq(random).show();
        }
    }
    function getRandomNumber() {
        var number = random(0, $('.group-banner').children().length - 1);
        if ($.inArray(number, showedBanner) != -1) {
            return getRandomNumber();
        } else {
            showedBanner.push(number);
            return number;
        }
    }
}
function setInterviewEmailBodyInfo() {
    //set work info
    var companyName = $('input#companyName').val();
    var workPlace = $('input#workPlace').val();

    $('span.spanCompanyName').text(companyName);
    $('span.spanWorkPlace').text(workPlace);

    //set time span
    var date = $.datepicker.formatDate('dd-mm-yy', new Date($('input#interviewDate').val()));
    var dayOfWeek = new Date($('input#interviewDate').val()).getDay() + 1;
    var time = $('input#interviewTime').val();

    console.log(date);
    console.log(dayOfWeek);
    console.log(time);

    $('span#spanInterviewTime').text(time);
    $('span#spanInterviewDayOfWeek').text(dayOfWeek == 1 ? "ngày Chủ nhật" : "ngày thứ " + dayOfWeek);
    $('span#spanInterviewDate').text(date);
}
function setInterviewEmailBody() {
    var interviewEmailTemplatePath = $('select#interviewEmailTemplate').val().substring
        ($('select#interviewEmailTemplate').val().indexOf('email-template') - 1);

    $('#interviewMailBody').load(interviewEmailTemplatePath, function (responseTxt, statusTxt, xhr) {
        if (statusTxt == "success") {
            setInterviewEmailBodyInfo();
        }
        if (statusTxt == "error")
            alert("Error: " + xhr.status + ": " + xhr.statusText);
    });
}

//show confirmation on delete 
function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}

function confirmDialog() {

    var result = confirm("Do you want to delete this?");

    if (result) {
        var form = $(this).closest('form');
        form.submit();
        return false;
    } else {
        return false;
    }
}

//show modal
function ShowAddInterviewModal(id) {
    $.ajax({
        url: '/Admin/AddInterviewSchedule?id=' + id,
        success: function (data) {
            $('#addInterviewModalContainer').html(data);
            $('#addInterviewModal').modal('show');
            setInterviewEmailBody();
        }
    });
}
function ShowEditInterviewModal(applyDetailId, interviewId) {
    $.ajax({
        url: '/Admin/EditInterviewSchedule?applyDetailId=' + applyDetailId + '&interviewId=' + interviewId,
        success: function (data) {
            $('#editInterviewModalContainer').html(data);
            $('#editInterviewModal').modal('show');
            setInterviewEmailBody();
        }
    });
}
function ShowMarkAbilityModal(applyDetailId, interviewId) {
    $.ajax({
        url: '/Admin/MarkAbility?applyDetailId=' + applyDetailId + '&interviewId=' + interviewId,
        success: function (data) {
            $('#markAbilityModalContainer').html(data);
            $('#markAbilityModal').modal('show');
        }
    });
}
function ShowApplyDetailCVModal(candidateId, applyDetailId) {
    $.ajax({
        url: '/Admin/ShowApplyDetailCV?candidateId=' + candidateId + '&applyDetailId=' + applyDetailId,
        success: function (data) {
            $('#applyDetailCVModalContainer').html(data);
            $('#applyDetailCVModal' + applyDetailId).modal('show');
        }
    });
}
function ShowApplyRequirementDetail(el, requirementId, candidateId) {
    var button = $(el);
    var buttonText = button.text();
    var loadingIcon = "<i class='fas fa-spinner fa-spin'></i>";
    $.ajax({
        url: '/Home/ApplyRequirementDetail?requirementId=' + requirementId + '&candidateId=' + candidateId,
        beforeSend: function () {
            $('.loading').show();
            button.append(loadingIcon);
        },
        success: function (data) {
            $('#applyRequirementDetailModalContainer').html(data);
            $('#applyRequirementDetailModal').modal('show');
        },
        complete: function () {
            $('.loading').hide();
            button.text(buttonText);
        }
    });
}



