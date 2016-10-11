function AddDistrict() {
    var parkid = document.getElementById("parkname").value;
    var array = "@Html.Raw(Json.Encode(@ViewBag.district))";
    for(var i = 0; i < array.length; i++) {
        jScriptArray[i] = array[i];
        alert(array[i]);
        alert(jScriptArray[i]);
    }
}



