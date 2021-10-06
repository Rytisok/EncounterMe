var map;
var markers = [];
var geojsonLayer;

function initialize(Lat, Lng)
{
    map = L.map('mapid').setView([Lat, Lng], 15);

    //mapbox map
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
        maxZoom: 18,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        tileSize: 512,
        zoomOffset: -1,
        id: "mapbox/outdoors-v11",
        accessToken: 'pk.eyJ1Ijoicnl0aXNvayIsImEiOiJja3R3b20wbGEybTl3MzBtcGdhZG96MnhqIn0.GIdSHPlZoTkmFHavTZZOqQ'
    }).addTo(map);

    //default open street maps map - blurry
    /*L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        tileSize: 512,
        zoomOffset: -1,
    }).addTo(mymap);*/

    L.control.scale({ imperial: true, metric: true }).addTo(map);

    map.on('zoomend', function ()
    {
        updateMarkerSize();
    });
}

function addMarker(Lat, Lng, text, geoJsonUrl)
{
    var currentZoom = map.getZoom();
    var icon = L.Icon.extend({
        options: {
            iconSize: [currentZoom * 4, currentZoom * 4],
            iconAnchor: [0, 0],
            popupAnchor: [15, -50]
        }
    });
    //creates new marker with icon and specified coords
    var markerIcon = new icon({ iconUrl: 'footprint.png' });
    var marker = L.marker([Lat, Lng], { icon: markerIcon });

    //assign trail information text
    marker.bindPopup(text);

    //show geojson on marker click
    marker.on('click',
    function (e) {
        showGeojson(geoJsonUrl);
        });

    //hide geojson on pop up close
    marker.getPopup().on('remove', function () {
        geojsonLayer.clearLayers();
    });

    marker.addTo(map);
    markers.push(marker);
}

//loads and displays geojson with the specified name
function showGeojson(geoJsonUrl)
{
    fetch(geoJsonUrl)
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            geojsonLayer = L.geoJSON(data);
            geojsonLayer.addTo(map);
        });
}

//updates all marker sizes based on zoom level
function updateMarkerSize() {
    var currentZoom = map.getZoom();
    var icon = new L.Icon({
        iconUrl: 'footprint.png',
        iconSize: [currentZoom * 4, currentZoom * 4],
        iconAnchor: [0, 0],
        popupAnchor: [15, (-50 / currentZoom)]
    });
    markers.forEach(element => element.setIcon(icon));
}