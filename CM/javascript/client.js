
function add() {
    if (validate()) {
        document.forms[0].submit();
    }
}

function update() {
    if (validate()) {
        document.forms[0].submit();
    }
}


/* Used by client_add.aspx and client_edit.aspx. */
function validate() {
    var msg = '';
    var f, o = null;

    f = $('#txtClientType');
    if ($.trim( f.val() ) == '') {
        msg += 'Client Type cannot be empty. \n';
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtCaseId');
    if ($.trim( f.val() ) == '') {
        msg += 'Case Id cannot be empty. \n';
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    if (o != null) {
        alert(msg);
        o.focus();
        return false;
    }
    return true;
}


/* Used by Default.aspx, to delete a client. */
function disable_case(id, case_id) {
    var c = confirm("Are you sure to disable case " + case_id + "?\nA disabled case will disappear from the client list.");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "client_delete.ashx",
        data: { ACTION: 2, ID: id },
        dataType: "html",
        timeout: 10000,
        success: function(data) {
            alert("Case " + case_id + " has been disabled");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}

function enable_case(id, case_id) {
    var c = confirm("Are you sure to enable case " + case_id + "?\nAn enabled case will appear in the client list.");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "client_delete.ashx",
        data: { ACTION: 3, ID: id },
        dataType: "html",
        timeout: 10000,
        success: function(data) {
            alert("Case " + case_id + " has been enabled");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}

function delete_case_permanently(id, case_id) {
    var c = confirm("Are you sure to delete case " + case_id + " permanently (this is irreversible)?");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "client_delete.ashx",
        data: { ACTION: 1, ID: id },
        dataType: "html",
        timeout: 10000,
        success: function(data) {
            alert("Case " + case_id + " has been permanently deleted");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}

/* Used by client_add.aspx and client_edit.aspx. */
function onClientTypeChange(type_id) {
    //alert(type_id);
    $('#txtAttorney').closest('tr').show();
    $('#txtPhone').closest('tr').show();
    $('#txtAddress').closest('tr').show();
    $('#txtDOB').closest('tr').show();
    $('#txtSSN').closest('tr').show();
    $('#txtFaultParty').closest('tr').show();
    $('#txtSettleType').closest('tr').show();
    $('#txtSettleAmount').closest('tr').show();
    
    $('#txtCaseNotes').closest('tr').show();
    $('#txtDateForPersClient').closest('tr').show();

    if (type_id == 1) {
        $('#txtAttorney').closest('tr').hide();
        $('#txtPhone').closest('tr').hide();
        $('#txtAddress').closest('tr').hide();
        $('#txtDOB').closest('tr').hide();
        $('#txtSSN').closest('tr').hide();
        $('#txtFaultParty').closest('tr').hide();
        $('#txtSettleType').closest('tr').hide();
        $('#txtSettleAmount').closest('tr').hide();
    }
    else if (type_id == 2 || type_id == 3) {
        $('#txtCaseNotes').closest('tr').hide();
        $('#txtDateForPersClient').closest('tr').hide();
    }
}

function do_print(id) {
    //alert(id);
    $('#print_id').val(id);
    document.forms[0].submit();
}