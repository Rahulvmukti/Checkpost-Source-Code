var mapOptions;
var map;
var markerStore = {};

var map_type;
var tm_str;
var array_path = {};
var infoWindow;
var c_code;
function pageLoad() {


}
function MapInit() {
    markerStore = {};

    mapOptions = {
        zoom: 9,
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
            //debugger;
            var markers = j.Table;
            var status = j.Table1;
            if (status == undefined || status.length == 0) {
                $("#lblConfigured").html("0");
                $("#lblOnline").html("0");
                $("#lblOffline").html("0");
            }
            if (status != undefined) {
                if (status.length > 0) {
                    $("#lblConfigured").html(status[0].Configured);
                    $("#lblOnline").html(status[0].Online);
                    $("#lblOffline").html(status[0].Offline);
                }
            }
            console.log(r)
            console.log(j)
            if (markers == undefined || markers.length == 0) {
                var myLatlng = new google.maps.LatLng(17.1232, 79.2086);
                map.set("center", myLatlng);
            }
            if (markers != undefined) {
                debugger
                for (var i = 0; i < markers.length; i++) {
                    var data = markers[i];
                    //if (data.latitude != 0 && data.longitude != 0) {
                    if (markerStore.hasOwnProperty(data.id)) {
                        markerStore[data.id].setPosition(new google.maps.LatLng(data.latitude, data.longitude));
                        var markerimage;

                        if (data.IsAro == "15") {
                            markerimage = 'images/MapImages/car-green_2.png';
                        }
                          else {
                            markerimage = data.status == "RUNNING" ? 'images/MapImages/car-green.png' : 'images/MapImages/car-red.png';
                        }
                        markerStore[data.id].setIcon(markerimage);
                    }
                    else {
                        console.log("latitude " + data.latitude)
                        console.log("longitude " + data.longitude)
                        var myLatlng2 = new google.maps.LatLng(data.latitude, data.longitude);
                        var markerimage;

                        if (data.IsAro == "15") {
                            markerimage = 'images/MapImages/car-green_2.png';
                        }
                         
                        else {
                            markerimage = data.status == "RUNNING" ? 'images/MapImages/car-green.png' : 'images/MapImages/car-red.png';
                        }
                      
                        var marker2 = new google.maps.Marker({
                            position: myLatlng2,
                            map: map,
                            info: data,
                            title: data.videoname,
                            icon: markerimage
                        });
                        marker2.addListener("click", () => {
                            
                            // LoadPlayer(data);
                        });
                         if (i == 0 && flag == 0) {
                            map.set("center", myLatlng2);
                        }
                        //}

                        var _d = new Date();
                        if (typeof data.UpdateDatetime === "undefined") {

                        }
                        else {
                            _d = new Date(data.UpdateDatetime);
                        }
                        _d = formatDate(_d);


                        (function (marker2, data) {
                            var infocontent = "<div class='panel panel-primary'><div class='panel-heading'>" + data.ACNum + "/" + data.PSNum + "</div> <div class='panel-body'>" + data.displayname + "<br/>" + data.displayMobile + "<br/>Last Updated :" + _d + "</div></div>";
                            google.maps.event.addListener(marker2, "click", function (e) {
                                if (marker2.icon == 'images/red.png') {
                                    infoWindow.setContent(infocontent);
                                    infoWindow.open(map, marker2);
                                } else {
                                    
                                     LoadPlayer(marker2.info);
                                }
                            });
                        })(marker2, data);
                        //  marker2.setMap(map);
                        markerStore[data.id] = marker2;

                    }
                    //}
                    if (flag == 0) {

                        flag = 1;
                    }
                }
            }
        }, error: function (err) {
            console.log(err.responseText);
        }

    });
 
    tm_str = setTimeout(function () { LoadLocation(location, 1) }, 10000);

}


var markerStore = {}; // Object to store markers
var polyline; // Variable to store the polyline
var map; // Your Google Maps object

