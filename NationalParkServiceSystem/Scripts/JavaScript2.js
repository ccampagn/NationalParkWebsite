function changepassword() {

    var currentpassword = document.getElementById("currentpassword").value;
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;

    if (currentpassword == "" || password == "" || password2 == "") {
        alert("All field must be fill out")
        return false;
    }
    if (password.length < 8 || password2.length < 8) {
        alert("All password must be at least 8 digits long")
        return false;
    }
    if (password != password2) {
        alert("Password does not match.")
        return false;
    }
    if (currentpassword == password) {
        alert("Can't use current password")
        return false;
    }
    return true;

}
function forgetpassword() {

    var username = document.getElementById("username").value;
    if (username == "") {
        alert("Username must be fill out")
        return false;
    }
    return true;
}
function checkforgetpassword() {

    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;

    if ( password == "" || password2 == "") {
        alert("All field must be fill out")
        return false;
    }
    if (password.length < 8 || password2.length < 8) {
        alert("All password must be at least 8 digits long")
        return false;
    }
    if (password != password2) {
        alert("Password does not match.")
        return false;
    }
    return true;
}
function validateemployee() {

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    if (username == "") {
        alert("Username/Password must be fill out")
        return false;
    }
    if (username.length < 8 || password.length < 8) {
        alert("Username/Password must be at least 8 digits long")
        return false;
    }
    return true;
}
function createemployee() {

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;

    if (username == "" || password == "" || password2 == "") {
        alert("All field must be fill out")
        return false;
    }
    if (username.length < 8 || password.length < 8 || password2.length < 8) {
        alert("All field must be at least 8 digits long")
        return false;
    }
    if (username.length >8) {
        alert("Id can only be 8 digits long.")
        return false;
    }
    if (password != password2) {
        alert("Password does not match.")
        return false;
    }
    return true;

}
