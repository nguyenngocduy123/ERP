function loadStaff() {
    ShowLoading();
    var code = $('#CodeStaff');
    var name = $('#NameStaff');
    var position = $('#Position');
    var branchId = $('#branchId');
    var departmentId = $('#DepartmentId');
    var StaffList = $('input[name="staffListcancel"]').val();
    $.get('/InternalNotifications/Search', { CodeStaff: code.val(), NameStaff: name.val(), Position: position.val(), branchId: branchId.val(), DepartmentId: departmentId.val(), StaffList: StaffList }, function (res) {
        var $html_response = $('<div>' + res + '</div>');
        $('.area-detail').html($html_response.find('.box').html());
        $tr_template = $html_response.find('.box #StaffList tr:first-child');
        HideLoading();

        $('.table-fixed-column-1').tableFixedColumn({
            leftTableWidth: '0%',
            rightTableWidth: '100%',
            leftTableColumnWidth: [0],
            rightTableColumnWidth: [20,80],
            columnHeight: 50,
            numberColumnFixedLeft: 0,
            contentHeight: '150px'
        });
    });
}

function CheckIsval() {
    var check = $('input[name="staff-checkbox"]:checked').map(function () {
        return $(this).val();
    }).get().join(',');
    var checkSave = $('input[name="staffListcancel"]');
    //console.log(checkSave);

    if (check != "") {
        if (checkSave.val() != "") {
            checkSave.val(checkSave.val() + ',' + check);
        }
        else {
            checkSave.val(check);
        }
    }
    if (checkSave.val() != "") {
        var formData = {
            check: checkSave.val()
        };
        $("#ListCheckStaff").html('');
        ClickEventHandler(true, "/InternalNotifications/AddStaff", "#ListCheckStaff", formData, loadTableFixedColumn);
        loadStaff();


    }
};

//$("#ListCheckStaff").on('click', '.CheckIsvalReinit', function () {
//});

//function CheckIsvalReinit() {
//    var checkSave = $('input[name="staffListcancel"]');
//    var formData = {
//        check: checkSave.val()
//    };
//    $("#ListCheckStaff").html('');
//    ClickEventHandler(true, "/InternalNotifications/AddStaff", "#ListCheckStaff", formData, loadTableFixedColumn);
//    loadStaff();
//};

function loadTableFixedColumn() {
    $('.table-fixed-column-2').tableFixedColumn({
        leftTableWidth: '0%',
        rightTableWidth: '100%',
        leftTableColumnWidth: [0],
        rightTableColumnWidth: [50, 250],
        columnHeight: 50,
        numberColumnFixedLeft: 0,
        contentHeight: '150px'
    });
};

//Fetch Department of University
var urDepartmentl = '/api/BackOfficeServiceAPI/FetchBranchDepartment';
var department = $("#DepartmentId"); // cache it

$("#branchId").change(function () {
    //console.log($(this).val());
    ShowLoading();
    department.empty(); // remove any existing options
    $(document.createElement('option'))
                .attr('value', '')
                .text('- Rỗng -')
                .appendTo(department).trigger('chosen:updated');
    var id = $(this).val(); // Use $(this) so you don't traverse the DOM again
    $.getJSON(urDepartmentl, { BranchId: id }, function (response) {
        department.empty(); // remove any existing options
        $(response).each(function () {
            $(document.createElement('option'))
                .attr('value', this.Id)
                .text(this.Staff_DepartmentId)
                .appendTo(department).trigger('chosen:updated');
            HideLoading();
        });
    });
});
function CheckAll() {
    if ($('input[name="checkAll"]').is(':checked')) {
        $('input[name="staff-checkbox"]').prop('checked', true);
    } else {
        $('input[name="staff-checkbox"]:checked').prop('checked', false);
    }
}
function ClearSearch() {
    $('#CodeStaff').val('');
    $('#NameStaff').val('');
    $('#Position').val('');
    $('#branchId').val('');
    $('#DepartmentId').val('');
}
$(document).ready(function () {
    CheckIsval();
    loadStaff();
});