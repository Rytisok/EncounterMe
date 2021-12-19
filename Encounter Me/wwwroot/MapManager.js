var map = null;
var markers = [];
var trailMarkers = [];
var geojsonLayer;
var iconSizeMultiplier = 0.08;
var currentMarker;
var filterWindow = null;
var positionMarker = null;
var dotNetObj = null;
var walkedTrailLine = null;
var lineToTrail = null;
var missionStats = null;

function initializeTrailMap(Lat, Lng, dotNetObjRef, drawPosition)
{
    if (map != null) {
        map.off();
        map.remove();
    }

    map = L.map('map').setView([Lat, Lng], 15);
    dotNetObj = dotNetObjRef;

    //mapbox map
    L.tileLayer('https://api.mapbox.com/styles/v1/rytisok/ckw68k17e2czt14o5uyi97e52/tiles/256/{z}/{x}/{y}?access_token=pk.eyJ1Ijoicnl0aXNvayIsImEiOiJja3R3b20wbGEybTl3MzBtcGdhZG96MnhqIn0.GIdSHPlZoTkmFHavTZZOqQ', {
        maxZoom: 18,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        detectRetina: true,
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

    if (drawPosition)
    {
        positionMarker = null;
        UpdatePositionMarker(Lat, Lng);
    }

    //creates search for trails button and assigns UpdateMarkers method to its onClick event
    let trailSearchEasyButton = L.easyButton('<mapText>Find capture points</mapText>', function (btn, map) {
        var bounds = map.getBounds();
        removeMarkers();
        dotNetObjRef.invokeMethodAsync("FindTrails", bounds.getSouthWest().lat, bounds.getSouthWest().lng, bounds.getNorthEast().lat, bounds.getNorthEast().lng);
    }, {
        position: 'topright'
    }).addTo(map);
    trailSearchEasyButton.button.style.border = 'none';
    trailSearchEasyButton.button.style.borderRadius = '4px';
    trailSearchEasyButton.button.style.backgroundColor = '#ADAFAE';
    trailSearchEasyButton.button.style.width = '280px';
    trailSearchEasyButton.button.style.fontFamily = 'source_code_promedium';

    //find trails in view on map init
    var bounds = map.getBounds();
    removeMarkers();
    dotNetObjRef.invokeMethodAsync("FindTrails", bounds.getSouthWest().lat, bounds.getSouthWest().lng, bounds.getNorthEast().lat, bounds.getNorthEast().lng);
}


function initializeWalkMap(Lat, Lng, dotNetObjRef) {

    if (map != null) {
        map.off();
        map.remove();
    }

    map = L.map('map').setView([Lat, Lng], 15);
    dotNetObj = dotNetObjRef;

    //mapbox map
    L.tileLayer('https://api.mapbox.com/styles/v1/rytisok/ckw68k17e2czt14o5uyi97e52/tiles/256/{z}/{x}/{y}?access_token=pk.eyJ1Ijoicnl0aXNvayIsImEiOiJja3R3b20wbGEybTl3MzBtcGdhZG96MnhqIn0.GIdSHPlZoTkmFHavTZZOqQ', {
        maxZoom: 25,
        attribution: "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
        detectRetina: true,
    }).addTo(map);


    L.control.scale({ imperial: true, metric: true }).addTo(map);

    map.on('zoomend', function () {
        updateMarkerSize();
    });

    let trailFilterEasyButton = L.easyButton('<img src="Images/dots.png" width="40" height="40">', function (btn, map) {
        dotNetObjRef.invokeMethodAsync("OpenDetails");
    }, {
        position: 'topright'
    }).addTo(map);
    trailFilterEasyButton.button.style.width = '48px';
    trailFilterEasyButton.button.style.height = '40px';


    AddCorners();

    //mission stats
    missionStats = L.control({ position: 'bottomleft' });

    missionStats.onAdd = function (map) {
        this._div = L.DomUtil.create('div', 'missionStats');
        this._div.style = 'margin-bottom: 35%; margin-left: 3px;';
        return this._div;
    };

    var missionInProgressText = L.control({ position: 'topcenter' });

    missionInProgressText.onAdd = function (map) {
        this._div = L.DomUtil.create('div', 'missionInProgress');
        this._div.style = 'margin-top: 5px;';
        this.update();
        return this._div;
    };

    missionInProgressText.update = function (props) {
        this._div.innerHTML = '<h5>Mission In Progress</h5>' + '<h6>Proceed on the marked route</h6>';
    };

    missionInProgressText.addTo(map);
    missionStats.addTo(map);

    markers = [];
    positionMarker = null;
    UpdatePositionMarker(Lat, Lng);
}


function AddCorners() {
    var corners = map._controlCorners,
        l = 'leaflet-',
        container = map._controlContainer;

    function createCorner(vSide, hSide) {
        var className = l + vSide + ' ' + l + hSide;

        corners[vSide + hSide] = L.DomUtil.create('div', className, container);
    }
    createCorner('top', 'left');
    createCorner('top', 'right');
    createCorner('bottom', 'left');
    createCorner('bottom', 'right');

    createCorner('top', 'center');
    createCorner('middle', 'center');
    createCorner('middle', 'left');
    createCorner('middle', 'right');
    createCorner('bottom', 'center');
}

function UpdateMissionStats(time, distance) {

    if (missionStats != null)
    {
        missionStats.update = function (props) {
            this._div.innerHTML = '<h5>Elapsed time:' + time + '</h5>' +
                '<h5>Mission progress:' + distance + '</h5>';
        };
    }

    missionStats.update();
}

//places dot icon on location
function UpdatePositionMarker(Lat, Lng)
{
    //if position marker is null, create one
    if (positionMarker == null)
    {
        CreateLocationMarker(Lat, Lng);
    }
    else {
        //just update position markers location
        positionMarker.marker.setLatLng([Lat, Lng]);
    }

}

function CreateLocationMarker(Lat, Lng)
{
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

    var marker = {
        marker: leafletMarker,
        sizeX: 28,
        sizeY: 28,
        geojson: null
    };
    positionMarker = marker;
    markers.push(positionMarker);
}

function myAlert(msg) {
    alert(msg);
}

function clearMarkers()
{
    trailMarkers.forEach(function (element) {
        map.removeLayer(element);
    });

    trailMarkers = [];
}

function addMarker(Lat, Lng, text, trailType)
{
    var currentZoom = map.getZoom();
    var sizeMultiplier = currentZoom * iconSizeMultiplier;
    var icon = L.Icon.extend({
        options: {
            iconSize: [40 * sizeMultiplier, 56 * sizeMultiplier],
            iconAnchor: [20 * sizeMultiplier, 28 * sizeMultiplier],
            popupAnchor: [0, (sizeMultiplier * -4 / currentZoom)]
        }
    });


    //creates new marker with icon and specified coords

    var trailIconUrl;

    switch (trailType)
    {
        case 1:
            trailIconUrl = 'Images/capture point red.png';
            break;
        case 2:
            trailIconUrl = 'Images/capture point green.png';
            break;
        case 3:
            trailIconUrl = 'Images/capture point blue.png';
            break;
        case 4:
            trailIconUrl = 'Images/capture point yeallow.png';
            break;
        default:
            trailIconUrl = 'Images/capture point neutral.png';
            break;
    }

    var markerIcon = new icon({ iconUrl: trailIconUrl});
    var leafletMarker = L.marker([Lat, Lng], { icon: markerIcon });
    var customOptions =
    {
        'className': 'popupCustom'
    }


    //assign trail information text
    leafletMarker.bindPopup(text, customOptions);

    var marker = {
        marker: leafletMarker,
        sizeX: 40,
        sizeY: 56,
        geojson: null
    };

    //show geojson on marker click
    leafletMarker.on('click',
        function (e)
        {
            //showGeojson(geoJsonUrl);
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

function removeMarkers() {
    trailMarkers.forEach(function (element) {
        map.removeLayer(element);
    });
}

function showTrailOnly()
{
    map.closePopup();
    //showGeojson(currentMarker.geojson);
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

        icon.options.iconSize = [element.sizeX * sizeMultiplier, element.sizeY * sizeMultiplier];
        icon.options.iconAnchor = [element.sizeX * sizeMultiplier / 2, element.sizeY * sizeMultiplier / 2];
        icon.options.popupAnchor = [0, (element.sizeY * -4 / currentZoom)];
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

function startTrail(ID)
{
    dotNetObj.invokeMethodAsync("StartTrail", ID);
}

function drawLines(polylinePoints)
{
    if (walkedTrailLine != null)
    {
        map.removeLayer(walkedTrailLine);
    }
    walkedTrailLine = L.polyline(polylinePoints, {
        color: '#00E1FF',
        weight: 7,
        opacity: 0.85,
        smoothFactor: 1
    }).addTo(map);
}

function drawLineToTrail(userPoint, trailPoint)
{
    removeLineToTrail();

    lineToTrail = L.polyline([userPoint, trailPoint], {
        color: 'white',
        weight: 7,
        opacity: 0.85,
        smoothFactor: 1,
        dashArray: '20, 20',
        lineCap: 'square',
        dashOffset: '20'
    }).addTo(map);
}

function removeLineToTrail() {
    if (lineToTrail != null) {
        map.removeLayer(lineToTrail);
    }
}

function futureFeature()
{
    alert("future feature");
}
