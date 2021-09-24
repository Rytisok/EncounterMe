var longitude = 0;
var latitude = 0;


//Geolocates the user using the browser
function Geolocate() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;
        })
    }
    else {
                alert("Geolocation API is not supported in your browser.");
            }
            
}

//Gets Latitude
function getLat() {

    return latitude;
}

//Gets Longitude
function getLong() {

    return longitude;
}