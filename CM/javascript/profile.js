
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

    f = $('#txtEmail');
    if ($.trim( f.val() ) == '') {
        msg += 'Email cannot be empty. \n';
        o = f;
        f.addClass('error');
    }
    else if (!isValidEmailAddress($.trim(f.val()))) {
        msg = 'Email is not valid. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtLastName');
    if ($.trim( f.val() ) == '') {
        msg += 'Last name cannot be empty. \n';
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }
    
    f = $('#txtFirstName');
    if ($.trim( f.val() ) == '') {
        msg += 'First name cannot be empty. \n';
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
function delete_case(id, case_id) {
    var c = confirm("Are you sure to delete case " + case_id + "?");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "client_delete.ashx",
        data: { ACTION: 1, ID: id },
        dataType: "json",
        timeout: 10000,
        success: function(data) {
            alert("Case " + case_id + " has been deleted");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}

