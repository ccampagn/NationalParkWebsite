function validateaddress() {
    var firstname = document.getElementById("firstname").value;
    var lastname = document.getElementById("lastname").value;
    var address1 = document.getElementById("address1").value;
    var city = document.getElementById("city").value;
    var country = document.getElementById("country").value;
    if (firstname == "" || lastname == "" || address1 == "" || city == "" || country == "") {
        alert("All Required Fields must be fill out")
        return false;
    }
    

    return true;
}