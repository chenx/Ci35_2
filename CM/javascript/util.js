//
// This javascript file contains general utility functions.
//

// From: http://stackoverflow.com/questions/2855865/jquery-regex-validation-of-e-mail-address
// Ref:  http://stackoverflow.com/questions/46155/validate-email-address-in-javascript
function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    //alert(pattern.test(emailAddress));
    return pattern.test(emailAddress);
}

// This also works and is shorter.
function isValidEmail(v) {
    return /^([a-zA-Z0-9]+[._-])*[a-zA-Z0-9]+@[a-zA-Z0-9-_\.]+\.[a-zA-Z]+$/.test(v);
}

// Allow only letters to entered.
function letterOnly(evt) {
    evt = (evt) ? evt : event;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));

    // note: charCode <= 31 is for control chars, such as backspace, delete.
    if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
        //alert('letter only');
        return false;
    }
    return true;
}

function numberOnly(evt) {
    evt = (evt) ? evt : event;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        //alert("Enter numerals only in this field.");
        return false;
    }
    return true;
}

