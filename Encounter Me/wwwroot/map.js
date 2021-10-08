var latitude = 0;
var longitude = 0;


function initialize(latitude, longitude) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;
            latlng = new google.maps.LatLng(latitude, longitude);
            var coords = new google.maps.LatLng(latitude, longitude);
            var options = {
                zoom: 15, center: coords,
                mapTypeControl: true,
                navigationControlOptions: {
                    style: google.maps.NavigationControlStyle.SMALL
                },
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map"), options);

            const marker = new google.maps.Marker({
                position: latlng,
                map,
                title: "",
            });

            const contentString =
                '<div id="content">' +
                '<div id="siteNotice">' +
                "</div>" +
                '<h1 id="firstHeading" class="firstHeading">Trail name</h1>' +
                '<div id="bodyContent">' +
                "<p>Trail information</p>" +
                "</div>" +
                "</div>";

            const infowindow = new google.maps.InfoWindow({
                content: contentString,
            });

            marker.addListener("click", () => {
                infowindow.open({
                    anchor: marker,
                    map,
                    shouldFocus: false,
                });
            });

            //setInterval(UpdateMap, 10);
        });

        } else {
            alert("Geolocation API is not supported in your browser.");
        }
}

function UpdateMap() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };

                infoWindow.setPosition(pos);
                infoWindow.setContent("Location found.");
                infoWindow.open(map);
                map.setCenter(pos);
            },
            () => {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
    }
}