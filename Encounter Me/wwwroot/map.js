function initialize() {
    //var latlng = new google.maps.LatLng(54.69, 25.28);
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
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
            const image = "footprint.png";

            const marker = new google.maps.Marker({
                position: latlng,
                map,
                icon: {
                    size: new google.maps.Size(64, 64),
                    scaledSize: new google.maps.Size(64, 64),
                    url: image
                },
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
        });

    } else {
        alert("Geolocation API is not supported in your browser.");
    }
}
}
