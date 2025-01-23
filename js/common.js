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

function centeredPopup(playerid, sname, playstatus, strid, isplayback = 0) {

    var popupWindow = null;
    var w = 460;
    var h = 268;
    var scroll = 'no';
    LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;
    settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable'
    popupWindow = window.open('WatchVideo.aspx?vid=' + playerid + '&sname=' + sname + "&playerType=" + playerType + "&playback=" + isplayback, "Watch Video", settings);

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

function downloadvideo_bk30march(servername, streamname, stateid, cur_stateid, isdwn) {


    $("#dwn_" + streamname).prop("target", "_blank");

    var foldername = "vellore1";
    if (servername == "vms4") {
        foldername = "vellore2";
    }
    dwnlnk = "https://elesouthdata.blob.core.windows.net/" + foldername + "/" + streamname + ".flv";



    $("#dwn_" + streamname).prop("href", dwnlnk);
    return true;

}


/*Tamilnadu 2021-46000+ booth-*/
function downloadPlayback(servername, streamname) {
    $("#dwn_" + streamname).prop("target", "_blank");

    dwnlnk = "https://" + servername + "/recordings/" + streamname + ".flv";


    $("#dwn_" + streamname).prop("href", dwnlnk);
    return true;
}


function downloadvideo(servername, streamname, stateid, cur_stateid, isdwn) {
    $("#dwn_" + streamname).prop("target", "_blank");

    //https://eletn21blob1.blob.core.windows.net/etv1-container/29-03-2021/000077.flv
    dwnlnk = "https://eletn21blob1.blob.core.windows.net/" + servername + "-container/06-04-2021/" + streamname + ".flv";

    $("#dwn_" + streamname).prop("href", dwnlnk);
    return true;

}

function downloadvideobydate(servername, streamname, stateid, cur_stateid, isdwn, seldate) {

    var mydate = new Date(seldate);
    //alert((("0" + mydate.getDate()).slice(-2))+ "-" +(("0" + (mydate.getMonth()+1)).slice(-2))+ "-" + mydate.getFullYear());

    $("#dwn_" + streamname).prop("target", "_blank");

    //https://punjabflc.blob.core.windows.net/punjabflc/22-09-21/FLVFILENAME
    dwnlnk = "https://punjabflc.blob.core.windows.net/punjabflc/" + (("0" + mydate.getDate()).slice(-2)) + "-" + (("0" + (mydate.getMonth() + 1)).slice(-2)) + "-" + mydate.getFullYear().toString().substr(-2) + "/" + streamname + ".flv";
    $("#dwn_" + streamname).prop("href", dwnlnk);
    return true;

}
function downloadvideobydatephasewise(servername, streamname, stateid, cur_stateid, isdwn, seldate) {

    var mydate = new Date(seldate);
    //alert((("0" + mydate.getDate()).slice(-2))+ "-" +(("0" + (mydate.getMonth()+1)).slice(-2))+ "-" + mydate.getFullYear());
    $("#dwn_" + streamname).prop("target", "_blank");

    //https://biharphase1pooling.blob.core.windows.net/biharpanchayatp1-container/24-09-21/AAAA-005120-AAAAA.flv
    //https://biharphase1pooling.blob.core.windows.net/biharpanchayatp2-container/24-09-21/AAAA-005120-AAAAA.flv

    dwnlnk = "https://biharphase1pooling.blob.core.windows.net/biharpanchayatp" + stateid + "-container/" + (("0" + mydate.getDate()).slice(-2)) + "-" + (("0" + (mydate.getMonth() + 1)).slice(-2)) + "-" + mydate.getFullYear() + "/" + streamname + ".flv";

    $("#dwn_" + streamname).prop("href", dwnlnk);
    return true;

}
function downloadvideoold(servername, streamname, stateid, cur_stateid, isdwn) {

    var sname = servername;

    $("#dwn_" + streamname).prop("target", "_blank");
    var ddlphase = "phase7";
    var vms1 = ['vms1', 'vms100', 'vms101', 'vms45', 'vms38'];
    var vms2 = ['vms2', 'vms56', 'vms49', 'vms49', 'vms67'];
    var vms3 = ['vms3', 'vms57', 'vms43', 'vms46'];
    var vms4 = ['vms4', 'vms50', 'vms44'];
    var vms5 = ['vms5', 'vms33'];
    var vms6 = ['vms6', 'vms64', 'vms70', 'vms59'];
    var vms7 = ['vms7', 'vms62', 'vms58', 'vms54', 'vms63'];
    var vms8 = ['vms8', 'vms68', 'vms35'];
    var vms66 = ['vms66', 'vms34'];
    var vms10 = ['vms10', 'vms55', 'vms69', 'vms60'];
    var vms11 = ['vms11', 'vms61', 'vms52'];
    var vms12 = ['vms12', 'vms53', 'vms47'];
    var vms13 = ['vms13', 'vms41', 'vms65'];
    var vms14 = ['vms14', 'vms666', 'vms42'];
    var vms15 = ['vms15', 'vms39'];
    var vms16 = ['vms16', 'vms36'];
    var vms17 = ['vms17', 'vms40', 'vms48'];
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
    //if (/Android/i.test(navigator.userAgent)) {
    //    if (playstatus == 'RUNNING') {
    //        newOpen(streamname, sname);
    //    }
    //    else {
    //        alert("Location is offline.");
    //    }
    //}
    //else {
    $('#myModal').on('shown.bs.modal', function (e) {
        var $invoker = $(e.relatedTarget);
        $('.modal-title').html("<b>AC: </b>" + $($invoker).attr("data-title") + "<br/><b>PS: </b>" + $($invoker).attr("data-content") + "<br/><b>TAB Number: </b>" + streamname);
        $('#myModal iframe').attr("src", "watchvideo.aspx?vid=" + streamname + "&sname=" + sname);
    });
    //}
    return false;
}

function loadhlsplayer(playerid, sname, sid, cur_stateid) {
    //$(".playercontrol").hide();

    //getJSfile("player/jwp8/jwplayer.js");
    var cssfile = $("<style>.jw-state-idle .jw-controls {background-color: transparent !important;}</style>");
    $("head").append(cssfile);

    var vidname = playerid;
    //var hlsurl = "http://" + sname + '.vmukti.com:8080/live-record/' + vidname + "/index.m3u8";

    var hlsurl = "http://" + sname + '/live-record/' + vidname + ".m3u8";

    var playerInstance = jwplayer(playerid);

    playerInstance.setup({
        key: "UeOwyRhgpc5Og22j+4YM7uzrcbTwxTV9r+XadsoRGr8=",
        file: hlsurl,
        width: "100%",
        aspectratio: "16:9",
        stretching: "exactfit",
        autostart: true,
        mute: false,
        logo: {
            file: "images/vmukti-New-H-2021-final.png",
            link: "http://www.vmukti.com/",
            linktarget: "_blank",
            position: "bottom-right"
        }
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
    var vidurl = "rtmp://" + sname + ':80/live-record/';

    if (sname == 'media1') {
        vidurl = "rtmp://" + sname + ':1938/dvr1/';
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
            file: "http://" + sname + ":8080/live-record/" + playerid + ".m3u8",
            type: "hls"
        }
        ],

        width: "100%",
        aspectratio: "16:9",
        androidhls: true,
        abouttext: "VMukti",
        aboutlink: "http://www.vmukti.com",
        logo: {
            file: "images/vmukti-New-H-2021-final.png",
            link: "http://www.vmukti.com/",
            linktarget: "_blank",
            position: "bottom-right"
        },
        autostart: "true"
    });

    jwplayer(vidname).onIdle(function () {
        setTimeout(function () {
            jwplayer(vidname).play();
        }, 5000);

    });

}

