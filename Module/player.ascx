<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="player.ascx.cs" Inherits="exam.Module.player" %>
<style type="text/css">
    #div_player1
    {
        pointer-events: none;
    }
    .jw-icon-rewind
    {
        display: none !important;
    }
    .jw-slider-time .jw-text-duration
    {
        display: none !important;
    }
    .jwplayer{
        position: unset!important;
    }
     .vjs-progress-control {
    display: none!important;
}
</style><style>
        .vjs-progress-control .vjs-loading-spinner {
    display: none!important;
}
             
    </style>
<%
    exam.Common cm = new exam.Common();
    if (!cm.isMobile())
    {
%>
<%--<script src="<%=ResolveUrl("~/js/jwp8/jwplayer.js") %>" type="text/javascript"></script>--%>
  <script src="js/rtmp.js" type="text/javascript"></script>
<%if (Session["playerType"] == "AmbicamPlayer")
    { %>
<script src="js/clapper/clappr.min.js"></script>
    <script src="js/clapper/main.js"></script>
    <script src="js/clapper/ace.js"></script>
<%}else{ %>
   <%--<script src="<%=ResolveUrl("~/js/jwp5/jwplayer.js") %>" type="text/javascript"></script>--%>
   <%-- <script src="<%=ResolveUrl("~/js/jwplayer.8.24.0.js") %>" type="text/javascript"></script>--%>
<%--<script src="<%=ResolveUrl("~/js/jwp8/jwplayer.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        jwplayer.key = 'UeOwyRhgpc5Og22j+4YM7uzrcbTwxTV9r+XadsoRGr8=';

        jwplayer.logo.file = "https://assets-jpcust.jwpsrv.com/watermarks/3u985tDd.png";
    </script>--%>
<script src="<%=ResolveUrl("~/js/flv.min.js") %>" type="text/javascript"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/flv.js/1.5.0/flv.min.js"></script>
<%} %>
<%--<script type="text/javascript">
    jwplayer.key = 'UeOwyRhgpc5Og22j+4YM7uzrcbTwxTV9r+XadsoRGr8=';
</script>--%>
<%}
    else
    { %>

<link href="<%=ResolveUrl("~/NewLayout/css/styles.css") %>" rel="stylesheet" type="text/css" /> 
 
 <%--<script src="<%=ResolveUrl("~/js/jwp5/jwplayer.js") %>" type="text/javascript"></script> --%>
<%}%>
<script src="js/screenfull.js" type="text/javascript"></script>
<div class="embed-responsive embed-responsive-16by9 mr-thin" style="border-radius: 0px; margin:0px">
    <div class="embed-responsive-item" id="div_player">
        <div style="display: none;" id="ifc<%=playerid %>">
        </div>
 <%--  <div id='<%=playerid %>'>
        </div> --%>
         
       <video id="<%=playerid%>" class="video-js vjs-default-skin" muted autoplay   controls style="width:100%">
          
    </video> 
         
		 <video id="RTMPplayer_<%=playerid %>" autoplay controls style="display:none" class="player"></video>

        <div class="playercontrolerr_<%=playerid %> plerror" style="display: none; font-family: Verdana;
            font-size: 25px;">
            <div>
                <p style="text-align: center">
                </p>
            </div>
        </div>
       <div id="playercontrol">
            <div align="left">
                <button type="button" class="btn btn-default btn-sm btnfull" id="btntogglefullscreen_<%=playerid %>"
                    onclick="loadfullscreen('<%=playerid %>')">
                    <i class="glyphicon glyphicon-resize-full"></i>
                </button>
            </div>
        </div>
        <script type="text/javascript"> 
             <%if (Session["MenuName"] == "GridView" ||Session["MenuName"] == "GridView1") {%>
            playerType = 'videoplayer'
           // LoadPlayer1('<%=playerid%>', '<%=playerURL %>', 'lstgrid', '', '<%=accode%>', '<%=psnum%>', '<%=kbps%>', '<%=mobno%>');
            <%}%>
            <%else {%> 
            playerType = 'videoplayer'
          
                    <% }%>   
            LoadPlayer('<%=playerid%>', '<%=playerURL %>', 'lstgrid', '', '<%=accode%>', '<%=psnum%>', '<%=kbps%>', '<%=mobno%>');
        </script>
    </div>
</div>
<script type="text/javascript">
    var playerid = '<%=playerid %>';
    document.getElementById("btntogglefullscreen_" + playerid).onclick = function () {
        if (!screenfull.enabled) {
            return false;
        }
        screenfull.toggle(document.getElementById('<%=playerid %>'));
    };
    $(document).on(screenfull.raw.fullscreenchange, function screenfullChange() {
        if (!screenfull.isFullscreen) {
            $('#btntogglefullscreen_' + playerid).find('i').toggleClass('glyphicon-resize-small glyphicon-resize-full');
        } else {
            $('#btntogglefullscreen_' + playerid).find('i').toggleClass('glyphicon-resize-full glyphicon-resize-small');
        }

    });

    function loadfullscreen(pid) {
        if (!screenfull.enabled) {
            return false;
        }

        screenfull.toggle(document.getElementById(playerid));
    }

    //function LoadPlayer1(playerid,sname) {
    //    debugger
    //    if (playerid.indexOf("_") >= 0) {
    //        playeridurl = playerid.split('_')[1];
    //    }
    //    var videoElement = document.getElementById(playerid);
    //    var flvPlayer = flvjs.createPlayer({
    //        type: 'flv',
    //        url: 'https://' + sname + '/live-record/' + playeridurl + '.flv',
    //        isLive: true,
    //        controls: true, // show controls
    //        controlBarVisibility: 'hover',
    //        enableStashBuffer: false,
    //        stashInitialSize: 128,
    //        seekBar: false // hide the seek bar
    //    });
    //    flvPlayer.attachMediaElement(videoElement);
    //    flvPlayer.load();
    //    flvPlayer.play();
    //}

</script>