function LoadLocation_route(location, selectedDate, location1, flag) {
    debugger;
    c_code = location;
    if (flag == 0) {
        myStopFunction();
        markerStore = {};
        array_path = {};
        MapInit();
        displayNoDataMessage();
    }

    debugger;
    $.ajax({
        type: "POST",
        url: "RouteView.aspx/LoadMap",
        data: JSON.stringify({ d: c_code, selectedDate: selectedDate, location: location1 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var j = jQuery.parseJSON(r.d);
            var markers = j.Table;
            var status = j.Table1;
            if (status != undefined) {
                if (status.length > 0) { 
                    $("#totalKM").html(status[0].totalKM); 
                }
            }
            var currentDate = new Date();
            var markerCount = 0; // Counter for markers

            if (markers == undefined || markers.length == 0) {
                var myLatlng = new google.maps.LatLng(31.1471, 75.3412);
                map.setCenter(myLatlng);
                displayNoDataMessage(); // Call the function to display "No Data Found" message
            } else {
                hideNoDataMessage(); // Call the function to hide the message
            }

            var pathCoordinates = []; // Array to store coordinates for the polyline

            if (markers != undefined) {
                debugger;

                for (var i = 0; i < markers.length; i++) {
                    var data = markers[i];

                    if (data.latitude != 0 && data.longitude != 0) {
                        var myLatlng2 = new google.maps.LatLng(data.latitude, data.longitude);
                        var markerimage = '';

                        var markerDate = new Date(data.UpdateDatetime || data.UpdateDate);
                        var markerTitle = "VehicleNumber: " + data.videoname + "\nCameraID: " + data.location + "\nspeed: " + data.speed;
                         if (i == markers.length - 1) { 
                            markerimage = 'images/MapImages/car-red.png';
                        } else {
                            if (i == 0) {
                                markerimage = 'images/MapImages/car-green.png';
                            } else {
                                markerimage = 'images/MapImages/car-green_2.png';
                            }
                        }
                        var marker2 = new google.maps.Marker({
                            position: myLatlng2,
                            map: map,
                            info: data,
                            title: markerTitle,
                            icon: markerimage
                        });

                        marker2.addListener("click", () => {
                       //     LoadPlayer1(marker2.info);
                        });

                        if (i == 0 && flag == 0) {
                            map.setCenter(myLatlng2);
                        }

                        var _d = new Date(data.UpdateDatetime || data.UpdateDate);
                        _d = formatDate(_d);

                        (function (marker2, data) {
                            var infocontent = "<div class='panel panel-primary'><div class='panel-heading'>" + data.ACNum + "/" + data.PSNum + "</div> <div class='panel-body'>" + data.displayname + "<br/>" + data.displayMobile + "<br/>Last Updated :" + _d + "</div></div>";

                            google.maps.event.addListener(marker2, "click", function (e) {
                                if (marker2.icon == 'images/red.png') {
                                    infoWindow.setContent(infocontent);
                                    infoWindow.open(map, marker2);
                                } else {
                                    //LoadPlayer1(marker2.info);
                                }
                            });
                        })(marker2, data);

                        markerStore[data.id] = marker2;
                        markerCount++; // Increment marker count

                        // Add marker position to the pathCoordinates array
                        pathCoordinates.push(myLatlng2);
                    }
                }
            }

            // Create or update the polyline with pathCoordinates
            if (polyline) {
                polyline.setPath(pathCoordinates);
            } else {
                polyline = new google.maps.Polyline({
                    path: pathCoordinates,
                    geodesic: true,
                    strokeColor: '#FF0000', // Color of the polyline
                    strokeOpacity: 1.0,
                    strokeWeight: 2
                });
                polyline.setMap(map);
            }

            console.log("Total Markers: " + markerCount); // Output marker count to console
        },
        error: function (err) {
            console.log(err.responseText);
        }
    });
    function LoadPlayer1(data) {
        //  var modal = document.getElementById("myModal");
        debugger;
        $("#myModal").show();
        //var span = document.getElementsByClassName("close")[0];
        if (flvjs.isSupported()) {
            var videoElement = document.getElementById('videoElement_6');
            var flvurl = 'wss://' + data.cdnsvc + '/live-record/' + data.videoname + '.flv';
            var flvPlayer = flvjs.createPlayer({
                type: 'flv',
                url: flvurl
            });
            flvPlayer.attachMediaElement(videoElement);
            flvPlayer.load();
            flvPlayer.play();
            flvPlayer.on(flvjs.Events.BUFFER_FULL, () => {
                setTimeout(() => {
                    flvPlayer.currentTime = delayInSeconds;
                }, delayInSeconds * 1000);
            });
            flvPlayer.on(flvjs.Events.BUFFER_FULL, () => {
                setTimeout(() => {
                    flvPlayer.currentTime = delayInSeconds;
                }, delayInSeconds * 1000);
            });
            var ResetzoomButton = document.getElementById("ResetzoomButton");
            var zoomInButton = document.getElementById("zoomInButton");
            var zoomOutButton = document.getElementById("zoomOutButton");
            var videoElement = document.getElementById('videoElement_6');
            var zoomLevel = 1;

             
        }
        document.getElementById("modalDistrict").innerHTML = data.district;
        document.getElementById("modalVehicle").innerHTML = data.Vehicle_No;
        document.getElementById("modalDetails").innerHTML = data.name + " / " + data.contact_no;
        modal.style.display = "block";
        span.onclick = function () {
            modal.style.display = "none";
            // jwplayer('videoElement_6').remove();
        }

        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
                // jwplayer('videoElement_6').remove();
            }
        }
    }
    function displayNoDataMessage() {
        debugger;
        // Show or display a message indicating no data found
        // $("#noDataMessage").show();

        // Set lblOnline to display "No Data Found"
        $("#lblnodata").html("No Data Found");
    }

    function hideNoDataMessage() {
        // Hide the message indicating no data found
        $("#lblnodata").html("");
    }
    tm_str = setTimeout(function () { LoadLocation_route(location, selectedDate, location1, 1); }, 10000);
}

