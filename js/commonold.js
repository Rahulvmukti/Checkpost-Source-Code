$(window).load(function () {
    "use strict";
    $('#loader').fadeOut();
});
$(document).ready(function () {
    var loc = window.location.href.split('/');
    var page = loc[loc.length - 1];
    $('ul.nav li.active').removeClass('active');
    $("ul.nav").find("li a[href='" + page + "']").parent().addClass('active');

});

function centeredPopup(playerid, sname, playstatus) {

    var popupWindow = null;
    var w = 460;
    var h = 268;
    var scroll = 'no';
    LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;
    settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable'
    popupWindow = window.open('WatchVideo.aspx?vid=' + playerid + '&sname=' + sname + "&playerType=" + playerType, "Watch Video", settings);

    return false;
}

function downloadvideo_ele(servername, streamname, stateid, cur_stateid, isdwn) {
    $("#dwn_" + streamname).prop("target", "_blank");
      var dwnlnk = "https://byelection.blob.core.windows.net/" + servername + "/";

    if (servername.replace('vms', '') > 7) {
        dwnlnk = "https://byelection2.blob.core.windows.net/" + servername + "/";
    }

    $("#dwn_" + streamname).prop("href", dwnlnk + streamname + ".flv");
    return true;

}

function downloadvideo(servername, streamname, stateid, cur_stateid, isdwn) {
  
    
    $("#dwn_" + streamname).prop("target", "_blank");
	
	var foldername = "vellore1";
	if(servername=="vms4")
	{
		foldername="vellore2";
	}
    dwnlnk = "https://elesouthdata.blob.core.windows.net/"+foldername+"/" + streamname + ".flv";
	
	
        
    $("#dwn_" + streamname).prop("href", dwnlnk);
    return true;

}

