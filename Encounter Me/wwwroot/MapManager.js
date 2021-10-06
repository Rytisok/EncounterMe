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
            iconAnchor: [currentZoom * 2, currentZoom * 2],
            popupAnchor: [0, (-200 / currentZoom)]
        }
    });
    //creates new marker with icon and specified coords
    var markerIcon = new icon({ iconUrl: 'footprint.png' });
    var marker = L.marker([Lat, Lng], { icon: markerIcon });

    //assign trail information text
    marker.bindPopup(text);

    //show geojson on marker click
    marker.on('click',
        function (e)
        {
            showGeojson(geoJsonUrl);
            //in case of clicking same marker while pop up is open, pop up is closed and marker click event is called again
            //calling open popup again prevents from geojson trail staying without popup staying open
            openPopUp(marker);
        });

    //hide geojson on pop up close
    marker.getPopup().on('remove', function () {
        removeGeojson();
    });

    marker.addTo(map);
    markers.push(marker);
}

function openPopUp(marker)
{
    if (!marker.getPopup.isOpen)
    {
        marker.openPopup();
    }
}

//loads and displays geojson with the specified name
function showGeojson(geoJsonUrl)
{
    fetch(geoJsonUrl)
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {

            removeGeojson();
            geojsonLayer = L.geoJSON(data);

            geojsonLayer.setStyle({
                color: '#25C0C0',
                weight: 7
            });

            geojsonLayer.addTo(map);
        });
}

function removeGeojson() {
    if (geojsonLayer != null) {
        map.removeLayer(geojsonLayer);
    }
}

//updates all marker sizes based on zoom level
function updateMarkerSize() {
    var currentZoom = map.getZoom();
    var icon = new L.Icon({
        iconUrl: 'footprint.png',
        iconSize: [currentZoom * 4, currentZoom * 4],
        iconAnchor: [currentZoom * 2, currentZoom * 2],
        popupAnchor: [0, (-200 / currentZoom)]
    });
    markers.forEach(element => element.setIcon(icon));
}