var mapOptions;
var map;
var markerStore = {};
var markerStore2 = {};
var map_type;
var tm_str;
var array_path = {};
var infoWindow;
var c_code;
function pageLoad() {


}
function MapInit() {
    markerStore = {};
    markerStore2 = {};
    mapOptions = {
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("MapArea"), mapOptions);
    infoWindow = new google.maps.InfoWindow();
}
function zoomToObject(obj) {

    var bounds = new google.maps.LatLngBounds();
    var points = obj.getPath().getArray();
    for (var n = 0; n < points.length; n++) {
        bounds.extend(points[n]);
    }
    map.fitBounds(bounds);
}
function myStopFunction() {
    clearTimeout(tm_str);
}
function formatDate(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear() + " " + strTime;
}
function LoadLocation(location, flag) {
    c_code = location;
    if (flag == 0) {
        myStopFunction();
        markerStore = {};
        markerStore2 = {};
        array_path = {};
        MapInit();


    }
    $.ajax({
        type: "POST",
        url: "MapView.aspx/LoadMap",
        data: '{d: "' + c_code + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var j = jQuery.parseJSON(r.d);

            var markers = j.Table;

            if (markers.length == 0) {
                var myLatlng = new google.maps.LatLng(23.048519000, 72.566414100);
                map.set("center", myLatlng);
            }
            for (var i = 0; i < markers.length; i++) {

                var data = markers[i];
                if (markerStore.hasOwnProperty(data.id)) {
                    markerStore[data.id].setMap(null);
                }
                if (markerStore2.hasOwnProperty(data.id)) {
                    markerStore[data.id].setMap(null);
                }
                var myLatlng = new google.maps.LatLng(data.boothlat, data.boothlng);
                if (i == 0 && flag == 0) {
                    map.set("center", myLatlng);
                }

                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: 'Operator Location',
                    icon: 'images/green.png'
                });
                // marker.setMap(map);
                var myLatlng2 = new google.maps.LatLng(data.latitude, data.longitude);
                var marker2 = new google.maps.Marker({
                    position: myLatlng2,
                    map: map,
                    title: 'Current Location',
                    icon: 'images/red.png'
                });
                var _d = new Date();
                if (typeof data.UpdateDatetime === "undefined") {

                }
                else {
                    _d = new Date(data.UpdateDatetime);
                }
                _d = formatDate(_d);

                (function (marker, data) {
                    var infocontent = "<div class='panel panel-primary'><div class='panel-heading'>" + data.ACNum + "/" + data.PSNum + "</div> <div class='panel-body'>" + data.displayname + "<br/>" + data.displayMobile + "<br/>Last Updated :" + _d + "</div></div>";
                    google.maps.event.addListener(marker, "click", function (e) {

                        infoWindow.setContent(infocontent);
                        infoWindow.open(map, marker);
                    });
                })(marker, data);
                (function (marker2, data) {
                    var infocontent = "<div class='panel panel-primary'><div class='panel-heading'>" + data.ACNum + "/" + data.PSNum + "</div> <div class='panel-body'>" + data.displayname + "<br/>" + data.displayMobile + "<br/>Last Updated :" + _d + "</div></div>";
                    google.maps.event.addListener(marker2, "click", function (e) {
                        infoWindow.setContent(infocontent);
                        infoWindow.open(map, marker2);
                    });
                })(marker2, data);
                //  marker2.setMap(map);
                markerStore[data.id] = marker;
                markerStore2[data.id] = marker2;
                var rpath = [{ lat: data.boothlat, lng: data.boothlng }, { lat: data.latitude, lng: data.longitude }];



                var roadPath = new google.maps.Polyline({
                    path: rpath,
                    geodesic: true,
                    strokeColor: '#FF0000',
                    strokeOpacity: 1.0,
                    strokeWeight: 1
                });

                //roadPath.setMap(map);
                if (array_path.hasOwnProperty(data.id)) {
                    //   array_path[data.id] = roadPath;
                    array_path[data.id].setMap(null);

                }

                array_path[data.id] = roadPath;


                array_path[data.id].setMap(map);
                if (flag == 0) {
                    zoomToObject(roadPath);
                    flag = 1;
                }

            }
        }, error: function (err) {
            console.log(err.responseText);
        }

    });

    tm_str = setTimeout(function () { LoadLocation(location, 1) }, 10000);

}
