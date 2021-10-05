var map;
var markers;
var geojsonLayer;

function initialize(Lat, Lng)
{
    var mymap = L.map('mapid').setView([Lat, Lng], 15);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
        maxZoom: 18,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        tileSize: 512,
        zoomOffset: -1,
        id: "mapbox/outdoors-v11",
        accessToken: 'pk.eyJ1Ijoicnl0aXNvayIsImEiOiJja3R3b20wbGEybTl3MzBtcGdhZG96MnhqIn0.GIdSHPlZoTkmFHavTZZOqQ'
    }).addTo(mymap);
    /*L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        tileSize: 512,
        zoomOffset: -1,
    }).addTo(mymap);*/
    L.control.scale({ imperial: true, metric: true }).addTo(mymap);
    map = mymap;

    map.on('zoomend', function ()
    {
        var currentZoom = mymap.getZoom();
        var icon = new L.Icon({
            iconUrl: 'footprint.png',
            iconSize: [currentZoom * 4, currentZoom * 4],
            iconAnchor: [0, 0],
            popupAnchor: [15, (-50 / currentZoom)]
        });
        markers.setIcon(icon);
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

    var markerIcon = new icon({ iconUrl: 'footprint.png' });
    var marker = L.marker([Lat, Lng], { icon: markerIcon });
    //assign trail information text
    marker.bindPopup(text);
    //show geojson on marker click
    marker.on('click',
    function (e) {
        markerOnClick(geoJsonUrl);
    });
    //hide geojson on pop up close
    marker.getPopup().on('remove', function () {
        geojsonLayer.clearLayers();
    });
    marker.addTo(map);;

    markers = marker;

}

function markerOnClick(geoJsonUrl)
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