function downloadvideoold(servername, streamname, stateid, cur_stateid, isdwn) {
  
  var sname = servername;

    $("#dwn_" + streamname).prop("target", "_blank");
	var ddlphase = "phase7";
     var vms1 = ['vms1', 'vms100', 'vms101', 'vms45', 'vms38'];
        var vms2 = ['vms2', 'vms56', 'vms49', 'vms49', 'vms67'];
        var vms3 = ['vms3','vms57','vms43','vms46'];
        var vms4 = ['vms4', 'vms50', 'vms44'];
        var vms5 = ['vms5', 'vms33'];
        var vms6 = ['vms6', 'vms64', 'vms70', 'vms59'];
        var vms7 = ['vms7', 'vms62', 'vms58', 'vms54', 'vms63'];
        var vms8 = ['vms8', 'vms68','vms35'];
        var vms66 = ['vms66','vms34'];
        var vms10 = ['vms10', 'vms55', 'vms69', 'vms60'];
        var vms11 = ['vms11', 'vms61', 'vms52'];
        var vms12 = ['vms12', 'vms53', 'vms47'];
        var vms13 = ['vms13', 'vms41', 'vms65'];
        var vms14 = ['vms14', 'vms666', 'vms42'];
        var vms15 = ['vms15', 'vms39'];
        var vms16 = ['vms16', 'vms36'];
        var vms17 = ['vms17', 'vms40','vms48'];
        var vms18 = ['vms18', 'vms37'];
        var vms19 = ['vms19', 'vms31'];
        var vms20 = ['vms20', 'vms32'];
        var vms21 = ['vms21'];
        var vms22 = ['vms22'];
        var vms23 = ['vms23'];
        var vms24 = ['vms24'];
        var vms25 = ['vms25'];
        var vms26 = ['vms26'];
        var vms27 = ['vms27'];
        var vms28 = ['vms28'];
        var vms29 = ['vms29'];
        var vms30 = ['vms30', 'vms51'];

        var vm_server;

        if (vms1.includes(sname)) {
            vm_server = "vms1";
        } else if (vms2.includes(sname)) {
            vm_server = "vms2";
        } else if (vms3.includes(sname)) {
            vm_server = "vms3";
        } else if (vms4.includes(sname)) {
            vm_server = "vms4";
        } else if (vms5.includes(sname)) {
            vm_server = "vms5";
        } else if (vms6.includes(sname)) {
            vm_server = "vms6";
        } else if (vms7.includes(sname)) {
            vm_server = "vms7";
        } else if (vms8.includes(sname)) {
            vm_server = "vms8";
        } else if (vms66.includes(sname)) {
            vm_server = "vms66";
        } else if (vms10.includes(sname)) {
            vm_server = "vms10";
        } else if (vms11.includes(sname)) {
            vm_server = "vms11";
        } else if (vms12.includes(sname)) {
            vm_server = "vms12";
        } else if (vms13.includes(sname)) {
            vm_server = "vms13";
        } else if (vms14.includes(sname)) {
            vm_server = "vms14";
        } else if (vms15.includes(sname)) {
            vm_server = "vms15";
        } else if (vms16.includes(sname)) {
            vm_server = "vms16";
        } else if (vms17.includes(sname)) {
            vm_server = "vms17";
        } else if (vms18.includes(sname)) {
            vm_server = "vms18";
        } else if (vms19.includes(sname)) {
            vm_server = "vms19";
        } else if (vms20.includes(sname)) {
            vm_server = "vms20";
        } else if (vms21.includes(sname)) {
            vm_server = "vms21";
        } else if (vms22.includes(sname)) {
            vm_server = "vms22";
        } else if (vms23.includes(sname)) {
            vm_server = "vms23";
        } else if (vms24.includes(sname)) {
            vm_server = "vms24";
        } else if (vms25.includes(sname)) {
            vm_server = "vms25";
        } else if (vms26.includes(sname)) {
            vm_server = "vms26";
        } else if (vms27.includes(sname)) {
            vm_server = "vms27";
        } else if (vms28.includes(sname)) {
            vm_server = "vms28";
        } else if (vms29.includes(sname)) {
            vm_server = "vms29";
        } else if (vms30.includes(sname)) {
            vm_server = "vms30";
        }


        var dwnlnk;
        var vmsall = vm_server;
       
        if (vmsall.replace('vms', '') >= 1 && vmsall.replace('vms', '') <= 5) {
            dwnlnk = "https://elesouthdata.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') >= 6 && vmsall.replace('vms', '') <= 10) {
            dwnlnk = "https://eledatawest.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') >= 13 && vmsall.replace('vms', '') <= 17) {
            dwnlnk = "https://tnweststore.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') >= 19 && vmsall.replace('vms', '') <= 22) {
            dwnlnk = "https://tnweststore.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') >= 23 && vmsall.replace('vms', '') <= 30) {
            dwnlnk = "https://tnsouthstore.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') == 18) {
            dwnlnk = "https://tnsouthstore.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') == 66) {
            dwnlnk = "https://eledatawest.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }
        if (vmsall.replace('vms', '') == 11 || vmsall.replace('vms', '') == 12) {
            dwnlnk = "https://elesouthdata.blob.core.windows.net/" + vm_server + "-" + ddlphase + "/";
        }

    $("#dwn_" + streamname).prop("href", dwnlnk + streamname + ".flv");
    return true;

}
function downloadcenteredPopup(playerid, sname, playstatus, stateid) {

    var popupWindow = null;
    var w = 640;
    var h = 410;
    var scroll = 'no';
    LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;
    settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable=no'
    popupWindow = window.open('WatchVideodownload.aspx?vid=' + playerid + '&sname=' + sname + '&phase=' + stateid, "Watch Video", settings);

    return false;
}

function playvideo(streamname, sname, playstatus) {
    if (/Android/i.test(navigator.userAgent)) {
        if (playstatus == 'RUNNING') {
            newOpen(streamname, sname);
        }
        else {
            alert("Location is offline.");
        }
    }
    else {
        $('#myModal').on('shown.bs.modal', function (e) {
            var $invoker = $(e.relatedTarget);
            $('.modal-title').html("<b>AC: </b>" + $($invoker).attr("data-title") + "<br/><b>PS: </b>" + $($invoker).attr("data-content") + "<br/><b>TAB Number: </b>" + streamname);
            $('#myModal iframe').attr("src", "watchvideo.aspx?vid=" + streamname + "&sname=" + sname);
        });
    }
    return false;
}

function loadhlsplayer(playerid, sname, sid, cur_stateid) {
    //$(".playercontrol").hide();

    //getJSfile("player/jwp8/jwplayer.js");
    var cssfile = $("<style>.jw-state-idle .jw-controls {background-color: transparent !important;}</style>");
    $("head").append(cssfile);

    var vidname = playerid;
    var hlsurl = "http://" + sname + '.vmukti.com:8080/live-record/' + vidname + "/index.m3u8";


    var playerInstance = jwplayer(playerid);

    playerInstance.setup({
        key: "UeOwyRhgpc5Og22j+4YM7uzrcbTwxTV9r+XadsoRGr8=",
        file: hlsurl,
        width: "100%",
        aspectratio: "16:9",
        stretching: "exactfit",
        autostart: true,
        mute: false
    });

    playerInstance.on("error", function (event) {

        setTimeout(function () { loadhlsplayer() }, 3000);
    });

    playerInstance.on("idle", function (event) {
        playerInstance.play(true);
    });
    playerInstance.on("play", function (event) {
        $('#divviddownload').show();
        clearTimeout();
        playerInstance.play();
    });
};

function LoadlivePlayer6(playerid, sname, plid) {

    var vidname = playerid;
    var vidurl = "rtmp://" + sname + '.vmukti.com:80/live-record/';

    if (sname == 'media1') {
        vidurl = "rtmp://" + sname + '.ambicam.com:1938/dvr1/';
    }

    if (plid == "mplayer") {
        vidname = "mplayer";
    }
    jwplayer(vidname).setup({

        sources: [{
            file: vidurl + playerid,
            type: "rtmp"
        },
            {
                file: "http://" + sname + ".vmukti.com:8080/live-record/" + playerid + "/index.m3u8",
                type: "hls"
            }
],

        width: "100%",
        aspectratio: "16:9",
        androidhls: true,
        abouttext: "VMukti",
        aboutlink: "http://www.vmukti.com",
        logo: {
            file: "images/vlogo.png",
            link: "http://www.vmukti.com/",
            linktarget: "_blank",
            position: "right-bottom"
        },
        autostart: "true"
    });

    jwplayer(vidname).onIdle(function () {
        setTimeout(function () {
            jwplayer(vidname).play();
        }, 5000);

    });

}

function LoadlivePlayer7(playerid, sname, plid) {
	
	 var vidname = playerid;
	 if (plid == "mplayer") {
        vidname = "mplayer";
    }
	if (playerid.indexOf("_") >= 0) {
        playerid = playerid.split('_')[1];
    }
    //var hlsurl = "http://" + sname + '.vmukti.com:8080/live-record/'+playerid+"/index.m3u8";
	var hlsurl = "https://" + sname + '.vmukti.com/live-record/'+playerid+"/index.m3u8";
	
            var playerInstance = jwplayer(vidname);

            playerInstance.setup({


                file: hlsurl,
                width: "100%",
                height: "100%",
                controls: true,
                stretching: "exactfit",

                androidhls: "true",
                autostart: "true"

            });

            playerInstance.on("error", function (event) {
                pl_flag = 1;
                $(".playerimage").show();
				   //$(".jw-error-msg").html("Please wait... Loading camera feed ... ")
                $(".jw-error-msg").hide();             
                //  playerInstance.setControls(false);
                clearTimeout();
                setTimeout(function () { playerInstance.stop(); playerInstance.play(true); }, 3000);

            });
            playerInstance.on("idle", function (event) {
                playerInstance.play(true);
                pl_flag = 1;
            });
            playerInstance.on("play", function (event) {
                $(".playerimage").hide();
                // playerInstance.setControls(false);
                if (pl_flag == 0) {
                
                    pl_flag = 1;
                }
                clearTimeout();
                playerInstance.play(true);
            });
}
function LoadlivePlayer7_OLD(playerid, sname, plid) {
    var vidname = playerid;
    var vidurl = "rtmp://" + sname + '.vmukti.com:80/live-record/';

    if (sname == 'media1') {
        vidurl = "rtmp://" + sname + '.ambicam.com:1938/dvr30/';
    }

    if (plid == "mplayer") {
        vidname = "mplayer";
    }
    if (playerid.indexOf("_") >= 0) {
        playerid = playerid.split('_')[1];
    }

    jwplayer(vidname).remove();
    var playerInstance = jwplayer(vidname);

    playerInstance.setup({
        sources: [{
            file: vidurl + playerid,
            type: "rtmp"
        },
            {
                file: "http://" + sname + ".vmukti.com:8080/live-record/" + playerid + "/index.m3u8",
                type: "hls"
            }
],

        width: "100%",
        aspectratio: "16:9",
        stretching: "exactfit",
        controls: true,
        androidhls: "true",
        abouttext: "VMukti Solutions Pvt. Ltd.",
        aboutlink: "http://www.vmukti.com",
        logo: {
            file: "images/vlogo.png",
            position: "bottom-right"
        },
        autostart: "true"
    });
    playerInstance.on("error", function (event) {
        $(".playercontrolerr_" + vidname).show();
        $('#btntogglefullscreen_' + vidname).hide();
        if (event.message == 'Error loading stream: ID not found on server') {
            // $("#nolive").show();

            setTimeout(function () { playerInstance.stop(); playerInstance.play(true); playerInstance.setControls(true); }, 5000);
        }
    });
    playerInstance.on("buffer", function (event) {
        $(".playercontrolerr_" + vidname).hide();

    });
    playerInstance.on("idle", function (event) {
        playerInstance.setControls(true);
        playerInstance.play(true);
    });
    playerInstance.on("play", function (event) {
        playerInstance.setControls(false);
        playerInstance.play(true);
        $(".playercontrolerr_" + vidname).hide();
        $('#btntogglefullscreen_' + vidname).show();
        // $("#nolive").hide();

    });


}
function LoadPlayer(playerid, sname, plid, schname, locname,location, did, prourl) {

    $(".plerror").hide();
    $("#playbackfromlive").addClass("active").siblings().removeClass("active");
    if (/Android/i.test(navigator.userAgent)) {
        //LoadlivePlayer6(playerid, sname, plid);
        newOpen(playerid, sname);
    }
    else if (/iPhone|iPad/i.test(navigator.userAgent)) {
        // LoadlivePlayer6(playerid, sname, plid);
        newOpen(playerid, sname);
    }
    else {
       var vidname = playerid;
        if (plid == "mplayer") {
            vidname = "mplayer";
            $('#schloc').show();
            $('#playbackcontrol').hide();
            $('#btnsettings').show();
            $("#playerimg").hide();
   $('#schloc').html("<h5><span style='color:#337AB7;'><b>Block : </b></span>" + schname + "<br/> <span style='color:#337AB7;'><b>PS No. : </span> </b>" + locname + "<br/> <span style='color:#337AB7;'><b>Location : </b></span>" + location + "</h5>");

           $('#btnsettings').click(function () {
                document.getElementById("iframesetting").src = 'camerasetting.aspx?did=' + did + '&prourl=' + prourl;
            });

            $('#playback').click(function () {

                LoadPlaybackPlayer6(plid, sname, playerid, plid);
            });
            $('#playbackfromlive').click(function () {

                $("#playerimg").hide();
                LoadPlayer(playerid, sname, plid, schname, locname, did);
            });
        }

		  if (playerType == "JWPlayer") {
           
            if (plid == "mplayer") {
                $('#mplayer').show();
            }
            $('#RTMPplayer').hide();
			  }
            else {
                $('#RTMPplayer_' + playerid).show();
                $('#' + playerid).hide();
            }
            $('#RTMPplayer').hide();
           
        LoadlivePlayer7(playerid, sname, plid);
    }
	  else if (playerType== "RTMPPlayer") {
            $('#RTMPplayer').show();
            if (plid == "mplayer") {
                $('#mplayer').hide();
            }
			   $('#RTMPplayer').show();
            }
            else {
                $('#RTMPplayer_' + playerid).show();
                $('#' + playerid).hide();
            }
            loadRTMPPayer(sname, playerid, plid);
           
        }
        
    }
    return false;
   }
