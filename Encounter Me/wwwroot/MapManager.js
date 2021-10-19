var map;
var markers = [];
var trailMarkers = [];
var geojsonLayer;
var iconSizeMultiplier = 0.08;
var currentMarker;
var filterWindow = null;

function initialize(Lat, Lng, dotNetObjRef)
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

    var markerIcon = new icon({ iconUrl: 'Images/rec.png' });
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
            size: 28,
            geojson: null
        };
        markers.push(marker);
    });

    //creates search for trails button and assigns UpdateMarkers method to its onClick event
    let trailSearchEasyButton = L.easyButton('<mapText>Search for trails</mapText>', function (btn, map) {
        dotNetObjRef.invokeMethodAsync("FindTrails");
    }, {
        position: 'topright'}).addTo(map);
    trailSearchEasyButton.button.style.width = '200px';

    let trailFilterEasyButton = L.easyButton('<img src="Images/filter.png" width="40" height="40">', function (btn, map) {
        openFilterWindow(dotNetObjRef);
    }, {
        position: 'topright'
    }).addTo(map);
    trailFilterEasyButton.button.style.width = '48px';
    trailFilterEasyButton.button.style.height = '40px';
}

function clearMarkers()
{
    trailMarkers.forEach(function (element) {
        map.removeLayer(element);
    });

    trailMarkers = [];
}

function addMarker(Lat, Lng, text, trailType, geoJsonUrl)
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

    var trailIconUrl;

    switch (trailType)
    {
        case 1:
            trailIconUrl = 'Images/forest.png';
            break;
        case 2:
            trailIconUrl = 'Images/temple.png';
            break;
        default:
            trailIconUrl = 'Images/footprint.png';
            break;
    }

    var markerIcon = new icon({ iconUrl: trailIconUrl});
    var leafletMarker = L.marker([Lat, Lng], { icon: markerIcon });

    //assign trail information text
    leafletMarker.bindPopup(text);

    var marker = {
        marker: leafletMarker,
        size: 50,
        geojson: geoJsonUrl
    };

    //show geojson on marker click
    leafletMarker.on('click',
        function (e)
        {
            showGeojson(geoJsonUrl);
            //in case of clicking same marker while pop up is open, pop up is closed and marker click event is called again
            //calling open popup again prevents from geojson trail staying without popup staying open
            openPopUp(leafletMarker);
            currentMarker = marker;
        });

    //hide geojson on pop up close
    leafletMarker.getPopup().on('remove', function () {
        removeGeojson();
    });

    leafletMarker.addTo(map);
    markers.push(marker);
    trailMarkers.push(leafletMarker);
}

function openPopUp(marker)
{
    if (!marker.getPopup.isOpen)
    {
        marker.openPopup();
    }
}

function showTrailOnly()
{
    map.closePopup();
    showGeojson(currentMarker.geojson);
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

function openFilterWindow(dotNetObjRef)
{
    if (filterWindow == null) {
        filterWindow = L.control();

        filterWindow.onAdd = function (map) {
            this._div = L.DomUtil.create('div', 'filter');
            this.update();
            L.DomEvent
                .disableClickPropagation(this._div)
                .disableScrollPropagation(this._div);
            return this._div;
        };

        // method that we will use to update the control based on feature properties passed
        filterWindow.update = function (d, l) {

            //3 and 10 are the default values
            var dif = (d ? d : 3);
            var len = (l ? l : 10);

            this._div.innerHTML = '<h4>Filter Trails</h4>' +
                '<filter>Difficulty:</filter><br/>' +
                '<input type="range" min="1" max="5" value="' + dif + '" class="slider" oninput="filterWindow.update(this.value, ' + len + ')" onchange="filterWindow.update(this.value, ' + len + ')" id="trailDiff">' + '<filter>' + dif + '★ </filter><br/>'+
                '<filter>Length:</filter><br/>' +
                '<input type="range" min="1" max="50" value="' + len + '" class="slider" oninput="filterWindow.update(' + dif + ', this.value)" onchange="filterWindow.update(' + dif + ', this.value)" id="trailLength">' + '<filter>' + len + 'km </filter><br/>';
            dotNetObjRef.invokeMethodAsync("FilterMarkers", parseInt(dif), parseInt(len));
        };

        filterWindow.addTo(map);
    }
    else
    {
        filterWindow.remove();
        filterWindow = null;
    }
    
}

function futureFeature()
{
    alert("future feature");
}