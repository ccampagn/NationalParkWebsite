function validatepassword() {

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    if (username == "" || password == "") {
        alert("All field must be fill out")
        return false;
    }
    return true;
}

function createaccount() {

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;
    var firstname= document.getElementById("firstname").value;
    var lastname = document.getElementById("lastname").value;
    var address1 = document.getElementById("address1").value;
    var city = document.getElementById("city").value;
    var state = document.getElementById("state").value;
    var zipcode = document.getElementById("zipcode").value;
    var country = document.getElementById("country").value;

    if (username == "" || password == "" || password2 == "" || firstname == "" || lastname =="" ||address1 == "" || city == ""|| state == "" || zipcode==""||country=="") {
        alert("All required field must be fill oust")
        return false;
    }
    if (password.length<8 || password2.length<8) {
        alert("All password must be at least 8 digits long")
        return false;
    }
    if (username.length < 4 || username.length > 255) {
        alert("Invalid Email Address")
        return false;
    }
    if (password!=password2) {
        alert("Password does not match.")
        return false;
    }
 
    return true;

}
