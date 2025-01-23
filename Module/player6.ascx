<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="player6.ascx.cs" Inherits="exam.Module.player6" %>
<script src="js/jwp6/jwplayer.js" type="text/javascript"></script>
<script type="text/javascript">
    jwplayer.key = 'gzBqq3hBwmtwCjW1HlG8vi3KHEVLaqRk/zMnHQ==';
</script>
<style type="text/css">
    #div_player1
    {
        pointer-events: none;
    }
</style>
<div class="embed-responsive embed-responsive-16by9">
    <div class="embed-responsive-item" id="div_player">
        <div id='<%=playerid %>' style="display: none">
        </div>
        <div style="display: none;" id="ifc<%=playerid %>">
        </div>
        <script type="text/javascript">
            LoadPlayer6('<%=playerid %>', '<%=playerURL %>', '<%=playerstateid%>', '<%=currentstateid%>');
            //loadhlsplayer('<%=playerid %>', '<%=playerURL %>', '<%=playerstateid%>', '<%=currentstateid%>');
        </script>
    </div>
</div>
<br />