/*RTMP Player*/


function loadRTMPPayer(sname, playerid, plid)
{
  var playerid1
    playerid1 = playerid
   
    //var wsvidurl = "ws://" + sname + '.ambicam.com:1948/dvr30/' + playerid;
    //var vidurl = "rtmp://" + sname + '.vmukti.com:80/live-record/';
	if (playerid.indexOf("_") >= 0) {
        playerid1 = playerid.split('_')[1];
    }

    var startInfo = {
        schema: "rtmp",
        host: sname + ".vmukti.com:80",
        app: "live-record",
        video: playerid1
    };
    var mp4MaxSize = 2000000;

    var props = {
        app: startInfo.app,
        flashver: 'MAC 11,6,602,180',
        swfUrl: 'http://192.168.0.6:5080/demos/Something.swf',
        tcUrl: startInfo.schema + '://' + startInfo.host + '/' + startInfo.app,
        fpad: false,
        audioCodecs: 0x0FFF,
        videoCodecs: 0x00FF,
        videoFunction: 1,
        pageUrl: 'http://192.168.0.6:5080/demos/Something.html',
        objectEncoding: 0
    };

    var rtmp = new RtmpJs.Browser.RtmpWsTransport({
        url:"wss://" + sname + ".vmukti.com:90"
    });
    rtmp.onresponse = function (e) {
        console.log('#response');
    };
    rtmp.onevent = function (e) {
        console.log('#event');
    };
    rtmp.onconnected = function (e) {
        console.log('#connected');

        rtmp.createStream(2, null);
    };
    rtmp.onstreamcreated = function (e) {
        console.log('#streamcreated: ' + e.streamId);
        console.log(e);

        var ns = e.stream;
        ns.ondata = function (message) {
            /*
    console.log('#packet (' + message.typeId + '): ' +
      Array.prototype.map.call(message.data,
      function (i) { return (i + 256).toString(16).substr(1); }).join(' ') );
      */
            if (message.data.length > 0) {
                mp4.packet(message.typeId, message.data);
            }
        };
        ns.oncallback = function () {
            console.log('#callback');
            console.log(arguments);
        };
        ns.onscriptdata = function (type, data) {
            console.log('#object: ' + type);
            console.log(arguments);
            if (type === 'onMetaData') {
                mp4.init(data);
            }
        };
        ns.play(startInfo.video);
        //rtmp.setBuffer(0x7D0);
    };
    rtmp.connect(props);
    var mp4 = {
        packets: 0,
        initialized: false,
        pendingPackets: [],
        init: function (metadata) {
            // live stream: no video information in flv metadata header
            // try to fake it
            // -> not so good, only first frame rendered
            metadata.videocodecid = 'avc1';
            metadata.avclevel = 21;
            metadata.avcprofile = 66;
            metadata.height = 360;
            metadata.width = 640;
            metadata.framerate = 10;


            console.log(RtmpJs.MP4.parseFLVMetadata(metadata));

            if (!metadata.audiocodecid && !metadata.videocodecid) {
                return; // useless metadata?
            }

            this.initialized = true;
            this.mux = new RtmpJs.MP4.MP4Mux(RtmpJs.MP4.parseFLVMetadata(metadata));
            this.mux.ondata = function (data) {
             //   console.log("mux.ondata: " + data.length)
                dataSource.onData(data)
            }.bind(this);
            this.mux.oncodecinfo = function (codecs) {
                dataSource.mimeType = 'video/mp4; codecs=\"' + codecs.join(',') + '\"';
                console.log(dataSource.mimeType);
            };

            // process pending data
            while (this.pendingPackets.length > 0) {
                this.mux.pushPacket.apply(this.mux, this.pendingPackets.shift());
            }
        },
        packet: function (type, data) {
            //document.getElementById('packets').textContent = ++this.packets;
            if (!this.initialized) {
                this.pendingPackets.push([type, new Uint8Array(data)]);
                return;
            }
            this.mux.pushPacket(type, new Uint8Array(data));
        }
    };

    var dataSource = {
        mimeType: '',
        onData: null
    };
    var mediaSource = new MediaSource();
    this.msewriter = new RtmpJs.MSEWriter(mediaSource);
    this.msewriter.listen(dataSource);
      if (plid == "mplayer")
    document.getElementById('RTMPplayer').src = URL.createObjectURL(mediaSource);
	 else
    document.getElementById('RTMPplayer_' + playerid).src = URL.createObjectURL(mediaSource);


}
function LoadSettings(did) {
    document.getElementById("iframesetting").src = 'camerasetting.aspx?did=' + did;
}
function LoadPlayer6(playerid, sname, sid, cur_stateid) {
    if (/Android/i.test(navigator.userAgent)) {
        // $('#' + playerid).html("<h4><a href='#' onclick=\"return newOpen('" + playerid + "','" + sname + "');\"><img src='images/play.png' alt='Play' /><p>" + playerid + "</p></a></h4>");
    }
    else {
        var hasFlash = false;

        try {
            var fo = new ActiveXObject('ShockwaveFlash.ShockwaveFlash');
            if (fo) {
                hasFlash = true;
            }
        } catch (e) {
            if (navigator.mimeTypes
        && navigator.mimeTypes['application/x-shockwave-flash'] != undefined
        && navigator.mimeTypes['application/x-shockwave-flash'].enabledPlugin) {
                hasFlash = true;
            }
        }
        if (hasFlash) {

            var vidname = playerid;
            //  var vidurl = "rtmp://" + sname + '.vmukti.com:80/vod/';
            var vidurl;
            if (sid == cur_stateid) {
                vidurl = "http://" + sname + '.vmukti.com:8080/phase1/';
            }
            else {
                vidurl = "https://updata.blob.core.windows.net/phase" + sid + "/";
            }

            jwplayer(playerid).setup({
                file: "rtmp://" + sname + ".vmukti.com:80/vod/" + vidname + ".flv",
                image: "images/player.jpg",
                width: "100%",
                aspectratio: "16:9",
                controls: false,
                abouttext: "VMukti",
                aboutlink: "http://www.vmukti.com",

                autostart: "true"
            });

            jwplayer(playerid).onPlay(function () {
                $('#divviddownload').show();
                jwplayer(playerid).setControls(true);
                jwplayer(playerid).play(true);
            });
            jwplayer(playerid).onError(function () {
                jwplayer(playerid).setControls(false);
            });
            jwplayer(playerid).onIdle(function () {
                setTimeout(function () {
                    jwplayer(playerid).play();
                }, 20000);
            });
        }
        else {

            $('#' + playerid).html('<h4>Please download and install flash player : <a href="https://get.adobe.com/flashplayer/">Click Here</a></h4>');

        }
    }
    return false;
}
function LoadPlaybackPlayer6(playerid, sname, strname, cur_stateid) {
    if (/Android/i.test(navigator.userAgent)) {
        // $('#' + playerid).html("<h4><a href='#' onclick=\"return newOpen('" + playerid + "','" + sname + "');\"><img src='images/play.png' alt='Play' /><p>" + playerid + "</p></a></h4>");
    }
    else {
        var hasFlash = false;

        try {
            var fo = new ActiveXObject('ShockwaveFlash.ShockwaveFlash');
            if (fo) {
                hasFlash = true;
            }
        } catch (e) {
            if (navigator.mimeTypes
        && navigator.mimeTypes['application/x-shockwave-flash'] != undefined
        && navigator.mimeTypes['application/x-shockwave-flash'].enabledPlugin) {
                hasFlash = true;
            }
        }
        if (hasFlash) {

            var vidname = playerid;
            //  var vidurl = "rtmp://" + sname + '.vmukti.com:80/vod/';
            var vidurl;

            jwplayer(playerid).remove();
            jwplayer(playerid).setup({
                file: "rtmp://" + sname + ".vmukti.com:80/vod/" + strname + ".flv",
                image: "images/player.jpg",
                width: "100%",
                aspectratio: "16:9",
                controls: true,
                abouttext: "VMukti",
                aboutlink: "http://www.vmukti.com",
                logo: {
                    file: "images/vlogo.png",
                    link: "http://www.vmukti.com/",
                    linktarget: "_blank",
                    position: "top-right"
                },
                autostart: "true"
            });



            jwplayer(playerid).on("error", function (event) {
                if (event.message == 'Error loading stream: ID not found on server') {
                    $("#playerimg").show();
                    setTimeout(function () { jwplayer(playerid).stop(); jwplayer(playerid).play(true); jwplayer(playerid).setControls(false); }, 10000);
                }

            });

            jwplayer(playerid).on("idle", function (event) {
                jwplayer(playerid).setControls(true);
                jwplayer(playerid).play(true);
            });
            jwplayer(playerid).on("play", function (event) {

                $("#playerimg").hide();

                jwplayer(playerid).setControls(true);
                jwplayer(playerid).play(true);
            });

        }
        else {

            $('#' + playerid).html('<h4>Please download and install flash player : <a href="https://get.adobe.com/flashplayer/">Click Here</a></h4>');

        }
    }
    return false;
}
function newOpen(playerid, sname) {
    //        var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
    //        if (isChrome) {
    //            window.location = 'vmplayer://?param1=' + sname + '.vmukti.com:80/live-record/' + playerid;
    //            setTimeout("window.location = 'mobile/VMPlayer.apk';", 5000);
    //        }
    //        else {
    //    var divid = "ifc" + playerid;

    //       }

    window.location = 'vmplayer://?param1=' + sname + '.vmukti.com:80/live-record/' + playerid;
    setTimeout("window.location = 'https://play.google.com/store/apps/details?id=com.vmplayer';", 1000);

    return false;
}
function testapp() {
    alert("hi");
    window.location = 'https://play.google.com/store/apps/details?id=com.vmukti.vmplayer';
}

window.closeModal = function () {
    $('#myModal1').modal('hide');
};