function updateemployee() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;
    if (username == "" || password == "" || password2 == "") {
        alert("Username/Password must be fill out")
        return false;
    }
    if (username.length < 8 || password.length < 8 || password2.length < 8) {
        alert("Username/Password must be at least 8 digits long")
        return false;
    }
    if (password != password2) {
        alert("Password Must Match")
        return false;
    }
    return true;
}
function updatepassword() {
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;
    if ( password == "" || password2 == "") {
        alert("Password must be fill out")
        return false;
    }
    if (password.length < 8 || password2.length < 8) {
        alert("Password must be at least 8 digits long")
        return false;
    }
    if (password != password2) {
        alert("Password Must Match")
        return false;
    }
    return true;
}