function LoadlivePlayer7(playerid, sname, plid, accode, psnum, kbps, mobno) { 
    var pl_flag
    var vidname = playerid;
    if (plid == "mplayer") {
        vidname = "mplayer";
    }
    if (playerid.indexOf("_") >= 0) {
        playerid = playerid.split('_')[1];
    }
    //var hlsurl = "http://" + sname + '.vmukti.com:8080/live-record/'+playerid+"/index.m3u8";
    var hlsurl = "https://" + sname + '/live-record/' + playerid + ".m3u8";
    //alert(hlsurl + "==Priya")
    var playerInstance = jwplayer(vidname);
    var er_flag
    er_flag = 0
    playerInstance.setup({
        file: hlsurl,
        stretching: 'exactfit',
        autostart: true,
        mute: false,
        preload: 'none',
        hlshtml: true,
        allowscriptaccess: 'always',
        autostart: true,
        width: 640,
        height: 360,
        logo: {
            file: "images/vmukti-New-H-2021-final.png",
            link: "http://www.vmukti.com/",
            linktarget: "_blank",
            position: "bottom-right"
        }
    });

    //playerInstance.on("ready", function () {
    //    var textOverlay1 = document.createElement("div");
    //    textOverlay1.style.position = "absolute";
    //    textOverlay1.style.left = "40%";
    //    textOverlay1.style.color = "#fff";
    //    textOverlay1.style.fontSize = "11px";
    //    textOverlay1.style.fontWeight = "100";
    //    textOverlay1.innerHTML = kbps;

    //    var textOverlay = document.createElement("div");
    //    textOverlay.style.position = "absolute";
    //    textOverlay.style.top = "0px";
    //    textOverlay.style.right = "0px";
    //    textOverlay.style.color = "#fff";
    //    textOverlay.style.textAlign = "right";
    //    textOverlay.style.fontSize = "11px";
    //    textOverlay.style.fontWeight = "100";
    //    textOverlay.innerHTML = accode + "|" + psnum + "<br/>" + mobno;

    //    var playerContainer = document.getElementById(playerInstance.id);
    //    playerContainer.appendChild(textOverlay);
    //    playerContainer.appendChild(textOverlay1);
    //});


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
    var vidurl = "rtmp://" + sname + '/live-record/';

    if (sname == 'media1') {
        vidurl = "rtmp://" + sname + '/dvr30/';
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
            file: "http://" + sname + ":8080/live-record/" + playerid + ".m3u8",
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
            file: "images/vmukti-New-H-2021-final.png",
            link: "http://www.vmukti.com/",
            linktarget: "_blank",
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
function LoadPlayer(playerid, sname, plid, schname, accode, psnum, kbps, mobno) {
    debugger
    $(".plerror").hide();
    $("#playbackfromlive").addClass("active").siblings().removeClass("active");
    var vidname = playerid;
    if (plid == "mplayer") {
        vidname = "mplayer";
        $('#schloc').show();
        $('#playbackcontrol').hide();
        $('#btnsettings').show();
        $("#playerimg").hide();
        $('#schloc').html('<div class="heading">' + playerid + '</div><div class="flex-row flex-wrap -mr-sm"><div class="col-12 md:col-4 col4"><p class="sub-heading"><b>District</b></p><p class="data"> ' + district + '</p></div><div class="col-12 md:col-4 col4"><p class="sub-heading"><b>Vehicle No</b></p><p class="data">' + 'location ' + '</p></div><div class="col-12 md:col-4 col4"><p class="sub-heading"><b>FSV Camera</b></p><p class="data"> ' + locname + ' </p></div></div>');
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
            $('#RTMPplayer').hide();
        }
        else {
            $('#RTMPplayer_' + playerid).hide();
            $('#' + playerid).show();
        }
        LoadlivePlayer7(playerid, sname, plid, accode, psnum, kbps, mobno);

    }
    else if (playerType == "RTMPPlayer") {

        if (plid == "mplayer") {
            $('#mplayer').hide();
            $('#RTMPplayer').show();
        }
        else {
            $('#RTMPplayer_' + playerid).show();
            $('#' + playerid).hide();
        }
        loadRTMPPayer(sname, playerid, plid);
    }
    else if (playerType == "AmbicamPlayer") {
        if (playerid.indexOf("_") >= 0) {
            playeridurl = playerid.split('_')[1];
        }
        var hlsurl = "https://" + sname + '/live-record/' + playeridurl + ".m3u8";
        //var player = new Clappr.Player({ source: "http://your.video/here.mp4", parentId: "#mplayer" });
        var playerElement = document.getElementById(playerid);
        var player = new Clappr.Player({
            source: hlsurl,
            //source: 'http://clappr.io/highline.mp4',
            //poster: 'http://clappr.io/poster.png',
            autoPlay: true,
            mute: true,
            height: $('#' + playerid).parent("div").height(),
            width: $('#' + playerid).parent("div").width()
        });

        player.attachTo(playerElement);
    }

    else if (playerType == "videoplayer") {
        debugger;
        /*  if (flvjs.isSupported()) {*/
        if (playerid.indexOf("_") >= 0) {
            playeridurl = playerid.split('_')[1];
            var url = 'wss://' + sname + '/live-record/' + playeridurl + '.flv';
        }
        else {
            var url = 'wss://' + sname + '/live-record/' + playerid + '.flv';
        }
        var videoElement = document.getElementById(playerid);
        var divvideoElement = document.getElementById("div_"+playerid);
        var videoContainer = videoElement.parentElement;
        var logoImg = document.createElement('img'); 
        logoImg.src = 'images/Player-Logo1.png';
        logoImg.style.position = 'absolute';
        logoImg.style.bottom = '60px';
        logoImg.style.right = '0px';
        logoImg.style.width = '25%';
        videoContainer.appendChild(logoImg);
        var flvPlayer = flvjs.createPlayer({
            type: 'flv',
            url: url, 
            isLive: true, 
            enableWorker: true,
            enableStashBuffer: true,
            stashInitialSize: undefined,
            isLive: true,
            lazyLoad: true,
            lazyLoadMaxDuration: 180000,
            lazyLoadRecoverDuration: 5,
            autoCleanupMaxBackwardDuration: 180,
            autoCleanupMinBackwardDuration: 120,
            accurateSeek: false,
            seekType: 'range',
            rangeLoadZeroStart: false,
            customSeekHandler: undefined,
            headers: undefined,
            customLoader: undefined,
            debug: false,
            hasAudio: true
        });
        flvPlayer.attachMediaElement(videoElement);
        flvPlayer.load();
        flvPlayer.play();
        //flvPlayer.on(flvjs.Events.BUFFER_FULL, () => {
        //    setTimeout(() => {
        //        flvPlayer.currentTime = delayInSeconds;
        //    }, delayInSeconds * 1000);
        //});


        flvPlayer.on(flvjs.Events.BUFFER_FULL, () => {
            // Listen for the "canplay" event
            flvPlayer.once('canplay', () => {
                // Set the delayed starting time for the video
                flvPlayer.currentTime = 1000;
                // Start playing the video
                flvPlayer.play();
            });
        });


        //flvPlayer.on(flvjs.Events.MEDIA_INFO, (mediaInfo) => {
        //    console.log('Media Info:', mediaInfo);
        //    const codecName = codecMap[mediaInfo.videoCodec] || mediaInfo.videoCodec;
        //    const initialVideoBitrate = getRandomVideoBitrate();
        //    divvideoElement.textContent = 'Width: ' + mediaInfo.width + ' | Height: ' + mediaInfo.height +
        //        '| Video Codec: ' + codecName;
        //});
    }

    return false;
}


/*RTMP Player*/
/*RTMP Player*/
function hexdump(buffer, len, blockSize) {

    if (typeof buffer === 'string') {
        console.log("buffer is string");
        //do nothing
    } else if (buffer instanceof ArrayBuffer && buffer.byteLength !== undefined) {
        console.log("buffer is ArrayBuffer");
        buffer = String.fromCharCode.apply(String, [].slice.call(new Uint8Array(buffer)));
    } else if (Array.isArray(buffer)) {
        console.log("buffer is Array");
        buffer = String.fromCharCode.apply(String, buffer);
    } else if (buffer.constructor === Uint8Array) {
        console.log("buffer is Uint8Array");
        buffer = String.fromCharCode.apply(String, [].slice.call(buffer));
    } else {
        console.log("Error: buffer is unknown...");
        return false;
    }


    blockSize = blockSize || 16;
    var lines = [];
    var hex = "0123456789ABCDEF";
    for (var b = 0; b < len; b += blockSize) {
        var block = buffer.slice(b, Math.min(b + blockSize, len));
        var addr = ("0000" + b.toString(16)).slice(-4);
        var codes = block.split('').map(function (ch) {
            var code = ch.charCodeAt(0);
            return " " + hex[(0xF0 & code) >> 4] + hex[0x0F & code];
        }).join("");
        codes += "   ".repeat(blockSize - block.length);
        var chars = block.replace(/[\x00-\x1F\x20]/g, '.');
        chars += " ".repeat(blockSize - block.length);
        lines.push(addr + " " + codes + "  " + chars);
    }
    return lines.join("\n");
}
function loadRTMPPayer(sname, playerid, plid) {
    if (null != rtmp) {
        rtmp.close();
    }
    // alert("ws://" + sname + ".vmukti.com:90")

    var playerid1
    playerid1 = playerid
    //var wsvidurl = "ws://" + sname + '.ambicam.com:1948/dvr30/' + playerid;
    //var vidurl = "rtmp://" + sname + '.vmukti.com:80/live-record/';
    if (playerid.indexOf("_") >= 0) {
        playerid1 = playerid.split('_')[1];
    }
    //rtmp://med1ia1.ambicam.com:1938/dvr7/9a9b29d8-2dd0-4c76-a642-688ca213205f
    var startInfo = {
        schema: "rtmp",
        // host: sname + ".vmukti.com:80",
        host: sname,
        // host: sname + ".ambicam.com:1938",
        //app: "live-record",
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
        //url:"wss://" + sname + ".vmukti.com:90"
        url: "wss://" + sname + ":90"
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
    onClosed = function (e) {
        console.log('#onClosed: HTML');
    };
    rtmp.onstreamcreated = function (e) {
        console.log('#streamcreated: ' + e.streamId);
        console.log(e);

        var ns = e.stream;
        ns.ondata = function (message) {

            //console.log('#packet (' + message.typeId + '): ' +
            //  Array.prototype.map.call(message.data,
            //  function (i) { return (i + 256).toString(16).substr(1); }).join(' ') );

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
        //alert(startInfo.video)
        ns.play(startInfo.video);
        //rtmp.setBuffer(0x7D0);
    };
    rtmp.connect(onClosed, props);
    var mp4 = {
        mFps: 15,
        mBuffSec: 3,
        mVPkt: 0,
        mAPkt: 0,
        mFlag: false,
        packets: 0,
        initialized: false,
        pendingPackets: [],
        init: function (metadata) {
            // live stream: no video information in flv metadata header
            // try to fake it
            // -> not so good, only first frame rendered
            //@NOTE: Pravin, we can change this
            //metadata.videocodecid = 'avc1';
            //metadata.avclevel = 21;
            //metadata.avcprofile = 66;
            //metadata.height = 360;
            //metadata.width = 640;
            //console.log("FPS: " + metadata.framerate);
            //metadata.framerate = this.mFps;
            this.mBuffSec = 0;
            console.log("meta.framerate=" + metadata.framerate + ", fps=" + metadata.fps + ", Buff: " + this.mBuffSec);
            this.mFps = metadata.fps;

            console.log(RtmpJs.MP4.parseFLVMetadata(metadata));
            if (!metadata.audiocodecid && !metadata.videocodecid) {
                return; // useless metadata?
            }

            this.initialized = true;
            this.mux = new RtmpJs.MP4.MP4Mux(RtmpJs.MP4.parseFLVMetadata(metadata));
            this.mux.ondata = function (data) {
                //console.log("mux.ondata: " + data.length)
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
            this.packets++;
            switch (type) {
                case 8:
                    this.mAPkt++;
                    if (this.mAPkt >= 8) {
                        console.log("audio, Fps: " + this.mVPkt);
                        this.mFps = Math.floor((this.mFps + this.mVPkt) / 2)
                        this.mVPkt = 0;
                        this.mAPkt = 0;
                    }
                    break;
                case 9:
                    this.mVPkt++;
                    break;
                default:
            }

            if (!this.mFlag) {
                var _offset = 12;
                var spsLen = data[_offset++];
                var sps = [];
                for (i = 0; i < spsLen; i++) {
                    sps[i] = data[_offset++];
                }

                _offset++;
                _offset++;
                var ppsLen = data[_offset++];
                var pps = [];
                for (i = 0; i < ppsLen; i++) {
                    pps[i] = data[_offset++];
                }

                console.log("Data: " + hexdump(data, data.length, 16));
                console.log("SPS: " + hexdump(sps, sps.length, 16));
                console.log("PPS: " + hexdump(pps, pps.length, 16));
                this.mFlag = true;
            }
            //document.getElementById('packets').textContent = this.packets + ", fps=" + this.mFps;
            if (!this.initialized) {
                this.pendingPackets.push([type, new Uint8Array(data)]);
                return;
            }

            this.mux.pushPacket(this.mFps, this.mBuffSec, type, new Uint8Array(data), (new Date()).getTime());
        }
    };
    /* var mp4 = {
         mLastTs: 0,
         mFps: 5,
         mLPkt: 0,
         mPkt: 0,
         mFlag: false,
         packets: 0,
         initialized: false,
         pendingPackets: [],
         init: function (metadata) {
             // live stream: no video information in flv metadata header
             // try to fake it
             // -> not so good, only first frame rendered
             //metadata.videocodecid = 'avc1';
             //metadata.avclevel = 21;
             //metadata.avcprofile = 66;
             //metadata.height = 360;
             //metadata.width = 640;
             //metadata.framerate = 10;
 
 
             console.log(RtmpJs.MP4.parseFLVMetadata(metadata));
 
             if (!metadata.audiocodecid && !metadata.videocodecid) {
                 return; // useless metadata?
             }
 
             this.initialized = true;
             this.mux = new RtmpJs.MP4.MP4Mux(RtmpJs.MP4.parseFLVMetadata(metadata));
             this.mux.ondata = function(data) {
                // console.log("mux.ondata: " + data.length)
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
             this.packets++;
             curTs = (new Date).getTime();
             if (0 == this.mLastTs) {
                 this.mLastTs = curTs;
             }
             switch (type) {
                 case 8:
                     break;
                 case 9:
                     this.mPkt++;
                     if (!this.mFlag && (curTs - this.mLastTs) > 3000) {
                         this.mFps = parseInt((this.mPkt - this.mLPkt)/3) - 1;
                         console.log("Calculated FPS: " + this.mFps);
                         if (this.mFps <= 7) {
                             this.mFps = 5;
                         } else if (this.mFps <= 12) {
                             this.mFps = 10;
                         } else if (this.mFps <= 17) {
                             this.mFps = 15;
                         }
                             
                         this.mLPkt = this.mPkt;
                         this.mLastTs = ((new Date).getTime());
                         this.mFlag = true;
                     }
                     break;
                 default:
             }
             //document.getElementById('packets').textContent = this.packets + " : " + this.mFps;
             if (!this.initialized) {
                 this.pendingPackets.push([type, new Uint8Array(data)]);
                 return;
             }
                 
             this.mux.pushPacket(this.mFps, type, new Uint8Array(data), (new Date()).getTime());
         }
     };*/

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
                vidurl = "http://" + sname + ':8080/phase1/';
            }
            else {
                vidurl = "https://updata.blob.core.windows.net/phase" + sid + "/";
            }

            jwplayer(playerid).setup({
                file: "rtmp://" + sname + ":80/vod/" + vidname + ".flv",
                image: "images/player.jpg",
                width: "100%",
                height: "100%",
                aspectratio: "16:9",
                controls: false,
                abouttext: "VMukti",
                aboutlink: "http://www.vmukti.com",

                autostart: "true",
                logo: {
                    file: "images/vmukti-New-H-2021-final.png",
                    link: "http://www.vmukti.com/",
                    linktarget: "_blank",
                    position: "bottom-right"
                }
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
                file: "rtmp://" + sname + ":80/vod/" + strname + ".flv",
                image: "images/player.jpg",
                width: "100%",
                aspectratio: "16:9",
                controls: true,
                abouttext: "VMukti",
                aboutlink: "http://www.vmukti.com",
                logo: {
                    file: "images/vmukti-New-H-2021-final.png",
                    link: "http://www.vmukti.com/",
                    linktarget: "_blank",
                    position: "bottom-right"
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
function LoadPlaybackPlayer() {
    if (FlvPlayer.isSupported()) {
        var flv = new FlvPlayer({
            container: '.flvplayer-app',

            url: '/008338.flv',
            decoder: './js/flvplayer-decoder-baseline.js',
            // decoder: './uncompiled/flvplayer-decoder-multiple.js',
            debug: true,
            live: false,
            loop: true,
            autoPlay: true,
            hasAudio: true,
            control: true,
            muted: false,
            volume: 0.7,
            frameRate: 30,
            maxTimeDiff: 200,
            videoChunk: 1024 * 1024,
            audioChunk: 64 * 1024,
            width: 640,
            height: 360,
        });


    } else {
        console.warn('Your browser does not support Flvplayer.js');
    }
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

    //window.location = 'vmplayer://?param1=' + sname + ':80/live-record/' + playerid;
    //setTimeout("window.location = 'https://play.google.com/store/apps/details?id=com.vmplayer';", 1000);

    //return false;
    return true;
}
function testapp() {
    alert("hi");
    window.location = 'https://play.google.com/store/apps/details?id=com.vmukti.vmplayer';
}

window.closeModal = function () {
    $('#myModal1').modal('hide');
};

function watchvideopopup(playerid, sname, playstatus, strid, isplayback = 0) {
    var modal = document.getElementById("myModal");
    modal.style.display = "block";
    $('#myModal iframe').attr("src", 'WatchVideo.aspx?vid=' + playerid + '&sname=' + sname + "&playerType=" + playerType + "&playback=" + isplayback + "&playstatus=" + playstatus + "&streamnamebkp=" + strid);
    // When the user clicks on <span> (x), close the modal
    var span = document.getElementsByClassName("close")[0];
    span.onclick = function () {
        modal.style.display = "none";
        $('#myModal iframe').attr("src", "");
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            $('#myModal iframe').attr("src", "");
        }
    }
}