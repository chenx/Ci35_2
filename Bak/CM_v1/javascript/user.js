
function add() {
    if (validate()) {
        document.forms[0].submit();
    }
}

function update() {
    if (validate_edit()) {
        document.forms[0].submit();
    }
}


/* Used by add.aspx */
function validate() {
    var msg = '';
    var f, o = null;

    f = $('#txtUserType');
    if ($.trim( f.val() ) == '') {
        msg = 'User Type cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    /* For passwords. */
    $('#txtPwd').removeClass('error');
    $('#txtPwd2').removeClass('error');

    if ($.trim( $('#txtPwd').val() ) != $.trim( $('#txtPwd2').val() )) {
        msg = 'Passwords must match. \n' + msg;
        $('#txtPwd').addClass('error');
        $('#txtPwd2').addClass('error');
    }
    
    f = $('#txtPwd2');
    if ($.trim( f.val() ).length < 6) {
        msg = 'Password (repeat) must be at least 6 characters in length. \n' + msg;
        o = f;
        f.addClass('error');
    }

    f = $('#txtPwd');
    if ($.trim( f.val() ).length < 6) {
        msg = 'Password must be at least 6 characters in length. \n' + msg;
        o = f;
        f.addClass('error');
    }


    f = $('#txtLogin');
    if ($.trim( f.val() ) == '') {
        msg = 'Login cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtEmail');
    if ($.trim( f.val() ) == '') {
        msg = 'Email cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtLastName');
    if ($.trim( f.val() ) == '') {
        msg = 'Last Name cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtFirstName');
    if ($.trim( f.val() ) == '') {
        msg = 'First Name cannot be empty. \n' + msg;
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


/* Used by edit.aspx */
function validate_edit() {
    var msg = '';
    var f, o = null;

    f = $('#txtUserType');
    if ($.trim( f.val() ) == '') {
        msg = 'User Type cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtEmail');
    if ($.trim( f.val() ) == '') {
        msg = 'Email cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtLastName');
    if ($.trim( f.val() ) == '') {
        msg = 'Last Name cannot be empty. \n' + msg;
        o = f;
        f.addClass('error');
    }
    else {
        f.removeClass('error');
    }

    f = $('#txtFirstName');
    if ($.trim( f.val() ) == '') {
        msg = 'First Name cannot be empty. \n' + msg;
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
function disable_user(id) {
    var c = confirm("Are you sure to disable user " + id + "?\nA disabled user cannot log in.");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "delete.ashx",
        data: { ACTION: 2, ID: id },
        dataType: "html",
        timeout: 10000,
        success: function(data) {
            alert("User " + id + " has been disabled");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}

function enable_user(id) {
    var c = confirm("Are you sure to enable user " + id + "?\nA user can log in after being enabled.");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "delete.ashx",
        data: { ACTION: 3, ID: id },
        dataType: "html",
        timeout: 10000,
        success: function(data) {
            alert("User " + id + " has been enabled");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}

function delete_user_permanently(id) {
    var c = confirm("Are you sure to delete user " + id + " permanently (this is irreversible)?");
    if (!c) return;

    $.ajax({
        type: "POST",
        url: "delete.ashx",
        data: { ACTION: 1, ID: id },
        dataType: "html",
        timeout: 10000,
        success: function(data) {
        alert("User " + id + " has been permanently deleted");
            setTimeout("location.reload(true);", 0);
        },
        error: function(request, status, err) {
            alert('Error: ' + err);
        }
    });
}
