var map;
var markers = [];
var geojsonLayer;
var iconSizeMultiplier = 0.08;

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

    var icon = L.Icon.extend({
        options: {
            iconSize: [28, 28],
            iconAnchor: [14, 14]
        }
    });

    var markerIcon = new icon({ iconUrl: 'rec.png' });
    var leafletMarker;

    //places dot icon and centers map on user location when location is acquired
    map.locate({ setView: true, watch: false }).on('locationfound', function (ev) {
        if (!leafletMarker) {
            leafletMarker = L.marker(ev.latlng, { icon: markerIcon });
        } else {
            leafletMarker.setLatLng(ev.latlng);
        }
        leafletMarker.addTo(map);

        var marker = {
            marker: leafletMarker,
            size: 28
        };
        markers.push(marker);
    });

}

function addMarker(Lat, Lng, text, geoJsonUrl)
{
    var currentZoom = map.getZoom();
    var sizeMultiplier = currentZoom * iconSizeMultiplier;
    var icon = L.Icon.extend({
        options: {
            iconSize: [50 * sizeMultiplier, 50 * sizeMultiplier],
            iconAnchor: [25 * sizeMultiplier, 25 * sizeMultiplier],
            popupAnchor: [0, (sizeMultiplier * -4 / currentZoom)]
        }
    });
    //creates new marker with icon and specified coords
    var markerIcon = new icon({ iconUrl: 'footprint.png' });
    var leafletMarker = L.marker([Lat, Lng], { icon: markerIcon });

    //assign trail information text
    leafletMarker.bindPopup(text);

    //show geojson on marker click
    leafletMarker.on('click',
        function (e)
        {
            showGeojson(geoJsonUrl);
            //in case of clicking same marker while pop up is open, pop up is closed and marker click event is called again
            //calling open popup again prevents from geojson trail staying without popup staying open
            openPopUp(leafletMarker);
        });

    //hide geojson on pop up close
    leafletMarker.getPopup().on('remove', function () {
        removeGeojson();
    });

    var marker = {
        marker: leafletMarker,
        size: 50};

    leafletMarker.addTo(map);
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
    
    markers.forEach(function (element) {
        var icon = element.marker.options.icon;
        var sizeMultiplier = currentZoom * iconSizeMultiplier;

        icon.options.iconSize = [element.size * sizeMultiplier, element.size * sizeMultiplier];
        icon.options.iconAnchor = [element.size * sizeMultiplier / 2, element.size * sizeMultiplier / 2];
        icon.options.popupAnchor = [0, (element.size * -4 / currentZoom)];
        element.marker.setIcon(icon);
    });
}
