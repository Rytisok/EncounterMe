var map;
var geojsonLayer;
var dotNetObj = null;
var positionMarker = null;

function initializeTrailWalking(Lat, Lng, dotNetObjRef)
{
    map = L.map('map').setView([Lat, Lng], 15);
    dotNetObj = dotNetObjRef;

    //mapbox map
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
        maxZoom: 18,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        tileSize: 512,
        zoomOffset: -1,
        detectRetina: true,
        id: "mapbox/outdoors-v11",
        accessToken: 'pk.eyJ1Ijoicnl0aXNvayIsImEiOiJja3R3b20wbGEybTl3MzBtcGdhZG96MnhqIn0.GIdSHPlZoTkmFHavTZZOqQ'
    }).addTo(map);

    L.control.scale({ imperial: true, metric: true }).addTo(map);
}

function UpdatePositionMarker(Lat, Lng) {
    //if position marker is null, create one
    if (positionMarker == null) {
        CreateUserLocationMarker(Lat, Lng);
    }
    else {
        //just update position markers location
        positionMarker.marker.setLatLng([Lat, Lng]);
    }

}

function CreateUserLocationMarker(Lat, Lng) {
    var leafletMarker;

    var icon = L.Icon.extend({
        options: {
            iconSize: [28, 28],
            iconAnchor: [14, 14]
        }
    });

    var markerIcon = new icon({ iconUrl: 'Images/rec.png' });

    leafletMarker = L.marker([Lat, Lng], { icon: markerIcon });
    leafletMarker.addTo(map);
}

//loads and displays geojson with the specified name
function showWalkGeojson(geoJsonData) {

    alert(geoJsonData);

    var trailStyle = {
        "color": "#25C0C0",
        "weight": 7
    };

    removeGeojson();

    geojsonLayer = L.geoJSON("var geojson =" + geoJsonData, {
        style: trailStyle
    }).addTo(map);
}

function removeGeojson() {
    if (geojsonLayer != null) {
        map.removeLayer(geojsonLayer);
    }
}